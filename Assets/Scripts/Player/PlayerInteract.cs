using UnityEngine.UI;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public IInteractable interactable;
    bool interactInput;

    GameObject leftHand;
    GameObject rightHand;
    InputSystem_Actions inputActions;
    public RectTransform reticle;
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
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 12f);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);


        bool isLookingAtInteractable = hit.collider != null && hit.collider.TryGetComponent<IInteractable>(out interactable);
        bool hasInteracted = inputActions.Player.Interact.WasPressedThisFrame();

        if (isLookingAtInteractable)
        {
            // Change reticle to half the size
            reticle.gameObject.GetComponent<RawImage>().color = Color.grey;
            reticle.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            // Change reticle back to original size
            reticle.gameObject.GetComponent<RawImage>().color = Color.white;
            reticle.localScale = new Vector3(1f, 1f, 1f);
        }

        if (hasInteracted && isLookingAtInteractable)
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
