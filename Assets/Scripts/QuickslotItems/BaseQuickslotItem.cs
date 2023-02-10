using UnityEngine;
public enum ItemType{
    IT_Bomb,
    IT_Potion
}
public abstract class BaseQuickslotItem : ScriptableObject, ISpellActivate {
    [Header("Базовые свойства предмета")]
    public ItemType ItemType;
    public abstract void Activate(GameObject Instigator);
}