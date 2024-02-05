using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("Slider")]
    public Slider Master_Slider;
    public Slider BGM_Slider;
    public Slider SFX_Slider;

    [Header("Button")]
    [SerializeField] private Button Back_Btn;

    #region Unity Callback
    private void Start()
    {
        Init();
    }
    #endregion

    #region Other Method
    private void Init()
    {
        //���� �ּҰ�
        Master_Slider.minValue = -40f;
        BGM_Slider.minValue = -40f;
        SFX_Slider.minValue = -40f;

        //���� �ִ밪 ����
        Master_Slider.maxValue = 10f;
        BGM_Slider.maxValue = 10f;
        SFX_Slider.maxValue = 10f;

        //���� ������ �߰����� ����
        Master_Slider.value = (Master_Slider.minValue + Master_Slider.maxValue) * 0.5f;
        BGM_Slider.value = (BGM_Slider.minValue + BGM_Slider.maxValue) * 0.5f;
        SFX_Slider.value = (SFX_Slider.minValue + SFX_Slider.maxValue) * 0.5f;

        //����� �ͼ� �⺻ ���� ����
        audioMixer.SetFloat("Master", Master_Slider.value);
        audioMixer.SetFloat("BGM", BGM_Slider.value);
        audioMixer.SetFloat("SFX", SFX_Slider.value);


        //������ ���� �� AddListener �߰�
        Master_Slider.onValueChanged.AddListener(delegate { SetVolume(Master_Slider.value, "Master"); });
        BGM_Slider.onValueChanged.AddListener(delegate { SetVolume(BGM_Slider.value, "BGM"); });
        SFX_Slider.onValueChanged.AddListener(delegate { SetVolume(SFX_Slider.value, "SFX"); });
    }

    public void SetVolume(float volume, string soundtype)
    {
        //���� ���� �Լ� ->���� AudioManager�� �־��ּ���

        switch (soundtype)
        {
            case "Master":
                volume = Master_Slider.value;
                break;


            case "BGM":
                volume = BGM_Slider.value;
                break;


            case "SFX":
                volume = SFX_Slider.value;
                break;
        }

        if (volume == -40f)
        {
            audioMixer.SetFloat(soundtype, -80f);
        }
        else
        {
            audioMixer.SetFloat(soundtype, volume);
        }
    }
    #endregion
}
