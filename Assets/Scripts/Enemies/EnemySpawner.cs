using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _minTimeToSpawn;
    [SerializeField] private float _maxTimeToSpawn;
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private GameObject _parent;
    private List<GameObject> _pool;

    private float _timeUntilSpawn;
    private int _lastSpawned;
    private int _poolSize;

    void OnEnable()
    {
        _timeUntilSpawn = Random.Range(_minTimeToSpawn, _maxTimeToSpawn);
        _pool = _enemyPool.GetPool();
        _poolSize = _enemyPool.PoolSize;
        _lastSpawned = 0;
        DayNightManager.Instance.SetKillsGoal(_poolSize);
    }

    void Update()
    {
        _timeUntilSpawn -= Time.deltaTime;

        if (_lastSpawned > _poolSize - 1)
        {
            _parent.SetActive(false);
            return;
        }

        if (_timeUntilSpawn <= 0)
        {
            SpawnEnemy();
            _timeUntilSpawn = Random.Range(_minTimeToSpawn, _maxTimeToSpawn);
        }
    }

    private void SpawnEnemy()
    {
        _pool[_lastSpawned].SetActive(true);
        Transform _point = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        _pool[_lastSpawned].transform.position = _point.position;
        _lastSpawned++;
    }
}
