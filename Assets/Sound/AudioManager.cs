using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Mixer")]
    [SerializeField] private AudioMixer mixer; 
    [SerializeField] private string musicParam = "MusicVolume";
    [SerializeField] private string sfxParam = "SFXVolume";

    [Header("Music")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private float musicFadeTime = 0.5f;

    [Header("SFX Pool")]
    [SerializeField] private Transform sfxPoolRoot;
    [SerializeField] private AudioMixerGroup sfxGroup;
    [SerializeField] private int poolSize = 16;

    [Header("Libraries (optional)")]
    public List<AudioClip> musicClips;        
    public List<AudioClip> sfxClips;

    private readonly Queue<AudioSource> _free = new();
    private readonly HashSet<AudioSource> _busy = new();

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        
        if (sfxPoolRoot == null)
        {
            sfxPoolRoot = new GameObject("SFXPool").transform;
            sfxPoolRoot.SetParent(transform);
        }
        for (int i = 0; i < poolSize; i++) _free.Enqueue(CreateSfxSource());
    }

    AudioSource CreateSfxSource()
    {
        var go = new GameObject("SFXSource");
        go.transform.SetParent(sfxPoolRoot);
        var src = go.AddComponent<AudioSource>();
        src.playOnAwake = false;
        src.loop = false;
        src.spatialBlend = 0f; 
        if (sfxGroup) src.outputAudioMixerGroup = sfxGroup;
        return src;
    }

    void Update()
    {
        if (_busy.Count == 0) return;
        var toRelease = new List<AudioSource>();
        foreach (var s in _busy) if (!s.isPlaying) toRelease.Add(s);
        foreach (var s in toRelease) { _busy.Remove(s); _free.Enqueue(s); }
    }

    // ---------- PUBLIC API ----------

    public static void PlayMusic(AudioClip clip, float? fade = null, bool loop = true)
        => Instance.StartCoroutine(Instance.CoPlayMusic(clip, fade ?? Instance.musicFadeTime, loop));

    public static void StopMusic(float? fade = null)
        => Instance.StartCoroutine(Instance.CoPlayMusic(null, fade ?? Instance.musicFadeTime, false));

    public static AudioSource PlaySFX2D(AudioClip clip, float vol = 1f, float pitchJitter = 0f)
        => Instance.PlaySfxInternal(clip, vol, pitchJitter, null, Vector3.zero, is3D:false);

    public static AudioSource PlaySFX3D(AudioClip clip, Vector3 pos, float vol = 1f, float pitchJitter = 0.05f)
        => Instance.PlaySfxInternal(clip, vol, pitchJitter, null, pos, is3D:true);

    public static AudioSource PlaySFXFollow(AudioClip clip, Transform follow, float vol = 1f, float pitchJitter = 0.05f)
        => Instance.PlaySfxInternal(clip, vol, pitchJitter, follow, Vector3.zero, is3D:true);

    public static void SetMusicVolume01(float v01) => Instance.SetDb(Instance.musicParam, v01);
    public static void SetSfxVolume01(float v01) => Instance.SetDb(Instance.sfxParam, v01);

    // ---------- IMPLEMENTATION ----------

    System.Collections.IEnumerator CoPlayMusic(AudioClip clip, float fade, bool loop)
    {
        if (musicSource == null)
        {
            var go = new GameObject("MusicSource");
            go.transform.SetParent(transform);
            musicSource = go.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.spatialBlend = 0f;
        }

        // fade out
        float t = 0f;
        float start = musicSource.volume;
        while (t < fade) { t += Time.unscaledDeltaTime; musicSource.volume = Mathf.Lerp(start, 0f, t/fade); yield return null; }

        if (clip == null) { musicSource.Stop(); yield break; }

        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();

        // fade in
        t = 0f;
        while (t < fade) { t += Time.unscaledDeltaTime; musicSource.volume = Mathf.Lerp(0f, 1f, t/fade); yield return null; }
        musicSource.volume = 1f;
    }

    AudioSource PlaySfxInternal(AudioClip clip, float vol, float jitter, Transform follow, Vector3 pos, bool is3D)
    {
        if (clip == null) return null;
        if (_free.Count == 0) _free.Enqueue(CreateSfxSource());

        var src = _free.Dequeue();
        _busy.Add(src);

        src.clip = clip;
        src.volume = Mathf.Clamp01(vol);
        src.pitch = 1f + Random.Range(-jitter, jitter);
        src.spatialBlend = is3D ? 1f : 0f;
        src.minDistance = 1f; src.maxDistance = 25f; // підкрути за потреби

        if (follow != null)
        {
            src.transform.position = follow.position;
            src.transform.SetParent(follow); // звук "їде" разом із об’єктом
        }
        else
        {
            src.transform.SetParent(sfxPoolRoot);
            src.transform.position = pos;
        }

        src.Play();
        return src;
    }

    void SetDb(string param, float v01)
    {
        // 0..1 -> дБ (логарифмічна крива). 0 = mute.
        if (v01 <= 0.0001f) { mixer.SetFloat(param, -80f); return; }
        mixer.SetFloat(param, Mathf.Log10(v01) * 20f);
    }
}
