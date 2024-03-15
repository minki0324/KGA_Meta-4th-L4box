using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{

    [Header("Slider")]
    public Slider Master_Slider;
    public Slider BGM_Slider;
    public Slider SFX_Slider;

    [Header("Button")]
    [SerializeField] private Button Back_Btn;

    //소리 음량 최대최소
    float minSound = -20f;
    float maxSound = -5f;


    #region Unity Callback

    private void Awake()
    {
   
    }


    private void Start()
    {
        Init();
        gameObject.SetActive(false);
        //PlayerPrefs.DeleteAll();

    }
    #endregion

    #region Other Method
    private void Init()
    {
        //볼륨 최소값
        Master_Slider.minValue = minSound;
        BGM_Slider.minValue = minSound;
        SFX_Slider.minValue = minSound;

        //볼륨 최대값 조절
        Master_Slider.maxValue = maxSound;
        BGM_Slider.maxValue = maxSound;
        SFX_Slider.maxValue = maxSound;

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
        AudioManager.Instance.audioMixer.SetFloat("Master", Master_Slider.value);
        AudioManager.Instance.audioMixer.SetFloat("BGM", BGM_Slider.value);
        AudioManager.Instance.audioMixer.SetFloat("SFX", SFX_Slider.value);


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
                PlayerPrefs.DeleteKey("MasterVolume");
                PlayerPrefs.SetFloat("MasterVolume", volume);
                break;


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

        if (volume == minSound)
        {
            AudioManager.Instance.audioMixer.SetFloat(soundtype, -80f);
        }
        else
        {
            AudioManager.Instance.audioMixer.SetFloat(soundtype, volume);
        }
    }
    #endregion
}
