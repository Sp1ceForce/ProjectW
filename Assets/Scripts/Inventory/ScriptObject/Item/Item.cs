using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ProjectW/Items/Item")]
public class Item : ScriptableObject
{
    public int id;
    public Sprite icon;
    public GameObject prefab;
    public bool itSelectedItem = true;
    public float timeToPickUp = 3000f;
    public float currentTimeToPickUp = 0f;
    [SerializeField] public Ingredient ingredient;
    [SerializeField] public BaseQuickslotItem quickslotItem;
    public bool itQuickSlotItem;

}
