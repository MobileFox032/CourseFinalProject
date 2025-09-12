using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject _shopUI;
    [SerializeField] private GameObject[] _farmPlots;

    public static ShopManager Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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
    // Update is called once per frame
    void Update()
    {

    }

    public void BuyFarmPlot()
    {
        
        if (MainManager.Instance.Gold - Constants.farmPlotBuyCost >= 0)
        {
            if (MainManager.Instance._currentFarmPlotIndex + 1 < _farmPlots.Length)
            {
                MainManager.Instance.SpendGold(Constants.farmPlotBuyCost);
                MainManager.Instance._currentFarmPlotIndex++;
                _farmPlots[MainManager.Instance._currentFarmPlotIndex].SetActive(true);
            }
        }
    }
}
