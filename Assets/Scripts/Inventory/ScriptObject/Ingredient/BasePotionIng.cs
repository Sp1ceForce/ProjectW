using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BasePotionIngredient", menuName = "ProjectW/Items/BasePotionIngredient")]

public class BasePotionIng : ScriptableObject
{
    [Header("Base Settings")]
    public int BaseAddHP;
    public float BaseSpeed;
    public float BaseTimeSpeed;
    public float BaseShield;
    public float BaseTimeShield;
    public float BaseDamageUp;
    public float BaseTimeDamageUp;
    public float BaseCooldown;
    public float BaseTimeCooldown;
    public float BaseTimeInvisibility;
    public float BaseDamageOnWitch;
    public float BaseFactorDamageOnWitch;
    public float BaseTimeFactorDamageOnWitch;
    public float BaseRadiusBlind;
    public float BaseTimeRadiusBlind;

    [Header("Mod_1 Settings")]
    public int Mod_1_AddHP;
    public float Mod_1_Speed;
    public float Mod_1_TimeSpeed;
    public float Mod_1_Shield;
    public float Mod_1_TimeShield;
    public float Mod_1_DamageUp;
    public float Mod_1_TimeDamageUp;
    public float Mod_1_Cooldown;
    public float Mod_1_TimeCooldown;
    public float Mod_1_TimeInvisibility;
    public float Mod_1_DamageOnWitch;
    public float Mod_1_FactorDamageOnWitch;
    public float Mod_1_TimeFactorDamageOnWitch;
    public float Mod_1_RadiusBlind;
    public float Mod_1_TimeRadiusBlind;
    [Header("Mod_2 Settings")]
    public int Mod_2_AddHP;
    public float Mod_2_Speed;
    public float Mod_2_TimeSpeed;
    public float Mod_2_Shield;
    public float Mod_2_TimeShield;
    public float Mod_2_DamageUp;
    public float Mod_2_TimeDamageUp;
    public float Mod_2_Cooldown;
    public float Mod_2_TimeCooldown;
    public float Mod_2_TimeInvisibility;
    public float Mod_2_DamageOnWitch;
    public float Mod_2_FactorDamageOnWitch;
    public float Mod_2_TimeFactorDamageOnWitch;
    public float Mod_2_RadiusBlind;
    public float Mod_2_TimeRadiusBlind;
}
