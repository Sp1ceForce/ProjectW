using UnityEngine;

[CreateAssetMenu(fileName = "HealingPotion", menuName = "ProjectW/Potions/HealingPotion", order = 0)]
public class HealingPotion : BaseQuickslotItem
{
    [Header("Свойства зелья лечения")]
    [SerializeField] int healingPower = 70;
    public override void Activate(GameObject Instigator)
    {
        Instigator.GetComponent<Witch>().Heal(healingPower);
    }
}