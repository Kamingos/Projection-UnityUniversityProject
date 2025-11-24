using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player.Controls
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementModuleMethods : MonoBehaviour
    {
        [SerializeField] private GameObject camera;
        [SerializeField] private FloorDetector floorDetector;


        [SerializeField] private int SENSITIVE;

        [SerializeField] public float minVerticalAngle = -80f; // Минимальный угол наклона вниз
        [SerializeField] public float maxVerticalAngle = 80f;  // Максимальный угол наклона вверх

        [SerializeField] private int SPEED;
        [SerializeField] private int JUMP_FORCE;

        private Rigidbody rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        Vector3 localDir;
        Vector3 finalResult;
        public void MoveDir(Vector3 dir)
        {
            localDir = transform.TransformDirection(dir.normalized) * SPEED * Time.deltaTime;

            finalResult = transform.localPosition + localDir;

            //transform.localPosition += new Vector3(0, 0, 0.1f) * SPEED * Time.deltaTime;
            rigidbody.MovePosition(finalResult);
        }

        public void Jump()
        {
            if (!floorDetector.IsOnFloor()) return;

            rigidbody.AddForce(Vector3.up * JUMP_FORCE, ForceMode.Impulse);
            //Debug.Log("Jump");
        }

        public void CameraRotate(Vector2 mouseDir)
        {
            if (camera == null) return;

            // Горизонтальное вращение (игрока/родительского объекта)
            float horizontalRotation = mouseDir.x * SENSITIVE * Time.deltaTime;
            transform.Rotate(0, horizontalRotation, 0, Space.World);

            // Вертикальное вращение (камеры)
            float verticalRotation = -mouseDir.y * SENSITIVE * Time.deltaTime;

            // Вычисляем новый угол
            float currentAngleX = camera.transform.localEulerAngles.x;
            if (currentAngleX > 180f) currentAngleX -= 360f;

            float newAngleX = currentAngleX + verticalRotation;
            newAngleX = Mathf.Clamp(newAngleX, minVerticalAngle, maxVerticalAngle);

            // Применяем вертикальное вращение
            camera.transform.localEulerAngles = new Vector3(newAngleX, 0, 0);
        }
    }
}
