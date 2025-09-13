using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private bool shouldFaceMoveDiraction = false;

    [SerializeField] private CharacterController controller;
    [SerializeField] private PlayerStatsConfig playerStats;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider _weaponCollider;
    [SerializeField] private float gravity = -9.8f;

    private Vector3 moveInput;
    private Vector3 velocity;
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
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * moveInput.y + right * moveInput.x;
        controller.Move(moveDirection * _speed * Time.deltaTime);
        if (shouldFaceMoveDiraction && moveDirection.sqrMagnitude > 0.001)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
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
