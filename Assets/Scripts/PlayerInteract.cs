using Unity.Multiplayer.Center.Common.Analytics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public IInteractable interactable;
    bool interactInput;


    InputSystem_Actions inputActions;
    void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    void OnEnable()
    {
        inputActions.Enable();
    }
    void OnDisable()
    {
        inputActions.Disable();
    }
    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);



        bool hasInteracted = inputActions.Player.Interact.WasPressedThisFrame();


        if (hasInteracted && hit.collider != null && hit.collider.TryGetComponent<IInteractable>(out interactable))
        {
            interactable.Interact(hit.collider);
            Debug.Log("A");
        }


    }
}
