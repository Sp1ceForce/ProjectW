using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ProjectW/Items/Item")]
public class Item : ScriptableObject
{
    [Header("Inside game parameters")]
    public string id;
    public Sprite icon;
    public GameObject prefab;
    [Header("Selected Item parameters")]
    public bool itSelectedItem = true;
    public float timeToPickUp = 0.5f;
    public float currentTimeToPickUp = 0f;
    [Header("Craft & Skill parameters")]
    [SerializeField] public Ingredient ingredient;
    [SerializeField] public BaseQuickslotItem quickslotItem;
    public bool itQuickSlotItem;
    // public BaseBomb bomb;
}
