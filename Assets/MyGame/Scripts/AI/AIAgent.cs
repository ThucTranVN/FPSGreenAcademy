using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    public AIStateID InitState;
    public AIStateMachine StateMachine;
    public NavMeshAgent NavMeshAgent;
    public Animator Animator;
    public Transform PlayerTransform;


    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        StateMachine = new AIStateMachine(this);
        StateMachine.RegisterState(new AIIdleState());
        StateMachine.RegisterState(new AIChasePlayerState());
        StateMachine.ChangeState(InitState);
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.Update();
    }
}
