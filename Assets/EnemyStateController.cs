using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
    public State CurrentState {get; private set;}
    public State IdleState {get; private set;}
    [SerializeField] float detectionRadius = 5f;
    public State RunningState {get; private set;}
    void Start()
    {
        IdleState = new IdleState(gameObject,detectionRadius);
        RunningState = new RunningState(gameObject); 
        CurrentState = IdleState;
    }
    public void SetState(State newState) {
        CurrentState = newState;
    }
    // Update is called once per frame
    void Update()
    {
        var newState = CurrentState.StateUpdate();
        if(newState !=null){
            SetState(newState);
        }
    }
}
