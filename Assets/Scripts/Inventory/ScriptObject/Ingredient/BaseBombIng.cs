using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BaseBombIngredient", menuName = "ProjectW/Items/BaseBombIngredient")]

public class BaseBombIng : ScriptableObject
{
    [Header("Base Settings")]
    public float BaseDamage;
    public float BaseFreeze;
    public float BaseTimeFreeze;
    public float BaseRadius;
    public float BaseDistancePush;
    public float BasePulling;
    public float BaseDamagePerSecond;
    public float BaseTimeCloud;
    [Header("Mod Settings")]
    public float ModDamage;
    public float ModFreeze;
    public float ModTimeFreeze;
    public float ModRadius;
    public float ModDistancePush;
    public float ModPulling;
    public float ModDamagePerSecond;
    public float ModTimeCloud;
}
