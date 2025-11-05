using UnityEngine;

public class FloorDetector : MonoBehaviour
{
    [SerializeField] float rayDist;
    public bool IsOnFloor()
    {
        Ray ray = new Ray(transform.position, Vector3.down * 3);

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, rayDist, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore))
        {
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Ray(transform.position, Vector3.down * 3));
    }
}
