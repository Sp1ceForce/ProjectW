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
    [SerializeField] public ResultSlot resultSlot;
    private Inventory inventory;
    public void AddToCraft()
    {
        // if (slot.item == null) return;
        // Debug.Log("THIS " + slot);
        // Debug.Log("AND THIS " + inventory.GetInventorySlot(transform).item.id);

        if (slot.item == null)
            slot = inventory.GetInventorySlot(transform);
        // Debug.Log("AGAIN THIS " + slot);
        // Debug.Log("AGAIN AND THIS " + slot.item.id);
        // Debug.Log("HOLA!!!");
        resultSlot.RefreshCraft();
    }

    private void Start()
    {
        inventory = transform.parent.GetComponent<Inventory>();
        if (slot == null)
            slot = inventory.GetInventorySlot(transform);
    }

}
