using UnityEngine;
using System;
public abstract class State {
    protected GameObject entity;
    protected Transform entityTransform;
    protected EnemyStateController stateController;
    public Action OnStateEnter;
    public Action OnStateExit;
    public State(GameObject EntityObject){
        entityTransform = EntityObject.transform;
        entity = EntityObject;
        stateController = EntityObject.GetComponent<EnemyStateController>();
    }
    public virtual void StateEnter(){
        OnStateEnter?.Invoke();
    }
    public virtual void StateExit(){
        OnStateExit?.Invoke();
    }
    public abstract State StateUpdate();
}
