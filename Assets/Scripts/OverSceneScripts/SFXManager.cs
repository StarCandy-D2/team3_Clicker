using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [Header("Audio Source & Clips")]
    public AudioSource audioSource;
    public AudioClip touchOutsideUISound;  // A 사운드
    public AudioClip buttonClickSound;     // B 사운드

    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;

    private void Awake()
    {
        // 싱글톤 패턴 적용
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // AudioSource 확보
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // 씬 전환 시 버튼 재스캔 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // 구독 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        // 초기 씬에서도 버튼 스캔
        TryFindRaycaster();
        AttachInterceptorToAllButtons();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬 전환될 때마다 버튼에 인터셉터 부착, Raycaster 갱신
        TryFindRaycaster();
        AttachInterceptorToAllButtons();
    }

    private void Update()
    {
        // 모바일 터치 감지
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 pos = Input.GetTouch(0).position;
            if (IsPointerOverUI(pos) && !IsPointerOverUIButton(pos))
                PlayTouchOutsideUISound();
        }

#if UNITY_EDITOR
        // 에디터용 마우스 테스트
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Input.mousePosition;
            if (IsPointerOverUI(pos) && !IsPointerOverUIButton(pos))
                PlayTouchOutsideUISound();
        }
#endif
    }

    private void AttachInterceptorToAllButtons()
    {
        Button[] buttons = FindObjectsOfType<Button>(true);
        foreach (Button btn in buttons)
        {
            if (btn.GetComponent<ButtonSFXInterceptor>() == null)
                btn.gameObject.AddComponent<ButtonSFXInterceptor>();
        }
    }

    private bool IsPointerOverUI(Vector2 pointerPos)
    {
        if (raycaster == null || eventSystem == null) return false;

        var pointerData = new PointerEventData(eventSystem) { position = pointerPos };
        var results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);
        return results.Count > 0;
    }

    private bool IsPointerOverUIButton(Vector2 pointerPos)
    {
        if (raycaster == null || eventSystem == null) return false;

        var pointerData = new PointerEventData(eventSystem) { position = pointerPos };
        var results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        foreach (var result in results)
        {
            if (result.gameObject.GetComponent<Button>() != null)
                return true;
        }
        return false;
    }

    private void TryFindRaycaster()
    {
        if (eventSystem == null)
            eventSystem = EventSystem.current;

        if (raycaster == null)
        {
            var canvas = FindObjectOfType<Canvas>();
            if (canvas != null)
                raycaster = canvas.GetComponent<GraphicRaycaster>();
        }
    }

    public void PlayTouchOutsideUISound()
    {
        if (touchOutsideUISound != null)
            audioSource.PlayOneShot(touchOutsideUISound);
    }

    public void PlayButtonClickSound()
    {
        Debug.Log("버튼 클릭 사운드 재생됨");
        if (buttonClickSound != null)
            audioSource.PlayOneShot(buttonClickSound);
        else
            Debug.LogWarning("buttonClickSound가 할당되지 않았습니다.");
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public float GetVolume()
    {
        return audioSource.volume;
    }
}