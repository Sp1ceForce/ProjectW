using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryQuickSlot : MonoBehaviour
{

    private int number;
    private SkillsController skillsController;
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform == transform.GetChild(i))
            {
                number = i;
            }
        }
        skillsController = GameObject.FindGameObjectWithTag("Player").GetComponent<SkillsController>();
    }
    public void sendToSkillController()
    {
        var item = transform.parent.GetComponent<Inventory>().GetInventorySlot(transform).item;
        if (item.itQuickSlotItem)
            skillsController.quickslotItems[number] = item.quickslotItem;
    }
    public void removeItemFromSkillController()
    {
        skillsController.quickslotItems.RemoveAt(number);
    }
}