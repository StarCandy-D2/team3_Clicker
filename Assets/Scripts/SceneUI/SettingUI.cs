using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;
using System.Collections;
using UnityEngine.Rendering;

public class SettingUI : MonoBehaviour
{
    [Header("볼륨 슬라이더")]
    public Slider bgmSlider;
    public Slider sfxSlider;
    public TMP_InputField bgmInput;
    public TMP_InputField sfxInput;
    public Toggle shaketoggle;
    public Toggle particletoggle;

    public bool shakeonoff;
    public bool particleonoff;
    private void Start()
    {
        StartCoroutine(InitializeUI());
    }

    public void OnBGMSliderChanged(float value)
    {
        bgmInput.text = Mathf.RoundToInt(value * 100).ToString();
        BGMManager.Instance.SetVolume(value);
        PlayerPrefs.SetFloat("BGMVolume", value);
    }

    private IEnumerator InitializeUI()
    {
        while (BGMManager.Instance == null || SFXManager.Instance == null)
            yield return null;

        float bgm = BGMManager.Instance.GetVolume();
        float sfx = SFXManager.Instance.GetVolume();

        bgmSlider.value = bgm;
        sfxSlider.value = sfx;
        bgmInput.text = Mathf.RoundToInt(bgm * 100).ToString();
        sfxInput.text = Mathf.RoundToInt(sfx * 100).ToString();
    }
    public void OnSFXSliderChanged(float value)
    {
        sfxInput.text = Mathf.RoundToInt(value * 100).ToString();

        if (SFXManager.Instance != null)
            SFXManager.Instance.SetVolume(value);
    }

    public void OnBGMInputChanged(string text)
    {
        if (int.TryParse(text, out int percent))
        {
            float v = Mathf.Clamp01(percent / 100f);
            bgmSlider.value = v;
            OnBGMSliderChanged(v);
        }
    }

    public void OnSFXInputChanged(string text)
    {
        if (int.TryParse(text, out int percent))
        {
            float v = Mathf.Clamp01(percent / 100f);
            sfxSlider.value = v;
            OnSFXSliderChanged(v);
        }
    }
    public void OnOffShake()
    {
        if (shakeonoff == true)
        {
            shakeonoff = false;
        }
        else
        {
            shakeonoff = true;
        }
        Debug.Log($"쉐이크{shakeonoff}");
    }
    public void OnOffParticle()
    {
        if (particleonoff == true)
        {
            particleonoff = false;
        }
        else { particleonoff = true; }

        Debug.Log($"파티클{particleonoff}");
    }

}