using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

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
    // private InventorySlot tmpSlot;
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
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                InventorySlot invSlot = inventory.GetInventorySlot(startParrent);
                Instantiate(invSlot.item.prefab,
                new Vector3(hit.point.x, hit.point.y + 1, hit.point.z),
                Quaternion.identity);
                inventory.RemoveItem(invSlot);
            }
            else
            {
                transform.SetParent(startParrent);
                transform.position = startPosition;
            }

        }
        slot = null;
    }

    public void SetItemToSlot(Transform slot)
    {
        this.slot = slot;
        transform.SetParent(slot);
        currentSlot = slot;
        transform.localPosition = Vector3.zero;
    }
}