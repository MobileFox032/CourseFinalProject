using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public static FarmManager Instance { get; private set; }

    public List<FarmItem> farmItems = new List<FarmItem>();
    private Dictionary<ItemsData, FarmItem> farmItemDictionary = new Dictionary<ItemsData, FarmItem>();
    public FarmSlot[] farmSlots;

    private int _day = 1;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void PlantSeed(ItemsData itemData, FarmSlot farmSlot)
    {
        if (farmSlot.isActiveAndEnabled)
        {
            if (farmSlot.farmItem == null || farmSlot.farmItem.itemData == null)
            {
                FarmItem newFarmItem = new FarmItem(itemData);
                newFarmItem.farmSlot = farmSlot;
                farmSlot.SetPlant(_day, newFarmItem);
                InventoryManager.Instance.RemoveItem(itemData);
            }
        }
    }
}
