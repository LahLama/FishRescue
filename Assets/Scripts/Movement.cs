using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
namespace LahLama
{

    public class Movement : MonoBehaviour
    {

        public Vector2 moveInput;
        private bool jumpPressed = false;
        public GameObject player;
        Rigidbody2D rb;
        Collider2D coll;
        public float moveSpeed = 5f;
        public float jumpForce = 5f;


        private void Start()
        {
            rb = player.GetComponent<Rigidbody2D>();
            coll = player.GetComponent<Collider2D>();
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
                jumpPressed = true;
        }

        void FixedUpdate()
        {
            // Horizontal movement
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        }
    }
}