using UnityEngine;

public class DontMoveScript : MonoBehaviour
{
    [SerializeField] float yPos;

    void Update()
    {
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
        transform.rotation = Quaternion.identity;
    }
}
