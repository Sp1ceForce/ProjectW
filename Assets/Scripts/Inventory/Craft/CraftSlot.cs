using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;

public class CraftSlot : MonoBehaviour, IAddToCraft
{
    // private Inventory inventory;
    [HideInInspector]
    public InventorySlot slot;
    [SerializeField] private ResultSlot resultSlot;
    public void AddToCraft()
    {
        if (slot.item == null) return;
        resultSlot.RefreshCraft();
    }

    private void Start()
    {
        slot = transform.parent.GetComponent<Inventory>().GetInventorySlot(transform);
    }

}
