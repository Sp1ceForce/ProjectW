using UnityEngine;
using UnityEngine.AI;
using System;
[Serializable]
public class RunningState : State
{
    GameObject playerObject;
    NavMeshAgent navAgent;
    public override void InitState(GameObject EntityObject) 
    {
        base.InitState(EntityObject);
        playerObject = GameObject.FindGameObjectWithTag("Player");
        navAgent = EntityObject.GetComponent<NavMeshAgent>();
        navAgent.updateRotation = false;
    }
    public override State StateUpdate()
    {
        if(!navAgent.enabled) return null;
        navAgent.destination = playerObject.transform.position;
        if (navAgent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            entityTransform.rotation = Quaternion.LookRotation(navAgent.velocity.normalized);
        }
        return null;
    }
}
