using System.Collections;
using UnityEngine;

public class FloorDetector : MonoBehaviour
{
    [SerializeField] private float rayDist;

    private bool isOnFloor = false;
    public bool IsOnFloor => isOnFloor;

    private void Awake()
    {
        StartCoroutine(coroutine());
    }

    IEnumerator coroutine()
    {
        WaitForSeconds wfs = new WaitForSeconds(0.1f);

        while (true)
        {
            isOnFloor = IsOnFloorMethod();

            yield return wfs;
        }
    }


    private bool IsOnFloorMethod()
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
        Gizmos.DrawRay(new Ray(transform.position, Vector3.down * rayDist));
    }
}
