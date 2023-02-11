using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class EnemyKnockbackComponent : MonoBehaviour
{
    float knockedTime = 0.3f;
    public void KnockBack(Transform Instigator, float knockbackForce){
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<EnemyStateController>().enabled = false;
        Vector3 knockdirection = transform.position - Instigator.transform.position;
        GetComponent<Rigidbody>().AddForce(knockdirection.normalized * knockbackForce,ForceMode.Impulse);
        StartCoroutine(KnockedCooldown());
    }
    IEnumerator KnockedCooldown(){
        yield return new WaitForSeconds(knockedTime);
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<EnemyStateController>().enabled = true;
    }
}
