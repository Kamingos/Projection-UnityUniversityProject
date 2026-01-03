using Scripts.InteractableObjects;
using UnityEngine;


public class DisableRBKinematic : MonoBehaviour, IInteractable
{
    [SerializeField] private Rigidbody rb;

    public void DoAction()
    {
        rb.isKinematic = false;
    }
}
