using UnityEngine;
using UnityEngine.Events;

public class TrigerPoint : MonoBehaviour
{
    [SerializeField] private BoxCollider collider;

    public UnityEvent<Collider> OnPlayerEnter;

    bool isPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayed) return;

        if (other.CompareTag("Player"))
        {
            OnPlayerEnter.Invoke(other);
            isPlayed = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, collider.transform.localScale);
        Gizmos.color = Color.white;
    }
}
