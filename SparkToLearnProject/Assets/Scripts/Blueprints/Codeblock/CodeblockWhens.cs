using System;
using System.Collections.Generic;
using UnityEngine;

public class CodeblockWhens : MonoBehaviour
{
    private WhenCodeblock _whenCodeblock;
    private CodeblockInput _input;

    public class WhenClass
    {
        public string Name;
        public CodeblockInput.InputTypes InputType;
        public Func<bool> WhenFunc;

        public WhenClass(string name, CodeblockInput.InputTypes inputType, Func<bool> func)
        {
            this.Name = name;
            this.InputType = inputType;
            this.WhenFunc = func;
        }
    }

    public List<WhenClass> Whens;

    private void Awake()
    {
        _whenCodeblock = GetComponent<WhenCodeblock>();
        _input = GetComponent<CodeblockInput>();

        // Create a list of if statements to be selected from the codeblock
        Whens = new List<WhenClass>();
        Whens.Add(new WhenClass("Key Press", CodeblockInput.InputTypes.KeyCode, () => { try { return Input.GetKeyDown(_input.GetKeyCode()); } catch { return false; } }));
        Whens.Add(new WhenClass("Key Held", CodeblockInput.InputTypes.KeyCode, () => { try { return Input.GetKey(_input.GetKeyCode()); } catch { return false; } }));
    }

    private void Update()
    {
        // Check for the selected when and if true execute
        if (Whens[_input.GetDropdown()].WhenFunc.Invoke())
        {
            _whenCodeblock.Execute();
        }
    }
}
