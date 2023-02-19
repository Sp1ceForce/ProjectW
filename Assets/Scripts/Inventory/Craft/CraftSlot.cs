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
        if (slot.item == null)
            slot = inventory.GetInventorySlot(transform);
        Debug.Log(resultSlot.GetComponent<ResultBombSlot>().craftSlot_1.slot.item
        );
        Debug.Log(slot.item);
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
