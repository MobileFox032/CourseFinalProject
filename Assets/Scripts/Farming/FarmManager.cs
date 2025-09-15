using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public static FarmManager Instance { get; private set; }

    public List<FarmItem> farmItems = new List<FarmItem>();

    public FarmSlot[] farmSlots;

    // private int _day = 1;
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
                farmSlot.SetPlant(DayNightManager.Instance.Day, newFarmItem);
                farmSlot.UpdateGrowth(DayNightManager.Instance.Day);
                InventoryManager.Instance.RemoveItem(itemData);
            }
        }
    }

    public void ChangeFarmSlotStatus(FarmSlot farmSlot)
    {
        if (farmSlot.isActiveAndEnabled)
        {
            farmSlot.SetSlotStatus(FarmPlotStatus.Cultivated);
        }
    }

    public bool CheckIfCanHarvest(FarmSlot farmSlot)
    {
        return farmSlot.CanHarvest(DayNightManager.Instance.Day);
    }

    public void Harvest(FarmSlot farmSlot)
    {
        MainManager.Instance.AddGold(farmSlot.farmItem.itemData.SellCostFullyGrown);
        MakeSlotEmpty(farmSlot);
    }

    public void MakeSlotEmpty(FarmSlot farmSlot)
    {
        farmSlot.RestoreSlotDefault();
    }
    public void NextDay()
    {
        foreach (var item in farmSlots)
        {
            item.UpdateGrowth(DayNightManager.Instance.Day);
        }
    }
}
