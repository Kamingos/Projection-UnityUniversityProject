using UnityEngine;

public class MovementModule : MonoBehaviour
{
    Rigidbody rigidbody;

    public void Init()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
}
