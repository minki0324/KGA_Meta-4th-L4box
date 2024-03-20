using UnityEngine;
using UnityEngine.UI;

public class AudioOption : MonoBehaviour
{
    [Header("Slider")]
    public Slider BGM_Slider;
    public Slider SFX_Slider;
    private float minSound = -20f;
    private float maxSound = -5f;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        //볼륨 최소값
        BGM_Slider.minValue = minSound;
        SFX_Slider.minValue = minSound;

        //볼륨 최대값 조절
        BGM_Slider.maxValue = maxSound;
        SFX_Slider.maxValue = maxSound;

        //시작 볼륨값 중간으로 조정
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            BGM_Slider.value = PlayerPrefs.GetFloat("BGMVolume");
        }
        else
        {
            BGM_Slider.value = BGM_Slider.maxValue;
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            SFX_Slider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
        else
        {
            SFX_Slider.value = SFX_Slider.maxValue;
        }
    }

    public void SetVolume(bool _isBgm)
    {
        float volume = 0f;
        string soundType = "BGM";
        if (_isBgm)
        {
            volume = BGM_Slider.value;
            soundType = "BGM";
            PlayerPrefs.SetFloat("BGMVolume", volume);
        }
        else
        {
            volume = SFX_Slider.value;
            soundType = "SFX";
            PlayerPrefs.SetFloat("SFXVolume", volume);
        }
        PlayerPrefs.Save();

        if (volume.Equals(minSound))
        {
            AudioManager.Instance.audioMixer.SetFloat(soundType, -80f);
        }
        else
        {
            AudioManager.Instance.audioMixer.SetFloat(soundType, volume);
        }
    }

    public void VolumeOffButton(bool _isBgm)
    {
        if (_isBgm)
        {
            BGM_Slider.value = minSound;
            PlayerPrefs.SetFloat("BGMVolume", -80f);
            AudioManager.Instance.audioMixer.SetFloat("BGM", -80f);
        }
        else
        {
            SFX_Slider.value = minSound;
            PlayerPrefs.SetFloat("SFXVolume", -80f);
            AudioManager.Instance.audioMixer.SetFloat("SFX", -80f);
        }
        PlayerPrefs.Save();
    }

    public void VolumeOnButton(bool _isBgm)
    {
        if (_isBgm)
        {
            BGM_Slider.value = maxSound;
            PlayerPrefs.SetFloat("BGMVolume", maxSound);
            AudioManager.Instance.audioMixer.SetFloat("BGM", maxSound);
        }
        else
        {
            SFX_Slider.value = maxSound;
            PlayerPrefs.SetFloat("SFXVolume", maxSound);
            AudioManager.Instance.audioMixer.SetFloat("SFX", maxSound);
        }
        PlayerPrefs.Save();
    }
    /*public void SetVolume(float volume, string soundtype)
    {
        switch (soundtype)
        {
            case "BGM":
                volume = BGM_Slider.value;
                PlayerPrefs.SetFloat("BGMVolume", volume);
                break;
            case "SFX":
                volume = SFX_Slider.value;
                PlayerPrefs.SetFloat("SFXVolume", volume);
                break;
        }
        PlayerPrefs.Save();

        if (volume.Equals(minSound))
        {
            AudioManager.Instance.audioMixer.SetFloat(soundtype, -80f);
        }
        else
        {
            AudioManager.Instance.audioMixer.SetFloat(soundtype, volume);
        }
    }*/
}
