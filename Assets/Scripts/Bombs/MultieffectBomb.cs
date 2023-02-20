using System;
using System.Collections;
using System.Collections.Generic;
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
        foreach(var entity in entitiesHit){
            entity.gameObject.AddComponent<FreezeComponent>().Init(bombStats);
        }
    }
}
public class BombEffectVacuum : IBombEffect
{
    [HideInInspector]
    public string Name = "Vacuum cleaner";
    public void ActivateEffect(Collider[] entitiesHit, Vector3 explosionPosition, TestHandler bombStats)
    {
        PullingZone pullingZone = GameObject.Instantiate(BombGlobalData.Instance.PullingZoneObject);
        pullingZone.Init(bombStats);
    }
}
public class BombEffectApplyBleeding : IBombEffect
{
    [HideInInspector]
    public string Name = "Apply bleeding";
    public void ActivateEffect(Collider[] entitiesHit, Vector3 explosionPosition, TestHandler bombStats)
    {
        foreach(var entity in entitiesHit){
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
        PoisonousFog fog = GameObject.Instantiate(BombGlobalData.Instance.PosionFogObject);
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
public class TestHandler{
    public float Damage;
    public float Freeze;
    public float TimeFreeze;
    public float Radius;
    public float DistancePush;
    public float Pulling;
    public float DamagePerSecond;
    public float TimeCloud;
    public TestHandler(){
        Damage = 100;
        Freeze = 100;
        TimeFreeze = 30;
        Radius = 10;
        DistancePush = 20;
        Pulling = 20;
        DamagePerSecond = 20;
        TimeCloud = 20;
    }
}
[CreateAssetMenu(fileName = "MultieffectBomb", menuName = "ProjectW/Bombs/MultieffectBomb", order = 0)]
public class MultieffectBomb : BaseBomb
{
    [SerializeField]
    List<BombEffectType> effectsList;
    [SerializeReference]
    List<IBombEffect> effectsClasses;
    [SerializeField]
    TestHandler tempHandler;
    [ContextMenu("Generate effect classes")]
    public void GenerateEffects()
    {
        foreach (var effect in effectsList)
        {
            effectsClasses.Add(BombEffectFactory.CreateEffect(effect));
        }
    }
    public override void InitiateBomb(List<BombEffectType> effects,BombHandler bombHandler)
    {
        effectsList = effects;
        foreach (var effect in effectsList)
        {
            effectsClasses.Add(BombEffectFactory.CreateEffect(effect));
        }
    }

    protected override void ExplosionLogic(GameObject instigator, Vector3 explosionPosition, Collider[] entitiesHit)
    {
        tempHandler = new TestHandler();
        foreach (var effectClass in effectsClasses)
        {
            effectClass.ActivateEffect(entitiesHit, explosionPosition, tempHandler);
        }
    }
}
