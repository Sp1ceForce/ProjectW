using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
public class ResultBombSlot : ResultSlot
{
    [SerializeField] private CraftSlot craftSlot_1;
    [SerializeField] private CraftSlot craftSlot_2;
    private Item Bomb;
    private float dopBomp;

    public override void RefreshCraft()
    {
        dopBomp = 1;

        Debug.Log(craftSlot_1.slot.item);
        //Получить эффекты из слотов 
        if (craftSlot_1.slot.item.ingredient != null)
        {
            Bomb = craftSlot_1.slot.item.ingredient.BombITem;
            // if (craftSlot_2.slot.item.ingredient != null)
            //     dopBomp = craftSlot_2.slot.item.ingredient.dopBomp;
            if (slot.item == null)
                inventory.AddItemToSelectedSlot(Bomb, transform, amount: 1);
        }
    }
    public override void removeComponentCraft()
    {
        if (slot.item == null)
            slot = inventory.GetInventorySlot(transform);
        inventory.RemoveItem(craftSlot_1.slot, 1);
        inventory.RemoveItem(craftSlot_2.slot, 1);
    }
    public override void removeResultCraft()
    {
        if (slot.item == null)
            slot = inventory.GetInventorySlot(transform);
        inventory.RemoveItem(slot, 1);
    }


}
