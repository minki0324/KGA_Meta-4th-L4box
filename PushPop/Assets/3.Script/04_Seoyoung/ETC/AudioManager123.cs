using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class AudioManager123 : MonoBehaviour
{
    public static AudioManager123 instance = null;


    [Header("AudioMixer")]
    [SerializeField]
    public AudioMixer audioMixer;

    [Header("0 : 전체 / 1 : 배경 / 2 : 효과음")]
    [SerializeField]
    private AudioSource[] audioSource_arr;

    [SerializeField]
    private List<AudioClip> bgmClip_List;

    [SerializeField]
    private List<AudioClip> sfxClip_List;

    [SerializeField]
    private List<Button> btn_List;

    #region Unity Callback
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }    
    }

    private void Start()
    {
        audioSource_arr = GetComponents<AudioSource>();
        SetAudioClip_BGM(0);
    }

    #endregion


    public void SetAudioClip_BGM(int index)
    {
        audioSource_arr[1].clip = bgmClip_List[index];       
        audioSource_arr[1].Play();
        audioSource_arr[1].loop = true;
        
    }

    public void SetAudioClip_SFX(int index)
    {
        //audioSource_arr[2].clip = sfxClip_List[index];
        audioSource_arr[2].PlayOneShot(sfxClip_List[index]);
     
    }
}
