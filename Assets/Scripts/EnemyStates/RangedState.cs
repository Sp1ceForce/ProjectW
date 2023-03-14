using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections;

enum RSStates{
    RS_MovingToPlayer,
    RS_Aiming,
    RS_Shoot,
}
[Serializable]
public class RangedState : State
{
    [Header("Статы снаряда")]
    [SerializeField] ProjectileScript projectileObject;
    [SerializeField] int projectileDamage;
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileLifetime;
    [Header("Кулдауны")]
    [SerializeField] float castTime = 0.5f;
    [SerializeField] float reloadTime = 2f;

    [Header("Радиусы действия")]
    [SerializeField] float shootDistance;
    
    NavMeshAgent navAgent;
    RSStates currentState;
    
    GameObject playerObject;
    Witch playerWitchComponent;
    
    bool canShoot;
    bool isReloading;
    public override void InitState(GameObject EntityObject) 
    {
        base.InitState(EntityObject);
        navAgent = EntityObject.GetComponent<NavMeshAgent>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        currentState = RSStates.RS_MovingToPlayer;
        canShoot = false;
        playerWitchComponent = playerObject.GetComponent<Witch>();
        entity.GetComponent<EnemyKnockbackComponent>().OnKnockedBack+=OnKnockedBack;
    }
    public override State StateUpdate()
    {
        switch(currentState){
            case RSStates.RS_MovingToPlayer:
            MoveToPlayer();
            break;
            case RSStates.RS_Aiming:
            Aim();
            break;
        }
        if(showDebugInfo) DrawDebugInfo();
        if(playerWitchComponent.isHidden) return stateController.IdleState;
        return null;  
    }
    public override void StateExit()
    {
        base.StateExit();
        currentState = RSStates.RS_MovingToPlayer;
        canShoot = false;
        if(isReloading){
            stateController.StopCoroutine(Reload());
            isReloading = false;
        }
    }
    void MoveToPlayer(){
        if(!navAgent.enabled) return;
        navAgent.destination = playerObject.transform.position;

        if(Vector3.Distance(entity.transform.position,playerObject.transform.position)<shootDistance){
            currentState = RSStates.RS_Aiming;
            navAgent.isStopped = true;
            navAgent.ResetPath();
            navAgent.updateRotation = false;
        }
    }
    void OnKnockedBack(){
        navAgent.enabled = true;
        currentState = RSStates.RS_MovingToPlayer;
    }
    void Aim(){
        float distanceToPlayer = Vector3.Distance(playerObject.transform.position,entityTransform.position);
        
        
        if(distanceToPlayer > shootDistance) {
            currentState = RSStates.RS_MovingToPlayer;
            navAgent.updateRotation = true;
            navAgent.isStopped = false;
            return;
        }
        var lookRot = Quaternion.LookRotation(playerObject.transform.position - entity.transform.position,Vector3.up);
        entityTransform.rotation = Quaternion.Slerp(entityTransform.rotation,lookRot,0.5f); 
        if(canShoot){
            currentState = RSStates.RS_Shoot;
            stateController.StartCoroutine(Shoot());
        }
        else if(!canShoot && !isReloading){
            stateController.StartCoroutine(Reload());
        }

    }
    protected override void DrawDebugInfo()
    {
        base.DrawDebugInfo();
        DebugExtension.DebugWireSphere(entityTransform.position,Color.yellow,shootDistance,UpdateRate);
    }
    IEnumerator Shoot(){
        Vector3 shootTargetPos = playerObject.transform.position;
        if(showDebugInfo) Debug.DrawLine(entityTransform.position,playerObject.transform.position,Color.red,1.5f); 
        yield return new WaitForSeconds(castTime);
        var projectile = GameObject.Instantiate(projectileObject,stateController.AttackPoint.transform.position,Quaternion.LookRotation(shootTargetPos - entityTransform.position,Vector3.up));
        projectile.Init(projectileSpeed,projectileDamage,projectileLifetime);
        canShoot = false;
        currentState = RSStates.RS_Aiming;
        stateController.StartCoroutine(Reload());
    }   
    IEnumerator Reload(){
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
        isReloading = false;
    }
}
