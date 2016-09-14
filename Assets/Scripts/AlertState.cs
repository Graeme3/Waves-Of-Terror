using UnityEngine;

public class AlertState : FSMState
{
    private float TimeCantSeePlayer = 10;

    public override void Reason(GameObject player, GameObject npc)
    {
        

        if (GameObjectUtils.NpcSeesTarget(npc, player))
        {
            StopTimeout(npc);
            npc.GetComponent<NPCControl>().SetTransition(Transition.Attack);
        }
        else
        {
            if(TimeoutStatus(npc) == FSMTimeoutStatus.NotStarted)
            {
                SetNewTimeout(npc, TimeCantSeePlayer);
            }

            if (TimeoutStatus(npc) == FSMTimeoutStatus.ReachedZero)
            {
                // back to patrol state
                npc.GetComponent<NPCControl>().SetTransition(Transition.Patrol);
            }
        }
    }

    public override void Act(GameObject player, GameObject npc)
    {
        // run EnemyAlert animation

        // the npc should be looking around?
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