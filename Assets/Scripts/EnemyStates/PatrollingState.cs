using UnityEngine;
using UnityEngine.AI;
using System;
[Serializable]
public class PatrollingState : State
{
    NavMeshAgent navAgent;
    public override void InitState(GameObject EntityObject) 
    {
        base.InitState(EntityObject);
        navAgent = EntityObject.GetComponent<NavMeshAgent>();
    }
    public override State StateUpdate()
    {
        if(!navAgent.enabled) return null;
        return null;
    }
}
