using System.Collections;
using UnityEngine;

public class PatrolState : FSMState
{
    private Transform[] Waypoints;
    private int MovementVelocity;
    private System.Random rand;

    public PatrolState(Transform[] waypoints, int movementVelocity = 10)
    {
        Waypoints = waypoints;
        MovementVelocity = movementVelocity;
        stateID = StateID.Patroling;
    }

    public override void Reason(GameObject player, GameObject npc)
    {
        //DEBUG
        npc.GetComponent<Renderer>().material.color = Color.green;
        // END DEBUG

        // if player is in range
        if (Vector3.Distance(player.transform.position, npc.transform.position) <= NPCControl.AlertDistanceInMeters )
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.Alert);
        }

        if (GameObjectUtils.NpcSeesTarget(npc, player))
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.Attack);
        }
    }

    public override void Act(GameObject player, GameObject npc)
    {
       
        if(TimeoutStatus(npc) == FSMTimeoutStatus.NotStarted)
        {
            SetNewTimeout(npc, NPCControl.SecondsToMoveToAnotherWaypoint);
        }


        // move to another waypoint if the timeout reaches zero
        if(TimeoutStatus(npc) == FSMTimeoutStatus.ReachedZero)
        {
            // get a random waypoint index
            rand = new System.Random();
            int waypointIndex = rand.Next(0, Waypoints.Length);

            // rotate towards the waypoint direction (EDIT: since i'm using navMeshAgent, i dont need to worry rotating the character)
            //RotateTowardsPosition(npc,Waypoints[waypointIndex]);;

            // move to the waypoint (using navMeshAgent)
            GameObjectUtils.MoveToPositionNavMesh(npc,Waypoints[waypointIndex].position);
            Debug.Log("Moving to waypoint " + waypointIndex);

            // set new timeout
            SetNewTimeout(npc, NPCControl.SecondsToMoveToAnotherWaypoint);
        }

        
    }

    private FSMTimeoutStatus TimeoutStatus(GameObject npc)
    {
        return npc.GetComponent<NPCControl>().TimeoutReachedZero;
    }

    // sets a timeout for the enemy to move to another waypoint
    private void SetNewTimeout(GameObject npc, float waitTime)
    {
        npc.GetComponent<NPCControl>().StartTimeout(waitTime);
    }

}