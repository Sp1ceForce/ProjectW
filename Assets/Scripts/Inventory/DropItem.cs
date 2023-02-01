using UnityEngine;
using UnityEngine.EventSystems;

public class DropItem : MonoBehaviour, IDropHandler
{
    private Inventory inventory;
    private InventorySlot inventorySlot;
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        var item = DragItem.dragItem;
        var childrens = transform.GetComponentsInChildren<DragItem>();
        // var SlotTag = transform.tag;
        if (item != null && childrens.Length == 0)
        {
            item.SetItemToSlot(transform);
        }
        else if (item != null && childrens.Length > 0)
        {
            InventorySlot inventorySlotFromDragItem = inventory.GetInventorySlot(item.transform);
            InventorySlot inventorySlotFromDrop = inventory.GetInventorySlot(transform);

            if (inventorySlotFromDragItem.item.id == inventorySlotFromDrop.item.id)
            {
                //Логика объединения
                //Если итоговый amount <=3
                //inventorySlotFromDrop.amount + inventorySlotFromDragItem.amount
                //iventory.remove(inventorySlotFromDragItem)
                //Если итоговый amount > 3
                //добавить в слот до трёх
                //скинуть обратно остатки
            }
            var slot = item.currentSlot;
            childrens[0].SetItemToSlot(slot);
            item.SetItemToSlot(transform);
        }
    }
}