using UnityEngine;

namespace Testbed
{
    public class CursorLocker : MonoBehaviour
    {
        private bool _isLocked;
        private bool _wasLockedOnFocus;
        
        private void Start()
        {
            IsLocked = true;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                _wasLockedOnFocus = _isLocked;
                IsLocked = false;
            }
            else if (_wasLockedOnFocus)
            {
                IsLocked = true;
            }
        }

        public bool IsLocked
        {
            get => _isLocked;
            set
            {
                if (_isLocked != value)
                {
                    _isLocked = value;
                    if (_isLocked)
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                    }
                    else
                    {
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }
                }
            }
        }
    }
}