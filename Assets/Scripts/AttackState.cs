using UnityEngine;

public class AttackState : FSMState
{
    public AttackState()
    {
        stateID = StateID.AttackingPlayer;
    }

    public override void Reason(GameObject player, GameObject npc)
    {
        //DEBUG
        npc.GetComponent<Renderer>().material.color = Color.red ;
        // END DEBUG

        // if it's seeing the player, there is nothing to do (cause this is the AttackState after all)
        if (GameObjectUtils.NpcSeesTarget(npc, player))
        {
            StopTimeout(npc);
            return;
        }

        // but if it's not seeing the player, the countdown will start
        // if the enemy cant see the plauer for N seconds, then i will transition back to Alert State
        if (TimeoutStatus(npc) == FSMTimeoutStatus.NotStarted)
            SetNewTimeout(npc, NPCControl.TimeCantSeePlayer);

        // if the timer reaches zero, it means that the enemy couldnt see the player for N seconds
        if ( TimeoutStatus(npc) == FSMTimeoutStatus.ReachedZero )
        {
            StopTimeout(npc);
            npc.GetComponent<NPCControl>().SetTransition(Transition.Alert);
        }
    }

    public override void Act(GameObject player, GameObject npc)
    {
        // shoot in the player direction
    }

    private FSMTimeoutStatus TimeoutStatus(GameObject npc)
    {
        return npc.GetComponent<NPCControl>().TimeoutReachedZero;
    }
    
    private void SetNewTimeout(GameObject npc, float waitTime)
    {
        npc.GetComponent<NPCControl>().StartTimeout(waitTime);
    }

    private void StopTimeout(GameObject npc)
    {
        npc.GetComponent<NPCControl>().StopTimeout();
    }
}