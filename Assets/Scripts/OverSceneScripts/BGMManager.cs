using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;

    public AudioClip musicA;
    public AudioClip musicB;
    public AudioClip musicC;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        InitAudioSourceIfNeeded();

        audioSource.loop = true;
        audioSource.volume = PlayerPrefs.GetFloat("BGMVolume", 0.1f);

        SceneManager.sceneLoaded += OnSceneLoaded;
        PlayInitialMusic(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        // Editor 재실행 대비용: audioSource가 파괴됐으면 다시 붙이기
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource was missing! Re-adding it.");
            InitAudioSourceIfNeeded();
        }
    }

    void InitAudioSourceIfNeeded()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void PlayInitialMusic(string sceneName)
    {
        switch (sceneName)
        {
            case "StartScene":
                PlayMusic(musicA);
                break;
            case "UFOScene":
            case "IntroScene":
                PlayMusic(musicB);
                break;
            case "MainScene":
            case "TutorialScene":
                PlayMusic(musicC);
                break;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene == null)
        {
            Debug.Log("Scene is null");
        }
        StartCoroutine(DelayedPlay(scene.name));
    }

    private IEnumerator DelayedPlay(string sceneName)
    {
        yield return null; // 1 프레임 대기
        PlayInitialMusic(sceneName);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("PlayMusic: clip is null");
            return;
        }

        if (audioSource == null || audioSource.gameObject == null)
        {
            Debug.LogWarning("PlayMusic: AudioSource missing, reinitializing.");
            InitAudioSourceIfNeeded();
        }

        if (audioSource.clip == clip && audioSource.isPlaying) return;

        audioSource.clip = clip;
        audioSource.Play();
    }

    public void StopMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.clip = null;
        }
    }

    public void SetVolume(float volume)
    {
        if (audioSource == null)
            InitAudioSourceIfNeeded();

        audioSource.volume = volume;
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public float GetVolume()
    {
        return audioSource != null ? audioSource.volume : 0.0f;
    }
}

