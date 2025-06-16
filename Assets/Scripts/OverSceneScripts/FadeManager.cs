using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    [Header("설정")]
    public Image fadeImage;       // 검정색 Panel의 Image
    public float defaultFadeTime = 1f;

    private Coroutine currentFade;

    private void Awake()
    {
        // 싱글톤 패턴
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 새 씬 로딩 시 자동 Fade In
        FadeTo(0f, defaultFadeTime);
    }

    public void FadeTo(float targetAlpha, float duration = -1f)
    {
        if (currentFade != null)
            StopCoroutine(currentFade);

        if (duration < 0f) duration = defaultFadeTime;
        currentFade = StartCoroutine(FadeRoutine(targetAlpha, duration));
    }

    private IEnumerator FadeRoutine(float targetAlpha, float duration)
    {
        Color color = fadeImage.color;
        float startAlpha = color.a;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, t);
            fadeImage.color = color;
            yield return null;
        }

        color.a = targetAlpha;
        fadeImage.color = color;
        currentFade = null;
    }

    // 씬 전환 시 어둡게 만들고 씬 이동
    public void FadeOutAndLoadScene(string sceneName)
    {
        StartCoroutine(FadeOutThenLoad(sceneName));
    }

    private IEnumerator FadeOutThenLoad(string sceneName)
    {
        FadeTo(1f);
        yield return new WaitUntil(() => currentFade == null);
        SceneManager.LoadScene(sceneName);
    }
}
