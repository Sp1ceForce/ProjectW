using UnityEngine;

public abstract class BaseQuickslotItem : ScriptableObject, ISpellActivate {
    public abstract void Activate(GameObject Instigator);
}