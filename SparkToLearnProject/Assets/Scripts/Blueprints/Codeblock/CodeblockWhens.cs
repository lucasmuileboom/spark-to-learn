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
        _input = GetComponent<CodeblockInput>();

        Whens = new List<WhenClass>();
        Whens.Add(new WhenClass("Key", CodeblockInput.InputTypes.Char, () => { try { return Input.GetKey(_input.GetChar().ToString().ToLower()); } catch { return false; } }));
    }

    private void Start()
    {
        _whenCodeblock = GetComponent<WhenCodeblock>();
    }

    private void Update()
    {
        if (Whens[_input.GetDropdown()].WhenFunc.Invoke())
        {
            _whenCodeblock.Execute();
        }
    }
}
