using Scripts.Player.Controls;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player.Controls
{
    [RequireComponent(typeof(MovementModuleMethods))]
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private MovementModuleMethods moveModule;
        [SerializeField] private InputActionAsset inputActions;

        private Mouse mouse;

        private InputAction vertMoveAction;
        private InputAction jumpAction;

        private Vector2 tempMoveDir = new Vector3(0,0,0);
        private Vector3 moveDir = new Vector3(0,0,0);
        private Vector2 mouseDir = new Vector3(0,0,0);

        private void Awake()
        {
            vertMoveAction = inputActions.FindAction("Move");
            jumpAction = inputActions.FindAction("Jump");

            mouse = Mouse.current;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            jumpAction.performed += (_) => moveModule.Jump();
        }

        private void Update()
        {
            tempMoveDir = vertMoveAction.ReadValue<Vector2>();

            moveDir = new Vector3(tempMoveDir.x, 0, tempMoveDir.y);

            mouseDir = mouse.delta.ReadValue();
        }

        private void LateUpdate()
        {
            moveModule.CameraRotate(mouseDir);
            moveModule.MoveDir(moveDir);
        }
    }
}