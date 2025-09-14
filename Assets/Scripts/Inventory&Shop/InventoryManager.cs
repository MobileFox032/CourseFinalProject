using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<ItemsData, InventoryItem> itemDictionary = new Dictionary<ItemsData, InventoryItem>();

    public InventorySlot[] inventorySlots;
    private int _selectedSlot = 0;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        inventorySlots[_selectedSlot].SelectedSlot();
    }

    public void AddItem(ItemsData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.AddToStack();
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot._item;
                if (itemInSlot == item)
                {
                    ChangeItemInSlot(item, slot);
                    return;
                }
            }
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            inventory.Add(newItem);
            itemDictionary.Add(itemData, newItem);
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot._item;
                if (itemInSlot == null)
                {
                    ChangeItemInSlot(newItem, slot);
                    return;
                }
            }
        }

    }
    public void SelectSlot(int _newSelectedSlot)
    {
        inventorySlots[_selectedSlot].NonSelectedSlot();
        inventorySlots[_newSelectedSlot].SelectedSlot();
        _selectedSlot = _newSelectedSlot;
    }
    public void ChangeItemInSlot(InventoryItem item, InventorySlot slot)
    {
        slot.SetItem(item);
    }
    public void RemoveItem(ItemsData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.RemoveFromStack();
            for (int i = 0; i < inventorySlots.Length; i++)
                {
                    InventorySlot slot = inventorySlots[i];
                    InventoryItem itemInSlot = slot._item;
                    if (itemInSlot == item)
                    {
                        slot.UpdateStackSizeText(item.stackSize);
                        break;
                    }
                }
            if (item.stackSize == 0)
            {
                inventory.Remove(item);
                itemDictionary.Remove(itemData);
                for (int i = 0; i < inventorySlots.Length; i++)
                {
                    InventorySlot slot = inventorySlots[i];
                    InventoryItem itemInSlot = slot._item;
                    if (itemInSlot == item)
                    {
                        slot.Clear(item);
                        break;
                    }
                }
            }
        }
    }
}
