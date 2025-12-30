using Scripts;
using System.Collections;
using UnityEngine;

public class FloorDetector : MonoBehaviour
{
    [SerializeField] private float rayDist;


    private bool _isOnFloor = false;
    public bool IsOnFloor => _isOnFloor;


    private FloorType _floorType;
    public FloorType FloorType => _floorType;


    private void Awake()
    {
        StartCoroutine(coroutine());
    }

    IEnumerator coroutine()
    {
        WaitForSeconds wfs = new WaitForSeconds(0.1f);

        while (true)
        {
            _isOnFloor = IsOnFloorMethod();

            yield return wfs;
        }
    }


    private bool IsOnFloorMethod()
    {
        Ray ray = new Ray(transform.position, Vector3.down * 3);

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, rayDist, 1, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.TryGetComponent<FloorTypeModule>(out FloorTypeModule item))
            {
                _floorType = item.Type;
            }
            
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Ray(transform.position, Vector3.down * rayDist));
    }
}
