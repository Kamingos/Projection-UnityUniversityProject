using UnityEngine;
using UnityEngine.Events;

public class TrigerPoint : MonoBehaviour
{
    [SerializeField] private BoxCollider collider;

    [SerializeField] private Color color = Color.yellow;


    [Header("одноразовая ли")]
    [SerializeField] private bool oneTimeUse;
    [Header("Должны ли совпадать направления взгляда")]
    [SerializeField] private bool isViewsMatch;
    [Header("допустимое отклонение (в градусах)")]
    [SerializeField] private float deltaDegree;

    public UnityEvent<Collider> OnPlayerEnter;

    bool _isPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (_isPlayed && oneTimeUse) return;

        if (other.CompareTag("Player"))
        {
            if (isViewsMatch)
            {
                float cos = Vector3.Dot(transform.forward, other.gameObject.transform.forward);
                float degree = Mathf.Acos(cos) * Mathf.Rad2Deg;

                if (degree > deltaDegree) return;
            }

            Debug.Log("good");
            OnPlayerEnter?.Invoke(other);
            _isPlayed = true;
        }
    }

    private void OnDrawGizmos()
    {
        if (collider == null) return;

        Gizmos.color = color;

        // Сохраняем текущую матрицу Gizmos
        Matrix4x4 originalMatrix = Gizmos.matrix;

        // Устанавливаем матрицу трансформации объекта
        // TRS = Translation (позиция), Rotation (поворот), Scale (масштаб)
        Gizmos.matrix = Matrix4x4.TRS(
            transform.position + transform.rotation * collider.center,
            transform.rotation,
            Vector3.Scale(transform.lossyScale, collider.size)
        );

        // Рисуем куб в центре локальных координат (0,0,0)
        Gizmos.DrawCube(Vector3.zero, Vector3.one);

        // Восстанавливаем матрицу
        Gizmos.matrix = originalMatrix;

        Gizmos.color = Color.white;
    }
}
