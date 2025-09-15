using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }
    [SerializeField] private GameObject _mainMenuUI;
    [SerializeField] private AudioClip _mainDayMusic;
    [SerializeField] private AudioClip _mainNightMusic;

    public AudioClip DayMusic => _mainDayMusic;
    public AudioClip NightMusic => _mainNightMusic;
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
        _mainMenuUI.SetActive(true);
        Time.timeScale = 1f;
    }

    public void PlayGame()
    {
        _mainMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
