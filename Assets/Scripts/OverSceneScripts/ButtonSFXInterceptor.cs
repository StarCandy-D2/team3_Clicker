using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSFXInterceptor : MonoBehaviour
{
    private void Awake()
    {
        Button button = GetComponent<Button>();

        // 이미 연결된 리스너가 있다면 유지한 채 B 효과음만 추가
        button.onClick.AddListener(() =>
        {
            if (SFXManager.Instance != null)
                SFXManager.Instance.PlayButtonClickSound();
        });
    }
}