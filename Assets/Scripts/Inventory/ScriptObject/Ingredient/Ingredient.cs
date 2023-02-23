using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "ProjectW/Items/Ingredient")]
public class Ingredient : ScriptableObject
{
    [SerializeField] public string Color;
    [SerializeField] public Item BombITem;
    [SerializeField] public BaseBombIng bombIng;
    [SerializeField] public Item PotionItem;
    [SerializeField] public BasePotionIng potionIng;

}
