using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;

public class CraftSlot : MonoBehaviour, IAddToCraft
{

    [HideInInspector]
    public InventorySlot slot;
    [SerializeField] public ResultSlot resultSlot;
    private Inventory inventory;
    public void AddToCraft()
    {
        if (slot.item == null)
            slot = inventory.GetInventorySlot(transform);
        resultSlot.RefreshCraft();
    }
    public void UpdateDataSlot()
    {
        slot = inventory.GetInventorySlot(transform);
    }
    private void Start()
    {
        inventory = transform.parent.GetComponent<Inventory>();
        if (slot == null)
            slot = inventory.GetInventorySlot(transform);
    }

}
