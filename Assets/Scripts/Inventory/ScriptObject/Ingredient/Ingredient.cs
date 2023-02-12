using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "ProjectW/Items/Ingredient")]
public class Ingredient : ScriptableObject
{
    [SerializeField] public ScriptableObject Effect_1;
    [SerializeField] public ScriptableObject Effect_2;
    [SerializeField] public ScriptableObject Effect_3;

}
