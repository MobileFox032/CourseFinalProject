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

        if (GoldManager.Instance.Gold - Constants.farmPlotBuyCost >= 0)
        {
            if (GoldManager.Instance.currentFarmPlotIndex + 1 < _farmPlots.Length)
            {
                GoldManager.Instance.SpendGold(Constants.farmPlotBuyCost);
                GoldManager.Instance.currentFarmPlotIndex++;
                _farmPlots[GoldManager.Instance.currentFarmPlotIndex].SetActive(true);
            }
        }
    }

    public void SellFarmPlot()
    {
        if (GoldManager.Instance.currentFarmPlotIndex > 0)
        {
            GoldManager.Instance.AddGold(Constants.farmPlotSellCost);
            _farmPlots[GoldManager.Instance.currentFarmPlotIndex].SetActive(false);
            GoldManager.Instance.currentFarmPlotIndex--;
        }
    }

    public void BuyItem(ItemsData item)
    {
        if (GoldManager.Instance.Gold - item.CostToBuy >= 0)
        {
            GoldManager.Instance.SpendGold(item.CostToBuy);
            InventoryManager.Instance.AddItem(item);
        }
        
    }

    public void SellItem(ItemsData item)
    {
        GoldManager.Instance.AddGold(item.SellCost);
        InventoryManager.Instance.RemoveItem(item); 
    }
}
