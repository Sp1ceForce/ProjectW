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
        //TODO: Создать класс EnemyHealthComponent и при попадании в противника наносить ему урон
        //if(other.collider.GetComponent<EnemyHealthComponent>()){}
        Destroy(gameObject);
    }
}