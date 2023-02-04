using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventBusSystem;
using TMPro;
[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int amount;
    public Transform transform;
    public TMP_Text countText;
    public GameObject iconGameObject;
    public InventorySlot(Item item, Transform transform, TMP_Text countText, GameObject iconGameOject, int amount)
    {
        this.item = item;
        this.amount = amount;
        this.transform = transform;
        this.countText = countText;
        this.iconGameObject = iconGameOject;
    }
}
public class Inventory : MonoBehaviour, IAddItem
{
    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private List<InventorySlot> items = new List<InventorySlot>();
    private List<Transform> slots = new List<Transform>();
    private int size;
    [SerializeField] private int steckCount = 3;
    public int SteckCount { get => steckCount; set => steckCount = value; }
    [SerializeField] private bool mainInventory = false;

    private void Start()
    {
        //Собрать UI слоты в список
        size = InventoryPanel.transform.childCount;
        for (int i = 0; i < size; i++)
        {
            slots.Add(InventoryPanel.transform.GetChild(i));
            if (items[i].item != null && items[i].amount != 0)
            {
                AddItemToUI(items[i], slots[i]);
            }
        }
        for (int i = 0; i < size; i++)
        {
            items[i].transform = slots[i];
        }
    }
    public void SendItemToAnotherInventory(Inventory other, InventorySlot otherSlot, InventorySlot thisSlot, int amount = 1)
    {
        other.AddItemToSelectedSlot(thisSlot.item, otherSlot.transform, amount);
        RemoveItem(thisSlot);
    }
    public void SwapItemBetwenInventory(Inventory other, InventorySlot otherSlot, InventorySlot thisSlot, int amount = 1)
    {
        InventorySlot tmpOther = otherSlot;
        InventorySlot tmpThis = thisSlot;
        Item itmpThis = thisSlot.item;
        Item itmpOther = otherSlot.item;
        GameObject GOThis = thisSlot.iconGameObject;
        GameObject GOOther = otherSlot.iconGameObject;
        // RemoveItem(thisSlot);
        // other.RemoveItem(otherSlot);

        // other.AddItemToSelectedSlot(TmpThis.item, tmpOther.transform, amount);
        thisSlot.item = itmpOther;
        thisSlot.amount = tmpOther.amount;
        thisSlot.iconGameObject = GOOther;
        thisSlot.iconGameObject.transform.position = tmpOther.iconGameObject.transform.position;
        // thisSlot.iconGameObject.GetComponent<Image>().sprite = itmpOther.icon;

        // AddItemToSelectedSlot(tmpOther.item, TmpThis.transform, amount);
        otherSlot.item = itmpThis;
        otherSlot.amount = tmpThis.amount;
        otherSlot.iconGameObject = GOThis;
        otherSlot.iconGameObject.transform.position = tmpThis.iconGameObject.transform.position;
        // otherSlot.iconGameObject.GetComponent<Image>().sprite = itmpThis.icon;



    }
    public InventorySlot AddItemToSelectedSlot(Item item, Transform parent, int amount = 1)
    {
        InventorySlot q = GetInventorySlot(parent);
        // Debug.Log(q.item);
        q.item = item;
        q.amount = amount;
        if (parent.childCount < 1)
            AddItemToUI(q, parent);
        return q;
    }
    public void AddItemToUI(InventorySlot invSlot, Transform objSlot)
    {
        iconPrefab.GetComponent<Image>().sprite = invSlot.item.icon;
        GameObject objectIcon = Instantiate(iconPrefab, objSlot);

        invSlot.iconGameObject = objectIcon;
        invSlot.transform = objSlot;
        invSlot.countText = objectIcon.GetComponentInChildren<TMP_Text>();
        invSlot.countText.SetText(invSlot.amount.ToString());
    }
    public void AddItem(Item item, int amount = 1)
    {
        //Нельзя добавить, если перебор
        // if (items.Count >= size) return;

        //добавить в ГОТОВЫЙ Слот(1 <= amount < 3) предмет
        foreach (InventorySlot slot in items)
        {
            if (slot.item != null)
            {
                if (slot.item.id == item.id && slot.amount < steckCount)
                {
                    slot.amount += amount;
                    return;
                }
            }
        }

        //Добавить предмет в ПЕРВЫЙ ПУСТОЙ слот
        for (int i = 0; i < size; i++)
        {
            if (items[i].item == null)
            {
                items[i].item = item;
                items[i].amount = amount;
                AddItemToUI(items[i], slots[i]);
                break;
            }
        }
    }
    public void RemoveItem(InventorySlot inventorySlot, int amount = 1)
    {
        int result = inventorySlot.amount - amount;
        if (result > 0)
        {
            inventorySlot.amount -= amount;
        }
        else
        {
            inventorySlot.amount = 0;
            inventorySlot.item = null;
            inventorySlot.countText = null;
            Destroy(inventorySlot.iconGameObject);
            inventorySlot.iconGameObject = null;
            // inventorySlot.transform = null;
        }
    }
    public void SwapItem(InventorySlot one, InventorySlot two)
    {
        InventorySlot tmpOne = one;
        int IndexOne = items.IndexOf(one);
        int IndexTwo = items.IndexOf(two);
        // Debug.Log(IndexOne);
        // Debug.Log(IndexTwo);
        items[IndexOne] = items[IndexTwo];
        items[IndexTwo] = tmpOne;

        Transform tmp = items[IndexOne].transform;
        items[IndexOne].transform = tmpOne.transform;
        items[IndexTwo].transform = tmp;
    }
    public InventorySlot GetInventorySlot(Transform trn)
    {
        int q = -1;
        if (slots.Contains(trn))
        {
            q = slots.IndexOf(trn);
        }
        if (q != -1)
        {
            return items[q];
        }
        else
            return null;
    }
    [ContextMenu("Clear Inventory")]
    private void ClearInventory()
    {
        foreach (InventorySlot slot in items)
        {
            RemoveItem(slot, slot.amount);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryPanel.SetActive(!InventoryPanel.active);
        }
    }
    private void OnEnable()
    {
        if (mainInventory)
            EventBus.Subscribe(this);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }


}
