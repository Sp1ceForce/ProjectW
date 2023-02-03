using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
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
        inventory.SwapItem(inventory.GetInventorySlot(item.currentSlot), inventory.GetInventorySlot(transform));
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

}