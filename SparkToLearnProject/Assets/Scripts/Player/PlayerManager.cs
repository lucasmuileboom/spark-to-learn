using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float _raycastGroundRange;
    [SerializeField] private Vector3[] _raycastGroundOffset;
    [SerializeField] private float _raycastRampRange;
    [SerializeField] private float _gravityMultiplier;

    private InputManager _inputManager;
    private MoveObject _moveObject;
    private RotateObject _rotateObjectPlayer;
    private RotateObject _rotateObjectcamera;
    
    private int _layerMaskGround = 1 << 8;
    private int _layerMaskRamp = 1 << 9;
    private bool _gravityIsOn = true;
    public bool _canRotate = true;
    public bool _canUseSkils = true;
    public bool _canEdit = false;
    private RaycastHit _hitRamp;

    [SerializeField] private ToggleUiActive[] _toggleUiActive;
    [SerializeField] private SetButtonPressedUI _setButtonPressedUI;
    [SerializeField] private ItemListCycle _itemListCycle;
    [SerializeField] private LockCursor _lockCursor;
    [SerializeField] private ObjectSpawnManager _objectSpawnManager;


    private void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _moveObject = GetComponent<MoveObject>();
        _rotateObjectPlayer = GetComponent<RotateObject>();
        _rotateObjectcamera = Camera.main.GetComponent<RotateObject>();
    }
    private void Update() 
    {
        PlayerVelocity();
        if (_canRotate)
        {
            PlayerRotation();
        }
        if (_canUseSkils) 
        { 
            Skils();
        }        
        BuildMenuArrows();
        if (!_gravityIsOn)
        {
            CheckGravity();
        }
    }
    private void PlayerVelocity()
    {
        Vector3 _playerVelocity = new Vector3();

        if (_inputManager.MoveForwardButtonDown())
        {
            _playerVelocity += this.transform.forward;
        }
        if (_inputManager.MoveBackwardButtonDown())
        {
            _playerVelocity -= this.transform.forward;
        }
        if (_inputManager.MoveRightButtonDown())
        {
            _playerVelocity += this.transform.right;
        }
        if (_inputManager.MoveLeftButtonDown())
        {
            _playerVelocity -= this.transform.right;
        }

        _playerVelocity.Normalize();

        _playerVelocity = Quaternion.Euler(RampRotation()) * _playerVelocity;

        if (_inputManager.FlyUpButtonDown())
        {
            _playerVelocity += this.transform.up;
            _gravityIsOn = false;
        }
        if (_inputManager.FlyDownButtonDown())
        {
            _playerVelocity -= this.transform.up;
        }

        if (_gravityIsOn)
        {
            _playerVelocity -= this.transform.up * _gravityMultiplier;
        }

        _moveObject.GiveRigidbodyVelocity(_playerVelocity);
    }
    private void PlayerRotation() 
    {
        Vector3 _playerRotation = new Vector3();
        Vector3 _cameraRotation = new Vector3();

        _cameraRotation.x = -Input.GetAxis("Mouse Y");
        _playerRotation.y = Input.GetAxis("Mouse X");

        _rotateObjectPlayer.AddRotationToObject(_playerRotation);
        _rotateObjectcamera.AddRotationToObject(_cameraRotation);
    }
    private void CheckGravity()
    {
        if (OnGround())
        {
            _gravityIsOn = true;
        }
    }
    private bool OnGround()
    {
        RaycastHit _hit;

        if (Physics.Raycast(transform.position, -Vector3.up, out _hit, _raycastGroundRange, _layerMaskGround))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * _range, Color.yellow);
            return true;
        }
        return false;
    }
    private bool OnRamp()
    {
        RaycastHit _hit;
        for (int i = 0; i < _raycastGroundOffset.Length; i++)
        {
            if (Physics.Raycast(transform.position + _raycastGroundOffset[i], -Vector3.up, out _hit, _raycastRampRange, _layerMaskRamp))
            {
                _hitRamp = _hit;
                _gravityIsOn = false;
                return true;
            }
        }
        return false;
    }
    public Vector3 RampRotation()
    {
        if (!OnRamp())
        {
            print("no ramp");
            return new Vector3(0, 0, 0);
        }
        print("ja ramp");
        print(_hitRamp.collider.transform.eulerAngles.y);

        if (_hitRamp.collider.transform.eulerAngles.y >= 89 && _hitRamp.collider.transform.eulerAngles.y <= 91)
        {
            return new Vector3(0, 0, -45);
        }
        else if (_hitRamp.collider.transform.eulerAngles.y >= 269 && _hitRamp.collider.transform.eulerAngles.y <= 271)
        {
            return new Vector3(0, 0, 45);
        }
        else if (_hitRamp.collider.transform.eulerAngles.y >= 179 && _hitRamp.collider.transform.eulerAngles.y <= 181)
        {
            return new Vector3(-45, 0, 0);
        }
        else if (_hitRamp.collider.transform.eulerAngles.y >= 359 || _hitRamp.collider.transform.eulerAngles.y <= 1)
        {
            return new Vector3(45, 0, 0);
        }

        return new Vector3(0, 0, 0);
    }
    private void Skils()
    {
        if (_inputManager.Skil1ButtonDown())
        {
            _toggleUiActive[0].SetUi(true);
            _toggleUiActive[1].SetUi(false);
            _toggleUiActive[2].SetUi(false);
            _toggleUiActive[3].SetUi(false);
            _setButtonPressedUI.Settext(0);
            _lockCursor.toggleCursor(true);
            _objectSpawnManager.enabled = false;
            _canEdit = false;
        }
        else if (_inputManager.Skil2ButtonDown())
        {
            _toggleUiActive[0].SetUi(false);
            _toggleUiActive[1].SetUi(true);
            _toggleUiActive[2].SetUi(false);
            _toggleUiActive[3].SetUi(false);
            _setButtonPressedUI.Settext(1);
            _lockCursor.toggleCursor(false);
            _objectSpawnManager.enabled = true;
            _canEdit = false;
        }
        else if (_inputManager.Skil3ButtonDown())
        {
            _toggleUiActive[0].SetUi(false);
            _toggleUiActive[1].SetUi(false);
            _toggleUiActive[2].SetUi(true);
            _toggleUiActive[3].SetUi(false);
            _setButtonPressedUI.Settext(2);
            _lockCursor.toggleCursor(false);
            _objectSpawnManager.enabled = false;
            _canEdit = true;
        }
        else if (_inputManager.Skil4ButtonDown())
        {
            _toggleUiActive[0].SetUi(false);
            _toggleUiActive[1].SetUi(false);
            _toggleUiActive[2].SetUi(false);
            _toggleUiActive[3].SetUi(true);
            _setButtonPressedUI.Settext(3);
            _lockCursor.toggleCursor(false);
            _objectSpawnManager.enabled = false;
            _canEdit = false;
        }
    }
    private void BuildMenuArrows()
    {
        if (_itemListCycle.gameObject.activeSelf) 
        {
            if (_inputManager.MenuDownButtonDown())
            {
                _itemListCycle.NextItem();
            }
            else if (_inputManager.MenuUpButtonDown())
            {
                _itemListCycle.PreviousItem();
            }
        }         
    }
}
