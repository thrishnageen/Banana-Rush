using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public State currentState;

    void Update()
    {
        RunState();
    }

    void RunState()
    {
        State nextState = currentState?.RunCurrentState(); //if not null, run current state like idle

        if (nextState != null)
        {
            //switch to next state
            SwitchToNextState(nextState);
        }
    }

    void SwitchToNextState(State nextState)
    {
        currentState = nextState;
    }
}
