using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
public class FreezeComponent : MonoBehaviour {
    [SerializeField] float startSpeed;
    public TestHandler Stats;
    public void Init(TestHandler handler){
        var navMeshAgent = GetComponent<NavMeshAgent>();
        if(!navMeshAgent) {
         
         return;   
        }
        Stats = handler;
        startSpeed = navMeshAgent.speed;
        navMeshAgent.speed = startSpeed - (startSpeed/100)*Mathf.Clamp(handler.Freeze,0,100);            
        StartCoroutine(UnfreezeTime(handler.FreezeTime));
    }  
    IEnumerator UnfreezeTime(float freezeTime){
        yield return new WaitForSeconds(freezeTime);
        Unfreeze();
    }
    public void Unfreeze(){
        GetComponent<NavMeshAgent>().speed = startSpeed;
        Destroy(this);   
    }
}