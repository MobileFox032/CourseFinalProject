using TMPro;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GoldManager.Instance.OnGoldChanged += UpdateUI;
        UpdateUI(GoldManager.Instance.Gold);
    }

    private void OnDestroy()
    {
        GoldManager.Instance.OnGoldChanged -= UpdateUI;
    }
    private void UpdateUI(int goldAmount)
    {
        _coinsText.text = goldAmount.ToString();
    }
}
