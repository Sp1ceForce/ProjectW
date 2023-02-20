using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
using UnityEditor;

public class ResultPotionSlot : ResultSlot
{
    [SerializeField] public CraftSlot craftSlot_1;
    [SerializeField] public CraftSlot craftSlot_2;
    [SerializeField] public CraftSlot craftSlot_3;

    [SerializeField] private DataIDItem DataIDItem;
    [SerializeField] private string KeyPotionId;
    private Item Potion;

    public override void RefreshCraft()
    {
        Item Item_1 = craftSlot_1.slot.item;
        Item Item_2 = craftSlot_2.slot.item;
        Item Item_3 = craftSlot_3.slot.item;

        bool Ingred_1 = false;
        bool Ingred_2 = false;
        bool Ingred_3 = false;

        if (Item_1 != null) Ingred_1 = Item_1.ingredient != null;
        if (Item_2 != null) Ingred_2 = Item_2.ingredient != null;
        if (Item_3 != null) Ingred_3 = Item_3.ingredient != null;

        string ID;
        if (Ingred_1)
        {
            Potion = Item_1.ingredient.PotionItem;
            ID = KeyPotionId + Item_1.id;
            if (Ingred_2)
            {
                ID = ID + Item_2.id;
                if (Ingred_3) ID = ID + Item_3.id;
            }

            if (DataIDItem.ItemIdList.Contains(ID))
            {
                CraftHandler(Ingred_1, Ingred_2, Ingred_3, DataIDItem.GetItemByKey(ID), Item_1, Item_2, Item_3);
            }
            else
            {
                Item newItem = CreateItem(Ingred_1, Ingred_2, Ingred_3, Item_1, Item_2, Item_3, ID);
                InitItem(newItem, ID);
                CraftHandler(Ingred_1, Ingred_2, Ingred_3, newItem, Item_1, Item_2, Item_3);
            }
        }
        if ((Ingred_2 && !Ingred_1) || (Ingred_3 && !Ingred_1))
            removeResultCraft();
        Potion = null;
    }
    public void RefreshCraft(bool Ingred_1, bool Ingred_2, bool Ingred_3)
    {
        Item Item_1 = craftSlot_1.slot.item;
        Item Item_2 = craftSlot_2.slot.item;
        Item Item_3 = craftSlot_3.slot.item;

        string ID;
        if (Ingred_1)
        {
            Potion = Item_1.ingredient.BombITem;
            ID = KeyPotionId + Item_1.id;
            if (Ingred_2)
            {
                ID = ID + Item_2.id;
                if (Ingred_3) ID = ID + Item_3.id;
            }

            if (DataIDItem.ItemIdList.Contains(ID))
            {
                CraftHandler(Ingred_1, Ingred_2, Ingred_3, DataIDItem.GetItemByKey(ID), Item_1, Item_2, Item_3);
            }
            else
            {
                Item newItem = CreateItem(Ingred_1, Ingred_2, Ingred_3, Item_1, Item_2, Item_3, ID);
                InitItem(newItem, ID);
                CraftHandler(Ingred_1, Ingred_2, Ingred_3, newItem, Item_1, Item_2, Item_3);
            }
        }
        if ((Ingred_2 && !Ingred_1) || (Ingred_3 && !Ingred_1))
            removeResultCraft();
        Potion = null;
    }
    private void CraftHandler(bool Ingred_1, bool Ingred_2, bool Ingred_3, Item newItem, Item Item_1, Item Item_2, Item Item_3)
    {
        InventorySlot q = inventory.AddItemToSelectedSlot(newItem, transform, amount: 1);
        PotionHandler handler;
        //Случай, когда уже есть один ингридиент и он уже добавил Handler - смотрим другие и применяем модификаторы
        if (q.iconGameObject.TryGetComponent<PotionHandler>(out handler))
        {
            if (Ingred_2) handler.UsePotionMod_1(Item_2.ingredient.potionIng);
            //         // inventory.UpdateUIIcon(q);
            //         // slot.item.icon = newItem.icon;
            if (Ingred_3) handler.UsePotionMod_2(Item_3.ingredient.potionIng);

            return;
        }
        //Если же Hadnler нет на предмете, то добавляем
        else
        {
            handler = q.iconGameObject.AddComponent<PotionHandler>();
        }
        if (Ingred_1) handler.UsePotionBase(Item_1.ingredient.potionIng);
        if (Ingred_2) handler.UsePotionMod_1(Item_2.ingredient.potionIng);
        if (Ingred_3) handler.UsePotionMod_2(Item_3.ingredient.potionIng);
        handler.potionItem = newItem;

    }
    private Item CreateItem(bool Ingred_1, bool Ingred_2, bool Ingred_3, Item Item_1, Item Item_2, Item Item_3, string ID)
    {
        Item newItem = ScriptableObject.CreateInstance<Item>();
        string name = null;
        if (Ingred_1)
            name = UnityEditor.AssetDatabase.GenerateUniqueAssetPath(DataIDItem.PotionPath
            + ID + "_" + Item_1.ingredient.Color + "_" + ".asset");
        if (Ingred_1 && Ingred_2)
            name = UnityEditor.AssetDatabase.GenerateUniqueAssetPath(DataIDItem.PotionPath
            + ID + "_" + Item_1.ingredient.Color + "_" + Item_2.ingredient.Color + ".asset");
        if (Ingred_1 && Ingred_2 && Ingred_3)
            name = UnityEditor.AssetDatabase.GenerateUniqueAssetPath(DataIDItem.PotionPath
            + ID + "_" + Item_1.ingredient.Color + "_" + Item_2.ingredient.Color + "_" + Item_3.ingredient.Color + ".asset");
        AssetDatabase.CreateAsset(newItem, name);
        AssetDatabase.SaveAssets();
        return newItem;
    }
    private Item InitItem(Item newItem, string ID)
    {
        //создать предмет в базе данных
        DataIDItem.Items.Add(newItem);
        DataIDItem.ItemIdList.Add(ID);

        //Иницилизировать предмет с его характеристиками
        newItem.id = ID;
        newItem.icon = Potion.icon; //Большой затык с разными иконками
        newItem.itQuickSlotItem = true;
        newItem.itSelectedItem = true;
        newItem.prefab = Potion.prefab;
        newItem.timeToPickUp = Potion.timeToPickUp;
        newItem.quickslotItem = Potion.quickslotItem;
        return newItem;
    }
    public override void removeComponentCraft()
    {
        if (slot == null) // || slot.item != null;
            slot = inventory.GetInventorySlot(transform);
        inventory.RemoveItem(craftSlot_1.slot, 1);
        inventory.RemoveItem(craftSlot_2.slot, 1);
        inventory.RemoveItem(craftSlot_3.slot, 1);

    }
    public override void removeResultCraft()
    {
        if (slot == null || slot.item == null) // || slot.item == null;
            slot = inventory.GetInventorySlot(transform);
        inventory.RemoveItem(slot, 1);
        Debug.Log("remove");
    }

}
