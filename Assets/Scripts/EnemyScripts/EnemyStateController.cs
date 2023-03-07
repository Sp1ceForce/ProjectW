using UnityEngine;
using System.Collections;
public enum EnemyBehaviourType{
    EBT_Idle,
    EBT_Patrolling,
    EBT_RangedFighter,
    EBT_MeleeFighter,
    EBT_Running
}

public static class StateFactory{
    public static State CreateState(EnemyBehaviourType behaviourType, GameObject entity){
        switch(behaviourType){
            case EnemyBehaviourType.EBT_Idle:
            return new StandingState();
            case EnemyBehaviourType.EBT_Patrolling: return new PatrollingState();
            case EnemyBehaviourType.EBT_RangedFighter: return new RangedState();
            case EnemyBehaviourType.EBT_MeleeFighter: return new MeleeState();
            case EnemyBehaviourType.EBT_Running: return new RunningState();
            default:
            Debug.Log("Such state is not implemented in the state factory");
            return new StandingState();
        }
    }
} 
public class EnemyStateController : MonoBehaviour
{
    public State CurrentState {get; private set;}
    public EnemyBehaviourType IdleType;
    public EnemyBehaviourType BattleType;
    public Transform AttackPoint;
    [SerializeReference] public State IdleState;
    [SerializeReference] public State BattleState;
    public bool IsAiming;
    void Start()
    {
        IdleState.InitState(gameObject);
        BattleState.InitState(gameObject);
        CurrentState = IdleState;
        StartCoroutine(AIUpdate());
    }
    [ContextMenu("Generate states classes")]
    public void GenerateStates(){
        IdleState = StateFactory.CreateState(IdleType,gameObject);
        BattleState = StateFactory.CreateState(BattleType,gameObject);
    }
    public void SetState(State newState) {
        CurrentState = newState;
    }
    // Update is called once per frame
    IEnumerator AIUpdate(){
        var waitForSeconds = new WaitForSeconds(0.05f);
        while(true){
            yield return waitForSeconds;
            var newState = CurrentState.StateUpdate();
            if(newState !=null){
                SetState(newState);
            }
        }
    }


}
