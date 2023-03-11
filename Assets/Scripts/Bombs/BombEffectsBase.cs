using System;
using UnityEngine;
[Flags]
public enum BombEffectType
{
    BE_DealDamage = 1 << 1,
    BE_PushAway = 1 << 2,
    BE_Freeze = 1 << 3,
    BE_Vacuum = 1 << 4,
    BE_ApplyBleeding = 1 << 5,
    BE_PoisonFog = 1 << 6
}
public interface IBombEffect
{
    public abstract void ActivateEffect(Collider[] entitiesHit, Vector3 explosionPosition, TestHandler bombStats);
}
public class BombEffectDealDamage : IBombEffect
{
    [HideInInspector]
    public string Name = "Deal damage";
    public void ActivateEffect(Collider[] entitiesHit, Vector3 explosionPosition, TestHandler bombStats)
    {
        foreach (var entity in entitiesHit)
        {
            EnemyHealthComponent healthComponent = entity.GetComponent<EnemyHealthComponent>();
            if (healthComponent != null) healthComponent.TakeDamage(int.Parse(bombStats.Damage.ToString()));
        }
    }
}
public class BombEffectPushAway : IBombEffect
{
    [HideInInspector]
    public string Name = "Push away";
    public void ActivateEffect(Collider[] entitiesHit, Vector3 explosionPosition, TestHandler bombStats)
    {
        foreach (var entity in entitiesHit)
        {
            EnemyKnockbackComponent knockbackComponent = entity.GetComponent<EnemyKnockbackComponent>();
            if (knockbackComponent != null) knockbackComponent.KnockBack(explosionPosition, bombStats.DistancePush);
        }
    }
}
public class BombEffectFreeze : IBombEffect
{
    [HideInInspector]
    public string Name = "Freeze";
    public void ActivateEffect(Collider[] entitiesHit, Vector3 explosionPosition, TestHandler bombStats)
    {
        foreach (var entity in entitiesHit)
        {
            var freezeComp = entity.gameObject.GetComponent<FreezeComponent>();
            if (freezeComp == null)
            {
                var freeze = entity.gameObject.AddComponent<FreezeComponent>();
                freeze.Init(bombStats);
            }
            else if (freezeComp.Stats.Freeze < bombStats.Freeze)
            {
                var freeze = entity.gameObject.AddComponent<FreezeComponent>();
                freeze.Init(bombStats);
            }
        }
    }
}
public class BombEffectVacuum : IBombEffect
{
    [HideInInspector]
    public string Name = "Vacuum cleaner";
    public void ActivateEffect(Collider[] entitiesHit, Vector3 explosionPosition, TestHandler bombStats)
    {
        PullingZone pullingZone = GameObject.Instantiate(BombGlobalData.Instance.PullingZoneObject, explosionPosition, Quaternion.identity);
        pullingZone.Init(bombStats);
    }
}
public class BombEffectApplyBleeding : IBombEffect
{
    [HideInInspector]
    public string Name = "Apply bleeding";
    public void ActivateEffect(Collider[] entitiesHit, Vector3 explosionPosition, TestHandler bombStats)
    {
        foreach (var entity in entitiesHit)
        {
            //TODO: Поставить проверку на тэг Enemy, чёт не хочется чтобы у камней было кровотечение
            //if(entity.tag == "Enemy")
            entity.gameObject.AddComponent<BleedingComponent>().Init(bombStats);
        }
    }
}
public class BombEffectPoisonFog : IBombEffect
{
    [HideInInspector]
    public string Name = "Create poison fog";
    public void ActivateEffect(Collider[] entitiesHit, Vector3 explosionPosition, TestHandler bombStats)
    {
        PoisonousFog fog = GameObject.Instantiate(BombGlobalData.Instance.PosionFogObject, explosionPosition, Quaternion.identity);
        fog.Init(bombStats);
    }
}
public class BombEffectFactory
{
    public static IBombEffect CreateEffect(BombEffectType effectType)
    {
        switch (effectType)
        {
            case BombEffectType.BE_DealDamage:
                return new BombEffectDealDamage();
            case BombEffectType.BE_PushAway:
                return new BombEffectPushAway();
            case BombEffectType.BE_Freeze:
                return new BombEffectFreeze();
            case BombEffectType.BE_Vacuum:
                return new BombEffectVacuum();
            case BombEffectType.BE_ApplyBleeding:
                return new BombEffectApplyBleeding();
            case BombEffectType.BE_PoisonFog:
                return new BombEffectPoisonFog();
            default:
                return new BombEffectDealDamage();
        }
    }
}
[Serializable]
public class TestHandler
{
    public int Damage;
    public float Freeze;
    public float FreezeTime;
    public float Radius;
    public float DistancePush;
    public float PullForce;
    public float PullzoneLifetime;
    public int BleedingDamagePerTick;
    public float BleedingTickInterval;
    public float BleedingTime;
    public int CloudDamagePerTick;
    public float CloudTickInterval;
    public float CloudLifetime;
    public TestHandler()
    {
        Damage = 20;
        Freeze = 60;
        FreezeTime = 30;
        Radius = 10;
        DistancePush = 20;
        PullForce = 20;
        PullzoneLifetime = 10f;
        BleedingDamagePerTick = 20;
        BleedingTickInterval = 0.1f;
        BleedingTime = 10f;
        CloudDamagePerTick = 20;
        CloudTickInterval = 0.1f;
        CloudLifetime = 20;
    }
    public TestHandler(BaseBombIng baseBomb, BaseBombIng ModBomb)
    {
        Damage = 20;
        Freeze = 60;
        FreezeTime = 30;
        Radius = 10;
        DistancePush = 20;
        PullForce = 20;
        PullzoneLifetime = 10f;
        BleedingDamagePerTick = 20;
        BleedingTickInterval = 0.1f;
        BleedingTime = 10f;
        CloudDamagePerTick = 20;
        CloudTickInterval = 0.1f;
        CloudLifetime = 20;

        if (baseBomb != null)
        {
            Damage = baseBomb.BaseDamage;
            Freeze = baseBomb.BaseFreeze;
            FreezeTime = baseBomb.BaseFreezeTime;
            Radius = baseBomb.BaseRadius;
            DistancePush = baseBomb.BaseDistancePush;
            PullForce = baseBomb.BasePullForce;
            PullzoneLifetime = baseBomb.BasePullzoneLifetime;
            BleedingDamagePerTick = baseBomb.BaseBleedingDamagePerTick;
            BleedingTickInterval = baseBomb.BaseBleedingTickInterval;
            BleedingTime = baseBomb.BaseBleedingTime;
            CloudDamagePerTick = baseBomb.BaseCloudDamagePerTick;
            CloudTickInterval = baseBomb.BaseCloudTickInterval;
            CloudLifetime = baseBomb.BaseCloudLifetime;
            Damage = baseBomb.BaseDamage;
        }

        if (ModBomb != null)
        {
            Freeze *= ModBomb.ModFreeze;
            FreezeTime *= ModBomb.ModFreezeTime;
            Radius *= ModBomb.ModRadius;
            DistancePush *= ModBomb.ModDistancePush;
            PullForce *= ModBomb.ModPullForce;
            PullzoneLifetime *= ModBomb.ModPullzoneLifetime;
            BleedingDamagePerTick *= ModBomb.ModBleedingDamagePerTick;
            BleedingTickInterval *= ModBomb.ModBleedingTickInterval;
            BleedingTime *= ModBomb.ModBleedingTime;
            CloudDamagePerTick *= ModBomb.ModCloudDamagePerTick;
            CloudTickInterval *= ModBomb.ModCloudTickInterval;
            CloudLifetime *= ModBomb.ModCloudLifetime;
        }
    }
}

