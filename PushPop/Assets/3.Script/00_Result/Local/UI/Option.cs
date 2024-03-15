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

    //�Ҹ� ���� �ִ��ּ�
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
        //���� �ּҰ�
        Master_Slider.minValue = minSound;
        BGM_Slider.minValue = minSound;
        SFX_Slider.minValue = minSound;

        //���� �ִ밪 ����
        Master_Slider.maxValue = maxSound;
        BGM_Slider.maxValue = maxSound;
        SFX_Slider.maxValue = maxSound;

        //���� ������ �߰����� ����
        #region ������ �ҷ�����
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


        //����� �ͼ� �⺻ ���� ����
        AudioManager.Instance.audioMixer.SetFloat("Master", Master_Slider.value);
        AudioManager.Instance.audioMixer.SetFloat("BGM", BGM_Slider.value);
        AudioManager.Instance.audioMixer.SetFloat("SFX", SFX_Slider.value);


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
