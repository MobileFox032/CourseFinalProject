using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _minTimeToSpawn;
    [SerializeField] private float _maxTimeToSpawn;
    [SerializeField] private EnemyPool _enemyPool;

    private float _timeUntilSpawn;

    void Start()
    {
        _timeUntilSpawn = Random.Range(_minTimeToSpawn, _maxTimeToSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        _timeUntilSpawn -= Time.deltaTime;

        if (!_enemyPool.HasFreeObjects())
        {
            _enemyPool._allSpawned = true;
            if (!_enemyPool.IsAnyActive())
            {
                enabled = false;
                return;
            }
        }

        if (_timeUntilSpawn <= 0)
        {
            SpawnEnemy();
            _timeUntilSpawn = Random.Range(_minTimeToSpawn, _maxTimeToSpawn);
        }
    }

    private void SpawnEnemy()
    {
        GameObject _enemy = _enemyPool.GetEnemy();
        
            if (!_enemyPool.HasFreeObjects())
            {
                enabled = false;
                return;
            }
            Transform _point = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            _enemy.transform.position = _point.position;
        
    }
}
