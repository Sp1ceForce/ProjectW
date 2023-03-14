using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System;
public class EnemyKnockbackComponent : MonoBehaviour
{
    public Action OnKnockedBack;
    float knockedTime = 0.3f;
    public void KnockBack(Vector3 instigatorPosition, float knockbackForce){
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<EnemyStateController>().enabled = false;
        Vector3 knockdirection = transform.position - instigatorPosition;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(knockdirection.normalized * knockbackForce,ForceMode.Impulse);
        StartCoroutine(KnockedCooldown());
        OnKnockedBack?.Invoke();
    }
    public void PullTowards(Vector3 instigatorPosition, float knockbackForce){
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<EnemyStateController>().enabled = false;
        Vector3 knockdirection = instigatorPosition - transform.position;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(knockdirection.normalized * knockbackForce,ForceMode.Impulse);
        StartCoroutine(KnockedCooldown());
    }
    IEnumerator KnockedCooldown(){
        yield return new WaitForSeconds(knockedTime);
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<EnemyStateController>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
