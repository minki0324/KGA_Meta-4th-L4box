using System.Collections;
using System.Collections.Generic;
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
        //���� �ּҰ�
        BGM_Slider.minValue = minSound;
        SFX_Slider.minValue = minSound;

        //���� �ִ밪 ����
        BGM_Slider.maxValue = maxSound;
        SFX_Slider.maxValue = maxSound;

        //���� ������ �߰����� ����
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            //Master_Slider.value = PlayerPrefs.GetFloat("BGMVolume");
        }
        else
        {
            BGM_Slider.value = (BGM_Slider.minValue + BGM_Slider.maxValue) * 0.5f;
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            //Master_Slider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
        else
        {
            SFX_Slider.value = (SFX_Slider.minValue + SFX_Slider.maxValue) * 0.5f;
        }

        //����� �ͼ� �⺻ ���� ����
        AudioManager.Instance.audioMixer.SetFloat("BGM", BGM_Slider.value);
        AudioManager.Instance.audioMixer.SetFloat("SFX", SFX_Slider.value);

        //������ ���� �� AddListener �߰�
        BGM_Slider.onValueChanged.AddListener(delegate { SetVolume(BGM_Slider.value, "BGM"); });
        SFX_Slider.onValueChanged.AddListener(delegate { SetVolume(SFX_Slider.value, "SFX"); });
    }

    public void SetVolume()
    {

    }

    public void SetVolume(float volume, string soundtype)
    {
        switch (soundtype)
        {
            case "BGM":
                volume = BGM_Slider.value;
                PlayerPrefs.DeleteKey("BGMVolume");
                PlayerPrefs.SetFloat("BGMVolume", volume);
                break;
            case "SFX":
                volume = SFX_Slider.value;
                PlayerPrefs.DeleteKey("SFXVolume");
                PlayerPrefs.SetFloat("SFXVolume", volume);
                break;
        }

        if (volume.Equals(minSound))
        {
            AudioManager.Instance.audioMixer.SetFloat(soundtype, -80f);
        }
        else
        {
            AudioManager.Instance.audioMixer.SetFloat(soundtype, volume);
        }
    }
}
