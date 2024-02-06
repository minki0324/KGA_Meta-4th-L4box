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

    private void Awake()
    {
       

    }

    private void Start()
    {
        Init();
    }
    #endregion

    #region Other Method
    private void Init()
    {
        //볼륨 최소값
        Master_Slider.minValue = -40f;
        BGM_Slider.minValue = -40f;
        SFX_Slider.minValue = -40f;

        //볼륨 최대값 조절
        Master_Slider.maxValue = 10f;
        BGM_Slider.maxValue = 10f;
        SFX_Slider.maxValue = 10f;

        //시작 볼륨값 중간으로 조정
        #region 볼륨값 불러오기
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            Master_Slider.value = PlayerPrefs.GetFloat("MasterVolume");
        }
        else
        {
            Master_Slider.value = (Master_Slider.minValue + Master_Slider.maxValue) * 0.5f;
        }

        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            Master_Slider.value = PlayerPrefs.GetFloat("BGMVolume");
        }
        else
        {
            BGM_Slider.value = (BGM_Slider.minValue + BGM_Slider.maxValue) * 0.5f;
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            Master_Slider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
        else
        {
            SFX_Slider.value = (SFX_Slider.minValue + SFX_Slider.maxValue) * 0.5f;
        }

        #endregion


        //오디오 믹서 기본 볼륨 조정
        audioMixer.SetFloat("Master", Master_Slider.value);
        audioMixer.SetFloat("BGM", BGM_Slider.value);
        audioMixer.SetFloat("SFX", SFX_Slider.value);


        //볼륨값 변경 시 AddListener 추가
        Master_Slider.onValueChanged.AddListener(delegate { SetVolume(Master_Slider.value, "Master"); });
        BGM_Slider.onValueChanged.AddListener(delegate { SetVolume(BGM_Slider.value, "BGM"); });
        SFX_Slider.onValueChanged.AddListener(delegate { SetVolume(SFX_Slider.value, "SFX"); });
    }

    public void SetVolume(float volume, string soundtype)
    {
        //볼륨 조절 함수 ->추후 AudioManager에 넣어주세요

        switch (soundtype)
        {
            case "Master":
                volume = Master_Slider.value;
                PlayerPrefs.SetFloat("MasterVolume", volume);
                break;


            case "BGM":
                volume = BGM_Slider.value;
                PlayerPrefs.SetFloat("BGMVolume", volume);
                break;


            case "SFX":
                volume = SFX_Slider.value;
                PlayerPrefs.SetFloat("SFXVolume", volume);
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
