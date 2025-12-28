using System;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player.Controls
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputActions;

        public event Action<InputAction.CallbackContext> OnMouseClick;
        public event Action<InputAction.CallbackContext> OnJumpPressed;

        private Vector2 _tempMoveDir = new Vector3(0,0,0);
        private Vector3 _moveDir = new Vector3(0,0,0);
        private Vector2 _mouseDir = new Vector3(0,0,0);
        public Vector3 MoveDir => _moveDir;
        public Vector3 MouseDir => _mouseDir;


        private Mouse _mouse;

        private InputAction _vertMoveAction;
        private InputAction _jumpAction;

        private InputAction _btnClick;

        private void Awake()
        {
            _vertMoveAction = inputActions.FindAction("Move");
            _jumpAction = inputActions.FindAction("Jump");
            _btnClick = inputActions.FindAction("Attack");

            _mouse = Mouse.current;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            _btnClick.started += (_) => OnMouseClick.Invoke(_);

            _jumpAction.performed += (_) => OnJumpPressed.Invoke(_);
        }

        private void Update()
        {
            _tempMoveDir = _vertMoveAction.ReadValue<Vector2>();

            _moveDir = new Vector3(_tempMoveDir.x, 0, _tempMoveDir.y);

            _mouseDir = _mouse.delta.ReadValue();
        }
    }
}