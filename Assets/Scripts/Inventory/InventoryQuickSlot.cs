using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryQuickSlot : MonoBehaviour
{

    private int number;
    private SkillsController skillsController;
    public Item item;
    [HideInInspector]
    public InventorySlot inventorySlot;
    [HideInInspector]
    public Inventory inventory;
    private void Awake()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform == transform.parent.GetChild(i))
            {
                number = i;
            }
        }
        skillsController = GameObject.FindGameObjectWithTag("Player").GetComponent<SkillsController>();
        inventory = transform.parent.GetComponent<Inventory>();
        inventorySlot = inventory.GetInventorySlot(transform);
        skillsController.InventoryQuickSlotItems[number] = this;

    }
    public void sendToSkillController()
    {
        inventorySlot = inventory.GetInventorySlot(transform);
        item = inventorySlot.item;
        if (item.itQuickSlotItem)
            skillsController.quickslotItems[number] = item.quickslotItem;
    }
    public void removeItemFromSkillController()
    {
        if (inventorySlot.amount > 1)
        {
            inventory.RemoveItem(inventorySlot, 1);
        }
        else
        {
            inventory.RemoveItem(inventorySlot, 1);
            skillsController.quickslotItems[number] = null;
        }
    }
    public void removeSlotFromSkillController()
    {
        if (inventorySlot.amount <= 1)
        {
            skillsController.quickslotItems[number] = null;
        }

    }
}
