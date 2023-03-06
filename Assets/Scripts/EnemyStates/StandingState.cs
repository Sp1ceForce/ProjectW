using UnityEngine;
using System;
[Serializable]
public class StandingState : State
{
    [SerializeField] float detectionRadius = 5f;
    [SerializeField] bool showDebugInfo;
    public override void InitState(GameObject EntityObject)
    {
        base.InitState(EntityObject);
        EntityObject.GetComponent<EnemyHealthComponent>().OnTakeDamage +=OnTakeDamage;
    }
    void OnTakeDamage(){
        stateController.SetState(stateController.BattleState);
    }
    public override State StateUpdate()
    {
        if(showDebugInfo) DebugExtension.DebugWireSphere(entityTransform.position,Color.green,detectionRadius);
        if(Physics.OverlapSphere(entityTransform.position,detectionRadius,LayerMask.GetMask("Player")).Length !=0) return stateController.BattleState;
        return null;
    }
}
