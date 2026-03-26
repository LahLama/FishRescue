using UnityEngine;

public class getState : MonoBehaviour, IInteractable
{
    public string Interact()
    {
        return GetComponent<foodStates>().currentState.ToString();
    }
}
