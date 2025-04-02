using System;
using UnityEngine;

public class AIStateMachine 
{
    public AIState[] states;
    public AIAgent agent;
    public AIStateID currentState;

    public AIStateMachine(AIAgent agent)
    {
        this.agent = agent;
        int numStates = Enum.GetNames(typeof(AIStateID)).Length;
        states = new AIState[numStates];
    }

    public void RegisterState(AIState state)
    {
        int index = (int)state.GetID();
        states[index] = state;
    }

    public AIState GetState(AIStateID stateId)
    {
        int index = (int)stateId;
        return states[index];
    }

    public void ChangeState(AIStateID newState)
    {
        GetState(currentState)?.Exit(agent);
        currentState = newState;
        GetState(currentState)?.Enter(agent);
    }

    public void Update()
    {
        GetState(currentState)?.Update(agent);
    }
}
