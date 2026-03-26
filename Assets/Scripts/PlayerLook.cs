using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    InputSystem_Actions inputActions;
    CharacterController cc;
    Camera cam;
    Vector2 cursorLocation;
    float verticalRotation = 0f;
    public float verticalClamp = 80f;
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
        // Vertical — rotate the camera, clamped
        verticalRotation -= cursorLocation.y * mouseSens;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalClamp, verticalClamp);
        cam.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

    }
}
