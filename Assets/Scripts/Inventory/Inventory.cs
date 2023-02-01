using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventBusSystem;
[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int amount;
    public Transform objectSlot;
    public InventorySlot(Item item, Transform objectSlot, int amount = 1)
    {
        this.item = item;
        this.objectSlot = objectSlot;
        this.amount = amount;

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
    private void Start()
    {
        size = InventoryPanel.transform.childCount;
        for (int i = 0; i < size; i++)
        {
            slots.Add(InventoryPanel.transform.GetChild(i));
        }


        foreach (InventorySlot inventorySlot in items)
        {
            iconPrefab.GetComponent<Image>().sprite = inventorySlot.item.icon;
            foreach (Transform slot in slots)
            {
                if (slot.childCount == 0)
                {
                    Instantiate(iconPrefab, slot);
                    inventorySlot.objectSlot = slot;
                    break;
                }
            }
        }
    }

    public void AddItem(Item item, int amount = 1)
    {
        foreach (InventorySlot slot in items)
        {
            if (slot.item.id == item.id && slot.amount < steckCount)
            {
                slot.amount += amount;
                return;
            }
        }

        if (items.Count >= size) return;

        iconPrefab.GetComponent<Image>().sprite = item.icon;
        foreach (Transform slot in slots)
        {
            if (slot.childCount == 0)
            {
                Instantiate(iconPrefab, slot);
                InventorySlot new_slot = new InventorySlot(item, slot, amount);
                items.Add(new_slot);
                break;
            }
        }

    }
    public void AddItem(Item item, Transform slotTrn, int amount = 1)
    {

        iconPrefab.GetComponent<Image>().sprite = item.icon;
        Instantiate(iconPrefab, slotTrn);
        InventorySlot new_slot = new InventorySlot(item, slotTrn, amount);
        items.Add(new_slot);
    }


    public void RemoveItem(InventorySlot inventorySlot, int amount = 1)
    {
        foreach (InventorySlot slot in items)
        {
            if (slot.item.id == inventorySlot.item.id)
            {
                if (slot.amount > 1) { slot.amount -= 1; break; }
                else
                {
                    Destroy(inventorySlot.objectSlot.gameObject);
                    items.Remove(inventorySlot);
                    break;
                }
            }
        }
    }
    public void SwapItem(InventorySlot itemOne, InventorySlot itemTwo)
    {
        Transform tmp = itemOne.objectSlot;
        itemOne.objectSlot = itemTwo.objectSlot;
        itemTwo.objectSlot = tmp;
    }
    public InventorySlot GetInventorySlot(Transform trn)
    {
        foreach (InventorySlot item in items)
        {
            if (item.objectSlot == trn) { return item; }
        }
        return null;
    }
    public Item GetItem(int i)
    {
        return i < items.Count ? items[i].item : null;
    }
    public int GetAmount(int i)
    {
        return i < items.Count ? items[i].amount : 0;
    }
    public int GetSize()
    {
        return items.Count;
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
        EventBus.Subscribe(this);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    // public void RemoveItem(Item item, int amount = 1)
    // {
    //     InventorySlot toDelete = null;
    //     foreach (InventorySlot slot in items)
    //     {
    //         if (slot.item.id == item.id)
    //         {
    //             if (slot.amount > 1) { slot.amount -= 1; break; }
    //             else { toDelete = slot; break; }
    //         }
    //     }
    //     if (toDelete != null)
    //     {
    //         Destroy(toDelete.objectSlot.gameObject);
    //         items.Remove(toDelete);
    //     }
    // }

}
