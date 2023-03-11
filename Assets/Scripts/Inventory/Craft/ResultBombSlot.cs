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
        Item Item_2 = craftSlot_2.slot.item;

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

            if (DataIDItem.ItemIdList.Contains(ID))    //где искать предмет? 
            {
                CraftHandler(Ingred_1, Ingred_2, DataIDItem.GetItemByKey(ID), Item_1, Item_2);
            }
            else
            {
                Item newItem = CreateItem(Ingred_1, Ingred_2, Item_1, Item_2, ID); //Вызов создания предмета в файле
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
        ////ЗДЕСЬ ОШИБКА!/////
        ////Для указания связи используется неправильный предмет из слота крафта, а должен использоваться добавленный из слота результата
        Debug.Log(q.item.id);
        // if (q.item.prefab.TryGetComponent<SelectedItem>(out SelectedItem selectedItem)) selectedItem.item = newItem;
        TestHandler handler = new TestHandler(
        Ingred_1 == true ? Item_1.ingredient.bombIng : null,
        Ingred_2 == true ? Item_2.ingredient.bombIng : null);
        List<BombEffectType> effectsList = new List<BombEffectType>();
        for (int i = 0; i < Item_1.ingredient.bombIng.effects.Count; i++)
            effectsList.Add(Item_1.ingredient.bombIng.effects[i]);
        if (Ingred_2)
            foreach (BombEffectType effect in Item_2.ingredient.bombIng.effects)
            {
                if (!effectsList.Contains(effect))
                    effectsList.Add(effect);
            }
        BaseBomb newBomb = new BaseBomb(handler, effectsList);
        newItem.quickslotItem = newBomb;

    }
    private Item CreateItem(bool Ingred_1, bool Ingred_2, Item Item_1, Item Item_2, string ID)
    {
        Item newItem = ScriptableObject.CreateInstance<Item>();
        // string name = null;
        // if (Ingred_1)
        //     name = UnityEditor.AssetDatabase.GenerateUniqueAssetPath(DataIDItem.BombPath
        //     + ID + "_" + Item_1.ingredient.Color + "_" + ".asset");
        // if (Ingred_2 && Ingred_1)
        //     name = UnityEditor.AssetDatabase.GenerateUniqueAssetPath(DataIDItem.BombPath
        //     + ID + "_" + Item_1.ingredient.Color + "_" + Item_2.ingredient.Color + ".asset");
        // AssetDatabase.CreateAsset(newItem, name);
        // AssetDatabase.SaveAssets();
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
        // newItem.quickslotItem = Bomb.quickslotItem;
        return newItem;
    }
    public override void removeComponentCraft()
    {
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
    }


}
