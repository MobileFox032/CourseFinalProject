using UnityEngine;

[CreateAssetMenu(fileName = "ItemsData", menuName = "Scriptable Objects/ItemsData")]
public class ItemsData : ScriptableObject
{
    [Header("Default")]
    [SerializeField] private int _id;
    [SerializeField] private bool _isStackable = true;
    [SerializeField] private Sprite _itemIcon;
    [SerializeField] private ItemsType _itemType;
    [Header("Weapon")]
    [SerializeField] private int _damage;

    [Header("Seeds")]
    [SerializeField] private int _costToBuy;
    [SerializeField] private int _sellCost;
    [SerializeField] private int _sellCostFullyGrown;
    [SerializeField] private int _timeToGrowth;
    [SerializeField] private GameObject _prefabSmall;
    [SerializeField] private GameObject _prefabMedium;
    [SerializeField] private GameObject _prefabLarge;
    public int CostToBuy => _costToBuy;
    public int SellCost => _sellCost;
    public int SellCostFullyGrown => _sellCostFullyGrown;
    public int TimeToGrowth => _timeToGrowth;
    public GameObject PrefabSmall => _prefabSmall;
    public GameObject PrefabMedium => _prefabMedium;
    public GameObject PrefabLarge => _prefabLarge;
    public int ID => _id;
    public bool IsStackable => _isStackable;
    public Sprite ItemIcon => _itemIcon;
    public ItemsType ItemType => _itemType;
    public int Damage => _damage;
}
