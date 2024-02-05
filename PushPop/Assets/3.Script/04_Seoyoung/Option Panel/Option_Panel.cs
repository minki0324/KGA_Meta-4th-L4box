using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Option_Panel : MonoBehaviour
{

    public static Option_Panel instance = null;

    [Header("오디오 믹서")]
    public AudioMixer audioMixer;


    [Header("슬라이더")]
    public Slider Master_Slider;
 
    public Slider BGM_Slider;

    public Slider SFX_Slider;

    [Header("버튼")]
    [SerializeField]
    private Button TimeSet_Btn;

    [SerializeField]
    private Button Back_Btn;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Init();
        gameObject.SetActive(false);
    }

    private void Init()
    {

        //버튼 초기화
        Back_Btn.onClick.AddListener(() => { gameObject.SetActive(false); });

        //볼륨 최소값
        Master_Slider.minValue = -40f;
        BGM_Slider.minValue = -40f;
        SFX_Slider.minValue = -40f;

        //볼륨 최대값 조절
        Master_Slider.maxValue = 0f;
        BGM_Slider.maxValue = 0f;
        SFX_Slider.maxValue = 0f;

        //시작 볼륨값 중간으로 조정
        Master_Slider.value = (Master_Slider.minValue + Master_Slider.maxValue) * 0.5f;
        BGM_Slider.value = (BGM_Slider.minValue + BGM_Slider.maxValue) * 0.5f;
        SFX_Slider.value = (SFX_Slider.minValue + SFX_Slider.maxValue) * 0.5f;


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

}
