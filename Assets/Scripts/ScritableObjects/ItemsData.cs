using UnityEngine;

[CreateAssetMenu(fileName = "ItemsData", menuName = "Scriptable Objects/ItemsData")]
public class ItemsData : ScriptableObject
{
    public int ID => _id;
    [SerializeField] private int _id;
    public int CostToBuy => _costToBuy;
    [SerializeField] private int _costToBuy;
    public int SellCost => _sellCost;
    [SerializeField] private int _sellCost;

    public int SellCostFullyGrown => _sellCostFullyGrown;
    [SerializeField] private int _sellCostFullyGrown;
    public int TimeToGrowth => _timeToGrowth;
    [SerializeField] private int _timeToGrowth;

    public bool IsStackable => _isStackable;
    [SerializeField] private bool _isStackable = true;

    public Sprite ItemIcon => _itemIcon;
    [SerializeField] private Sprite _itemIcon;

    public GameObject PrefabSmall => _prefabSmall;
    [SerializeField] private GameObject _prefabSmall;
    public GameObject PrefabMedium => _prefabMedium;
    [SerializeField] private GameObject _prefabMedium;
    public GameObject PrefabLarge => _prefabLarge;
    [SerializeField] private GameObject _prefabLarge;
    public ItemsType ItemType => _itemType;
    [SerializeField] private ItemsType _itemType; 
}
