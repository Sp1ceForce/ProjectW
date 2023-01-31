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
    public InventorySlot(Item item, int amount = 1)
    {
        this.item = item;
        this.amount = amount;
    }
}
public class Inventory : MonoBehaviour, IAddItem
{
    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private List<InventorySlot> items = new List<InventorySlot>();
    [SerializeField] private List<Transform> slots = new List<Transform>();
    [SerializeField] private int size;
    [SerializeField] private int steckCount = 3;
    private void Start()
    {
        size = InventoryPanel.transform.childCount;
        for (int i = 0; i < size; i++)
        {
            slots.Add(InventoryPanel.transform.GetChild(i));
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
        InventorySlot new_slot = new InventorySlot(item, amount);
        items.Add(new_slot);
        UpdateUI();
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
    private void UpdateUI()
    {
        for (int i = 0; i < GetSize() && i < slots.Count; i++)
        {
            iconPrefab.GetComponent<Image>().sprite = GetItem(i).icon;
            if (slots[i].childCount == 0)
                Instantiate(iconPrefab, slots[i]);
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
        EventBus.Subscribe(this);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }
}
