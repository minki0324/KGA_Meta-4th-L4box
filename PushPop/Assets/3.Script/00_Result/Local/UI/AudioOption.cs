using UnityEngine;
using UnityEngine.UI;

public class AudioOption : MonoBehaviour
{
    [Header("Slider")]
    [SerializeField] private Slider bgm_Slider; //�ɼ�â�� BGM�����̴�
    [SerializeField] private Slider sfx_Slider; //�ɼ�â�� SFX�����̴�
    private float minSound = -20f;  //AudioMixer�� �ּ� ����
    private float maxSound = -5f;   //AudioMixer�� �ִ� ����

    private void Start()
    {
        Init();
    }

    private void Init()
    {//�� �ʱ�ȭ

        //���� �ּҰ�
        bgm_Slider.minValue = minSound;
        sfx_Slider.minValue = minSound;

        //���� �ִ밪 ����
        bgm_Slider.maxValue = maxSound;
        sfx_Slider.maxValue = maxSound;

        bgm_Slider.onValueChanged.AddListener(delegate { SetVolume(true); });
        sfx_Slider.onValueChanged.AddListener(delegate { SetVolume(false); });

        //���� ������ �߰����� ����
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
    {//Slider���� onValueChange�� ȣ��� �Լ�,
     //���� ���� �Լ�

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
    {//���� ���� ��ư �Լ�
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
    {//���� �Ѵ� ��ư �Լ�
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
