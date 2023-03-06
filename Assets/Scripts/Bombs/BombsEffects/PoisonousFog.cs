using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class PoisonousFog : MonoBehaviour {
    
    TestHandler stats;
    List<IHealthComponent> entitiesInside;
    
    public void Init(TestHandler handler){
        entitiesInside = new List<IHealthComponent>();
        stats = handler;
        GetComponent<SphereCollider>().radius = stats.Radius;
        StartCoroutine(DamageRoutine());
        Destroy(gameObject,stats.PullzoneLifetime);
    }  
    IEnumerator DamageRoutine(){
        while(true){
            yield return new WaitForSeconds(stats.CloudTickInterval);
            foreach(var entity in entitiesInside){
                if(entity != null)
                entity.TakeDamage(stats.CloudDamagePerTick);
            }
            entitiesInside.RemoveAll(x => x == null);
        }
    }
    private void OnTriggerEnter(Collider other) {
        var hpComponent = other.GetComponent<IHealthComponent>();
        if(hpComponent != null && !entitiesInside.Contains(hpComponent)) entitiesInside.Add(hpComponent);
    }
    private void OnTriggerExit(Collider other) {
        var hpComponent = other.GetComponent<IHealthComponent>();
        if(hpComponent != null && entitiesInside.Contains(hpComponent)) entitiesInside.Remove(hpComponent);
    }
}