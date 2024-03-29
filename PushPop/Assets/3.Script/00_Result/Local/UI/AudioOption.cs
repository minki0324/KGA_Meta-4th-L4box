using UnityEngine;
using UnityEngine.UI;

public class AudioOption : MonoBehaviour
{
    [Header("Slider")]
    [SerializeField] private Slider bgm_Slider; //옵션창의 BGM슬라이더
    [SerializeField] private Slider sfx_Slider; //옵션창의 SFX슬라이더
    private float minSound = -20f;  //AudioMixer의 최소 음량
    private float maxSound = -5f;   //AudioMixer의 최대 음량

    private void Start()
    {
        Init();
    }

    private void Init()
    {//값 초기화

        //볼륨 최소값
        bgm_Slider.minValue = minSound;
        sfx_Slider.minValue = minSound;

        //볼륨 최대값 조절
        bgm_Slider.maxValue = maxSound;
        sfx_Slider.maxValue = maxSound;

        bgm_Slider.onValueChanged.AddListener(delegate { SetVolume(true); });
        sfx_Slider.onValueChanged.AddListener(delegate { SetVolume(false); });

        //시작 볼륨값 중간으로 조정
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            bgm_Slider.value = PlayerPrefs.GetFloat("BGMVolume");
        }
        else
        {
            bgm_Slider.value = -15f;
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            sfx_Slider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
        else
        {
            sfx_Slider.value = -15f;
        }
    }

    public void SetVolume(bool _isBgm)
    {//Slider들의 onValueChange에 호출될 함수,
     //음량 조절 함수

        float volume = 0f;
        string soundType = "BGM";
        if (_isBgm)
        {
            volume = bgm_Slider.value;
            soundType = "BGM";
            PlayerPrefs.SetFloat("BGMVolume", volume);
        }
        else
        {
            volume = sfx_Slider.value;
            soundType = "SFX";
            PlayerPrefs.SetFloat("SFXVolume", volume);
        }
        PlayerPrefs.Save();

        if (volume.Equals(minSound))
        {
            AudioManager.Instance.AudioMixer.SetFloat(soundType, -80f);
        }
        else
        {
            AudioManager.Instance.AudioMixer.SetFloat(soundType, volume);
        }
    }

    public void VolumeOffButton(bool _isBgm)
    {//볼륨 끄는 버튼 함수
        if (_isBgm)
        {
            bgm_Slider.value = minSound;
            PlayerPrefs.SetFloat("BGMVolume", -80f);
            AudioManager.Instance.AudioMixer.SetFloat("BGM", -80f);
        }
        else
        {
            sfx_Slider.value = minSound;
            PlayerPrefs.SetFloat("SFXVolume", -80f);
            AudioManager.Instance.AudioMixer.SetFloat("SFX", -80f);
        }
        PlayerPrefs.Save();
    }

    public void VolumeOnButton(bool _isBgm)
    {//볼륨 켜는 버튼 함수
        if (_isBgm)
        {
            bgm_Slider.value = maxSound;
            PlayerPrefs.SetFloat("BGMVolume", maxSound);
            AudioManager.Instance.AudioMixer.SetFloat("BGM", maxSound);
        }
        else
        {
            sfx_Slider.value = maxSound;
            PlayerPrefs.SetFloat("SFXVolume", maxSound);
            AudioManager.Instance.AudioMixer.SetFloat("SFX", maxSound);
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
