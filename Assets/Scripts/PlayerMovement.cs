using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    InputSystem_Actions inputActions;
    CharacterController cc;
    float moveScale = 1;
    public float walkMod = 1f;

    public float sprintMod = 4f;


    void Awake()
    {
        inputActions = new InputSystem_Actions();
        cc = GetComponent<CharacterController>();
        moveScale = walkMod;
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
        Vector2 moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        float jumpInput = inputActions.Player.Jump.ReadValue<float>();
        bool sprintInput = inputActions.Player.Sprint.ReadValue<float>() > 0;

        Vector3 move3d = new Vector3(moveInput.x, jumpInput, moveInput.y);
        cc.Move(move3d * moveScale * Time.deltaTime);

        if (!cc.isGrounded)
        {
            Vector3 gravityDown = new Vector3(0, -9.8f, 0);
            cc.SimpleMove(gravityDown);
        }

        if (sprintInput)
        {
            moveScale = sprintMod;
        }
        else
        {
            moveScale = walkMod;
        }
        // Debug.Log(move3d * moveScale);
    }
}
