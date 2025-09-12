using System;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }
    public int Gold => currentGoldAmount;
    private int currentGoldAmount = 200;
    public int _currentFarmPlotIndex = 0;

    public event Action<int> OnGoldChanged;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void AddGold(int amount)
    {
        currentGoldAmount += amount;
        OnGoldChanged?.Invoke(currentGoldAmount);
    }
    
    public void SpendGold(int amount)
    {
        currentGoldAmount -= amount;
        OnGoldChanged?.Invoke(currentGoldAmount);    
    }
}
