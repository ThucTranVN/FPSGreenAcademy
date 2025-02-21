using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class RobotEnemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private FirstPersonController player;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(agent != null && player != null)
        {
            agent.SetDestination(player.transform.position);
        }
    }
}
