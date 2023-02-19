using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static DragItem dragItem;
    public Transform currentSlot;
    private Vector3 startPosition;
    private Transform startParrent;
    public Transform StartParrent { get => startParrent; }
    private CanvasGroup canvasGroup;
    private RectTransform dragLayer;
    private Transform slot;
    private Inventory inventory;
    private ResultSlot resultSlot;
    private bool itResultSlot = false;
    public CraftSlot craftSlot;
    private bool itCraftSlot = false;
    private InventoryQuickSlot quickSlot;
    private bool itQuickSlot = false;
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        dragLayer = GameObject.FindGameObjectWithTag("DragLayer").GetComponent<RectTransform>();
        currentSlot = transform.parent;
        // inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itResultSlot = transform.parent.TryGetComponent<ResultSlot>(out resultSlot))
        {
            Debug.Log(itResultSlot);
            resultSlot.removeComponentCraft();
            // resultSlot.removeResultCraft();
            // resultSlot.RefreshCraft();

        };
        if (itCraftSlot = transform.parent.TryGetComponent<CraftSlot>(out craftSlot))
        {
            var resSlot = craftSlot.resultSlot;
            resSlot.removeResultCraft();
            if (resSlot.bomb)
            {
                var resBombSlot = resSlot.gameObject.GetComponent<ResultBombSlot>();
                if (craftSlot != resBombSlot.craftSlot_1 && resBombSlot.craftSlot_1.slot.item != null)
                {
                    resBombSlot.RefreshCraft(true, false);

                }


            }
            // craftSlot.resultSlot.RefreshCraft();

        };
        if (itQuickSlot = TryGetComponent<InventoryQuickSlot>(out quickSlot))
        {
            quickSlot.removeItemFromSkillController();
        }
        inventory = transform.parent.parent.gameObject.GetComponent<Inventory>();
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
                Debug.Log(currentSlot.name);
                InventorySlot invSlot = inventory.GetInventorySlot(currentSlot);
                Instantiate(invSlot.item.prefab,
                new Vector3(hit.point.x, hit.point.y + 1, hit.point.z),
                Quaternion.identity);
                inventory.RemoveItem(inventorySlot: invSlot);
            }
            else
            {
                transform.SetParent(startParrent);
                transform.position = startPosition;
            }
        }

        slot = null;
        resultSlot = null;
        itResultSlot = false;
        craftSlot = null;
        itCraftSlot = false;
        quickSlot = null;
        itQuickSlot = false;
    }

    public void SetItemToSlot(Transform slot)
    {
        startParrent = null;
        this.slot = slot;
        transform.SetParent(slot);
        currentSlot = slot;
        transform.localPosition = Vector3.zero;
    }
}