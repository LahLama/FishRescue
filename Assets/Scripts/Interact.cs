using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
namespace LahLama
{

    public class Interact : MonoBehaviour
    {

        public float interactInput;
        Rigidbody2D rb;
        Collider2D coll;
        private PlayerInputActions inputActions;
        private IInteractable currentInteractible;
        IInteractable isInteractible;
        void Awake()
        {
            inputActions = new PlayerInputActions(); // Initialize Input Actions
            rb = GetComponent<Rigidbody2D>();
            coll = GetComponent<Collider2D>();
        }
        void OnEnable()
        {
            inputActions.Enable();
        }

        void OnDisable()
        {
            inputActions.Disable();
        }

        void FixedUpdate()
        {
            IsInteracting();
            if (currentInteractible != null && IsInteracting() > 0)
            {
                currentInteractible.Interact();
            }
        }

        float IsInteracting()
        {
            interactInput = inputActions.Player.Interact.ReadValue<float>();
            return interactInput;
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent<IInteractable>(out var interactable))
            {
                currentInteractible = interactable;
            }
        }

        void OnTriggerExit2D(Collider2D collider)
        {
            currentInteractible = null;
        }
    }
}