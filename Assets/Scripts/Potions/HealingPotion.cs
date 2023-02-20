using UnityEngine;

[CreateAssetMenu(fileName = "HealingPotion", menuName = "ProjectW/Potions/HealingPotion", order = 0)]
public class HealingPotion : BaseQuickslotItem
{
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
    public override void Activate(GameObject Instigator)
    {
        if (AddHP != 0) Instigator.GetComponent<Witch>().Heal(AddHP);
        if (Speed != 0) { }
        if (Shield != 0) { }
        if (DamageUp != 0) { }
        if (Cooldown != 0) { }
        if (TimeInvisibility > 0) { }
        if (FactorDamageOnWitch != 0) { }
        if (RadiusBlind > 0) { }
        if (Speed != 0) { }

    }
    public void usePotionHandler(PotionHandler handler)
    {
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