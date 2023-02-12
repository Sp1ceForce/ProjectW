using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
public abstract class ResultSlot : MonoBehaviour, IRefreshCraft
{
    protected InventorySlot slot;
    protected Inventory inventory;
    public abstract void RefreshCraft();
    public abstract void removeComponentCraft();
    public abstract void removeResultCraft();

    private void Start()
    {
        inventory = transform.parent.GetComponent<Inventory>();
        if (slot == null)
            slot = inventory.GetInventorySlot(transform);
    }

}
