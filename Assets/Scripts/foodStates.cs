using System;
using UnityEngine;

public class foodStates : MonoBehaviour
{
    public enum State { Raw, Preping, Done, Trash }

    public State currentState = State.Raw;

    void Update()
    {
        switch (currentState)
        {
            case State.Raw: HandleRaw(); break;
            case State.Preping: HandlePreping(); break;
            case State.Done: HandleDone(); break;
            case State.Trash: HandleTrash(); break;
        }

    }

    public void SetState(State newState)
    {
        currentState = newState;
    }
    private void HandlePreping()
    {
        //
    }

    private void HandleRaw()
    {
        //
    }
    private void HandleTrash()
    {
        //
    }
    private void HandleDone()
    {
        //
    }

}
