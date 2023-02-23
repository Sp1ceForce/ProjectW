using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHandler : MonoBehaviour
{
    public Item bombItem;

    public int Damage;
    public float Freeze;
    public float TimeFreeze;
    public float Radius;
    public float PushForce;
    public float Pulling;
    public float DamagePerSecond;
    public float TimeCloud;

    public void UseBombBase(BaseBombIng baseBomb)
    {
        Damage = baseBomb.BaseDamage;
        Freeze = baseBomb.BaseFreeze;
        TimeFreeze = baseBomb.BaseTimeFreeze;
        Radius = baseBomb.BaseRadius;
        PushForce = baseBomb.BasePushForce;
        Pulling = baseBomb.BasePulling;
        DamagePerSecond = baseBomb.BaseDamagePerSecond;
        TimeCloud = baseBomb.BaseTimeCloud;
    }
    public void UseBombMod(BaseBombIng baseBomb)
    {
        Damage *= baseBomb.ModDamage;
        Freeze *= baseBomb.ModFreeze;
        TimeFreeze *= baseBomb.ModTimeFreeze;
        Radius *= baseBomb.ModRadius;
        PushForce *= baseBomb.ModPushForce;
        Pulling *= baseBomb.ModPulling;
        DamagePerSecond *= baseBomb.ModDamagePerSecond;
        TimeCloud *= baseBomb.ModTimeCloud;
    }
    public void InitFromAnotherHandler(BombHandler handler)
    {
        bombItem = handler.bombItem;

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
