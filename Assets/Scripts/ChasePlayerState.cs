using UnityEngine;
using System.Collections;

public class ChasePlayerState : FSMState
{
    public ChasePlayerState()
    {
        stateID = StateID.ChasingPlayer;
    }

    public override void Reason(GameObject player, GameObject npc)
    {
        // If the player has gone 30 meters away from the NPC, fire LostPlayer transition
        if (Vector3.Distance(npc.transform.position, player.transform.position) >= 10)
        {
            Debug.Log("Reason: lost player");
            npc.GetComponent<NPCControl>().SetTransition(Transition.LostPlayer);
        }
        Debug.Log("Reason: ChasePlayerState");
    }

    public override void Act(GameObject target, GameObject npc)
    {
        // Follow the path of waypoints
        // Find the direction of the player 

        npc.GetComponent<NavMeshAgent>().SetDestination(target.transform.position);

        Debug.Log("Act: ChasePlayerState");

        //var rigidBody = npc.GetComponent<Rigidbody>();

        //Vector3 vel = rigidBody.velocity;
        //Vector3 moveDir = player.transform.position - npc.transform.position;

        //// Rotate towards the waypoint
        //npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation,
        //                                          Quaternion.LookRotation(moveDir),
        //                                          5 * Time.deltaTime);
        //npc.transform.eulerAngles = new Vector3(0, npc.transform.eulerAngles.y, 0);

        //vel = moveDir.normalized * 10;

        //// Apply the new Velocity
        //rigidBody.velocity = vel;
    }

    public override string ToString()
    {
        return "ChasePlayerState";
    }

} // ChasePlayerState