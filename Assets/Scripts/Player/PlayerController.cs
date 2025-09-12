using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private PlayerStatsConfig playerStats;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider _weaponCollider;

    private Vector2 moveInput;
    private bool _isPlayerWalking;
    private float _speed;
    private int _maxHealth;
    private int _currentHealth;
    private int _basicDamage;
    void Start()
    {
        _speed = playerStats.Speed;
        _maxHealth = playerStats.Health;
        _currentHealth = _maxHealth;
        _basicDamage = playerStats.BasicDamage;

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (!_isPlayerWalking)
        {
            _isPlayerWalking = true;
            _animator.SetBool(Constants.playerWalk, _isPlayerWalking);
        }
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnAttackClick(InputAction.CallbackContext context)
    {
        _isPlayerWalking = false;
        _animator.SetBool(Constants.playerWalk, _isPlayerWalking);
        _animator.SetTrigger(Constants.attackAnim);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        PlayerHealth.Instance.SetCurrentHealth(_currentHealth);
    }
    void Update()
    {
        Vector3 moveVector = new Vector3(moveInput.x, 0, moveInput.y);
        if (moveVector != Vector3.zero)
        {
 
            _rb.transform.rotation = Quaternion.Slerp(_rb.transform.rotation, Quaternion.LookRotation(moveVector), 0.15f);
        }

        _rb.MovePosition(transform.position + moveVector * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shop"))
        {
            ShopManager.Instance.ActivateShop();
        }
    }

    private void OnTriggerExit(Collider other)
    {
       if (other.CompareTag("Shop"))
        {
            ShopManager.Instance.DeactivateShop();
        } 
    }

    public void EnableWeaponCollider()
    {
        _weaponCollider.enabled = true;
    }

    public void DisableWeapon()
    {
        _weaponCollider.enabled = false;
    }
}
