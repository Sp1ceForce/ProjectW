using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ProjectW/Item")]
public class Item : ScriptableObject
{
    public int id;
    public Sprite icon;
    public float timeToPickUp = 3000f;
    public float currentTimeToPickUp = 0f;
    public GameObject prefab;
    public bool itSelectedItem = true;

}
