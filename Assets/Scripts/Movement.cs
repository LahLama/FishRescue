using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
namespace LahLama
{

    public class Movement : MonoBehaviour
    {

        public Vector2 moveInput;
        Rigidbody2D rb;
        Collider2D coll;
        public float moveSpeed = 5f;

        private PlayerInputActions inputActions;
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

        private void OnMove()
        {
            moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        }


        void FixedUpdate()
        {
            OnMove();
            // Horizontal movement
            rb.linearVelocity = moveInput.normalized * moveSpeed;
        }
    }
}