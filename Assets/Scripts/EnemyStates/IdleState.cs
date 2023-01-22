using UnityEngine;

public class IdleState : State
{
    float detectionRadius;
    public IdleState(GameObject EntityObject, float DetectionRadius) : base(EntityObject)
    {
        detectionRadius = DetectionRadius;
        EntityObject.GetComponent<EnemyHealthComponent>().OnTakeDamage +=OnTakeDamage;
    }
    void OnTakeDamage(){
        stateController.SetState(stateController.RunningState);
    }
    public override State StateUpdate()
    {
        if(Physics.OverlapSphere(entityTransform.position,detectionRadius,LayerMask.GetMask("Player")).Length !=0) return stateController.RunningState;
        return null;
    }
}
