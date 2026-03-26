using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    InputSystem_Actions inputActions;
    CharacterController cc;
    Camera cam;
    Vector2 cursorLocation;
    public float mouseSens = 0.1f;
    void Awake()
    {
        inputActions = new InputSystem_Actions();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
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
        cursorLocation = inputActions.Player.Look.ReadValue<Vector2>();
        transform.Rotate(Vector3.up * cursorLocation.x * mouseSens);
        // Debug.Log(cursorLocation);
    }
}
