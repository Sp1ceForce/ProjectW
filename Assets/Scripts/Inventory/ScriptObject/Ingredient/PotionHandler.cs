using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHandler : MonoBehaviour
{
    public Item potionItem;
    public int AddHP;
    public float Speed;
    public float TimeSpeed;
    public float Shield;
    public float TimeShield;
    public float DamageUp;
    public float TimeDamageUp;
    public float Cooldown;
    public float TimeCooldown;
    public float TimeInvisibility;
    public float FactorDamageOnWitch;
    public float TimeFactorDamageOnWitch;
    public float RadiusBlind;
    public float TimeRadiusBlind;

    public void UsePotionBase(BasePotionIng basePotion)
    {
        AddHP = basePotion.BaseAddHP;
        Speed = basePotion.BaseSpeed;
        TimeSpeed = basePotion.BaseTimeSpeed;
        Shield = basePotion.BaseShield;
        TimeShield = basePotion.BaseTimeShield;
        DamageUp = basePotion.BaseDamageUp;
        TimeDamageUp = basePotion.BaseTimeDamageUp;
        Cooldown = basePotion.BaseCooldown;
        TimeCooldown = basePotion.BaseTimeCooldown;
        TimeInvisibility = basePotion.BaseTimeInvisibility;
        FactorDamageOnWitch = basePotion.BaseFactorDamageOnWitch;
        TimeFactorDamageOnWitch = basePotion.BaseTimeFactorDamageOnWitch;
        RadiusBlind = basePotion.BaseRadiusBlind;
        TimeRadiusBlind = basePotion.BaseTimeRadiusBlind;

    }
    public void UsePotionMod_1(BasePotionIng basePotion)
    {
        AddHP *= basePotion.Mod_1_AddHP;
        Speed *= basePotion.Mod_1_Speed;
        TimeSpeed *= basePotion.Mod_1_TimeSpeed;
        Shield *= basePotion.Mod_1_Shield;
        TimeShield *= basePotion.Mod_1_TimeShield;
        DamageUp *= basePotion.Mod_1_DamageUp;
        TimeDamageUp *= basePotion.Mod_1_TimeDamageUp;
        Cooldown *= basePotion.Mod_1_Cooldown;
        TimeCooldown *= basePotion.Mod_1_TimeCooldown;
        TimeInvisibility *= basePotion.Mod_1_TimeInvisibility;
        FactorDamageOnWitch *= basePotion.Mod_1_FactorDamageOnWitch;
        TimeFactorDamageOnWitch *= basePotion.Mod_1_TimeFactorDamageOnWitch;
        RadiusBlind *= basePotion.Mod_1_RadiusBlind;
        TimeRadiusBlind *= basePotion.Mod_1_TimeRadiusBlind;
    }
    public void UsePotionMod_2(BasePotionIng basePotion)
    {
        AddHP *= basePotion.Mod_2_AddHP;
        Speed *= basePotion.Mod_2_Speed;
        TimeSpeed *= basePotion.Mod_2_TimeSpeed;
        Shield *= basePotion.Mod_2_Shield;
        TimeShield *= basePotion.Mod_2_TimeShield;
        DamageUp *= basePotion.Mod_2_DamageUp;
        TimeDamageUp *= basePotion.Mod_2_TimeDamageUp;
        Cooldown *= basePotion.Mod_2_Cooldown;
        TimeCooldown *= basePotion.Mod_2_TimeCooldown;
        TimeInvisibility *= basePotion.Mod_2_TimeInvisibility;
        FactorDamageOnWitch *= basePotion.Mod_2_FactorDamageOnWitch;
        TimeFactorDamageOnWitch *= basePotion.Mod_2_TimeFactorDamageOnWitch;
        RadiusBlind *= basePotion.Mod_2_RadiusBlind;
        TimeRadiusBlind *= basePotion.Mod_2_TimeRadiusBlind;
    }
    public void InitFromAnotherHandler(PotionHandler handler)
    {
        potionItem = handler.potionItem;

        AddHP = handler.AddHP;
        Speed = handler.Speed;
        TimeSpeed = handler.TimeSpeed;
        Shield = handler.Shield;
        TimeShield = handler.TimeShield;
        DamageUp = handler.DamageUp;
        TimeDamageUp = handler.TimeDamageUp;
        Cooldown = handler.Cooldown;
        TimeCooldown = handler.TimeCooldown;
        TimeInvisibility = handler.TimeInvisibility;
        FactorDamageOnWitch = handler.FactorDamageOnWitch;
        TimeFactorDamageOnWitch = handler.TimeFactorDamageOnWitch;
        RadiusBlind = handler.RadiusBlind;
        TimeRadiusBlind = handler.TimeRadiusBlind;
    }
}
