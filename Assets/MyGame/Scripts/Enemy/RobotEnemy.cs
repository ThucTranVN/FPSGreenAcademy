using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class RobotEnemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private FirstPersonController player;
    private MyEnemyHealth enemyHealth;
    private const string PLAYER_TAG = "Player"; 

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<MyEnemyHealth>();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            if(enemyHealth != null)
            {
                enemyHealth.SelfDestroy();
            }
        }
    }
}
