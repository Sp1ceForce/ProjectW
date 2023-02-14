using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHandler : MonoBehaviour
{
    public float Damage;
    public float Freeze;
    public float TimeFreeze;
    public float Radius;
    public float DistancePush;
    public float Pulling;
    public float DamagePerSecond;
    public float TimeCloud;

    public void UseBombBase(BaseBombIng baseBomb)
    {
        Damage = baseBomb.BaseDamage;
        Freeze = baseBomb.BaseFreeze;
        TimeFreeze = baseBomb.BaseTimeFreeze;
        Radius = baseBomb.BaseRadius;
        DistancePush = baseBomb.BaseDistancePush;
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
        DistancePush *= baseBomb.ModDistancePush;
        Pulling *= baseBomb.ModPulling;
        DamagePerSecond *= baseBomb.ModDamagePerSecond;
        TimeCloud *= baseBomb.ModTimeCloud;
    }
    public void InitFromAnotherHandler(BombHandler handler)
    {
        Damage = handler.Damage;
        Freeze = handler.Freeze;
        TimeFreeze = handler.TimeFreeze;
        Radius = handler.Radius;
        DistancePush = handler.DistancePush;
        Pulling = handler.Pulling;
        DamagePerSecond = handler.DamagePerSecond;
        TimeCloud = handler.TimeCloud;
    }
}
