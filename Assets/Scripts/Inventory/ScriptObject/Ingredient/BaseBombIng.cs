using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BaseBombIngredient", menuName = "ProjectW/Items/BaseBombIngredient")]

public class BaseBombIng : ScriptableObject
{
    [Header("Effects Settings")]
    [SerializeField] public List<BombEffectType> effects;
    [Header("Base Settings")]
    public int BaseDamage;
    public float BaseFreeze;
    public float BaseFreezeTime;
    public float BaseRadius;
    public float BaseDistancePush;
    public float BasePullForce;
    public float BasePullzoneLifetime;
    public int BaseBleedingDamagePerTick;
    public float BaseBleedingTickInterval;
    public float BaseBleedingTime;
    public int BaseCloudDamagePerTick;
    public float BaseCloudTickInterval;
    public float BaseCloudLifetime;
    [Header("Mod Settings")]
    public int ModDamage;
    public float ModFreeze;
    public float ModFreezeTime;
    public float ModRadius;
    public float ModDistancePush;
    public float ModPullForce;
    public float ModPullzoneLifetime;
    public int ModBleedingDamagePerTick;
    public float ModBleedingTickInterval;
    public float ModBleedingTime;
    public int ModCloudDamagePerTick;
    public float ModCloudTickInterval;
    public float ModCloudLifetime;
}
