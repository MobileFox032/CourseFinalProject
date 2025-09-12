using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "Scriptable Objects/Items")]
public class Items : ScriptableObject
{
    public int ID => _id;
    [SerializeField] private int _id;
    public int CostToBuy => _costToBuy;
    [SerializeField] private int _costToBuy;
    public int TimeToGrowth => _timeToGrowth;
    [SerializeField] private int _timeToGrowth;
    public int SellCost => _sellCost;
    [SerializeField] private int _sellCost;
    public GameObject PrefabSmall => _prefabSmall;
    [SerializeField] private GameObject _prefabSmall;
    public GameObject PrefabMedium => _prefabMedium;
    [SerializeField] private GameObject _prefabMedium;
    public GameObject PrefabLarge => _prefabLarge;
    [SerializeField] private GameObject _prefabLarge;
}
