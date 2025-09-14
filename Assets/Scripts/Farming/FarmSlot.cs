using UnityEngine;

public class FarmSlot : MonoBehaviour
{
    public FarmItem farmItem;
    private int _plantedOnDay;
    private FarmPlotStatus _farmPlotStatus = FarmPlotStatus.Dirt;
    [SerializeField] private FarmSlotStatusPrefabs[] _farmPlotStatusPrefabs;
    [SerializeField] private Transform _spawnPlantPoint;
    private GameObject _lastPlantStatus;

    public void SetPlant(int _day, FarmItem _item)
    {
        _plantedOnDay = _day;
        farmItem = _item;
    }

    public void SetSlotStatus(FarmPlotStatus _status)
    {
        _farmPlotStatus = _status;
        foreach (var item in _farmPlotStatusPrefabs)
        {
            if (item.Status == _status)
            {
                item.Prefab.SetActive(true);
                break;
            }
            else
            {
                item.Prefab.SetActive(false);
            }
        }
    }

    public void UpdateGrowth(int currentDay)
    {
        if (farmItem == null || farmItem.itemData == null)
        {
            return;
        }

        int daysGrowing = currentDay - _plantedOnDay;
        int stageIndex;

        if (daysGrowing <= 0)
        {
            stageIndex = 0;
        }
        else
        {
            stageIndex = Mathf.Min(1 + Mathf.FloorToInt((daysGrowing - 1) * 3f / farmItem.itemData.TimeToGrowth), 3);
        }

        SetStage(stageIndex, farmItem.itemData);
    }

    private void SetStage(int stageIndex, ItemsData data)
    {
        GameObject newPrefab = data.GrowthPrefabs[stageIndex];

        if (_lastPlantStatus != null)
            Destroy(_lastPlantStatus);

        _lastPlantStatus = Instantiate(newPrefab, _spawnPlantPoint.position, _spawnPlantPoint.rotation);
    }

}
