using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class SettingUI : MonoBehaviour
{
    [Header("볼륨 슬라이더")]
    public Slider bgmSlider;
    public Slider sfxSlider;
    public TMP_InputField bgmInput;
    public TMP_InputField sfxInput;

    private void Start()
    {
        // 초기화
        float bgm = PlayerPrefs.GetFloat("BGMVolume", 1f);
        float sfx = PlayerPrefs.GetFloat("SFXVolume", 1f);
        bgmSlider.value = bgm;
        sfxSlider.value = sfx;
        bgmInput.text = Mathf.RoundToInt(bgm * 100).ToString();
        sfxInput.text = Mathf.RoundToInt(sfx * 100).ToString();
    }

    public void OnBGMSliderChanged(float value)
    {
        bgmInput.text = Mathf.RoundToInt(value * 100).ToString();
        BGMManager.Instance.SetVolume(value);
        PlayerPrefs.SetFloat("BGMVolume", value);
    }

    //public void OnSFXSliderChanged(float value)
    //{
    //    sfxInput.text = Mathf.RoundToInt(value * 100).ToString();
    //    SFXManager.Instance.SetVolume(value);
    //    PlayerPrefs.SetFloat("SFXVolume", value);
    //}

    public void OnBGMInputChanged(string text)
    {
        if (int.TryParse(text, out int percent))
        {
            float v = Mathf.Clamp01(percent / 100f);
            bgmSlider.value = v;
            OnBGMSliderChanged(v);
        }
    }

    //public void OnSFXInputChanged(string text)
    //{
    //    if (int.TryParse(text, out int percent))
    //    {
    //        float v = Mathf.Clamp01(percent / 100f);
    //        sfxSlider.value = v;
    //        OnSFXSliderChanged(v);
    //    }
    //}
}