using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject _shopUI;
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
}
