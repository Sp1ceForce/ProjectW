using UnityEngine;
using System;
public abstract class State {
    protected GameObject entity;
    protected Transform entityTransform;
    protected EnemyStateController stateController;
    public float UpdateRate;
    public Action OnStateEnter;
    public Action OnStateExit;
    [SerializeField] protected bool showDebugInfo;
    public virtual void InitState(GameObject EntityObject){
        entityTransform = EntityObject.transform;
        entity = EntityObject;
        stateController = EntityObject.GetComponent<EnemyStateController>();
    }
    public virtual void StateEnter(){
        OnStateEnter?.Invoke();
    }
    protected virtual void DrawDebugInfo(){}
    public virtual void StateExit(){
        OnStateExit?.Invoke();
    }
    public abstract State StateUpdate();
}
