using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;

    public static PlayerHealth Instance { get; private set; }

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _healthSlider.maxValue = Constants.playerMaxHealth;
        _healthSlider.value = Constants.playerMaxHealth;
    }

    public void SetCurrentHealth(int _currentHealth)
    {
        _healthSlider.value = _currentHealth;
    }
}
