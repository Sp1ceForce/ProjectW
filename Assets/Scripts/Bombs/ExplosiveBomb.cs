using UnityEngine;

[CreateAssetMenu(fileName = "ExplosiveBomb", menuName = "ProjectW/Bombs/ExplosiveBomb", order = 0)]
public class ExplosiveBomb : BaseBomb {
    [Header("Характеристики обычной бомбы")]
    [SerializeField] int damage;
    [SerializeField] float pushForce;
    protected override void ExplosionLogic(GameObject Instigator, Vector3 ExplosionPosition, Collider[] EntitiesHit){
        DealDamage(EntitiesHit,damage);
        PushAway(EntitiesHit,pushForce,ExplosionPosition);
    }
}