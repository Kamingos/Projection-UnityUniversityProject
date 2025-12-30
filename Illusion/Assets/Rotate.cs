using UnityEngine;

public class Rotate : MonoBehaviour
{
    void Update()
    {
        transform.localRotation *= Quaternion.Euler(0, 0.25f, 0);
    }
}
