using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DayNightManager : MonoBehaviour
{
    public static DayNightManager Instance { get; private set; }
    [SerializeField] private Material skyboxMaterial;
    [SerializeField] private Color _dayColor;
    [SerializeField] private Color _nightColor;
    [SerializeField] private Volume _volume;
    [SerializeField] private GameObject _enemySpawner;
    private ColorAdjustments colorAdjustments;
    private float _duration = 1f;
    private int _day = 1;
    public int Day => _day;

    private int _needToKillAtThisDay;
    private int _killedThisDay;

    public void SetKillsGoal(int _goal)
    {
        _needToKillAtThisDay = _goal;
        _killedThisDay = 0;
    }

    public void IncrementKilledCount()
    {
        _killedThisDay++;
        if (_killedThisDay >= _needToKillAtThisDay)
        {
            BlendToDay();
        }
    }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _volume.profile.TryGet(out colorAdjustments);
        skyboxMaterial.SetFloat(Constants.shaderBlendProperty, Constants.shaderDayBlend);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BlendToDay()
    {
        AudioManager.PlayMusic(MainManager.Instance.DayMusic, 1.0f, true);
        StartBlend(Constants.shaderDayBlend, _dayColor);
        _day++;
        FarmManager.Instance.NextDay();
    }
    public void BlendToNight()
    {
        AudioManager.PlayMusic(MainManager.Instance.NightMusic, 1.0f, true);
        StartBlend(Constants.shaderNightBlend, _nightColor);
        _enemySpawner.SetActive(true);
    }

    private void StartBlend(float _targetBlend, Color _targetColor)
    {
        StartCoroutine(BlendSkybox(_targetBlend));
        if (colorAdjustments != null)
        {
            StartCoroutine(BlendColor(_targetColor));
        }
    }

    private IEnumerator BlendSkybox(float _target)
    {
        float _startBlend = skyboxMaterial.GetFloat(Constants.shaderBlendProperty);
        float _currentTime = 0f;

        while (_currentTime < _duration)
        {
            float currentBlend = Mathf.Lerp(_startBlend, _target, _currentTime / _duration);
            skyboxMaterial.SetFloat(Constants.shaderBlendProperty, currentBlend);
            _currentTime += Time.deltaTime;
            yield return null;
        }

        skyboxMaterial.SetFloat(Constants.shaderBlendProperty, _target);
    }
    
    private IEnumerator BlendColor(Color _target)
    {
        Color _startColor = colorAdjustments.colorFilter.value;
        float _currentTime = 0f;

        while (_currentTime < _duration)
        {                  
            Color _currentColor = Color.Lerp(_startColor, _target, _currentTime / _duration);
            colorAdjustments.colorFilter.value = _currentColor;
            _currentTime += Time.deltaTime;
            yield return null;
        }

        colorAdjustments.colorFilter.value = _target;
    }
}
