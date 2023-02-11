using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
public abstract class ResultSlot : MonoBehaviour, IRefreshCraft
{
    private InventorySlot slot;
    private Inventory inventory;
    public abstract void RefreshCraft();

    private void Start()
    {
        inventory = transform.parent.GetComponent<Inventory>();
        slot = inventory.GetInventorySlot(transform);
    }

}
