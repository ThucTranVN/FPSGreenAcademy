using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AILocomotion : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private int SpeedId = Animator.StringToHash("Speed");

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent.hasPath)
        {
            animator.SetFloat(SpeedId, navMeshAgent.velocity.magnitude);
        }
        else
        {
            animator.SetFloat(SpeedId, 0);
        }
    }
}
