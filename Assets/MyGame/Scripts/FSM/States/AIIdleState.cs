using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdleState : AIState
{
    private Vector3 playerDirection;
    private float maxSightDistance = 20;

    public void Enter(AIAgent agent)
    {
        agent.NavMeshAgent.ResetPath();
    }

    public void Exit(AIAgent agent)
    {
        
    }

    public AIStateID GetID()
    {
        return AIStateID.Idle;
    }

    public void Update(AIAgent agent)
    {
        if(agent.PlayerTransform == null)
        {
            return;
        }

        playerDirection = agent.PlayerTransform.position - agent.transform.position;

        if (playerDirection.magnitude > maxSightDistance)
        {
            return;
        }

        Vector3 agentDirection = agent.transform.forward;

        playerDirection.Normalize();

        float dotProduct = Vector3.Dot(playerDirection, agentDirection);

        if(dotProduct >= 0)
        {
            agent.StateMachine.ChangeState(AIStateID.ChasePlayer);
        }
    }
}
