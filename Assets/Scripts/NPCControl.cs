using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NPCControl : MonoBehaviour
{
    #region global FSM state variables
    public static float TimeCantSeePlayer = 10;
    public static int AlertDistanceInMeters = 20;
    public static int SecondsToMoveToAnotherWaypoint = 5;
    #endregion

    public GameObject target; // target = player; //gameObject = startPoint
    public Transform[] PatrolWayPoints;
    FSMSystem fsm;

    // timeout related
    private FSMTimeout timeout;
    public FSMTimeoutStatus TimeoutReachedZero { get { return timeout != null ? timeout.Status : FSMTimeoutStatus.NotStarted; } }

    public void SetTransition(Transition t) { fsm.PerformTransition(t); }

    public void Start()
    {
        MakeFSM();
    }

    public void FixedUpdate()
    {
        fsm.CurrentState.Reason(target, gameObject);
        fsm.CurrentState.Act(target, gameObject);
        Debug.DrawLine(transform.position, transform.forward * 50);
    }

    // The NPC has two states: FollowPath and ChasePlayer
    // If it's on the first state and SawPlayer transition is fired, it changes to ChasePlayer
    // If it's on ChasePlayerState and LostPlayer transition is fired, it returns to FollowPath
    private void MakeFSM()
    {
        //FollowPathState follow = new FollowPathState(/*path*/);
        //follow.AddTransition(Transition.SawPlayer, StateID.ChasingPlayer);

        //ChasePlayerState chase = new ChasePlayerState();
        //chase.AddTransition(Transition.LostPlayer, StateID.FollowingPath);

        PatrolState patrolState = new PatrolState(PatrolWayPoints);
        patrolState.AddTransition(Transition.Alert, StateID.AlertNpc);
        patrolState.AddTransition(Transition.Attack, StateID.AttackingPlayer);

        AlertState alertState = new AlertState();
        alertState.AddTransition(Transition.Attack, StateID.AttackingPlayer);
        alertState.AddTransition(Transition.Patrol, StateID.Patroling);

        AttackState attackState = new AttackState();
        attackState.AddTransition(Transition.Alert, StateID.AlertNpc);


        fsm = new FSMSystem();
        fsm.AddState(patrolState);
        fsm.AddState(alertState);
        fsm.AddState(attackState);
        //fsm.AddState(chase);
        //fsm.AddState(follow);

        Debug.Log("First State: " + fsm.CurrentState.ToString());

    }

    // i can only have one active FSM state
    // If the state needs to use some kind of countdown, it can make use of this timeout system based on coroutines.
    // Everytime I set a new Timeout, I need to stop the last timeout (aka stop the coroutine if it exists) and
    //   start a new timeout and a new coroutine.
    // This method can be called by any FSM State (since every Reason and Act state methods have acess to the GameObject, and
    //   there is only one FSM for every gameObject (so it's a 1:1 relation).
    // The call on the FSM State is like npc.getComponent<NPCControl>().StartTimeout(time);
    public void StartTimeout(float waitTime)
    {
        StopTimeout();
        timeout = new FSMTimeout(waitTime);
        StartCoroutine(timeout.Coroutine);
    }

    public void StopTimeout()
    {
        if(timeout != null)
        {
            StopCoroutine(timeout.Coroutine);
            timeout = null;
        }
    }
}