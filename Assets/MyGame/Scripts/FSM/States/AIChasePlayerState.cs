using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChasePlayerState : AIState
{
    private float timer = 0;
    private float maxTime;
    private float maxDistance;

    public void Enter(AIAgent agent)
    {
        
    }

    public void Exit(AIAgent agent)
    {
        
    }

    public AIStateID GetID()
    {
        return AIStateID.ChasePlayer;
    }

    public void Update(AIAgent agent)
    {
        if (!agent.NavMeshAgent.enabled)
        {
            return;
        }

        timer -= Time.deltaTime;
        //Debug.Log($"agent.NavMeshAgent.hasPath {agent.NavMeshAgent.hasPath}");
        //if (!agent.NavMeshAgent.hasPath)
        //{
            
        //}

        agent.NavMeshAgent.destination = agent.PlayerTransform.position;
    }
}
