using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private NavMeshAgent _enemyAgent;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _damage = 10;

    private int _currentHealth;
    private Transform _currentPoint;
    private Transform[] _targetPoints;
    private FarmSlot[] _targets;
    private FarmSlot _currentTarget;
    private float _attackRange = 1.4f;
    private float _attackCooldown = 1.0f;
    private float _attackAnimCD;
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    void Update()
    {
        if (!_currentTarget.isActiveAndEnabled || !_currentTarget.HasPlant())
        {
            SetTarget();
        }
        if (!_enemyAgent.pathPending && _enemyAgent.remainingDistance <= Mathf.Max(_enemyAgent.stoppingDistance, _attackRange))
        {
            _enemyAgent.isStopped = true;
            _attackAnimCD -= Time.deltaTime;
            if (_attackAnimCD <= 0f)
            {
                TryAttack();
                _attackAnimCD = _attackCooldown;
            }

        }
        else
        {
            _enemyAgent.isStopped = false;
        }
    }

    private void OnEnable()
    {
        SetParams();
    }

    private void SetParams()
    {
        _attackAnimCD = _attackCooldown;
        _enemyAgent.stoppingDistance = _attackRange * 0.9f;
        _currentHealth = _maxHealth;

        _targets = TargetPoints.Instance.GetTargets();
        SetTarget();
  
    }

    private void SetTarget()
    {
        for (int i = 0; i < _targets.Length; i++)
        {
            _currentTarget = _targets[Random.Range(0, _targets.Length)];
            if (_currentTarget.HasPlant())
            {
                _enemyAgent.SetDestination(_currentTarget.transform.position); 
                return;
            }
        }

    }

    private void TryAttack()
    {
        _animator.SetTrigger(Constants.EnemyAttack);
        _currentTarget.TakeDamage(_damage);
    }
    private void Die()
    {
        DayNightManager.Instance.IncrementKilledCount();
        Destroy(this.gameObject);
    }

}
