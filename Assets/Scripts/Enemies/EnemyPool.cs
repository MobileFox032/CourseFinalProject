using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _poolSize = 10;
    public int PoolSize => _poolSize;
    [SerializeField] private FarmSlot[] _targets;

    private List<GameObject> _pool;

    void OnEnable()
    {
        SetPool();
    }

    void Update()
    {

    }
    public List<GameObject> GetPool()
    {
        return _pool;
    }
    private void SetPool()
    {
        _pool = new List<GameObject>();

        int _targetCount = 0;
        for (int i = 0; i < _targets.Length; i++)
        {
            if (_targets[i].isActiveAndEnabled)
            {
                _targetCount++;
            }
        }
        if (_targetCount > 0)
        {
            _poolSize = _targetCount * 2;
        }
        else
        {
            _poolSize = 2;
        }

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject _enemy = Instantiate(_enemyPrefab);
            _enemy.SetActive(false);
            _pool.Add(_enemy);
        }
    }
    public bool HasFreeObjects()
    {
        int _currentCount = 0;
        for (int i = 0; i < _poolSize; i++)
        {
            if (_pool[i].activeInHierarchy)
            {
                _currentCount++;
            }
        }
        return _currentCount < _poolSize;
    }
    public GameObject GetEnemy()
    {
        foreach (var enemy in _pool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                return enemy;
            }
        }

        return null;
    }
}
