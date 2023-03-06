using UnityEngine;
using System;
[Serializable]
public abstract class BaseQuickslotItem : ISpellActivate {
    [HideInInspector] 
    public string Name;
    public abstract void Activate(GameObject Instigator);
}