using UnityEngine;

public class FarmSlot : MonoBehaviour
{
    public FarmItem farmItem;
    private int _plantedOnDay;
    [SerializeField] private GameObject _seedPrefab;

    public void SetPlant(int _day, FarmItem _item)
    {
        _plantedOnDay = _day;
        farmItem = _item;
        _seedPrefab.SetActive(true);
    }
}
