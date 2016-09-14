using System.Collections;
using UnityEngine;

public class GameObjectUtils
{
    public static void RotateTowardsPosition(GameObject obj, Vector3 position)
    {
        obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation,Quaternion.LookRotation(position),5 * Time.deltaTime);
        obj.transform.eulerAngles = new Vector3(0, obj.transform.eulerAngles.y, 0);
    }

    public static void MoveToPositionNavMesh(GameObject obj, Vector3 position)
    {
        // run obj movement animation

        obj.GetComponent<NavMeshAgent>().SetDestination(position);
    }

    public static bool NpcSeesTarget(GameObject npc, GameObject target)
    {
        // TODO: Vanilson
        float coneOfVision = 60; // 60 degrees
        float angle = Vector3.Angle(npc.transform.forward, target.transform.position);
        if(angle <= coneOfVision)
        {
            return true;
        }
        return false;
    }

}

public class FSMTimeout
{
    public IEnumerator Coroutine { get; set; }
    public FSMTimeoutStatus Status { get; private set; }

    public FSMTimeout(float waitTime)
    {
        this.Status = FSMTimeoutStatus.NotStarted;
        Coroutine = WaitAndSetValue(waitTime);
    }

    private IEnumerator WaitAndSetValue(float waitTime)
    {
        this.Status = FSMTimeoutStatus.Started;
        yield return new UnityEngine.WaitForSeconds(waitTime);
        this.Status = FSMTimeoutStatus.ReachedZero;
    }
}

public enum FSMTimeoutStatus
{
    NotStarted = 0,
    Started = 1,
    ReachedZero = 2
}