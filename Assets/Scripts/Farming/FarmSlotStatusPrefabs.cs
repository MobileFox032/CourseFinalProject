using UnityEngine;

public class FarmSlotStatusPrefabs : MonoBehaviour
{
    [SerializeField] private FarmPlotStatus _status;
    [SerializeField] private GameObject _prefab;

    public FarmPlotStatus Status => _status;
    public GameObject Prefab => _prefab;
}
