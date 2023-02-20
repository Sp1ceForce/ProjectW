using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class DropItem : MonoBehaviour, IDropHandler
{
    private Inventory fromInventory;
    private Inventory thisInventory;
    private ResultSlot resultSlot;
    private bool itResultSlot = false;
    private CraftSlot craftSlot;
    private bool itCraftSlot = false;
    private InventoryQuickSlot quickSlot;
    private bool itQuickSlot = false;
    private event Action addItemToCraftSlot;

    private void Start()
    {

        // mainInventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        thisInventory = transform.parent.GetComponent<Inventory>();
        fromInventory = null;
        if (itResultSlot = TryGetComponent<ResultSlot>(out resultSlot))
        {
            // Debug.Log(itResultSlot);
        };
        if (itCraftSlot = TryGetComponent<CraftSlot>(out craftSlot))
        {
            // Debug.Log(itCraftSlot);
            // addItemToCraftSlot += craftSlot.AddToCraft; //// !!!!! ОТПИШИСЬ!!!!!
            // Debug.Log("HOLA!!!");
        };
        if (itQuickSlot = TryGetComponent<InventoryQuickSlot>(out quickSlot))
        {

        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        var item = DragItem.dragItem;
        var childrens = transform.GetComponentsInChildren<DragItem>();

        //Обработка возможности положить предмет в результирующий слот крафта
        if (itResultSlot)
        {
            var slot = item.currentSlot;
            item.SetItemToSlot(slot);
            return;
        }

        fromInventory = item.StartParrent.parent.GetComponent<Inventory>();
        // Операции в пределах одного инвентаря
        if (thisInventory == fromInventory)
        {
            thisInventory.SwapItem(thisInventory.GetInventorySlot(item.currentSlot), thisInventory.GetInventorySlot(transform));
            if (item != null && childrens.Length == 0)
            {
                item.SetItemToSlot(transform);
            }
            else if (item != null && childrens.Length > 0)
            {
                var slot = item.currentSlot;
                childrens[0].SetItemToSlot(slot); //предмет меняемый из этого слота
                item.SetItemToSlot(transform); //предмет перемещаемый в этот слот
                if (itQuickSlot)
                {
                    childrens[0].transform.parent.GetComponent<InventoryQuickSlot>().sendToSkillController();
                    quickSlot.sendToSkillController();
                }


            }
        }
        // Операции между инвентарями
        else
        {
            if (item != null && childrens.Length == 0)
            {
                fromInventory.SendItemToAnotherInventory(
                thisInventory, thisInventory.GetInventorySlot(transform),
                fromInventory.GetInventorySlot(item.currentSlot),
                fromInventory.GetInventorySlot(item.currentSlot).amount
            );
                item.SetItemToSlot(transform);
            }
            else if (item != null && childrens.Length > 0)
            {
                fromInventory.SwapItemBetwenInventory(thisInventory, thisInventory.GetInventorySlot(transform),
                fromInventory.GetInventorySlot(item.currentSlot),
                fromInventory.GetInventorySlot(item.currentSlot).amount);

                var slot = item.currentSlot;
                childrens[0].SetItemToSlot(slot);
                item.SetItemToSlot(transform);
            }
        }
        fromInventory = null;

        //Обработка возможности положить предмет в слот для крафта
        if (itCraftSlot)
        {
            if (item.craftSlot != null)
                item.craftSlot.UpdateDataSlot();
            craftSlot.AddToCraft();
        }
        if (itQuickSlot) { quickSlot.sendToSkillController(); }
    }

}