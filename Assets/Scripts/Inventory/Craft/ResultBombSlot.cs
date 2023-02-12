using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
public class ResultBombSlot : ResultSlot
{
    [SerializeField] private CraftSlot craftSlot_1;
    [SerializeField] private CraftSlot craftSlot_2;
    private AbstractEffect effect_1;
    private AbstractEffect effect_2;

    public override void RefreshCraft()
    {
        //Получить эффекты из слотов 
        if (craftSlot_1.slot.item.ingredient != null)
            effect_1 = craftSlot_1.slot.item.ingredient.Effect_1;
        if (craftSlot_2.slot.item.ingredient != null)
            effect_2 = craftSlot_2.slot.item.ingredient.Effect_2;

        //сотворить в слоте бомбу с нужным эффектами
        if (effect_1.bombTag1 == effect_2.bombTag2) { }
        // Inventory.

    }
}
