using UnityEngine;

public class Rotate : MonoBehaviour
{
    void Update()
    {
        transform.rotation *= Quaternion.Euler(0, 0.25f, 0);
    }
}
