using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class DropItem : MonoBehaviour, IDropHandler
{
    private Inventory fromInventory;
    private Inventory thisInventory;
    private void Start()
    {
        // mainInventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        thisInventory = transform.parent.GetComponent<Inventory>();
        fromInventory = null;
    }
    public void OnDrop(PointerEventData eventData)
    {
        var item = DragItem.dragItem;
        var childrens = transform.GetComponentsInChildren<DragItem>();

        fromInventory = item.StartParrent.parent.GetComponent<Inventory>();
        Debug.Log(fromInventory);
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
                childrens[0].SetItemToSlot(slot);
                item.SetItemToSlot(transform);
            }
        }
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

    }

}