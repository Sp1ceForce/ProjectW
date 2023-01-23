using UnityEngine;
using UnityEngine.AI;

public class RunningState : State
{
    GameObject playerObject;
    NavMeshAgent navAgent;
    public RunningState(GameObject EntityObject) : base(EntityObject)
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        navAgent = EntityObject.GetComponent<NavMeshAgent>();
    }
    public override State StateUpdate()
    {
        navAgent.destination = playerObject.transform.position;
        return null;
    }
}
