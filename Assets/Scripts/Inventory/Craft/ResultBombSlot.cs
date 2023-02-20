using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
using UnityEditor;

public class ResultBombSlot : ResultSlot
{
    [SerializeField] public CraftSlot craftSlot_1;
    [SerializeField] public CraftSlot craftSlot_2;
    [SerializeField] private DataIDItem DataIDItem;
    [SerializeField] private string KeyBombId;
    private Item Bomb;

    public override void RefreshCraft()
    {
        Item Item_1 = craftSlot_1.slot.item;
        Debug.Log(Item_1);
        Item Item_2 = craftSlot_2.slot.item;
        Debug.Log(Item_2);

        bool Ingred_1 = false;
        if (Item_1 != null)
            Ingred_1 = Item_1.ingredient != null;
        bool Ingred_2 = false;
        if (Item_2 != null)
            Ingred_2 = Item_2.ingredient != null;
        string ID;
        if (Ingred_1)
        {
            Bomb = Item_1.ingredient.BombITem;
            ID = KeyBombId + Item_1.id;
            if (Ingred_2) ID = ID + Item_2.id;

            if (DataIDItem.ItemIdList.Contains(ID))
            {
                CraftHandler(Ingred_1, Ingred_2, DataIDItem.GetItemByKey(ID), Item_1, Item_2);
                Debug.Log("before remove");
            }
            else
            {
                Item newItem = CreateItem(Ingred_1, Ingred_2, Item_1, Item_2, ID);
                InitItem(newItem, ID);
                CraftHandler(Ingred_1, Ingred_2, newItem, Item_1, Item_2);
            }
        }
        if (Ingred_2 && !Ingred_1)
            removeResultCraft();

        Bomb = null;
    }
    public void RefreshCraft(bool Ingred_1, bool Ingred_2)
    {
        Item Item_1 = craftSlot_1.slot.item;
        Item Item_2 = craftSlot_2.slot.item;
        string ID;
        if (Ingred_1)
        {
            Bomb = Item_1.ingredient.BombITem;
            ID = KeyBombId + Item_1.id;
            if (Ingred_2) ID = ID + Item_2.id;

            if (DataIDItem.ItemIdList.Contains(ID))
            {
                CraftHandler(Ingred_1, Ingred_2, DataIDItem.GetItemByKey(ID), Item_1, Item_2);
            }
            else
            {
                Item newItem = CreateItem(Ingred_1, Ingred_2, Item_1, Item_2, ID);
                InitItem(newItem, ID);
                CraftHandler(Ingred_1, Ingred_2, newItem, Item_1, Item_2);
            }
        }
        if (Ingred_2 && !Ingred_1)
            removeResultCraft();
        Bomb = null;
    }

    private void CraftHandler(bool Ingred_1, bool Ingred_2, Item newItem, Item Item_1, Item Item_2)
    {
        InventorySlot q = inventory.AddItemToSelectedSlot(newItem, transform, amount: 1);
        BombHandler handler;
        if (q.iconGameObject.TryGetComponent<BombHandler>(out handler))
        {
            if (Ingred_2) handler.UseBombMod(Item_2.ingredient.bombIng);
            return;
        }
        else
        {
            handler = q.iconGameObject.AddComponent<BombHandler>();
        }
        if (Ingred_1) handler.UseBombBase(Item_1.ingredient.bombIng);
        if (Ingred_2) handler.UseBombMod(Item_2.ingredient.bombIng);

        handler.bombItem = newItem;
    }
    private Item CreateItem(bool Ingred_1, bool Ingred_2, Item Item_1, Item Item_2, string ID)
    {
        Item newItem = ScriptableObject.CreateInstance<Item>();
        string name = null;
        if (Ingred_1)
            name = UnityEditor.AssetDatabase.GenerateUniqueAssetPath(DataIDItem.BombPath
            + ID + "_" + Item_1.ingredient.Color + "_" + ".asset");
        if (Ingred_2 && Ingred_1)
            name = UnityEditor.AssetDatabase.GenerateUniqueAssetPath(DataIDItem.BombPath
            + ID + "_" + Item_1.ingredient.Color + "_" + Item_2.ingredient.Color + ".asset");
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
        newItem.icon = Bomb.icon; //Большой затык с разными иконками
        newItem.itQuickSlotItem = true;
        newItem.itSelectedItem = true;
        newItem.prefab = Bomb.prefab;
        newItem.timeToPickUp = Bomb.timeToPickUp;
        newItem.quickslotItem = Bomb.quickslotItem;
        return newItem;
    }
    public override void removeComponentCraft()
    {
        Debug.Log(slot);
        if (slot == null) // || slot.item != null;
            slot = inventory.GetInventorySlot(transform);
        inventory.RemoveItem(craftSlot_1.slot, 1);
        inventory.RemoveItem(craftSlot_2.slot, 1);
    }
    public override void removeResultCraft()
    {
        if (slot == null || slot.item == null) // || slot.item == null;
            slot = inventory.GetInventorySlot(transform);
        inventory.RemoveItem(slot, 1);
        Debug.Log("remove");
    }


}
