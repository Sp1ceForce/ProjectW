using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
public class ResultBombSlot : ResultSlot
{
    [SerializeField] private CraftSlot craftSlot_1;
    [SerializeField] private CraftSlot craftSlot_2;
    private string Bomb;
    private float dopBomp;

    public override void RefreshCraft()
    {
        //Получить эффекты из слотов 
        if (craftSlot_1.slot.item.ingredient != null)
            Bomb = craftSlot_1.slot.item.ingredient.Bomb;
        if (craftSlot_2.slot.item.ingredient != null)
            dopBomp = craftSlot_2.slot.item.ingredient.dopBomp;
        //сотворить в слоте бомбу с нужным эффектами
        //Bomb * dopBomp;
    }
}
