using UnityEngine;
using UnityEngine.AI;
using System;
[Serializable]
public class StandingState : State
{
    [SerializeField] float detectionRadius = 5f;
    Vector3 startPos;
    public override void InitState(GameObject EntityObject)
    {
        base.InitState(EntityObject);
        EntityObject.GetComponent<EnemyHealthComponent>().OnTakeDamage +=OnTakeDamage;
        startPos = entityTransform.position;
    }
    public override void StateEnter()
    {
        base.StateEnter();
        entity.GetComponent<NavMeshAgent>().destination = startPos;
    }
    void OnTakeDamage(){
        stateController.SetState(stateController.BattleState);
    }
    public override State StateUpdate()
    {
        if(showDebugInfo) DrawDebugInfo();
        if(Physics.OverlapSphere(entityTransform.position,detectionRadius,LayerMask.GetMask("Player")).Length !=0) return stateController.BattleState;
        return null;
    }
    protected override void DrawDebugInfo()
    {
        DebugExtension.DebugWireSphere(entityTransform.position,Color.green,detectionRadius);
    }
}
