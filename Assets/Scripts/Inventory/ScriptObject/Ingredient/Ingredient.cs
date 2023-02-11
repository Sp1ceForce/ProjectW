using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "ProjectW/Items/Ingredient")]
public class Ingredient : ScriptableObject
{
    [SerializeField] public AbstractEffect Effect_1;
    [SerializeField] public AbstractEffect Effect_2;
    [SerializeField] public AbstractEffect Effect_3;

}
