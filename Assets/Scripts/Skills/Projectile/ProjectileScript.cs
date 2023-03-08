using UnityEngine;

public class ProjectileScript : MonoBehaviour {
    [SerializeField] float projectileMoveSpeed;
    [SerializeField] int projectileDamage;
    Rigidbody rb;

    public void Init(float moveSpeed, int damage, float lifetime){
        rb = GetComponent<Rigidbody>();
        projectileMoveSpeed = moveSpeed;
        projectileDamage = damage;
        Destroy(gameObject, lifetime);
    }
    private void Update() {
        rb.velocity = transform.forward * projectileMoveSpeed;
    }
    private void OnCollisionEnter(Collision other) {
        var healthComponent = other.collider.GetComponent<IHealthComponent>();
        if(healthComponent != null) healthComponent.TakeDamage(projectileDamage);
        Destroy(gameObject);
    }
}