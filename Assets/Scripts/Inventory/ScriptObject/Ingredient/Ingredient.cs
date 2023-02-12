using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "ProjectW/Items/Ingredient")]
public class Ingredient : ScriptableObject
{
    public int idPotion = 21;//id зелья.

    public string Potion = "heal"; //Основная характеристика зелья Компонент Heal
    public int idBomb = 9;
    public string Bomb = "damage"; //Основная характеристика бомбы Компонент Bomb
    public int idDopBomb = 11;
    public float dopBomp = 1.5f; //Модификатор бомбы

}
