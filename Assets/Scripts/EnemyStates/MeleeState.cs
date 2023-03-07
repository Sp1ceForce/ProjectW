using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections;
using System.Collections.Generic;

enum MSState{
    MS_Running,
    MS_PreparingAttack,
    MS_Charging,
    MS_Attacking
}

[Serializable]
public class MeleeState : State
{
    NavMeshAgent navAgent;
    GameObject playerObject;
    MSState currentState;
    [SerializeField] int attackDamage;
    [SerializeField] float attackPrepareTime = 1f;
    [SerializeField] float attackDistance;
    [SerializeField] float aimStartDistance;
    [SerializeField] float attackRadius;
    
    [SerializeField] bool showDebugInfo;

    public Action OnAttackWindupStart;
    public Action OnAttackPerformed;
    public override void InitState(GameObject EntityObject) 
    {
        base.InitState(EntityObject);
        playerObject = GameObject.FindGameObjectWithTag("Player");
        navAgent = EntityObject.GetComponent<NavMeshAgent>();
    }
    public override State StateUpdate()
    {
        switch(currentState){
            case MSState.MS_Running:
            MoveToPlayer();
            break;
            case MSState.MS_PreparingAttack:
            stateController.StartCoroutine(PrepareAttack());
            break;
            case MSState.MS_Attacking:
            Attack();
            break;
        }
        if(showDebugInfo){
            DrawDebugInfo();
        }
        return null;
    }
    void DrawDebugInfo(){
        DebugExtension.DebugWireSphere(stateController.AttackPoint.position,Color.red,attackRadius);
        DebugExtension.DebugWireSphere(entity.transform.position,Color.yellow,aimStartDistance);
        Debug.DrawLine(entity.transform.position,entity.transform.position + entity.transform.forward*attackDistance,Color.magenta);
    }
    void Attack(){
        var attackedEntities = Physics.OverlapSphere(stateController.AttackPoint.position,attackRadius,LayerMask.GetMask("Player"));
        foreach(var attackedEntity in attackedEntities){
            var healthComponent = attackedEntity.GetComponent<IHealthComponent>();
            if(healthComponent!= null){
                Debug.Log(attackedEntity.name);
                healthComponent.TakeDamage(attackDamage);
            }
        }
        navAgent.enabled = true;
        currentState = MSState.MS_Running;
    }
    IEnumerator PrepareAttack(){
        currentState= MSState.MS_Charging;
        navAgent.enabled = false;
        yield return new WaitForSeconds(attackPrepareTime);
        currentState = MSState.MS_Attacking;
    }
    void MoveToPlayer(){
        if(!navAgent.enabled) return;
        navAgent.destination = playerObject.transform.position;
        if(Vector3.Distance(entity.transform.position,playerObject.transform.position)<aimStartDistance){
            var lookRot = Quaternion.LookRotation(playerObject.transform.position - entity.transform.position,Vector3.up);
            entityTransform.rotation = Quaternion.Slerp(entityTransform.rotation,lookRot,0.5f);
            if(Vector3.Distance(stateController.AttackPoint.transform.position,playerObject.transform.position)<attackDistance){
                Debug.Log(Vector2.Distance(stateController.AttackPoint.transform.position,playerObject.transform.position));
                currentState = MSState.MS_PreparingAttack;
            }
        }
//Physics.OverlapSphere(stateController.AttackPoint.position,attackRadius,LayerMask.GetMask("Player")).Length>0
     
    }
}
