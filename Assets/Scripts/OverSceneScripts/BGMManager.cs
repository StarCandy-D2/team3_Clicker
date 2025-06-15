using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;

    public AudioClip musicA;
    public AudioClip musicB;
    public AudioClip musicC;

    //private bool isInMusicZone = false;

    private AudioSource audioSource;
    //private Transform player;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource가 없어서 새로 추가합니다.");
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.loop = true;
        audioSource.volume = PlayerPrefs.GetFloat("BGMVolume", 0.1f);

        SceneManager.sceneLoaded += OnSceneLoaded;
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "StartScene":
                PlayMusic(musicA);
                break;       
            case "UFOScene":
                PlayMusic(musicB);
                break;
            case "MainScene":
                PlayMusic(musicC);
                break;
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        { SceneManager.LoadScene("UFOScene"); }
    }
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("PlayMusic()에 null AudioClip이 전달되었습니다!");
            return;
        }

        if (audioSource == null)
        {
            Debug.LogWarning("audioSource가 유효하지 않음 (파괴되었거나 초기화되지 않음)");
            return;
        }

        if (audioSource.clip == clip && audioSource.isPlaying) return;

        Debug.Log($"PlayMusic 호출: {clip.name}");

        audioSource.clip = clip;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
        audioSource.clip = null;
    }
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }
    public float GetVolume()
    {
        return audioSource.volume;
    }
}
