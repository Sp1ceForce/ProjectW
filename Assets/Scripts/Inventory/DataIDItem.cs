using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DataIDItem", menuName = "ProjectW/Items/DataIDItem")]

public class DataIDItem : ScriptableObject
{
    [SerializeField] public List<string> ItemIdList;
    [SerializeField] public List<Item> Items;
    [SerializeField] public string BombPath = "Assets/Scripts/Inventory/ScriptObject/Item/Item_Bomb_";
    [SerializeField] public string PotionPath = "Assets/Scripts/Inventory/ScriptObject/Item/Item_Potion_";

    public Item GetItemByKey(string Key)
    {
        if (ItemIdList.Contains(Key))
        {
            foreach (Item item in Items)
            {
                if (item.id == Key) return item;
            }
        }
        return null;
    }
}