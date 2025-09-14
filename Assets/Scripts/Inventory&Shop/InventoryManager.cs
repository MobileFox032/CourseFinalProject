using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<ItemsData, InventoryItem> itemDictionary = new Dictionary<ItemsData, InventoryItem>();
    [SerializeField] private ItemsData[] startingItems;
    [SerializeField] private WeaponItem[] weaponItems;

    public InventorySlot[] inventorySlots;
    private int _selectedSlot = 0;
    private WeaponItem _currentActiveWeapon;
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
        foreach (var item in startingItems)
        {
            AddItem(item);
        }
        foreach (var item in weaponItems)
        {
            if (item.ID == inventorySlots[_selectedSlot]._item.itemData.ID)
                {
                    item.EnablePrefab(true);
                    _currentActiveWeapon = item;
                    break;
                }
        }
    }

    public void AddItem(ItemsData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.AddToStack();
            ChangeItemInSlot(item, item.itemSlot);
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
        if (inventorySlots[_selectedSlot]._item != null && inventorySlots[_selectedSlot]._item.itemData != null)
        {
            
            if (inventorySlots[_selectedSlot]._item.itemData.ItemType == ItemsType.Weapon)
            {
                foreach (var item in weaponItems)
                {
                    if (item.ID == inventorySlots[_selectedSlot]._item.itemData.ID)
                    {
                        item.EnablePrefab(true);
                        _currentActiveWeapon = item;
                        break;
                    }
                }
            }
            else
            {
                if (_currentActiveWeapon != null)
                {
                    _currentActiveWeapon.EnablePrefab(false);
                }
            }
        }else
        {
            _currentActiveWeapon.EnablePrefab(false);
        }
    }
    public void ChangeItemInSlot(InventoryItem item, InventorySlot slot)
    {
        item.itemSlot = slot;
        slot.SetItem(item);
    }
    public void RemoveItem(ItemsData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.RemoveFromStack();
            item.itemSlot.UpdateStackSizeText(item.stackSize);

            if (item.stackSize == 0)
            {
                inventory.Remove(item);
                itemDictionary.Remove(itemData);
                item.itemSlot.Clear(item);
            }
        }
    }

    public InventoryItem GetItemInActiveSlot()
    {
        return inventorySlots[_selectedSlot]._item;
    }
}
