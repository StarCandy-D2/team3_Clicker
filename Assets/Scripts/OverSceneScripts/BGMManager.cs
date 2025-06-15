using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;

    public AudioClip musicA;
    public AudioClip musicB;
    public AudioClip musicC;

    private bool isInMusicZone = false;

    private AudioSource audioSource;
    //private Transform player;

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

    

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;

        float savedVolume = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
        audioSource.volume = savedVolume;
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log(SceneManager.GetActiveScene().name);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("dd");
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
    //void Update()
    //{
    //    if (SceneManager.GetActiveScene().name != "MainScene") return;

    //    if (player == null)
    //    {
    //        GameObject playerObj = GameObject.FindWithTag("Player");
    //        if (playerObj != null)
    //            player = playerObj.transform;
    //        return;
    //    }
    //}
    public void PlayMusic(AudioClip clip)
    {
        Debug.Log($"{clip.name}");
        if (audioSource.clip == clip) return;

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
