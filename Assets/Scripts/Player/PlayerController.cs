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
    [SerializeField] private AudioClip _attackSound;
    [SerializeField] private AudioClip _runSound;

    private Vector3 moveInput;
    private Vector3 velocity;
    private bool _isPlayerWalking;
    private float _speed;
    private int _maxHealth;
    private int _currentHealth;
    private int _basicDamage;
    private int clickedSlot;
    private ItemsType _activeItemType;
    private FarmSlot _inFarmSlot;
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

    public void OnActionButtonClick(InputAction.CallbackContext context)
    {
        InventoryItem _activeItem = InventoryManager.Instance.GetItemInActiveSlot();
        if (_activeItem != null)
        {
            _isPlayerWalking = false;
            _animator.SetBool(Constants.playerWalk, _isPlayerWalking);
            ItemsData _activeItemData = _activeItem.itemData;
            _activeItemType = _activeItemData.ItemType;
            if (_activeItemType == ItemsType.Weapon)
            {
                if (_inFarmSlot != null)
                {
                    if (FarmManager.Instance.CheckIfCanHarvest(_inFarmSlot))
                    {
                        _animator.SetTrigger(Constants.gatheringAnim);
                        FarmManager.Instance.Harvest(_inFarmSlot);
                        return;
                    }
                    _animator.SetTrigger(Constants.farmingPlotAnim);
                    FarmManager.Instance.ChangeFarmSlotStatus(_inFarmSlot);
                }

            }
            else if (_activeItemType == ItemsType.Seed)
            {
                if (_inFarmSlot != null)
                {
                    _isPlayerWalking = false;
                    _animator.SetBool(Constants.playerWalk, _isPlayerWalking);
                    _animator.SetTrigger(Constants.gatheringAnim);
                    FarmManager.Instance.PlantSeed(_activeItemData, _inFarmSlot);
                }
            }
        }
        
    }

    public void OnEscapeClick(InputAction.CallbackContext context)
    {
        MainManager.Instance.Pause();
    }
    public void OnAttackClick(InputAction.CallbackContext context)
    {
        _isPlayerWalking = false;
        _animator.SetBool(Constants.playerWalk, _isPlayerWalking);
        _animator.SetTrigger(Constants.attackAnim);
    }

    public void SoundFX()
    {
        AudioManager.PlaySFX2D(_attackSound, 0.8f);
    }

    public void OnInventorySlotsClick(InputAction.CallbackContext context)
    {
        if (int.TryParse(context.control.displayName, out clickedSlot))
        {
            InventoryManager.Instance.SelectSlot(clickedSlot - 1);
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        PlayerHealth.Instance.SetCurrentHealth(_currentHealth);
    }
    void Update()
    {
        if (moveInput.x == 0 && moveInput.y == 0)
        {
            _isPlayerWalking = false;
            _animator.SetBool(Constants.playerWalk, _isPlayerWalking);
        }
        else
        {
            AudioManager.PlaySFXFollow(_runSound, transform, 0.6f, 0.07f);
        }
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
        }else if (other.CompareTag("FarmSlot"))
        {
            if (other.gameObject.TryGetComponent(out FarmSlot farmSlot))
            {
                _inFarmSlot = farmSlot;    
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
       if (other.CompareTag("Shop"))
        {
            ShopManager.Instance.DeactivateShop();
        }else if (other.CompareTag("FarmSlot"))
        {
            _inFarmSlot = null;
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
