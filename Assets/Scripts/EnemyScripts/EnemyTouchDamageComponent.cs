using UnityEngine;

public class EnemyTouchDamageComponent : MonoBehaviour
{
    [SerializeField] int damage;
    private void OnCollisionEnter(Collision other) {
        var healthComp = other.collider.GetComponent<Witch>();
        if(healthComp) healthComp.TakeDamage(damage);
    }
}
