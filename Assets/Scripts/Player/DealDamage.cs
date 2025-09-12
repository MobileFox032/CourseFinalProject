using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private int _damage;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            enemy.TakeDamage(_damage);
        }
    }
}
