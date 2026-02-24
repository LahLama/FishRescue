using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
namespace LahLama
{

    public class Interact : MonoBehaviour
    {

        public float interactInput;
        private PlayerInputActions inputActions;
        private IInteractable currentInteractible;
        void Awake()
        {
            inputActions = new PlayerInputActions(); // Initialize Input Actions
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