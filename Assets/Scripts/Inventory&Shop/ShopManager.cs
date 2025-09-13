using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject _shopUI;
    [SerializeField] private GameObject[] _farmPlots;

    public static ShopManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void ActivateShop()
    {
        _shopUI.SetActive(true);

    }

    public void DeactivateShop()
    {
        _shopUI.SetActive(false);
    }

    public void BuyFarmPlot()
    {

        if (MainManager.Instance.Gold - Constants.farmPlotBuyCost >= 0)
        {
            if (MainManager.Instance.currentFarmPlotIndex + 1 < _farmPlots.Length)
            {
                MainManager.Instance.SpendGold(Constants.farmPlotBuyCost);
                MainManager.Instance.currentFarmPlotIndex++;
                _farmPlots[MainManager.Instance.currentFarmPlotIndex].SetActive(true);
            }
        }
    }

    public void SellFarmPlot()
    {
        if (MainManager.Instance.currentFarmPlotIndex > 0)
        {
            MainManager.Instance.AddGold(Constants.farmPlotSellCost);
            _farmPlots[MainManager.Instance.currentFarmPlotIndex].SetActive(false);
            MainManager.Instance.currentFarmPlotIndex--;
        }
    }

    public void BuyItem(ItemsData item)
    {
        if (MainManager.Instance.Gold - item.CostToBuy >= 0)
        {
            MainManager.Instance.SpendGold(item.CostToBuy);
            InventoryManager.Instance.AddItem(item);
        }
        
    }

    public void SellItem(ItemsData item)
    {
        MainManager.Instance.AddGold(item.SellCost);
        InventoryManager.Instance.RemoveItem(item); 
    }
}
