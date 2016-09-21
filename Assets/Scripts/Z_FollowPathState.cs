using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class FollowPathState : FSMState
{
    //private int CurrentWayPoint;
    //private Transform[] Waypoints;

    public FollowPathState(/*Transform[] waypoints*/)
    {
        //this.Waypoints = waypoints;

        stateID = StateID.FollowingPath;
    }

    public override void Reason(GameObject player, GameObject npc)
    {
        /*
        RaycastHit hit;
        if (Physics.Raycast(npc.transform.position, npc.transform.forward, out hit, 5F))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                Debug.Log("FOUND PLAYER");
                npc.GetComponent<NPCControl>().SetTransition(Transition.SawPlayer);
            }
        }
        */

        // If the Player passes less than 15 meters away in front of the NPC
        if (Vector3.Distance(npc.transform.position, player.transform.position) < 10F)
        {
            Debug.Log("FOUND PLAYER");
            npc.GetComponent<NPCControl>().SetTransition(Transition.SawPlayer);
        }
        Debug.Log("Reason: FollowPathState");
    }

    public override void Act(GameObject player, GameObject npc)
    {
        // Follow the path of waypoints
        // Find the direction of the current way point 

        Debug.Log("Act: FollowPathState");
        #region cycling through areas
        /*
        Vector3 vel = npc.rigidbody.velocity;
        Vector3 moveDir = waypoints[currentWayPoint].position - npc.transform.position;

        if (moveDir.magnitude < 1)
        {
            currentWayPoint++;
            if (currentWayPoint >= waypoints.Length)
            {
                currentWayPoint = 0;
            }
        }
        else
        {
            vel = moveDir.normalized * 10;

            // Rotate towards the waypoint
            npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation,
                                                      Quaternion.LookRotation(moveDir),
                                                      5 * Time.deltaTime);
            npc.transform.eulerAngles = new Vector3(0, npc.transform.eulerAngles.y, 0);

        }

        // Apply the Velocity
        npc.rigidbody.velocity = vel;
        */
        #endregion

    }

    public override string ToString()
    {
        return "FollowPathState";
    }

} // FollowPathState