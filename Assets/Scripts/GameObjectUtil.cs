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
        obj.GetComponent<NavMeshAgent>().Resume();
        obj.GetComponent<NavMeshAgent>().SetDestination(position);
    }

    public static void StopNavMesh(GameObject obj)
    {
        obj.GetComponent<NavMeshAgent>().Stop();
    }

    public static bool NpcSeesTarget(GameObject npc, GameObject target)
    {
        // TODO: Vanilson
        float fieldOfViewRange = 60.0F; // 60 degrees
        float npcRayMaximumView = 100.0F; // 20 meters, for instance

        Vector3 rayDirectionFromNpcToTarget = target.transform.position - npc.transform.position;
        // angleBetweenNpcAndTarget <= fieldOfViewRange
        if (Vector3.Angle(rayDirectionFromNpcToTarget, npc.transform.forward) <= fieldOfViewRange)
        {
            RaycastHit hit;
            if(Physics.Raycast(npc.transform.position,rayDirectionFromNpcToTarget,out hit, npcRayMaximumView))
            {
                return hit.transform.tag == "Player";
            }
        }
        return false;
    }

    public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
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