using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }
    [SerializeField] private GameObject _mainMenuUI;
    [SerializeField] private AudioClip _mainDayMusic;
    [SerializeField] private AudioClip _mainNightMusic;
    [SerializeField] private AudioClip _buttonPressedSound;

    public AudioClip DayMusic => _mainDayMusic;
    public AudioClip NightMusic => _mainNightMusic;
    public AudioClip ButtonSound => _buttonPressedSound;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        AudioManager.PlayMusic(_mainDayMusic);
    }
    void Start()
    {
        Pause();
    }

    void Update()
    {

    }

    public void Pause()
    {
        AudioManager.PlaySFX2D(_buttonPressedSound);
        _mainMenuUI.SetActive(true);
        Time.timeScale = 1f;
    }

    public void PlayGame()
    {
        AudioManager.PlaySFX2D(_buttonPressedSound);
        _mainMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
