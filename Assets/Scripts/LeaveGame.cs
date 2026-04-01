using UnityEngine;
using UnityEngine.InputSystem;

public class LeaveGame : MonoBehaviour
{
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
        if (inputActions.UI.Cancel.triggered)
        {
            Application.Quit();
        }
    }
    public void onClick()
    {
        Application.Quit();
    }
}
