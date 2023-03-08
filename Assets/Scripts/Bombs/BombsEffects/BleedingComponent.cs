using UnityEngine;
using System.Collections;
public class BleedingComponent : MonoBehaviour {
    TestHandler handler;
    public void Init(TestHandler handler){
        this.handler = handler;
        StartCoroutine(ApplyBleedingDamage());
    }    
    IEnumerator ApplyBleedingDamage(){
        float timeRemaining = handler.BleedingTime;
        var healthComponent = GetComponent<EnemyHealthComponent>();
        if(healthComponent!=null){
            while(timeRemaining>0){
                healthComponent.TakeDamage(handler.BleedingDamagePerTick);
                Debug.Log(healthComponent.CurrentHealth);
                yield return new WaitForSeconds(handler.BleedingTickInterval);
                timeRemaining-=handler.BleedingTickInterval;
            }
        }
    }
}