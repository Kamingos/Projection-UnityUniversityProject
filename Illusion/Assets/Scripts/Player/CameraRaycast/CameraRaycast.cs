using Scripts.InteractableObjects;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRaycast : MonoBehaviour
{
    [SerializeField] private Camera camera;

    [SerializeField] private InputActionAsset inputActions;

    [SerializeField] float secondsForUpdate;

    [SerializeField] float rayDuration = 3.5f;

    private InputAction btnClick;

    private void Awake()
    {
        btnClick = inputActions.FindAction("Attack");

        btnClick.started += (_) => OnAttack(_);

        StartCoroutine(updateRaycast());
    }

    private void OnValidate()
    {
        if (secondsForUpdate < 0.5f) secondsForUpdate = 0.5f;
    }

    public void OnAttack(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started)
            if (CheckInteractable(out IInteractable interactable))
            {
                interactable.DoAction();
            }
    }

    IEnumerator updateRaycast()
    {
        WaitForSeconds wfs = new(secondsForUpdate);

        while (true)
        {
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out RaycastHit raycastHit, rayDuration))
            {
                Debug.Log(raycastHit.collider.name);
            }

            yield return wfs;
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(camera.transform.position, camera.transform.forward, Color.green, rayDuration);
    }

    private bool CheckInteractable(out IInteractable interactable)
    {
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out RaycastHit raycastHit, rayDuration, 1))
        {
            Debug.Log(raycastHit.collider.name);

            if (raycastHit.collider.gameObject.TryGetComponent<IInteractable>(out IInteractable _interactable))
            {
                
                interactable = _interactable;

                return true;
            }
        }
        interactable = null;

        return false;
    }
}
