using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(PlayerInput))]
public class MovementModule : MonoBehaviour
{
    Rigidbody rigidbody;
    PlayerInput playerInput;
    InputAction actionMove;
    

    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        playerInput = GetComponent<PlayerInput>();

        actionMove = playerInput.actions["Move"];


    }

    public void Update()
    {
        transform.position += actionMove.ReadValue<Vector3>() * Time.deltaTime * 100;
    }
}
