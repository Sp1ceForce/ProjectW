using UnityEngine;


[CreateAssetMenu(fileName = "ExplosiveBomb", menuName = "ProjectW/Bombs/ExplosiveBomb", order = 0)]
public class ExplosiveBomb : BaseBomb
{

    public int Damage;
    public float Freeze;
    public float TimeFreeze;
    public float Radius;
    public float PushForce;
    public float Pulling;
    public float DamagePerSecond;
    public float TimeCloud;

    protected override void ExplosionLogic(GameObject Instigator, Vector3 ExplosionPosition, Collider[] EntitiesHit)
    {
        if (Damage != 0)
            DealDamage(EntitiesHit, Damage);
        if (PushForce != 0)
            PushAway(EntitiesHit, PushForce, ExplosionPosition);
        if (Freeze != 0) { }
        if (Pulling != 0) { }
        if (DamagePerSecond != 0) { }
        if (TimeCloud != 0) { }

    }

    public void useBombHandler(BombHandler handler)
    {
        Damage = handler.Damage;
        Freeze = handler.Freeze;
        TimeFreeze = handler.TimeFreeze;
        Radius = handler.Radius;
        PushForce = handler.PushForce;
        Pulling = handler.Pulling;
        DamagePerSecond = handler.DamagePerSecond;
        TimeCloud = handler.TimeCloud;
    }
}