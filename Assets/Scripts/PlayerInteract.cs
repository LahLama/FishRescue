using System.Collections.Generic;
using System.Xml.Schema;
using Unity.Multiplayer.Center.Common.Analytics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public IInteractable interactable;
    bool interactInput;

    GameObject leftHand;
    GameObject rightHand;
    InputSystem_Actions inputActions;
    void Awake()
    {
        inputActions = new InputSystem_Actions();
        leftHand = GameObject.FindGameObjectWithTag("invL");
        rightHand = GameObject.FindGameObjectWithTag("invR");
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
            {
                foreach (var script in hit.collider.GetComponents<IInteractable>())
                {
                    script.Interact(hit.collider);
                }
            }
        }
    }
}
