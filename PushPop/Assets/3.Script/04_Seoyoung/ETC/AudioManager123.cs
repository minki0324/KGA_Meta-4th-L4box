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
    private List<AudioClip> pushSfxClip_List;

    [SerializeField]
    private List<AudioClip> speedSfxClip_List;

    [SerializeField]
    private List<AudioClip> memorySfxClip_List;

    [SerializeField]
    private List<AudioClip> bombSfxClip_List;

    [SerializeField]
    private List<AudioClip> commonSfxClip_List;


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

    public void SetAudioClip_SFX(Mode gameMode, int index)
    {     
        switch(gameMode)
        {
            case Mode.PushPush:
                audioSource_arr[2].PlayOneShot(pushSfxClip_List[index]);
                break;

            case Mode.Speed:
                audioSource_arr[2].PlayOneShot(speedSfxClip_List[index]);
                break;

            case Mode.Memory:
                audioSource_arr[2].PlayOneShot(memorySfxClip_List[index]);
                break;

            case Mode.Bomb:
                audioSource_arr[2].PlayOneShot(bombSfxClip_List[index]);
                break;
        }
    }

    public void SetCommonAudioClip_SFX(int index)
    {
        audioSource_arr[2].PlayOneShot(commonSfxClip_List[index]);
    }
}
