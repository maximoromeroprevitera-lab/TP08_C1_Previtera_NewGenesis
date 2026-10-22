using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider SliderVolume;
    [SerializeField] private Slider SliderVolumeUi;
    [SerializeField] private Slider SliderVolumeEffect;
    [SerializeField] private Slider SliderVolumeMaster;
    private void Start()
    {
        SliderVolume.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        SliderVolumeUi.value = PlayerPrefs.GetFloat("UIVolume", 1f);
        SliderVolumeEffect.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        SliderVolumeMaster.value = PlayerPrefs.GetFloat("MasterVolume", 1f);

        SliderVolume.onValueChanged.AddListener(SetMusicVolume);
        SliderVolumeUi.onValueChanged.AddListener(SetUiVolume);
        SliderVolumeEffect.onValueChanged.AddListener(SetEffectVolume);
        SliderVolumeMaster.onValueChanged.AddListener(SetMasterVolume);

        SetMusicVolume(SliderVolume.value);
        SetUiVolume(SliderVolumeUi.value);
        SetEffectVolume(SliderVolumeEffect.value);
        SetMasterVolume(SliderVolumeMaster.value);
    }
    private void SetMusicVolume(float value)
    {
        SetVolume("MusicVolume", value);
    }

    private void SetUiVolume(float value)
    {
        SetVolume("UiVolume", value);
    }
    private void SetEffectVolume(float value)
    {
        SetVolume("SFXVolume", value);
    }
    private void SetMasterVolume(float value)
    {
        SetVolume("MasterVolume", value);
    }
    private void SetVolume(string parameter, float value)
    {
        float volume = Mathf.Log10(value) * 20f;

        audioMixer.SetFloat(parameter, volume);
        PlayerPrefs.SetFloat(parameter, value);
    }
}
