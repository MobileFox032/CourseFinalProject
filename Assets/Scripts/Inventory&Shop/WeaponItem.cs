using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private GameObject _prefab;

    public int ID => _id;

    public void EnablePrefab(bool _enable)
    {
        _prefab.SetActive(_enable);
    }
}
