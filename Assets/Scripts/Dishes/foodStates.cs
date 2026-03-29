using System;
using UnityEngine;

public class foodStates : MonoBehaviour
{
    public enum State { UnWashed, Raw, Preping, Done, Trash }

    public State currentState = State.Raw;


    public void SetState(State newState)
    {
        currentState = newState;
    }


}
