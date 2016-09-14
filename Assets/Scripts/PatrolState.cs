using System.Collections;
using UnityEngine;

public class PatrolState : FSMState {
    
    private int AlertDistanceInMeters = 10;
    private int SecondsToMoveToAnotherWaypoint = 10;
    private Transform[] Waypoints;
    private int MovementVelocity;
    private System.Random rand;

    public PatrolState(Transform[] waypoints, int movementVelocity = 10)
    {
        Waypoints = waypoints;
        MovementVelocity = movementVelocity;
    }

    public override void Reason(GameObject player, GameObject npc)
    {
        // if player is in range
        if(Vector3.Distance(player.transform.position, npc.transform.position) <= AlertDistanceInMeters )
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.Alert);
        }
    }

    public override void Act(GameObject player, GameObject npc)
    {
       
        if(TimeoutStatus(npc) == FSMTimeoutStatus.NotStarted)
        {
            SetNewTimeout(npc,SecondsToMoveToAnotherWaypoint);
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

            // set new timeout
            SetNewTimeout(npc, 10);
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