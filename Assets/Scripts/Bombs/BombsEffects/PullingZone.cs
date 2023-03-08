using UnityEngine;

public class PullingZone : MonoBehaviour {
    TestHandler stats;
    public void Init(TestHandler handler){
        stats = handler;
        GetComponent<SphereCollider>().radius = stats.Radius;
        Destroy(gameObject,stats.PullzoneLifetime);
    }  
    private void OnTriggerStay(Collider other) {
        var knockbackComponent = other.gameObject.GetComponent<EnemyKnockbackComponent>();
        if(!knockbackComponent) return;
        knockbackComponent.PullTowards(transform.position,stats.PullForce);
    }
}