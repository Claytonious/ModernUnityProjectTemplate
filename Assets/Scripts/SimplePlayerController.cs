using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace Testbed
{
    public class SimplePlayerController : MonoBehaviour
    {
        [SerializeField]
        private InputActionAsset _inputActionAsset;

        [SerializeField]
        private float _moveSpeed = 7f;
        [SerializeField]
        private float _lookSensitivity = 0.35f;

        private InputAction _moveXAxisAction, _moveZAxisAction, _lookXAxis, _lookYAxis, _toggleCursorLockAction;
        private CursorLocker _cursorLocker;
        
        private void Start()
        {
            _cursorLocker = GetComponent<CursorLocker>();
            
            var map = _inputActionAsset.FindActionMap("Default");
            Assert.IsNotNull(map);
            _moveXAxisAction = map.FindAction("MoveXAxis");
            Assert.IsNotNull(_moveXAxisAction);
            _moveZAxisAction = map.FindAction("MoveZAxis");
            Assert.IsNotNull(_moveZAxisAction);
            _lookXAxis = map.FindAction("LookXAxis");
            Assert.IsNotNull(_lookXAxis);
            _lookYAxis = map.FindAction("LookYAxis");
            Assert.IsNotNull(_lookXAxis);
            _toggleCursorLockAction = map.FindAction("ToggleCursorLock");
            Assert.IsNotNull(_toggleCursorLockAction);
            
            _toggleCursorLockAction.performed += HandleToggleCursorLock;
            
            map.Enable();
        }

        private void HandleToggleCursorLock(InputAction.CallbackContext context)
        {
            _cursorLocker.IsLocked = !_cursorLocker.IsLocked;
        }

        private void Update()
        {
            var xform = transform;
            
            var pos = xform.position;
            pos += xform.right * _moveXAxisAction.ReadValue<float>() * Time.deltaTime * _moveSpeed;
            pos += xform.forward * _moveZAxisAction.ReadValue<float>() * Time.deltaTime * _moveSpeed;
            xform.position = pos;

            if (_cursorLocker.IsLocked)
            {
                var eulers = xform.eulerAngles;
                eulers.y += _lookYAxis.ReadValue<float>() * _lookSensitivity;
                eulers.x += _lookXAxis.ReadValue<float>() * _lookSensitivity;
                xform.eulerAngles = eulers;
            }
        }
    }
}
