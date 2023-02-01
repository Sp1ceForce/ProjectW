using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static DragItem dragItem;
    public Transform currentSlot;
    private Vector3 startPosition;
    private Transform startParrent;
    private CanvasGroup canvasGroup;
    private RectTransform dragLayer;
    private Transform slot;
    private Inventory inventory;
    private InventorySlot inventorySlot;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        dragLayer = GameObject.FindGameObjectWithTag("DragLayer").GetComponent<RectTransform>();
        currentSlot = transform.parent;
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        slot = null;
        dragItem = this;
        inventorySlot = inventory.GetInventorySlot(transform.parent);
        if (Input.GetMouseButton(0))
        {
            inventorySlot.objectSlot = null;
        }
        if (Input.GetMouseButton(1))
        {
            if (inventorySlot.amount != 1)
            {
                inventorySlot.amount -= 1;
                inventory.AddItem(inventorySlot.item, inventorySlot.objectSlot, inventorySlot.amount);
                inventorySlot.amount = 1;
            }
        }
        startPosition = transform.position;
        startParrent = transform.parent;
        transform.SetParent(dragLayer);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragItem = null;
        canvasGroup.blocksRaycasts = true;
        if (slot == null)
        {
            inventorySlot.objectSlot = startParrent; ////
            transform.SetParent(startParrent);
            transform.position = startPosition;
        }
        slot = null;
    }

    public void SetItemToSlot(Transform slot)
    {
        this.slot = slot;
        inventorySlot.objectSlot = slot;     ////
        transform.SetParent(slot);
        currentSlot = slot;
        transform.localPosition = Vector3.zero;
    }
}