using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }
    [SerializeField] private GameObject _mainMenuUI;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
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
