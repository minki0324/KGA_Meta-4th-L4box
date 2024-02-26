using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;


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

    public float startVolume;
    public float fadeTime = 50f;

    #region Unity Callback
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
            return;
        }
    }

    private void Start()
    {
        audioSource_arr = GetComponents<AudioSource>();
        audioSource_arr[2].clip = bgmClip_List[0];
    }

    #endregion


    public void SetAudioClip_BGM(int index)
    {
        StartCoroutine(BGM_Fade(index));

        //audioSource_arr[1].clip = bgmClip_List[index];
        //audioSource_arr[1].Play();
        //audioSource_arr[1].loop = true;
    }

    public void SetAudioClip_SFX(int index, bool bLoop)
    {
        switch (GameManager.Instance.gameMode)
        {
            case Mode.PushPush:

                audioSource_arr[2].clip = pushSfxClip_List[index];
                audioSource_arr[2].Play();
                audioSource_arr[2].loop = bLoop;
                break;

            case Mode.Speed:
                audioSource_arr[2].clip = speedSfxClip_List[index];
                audioSource_arr[2].Play();
                audioSource_arr[2].loop = bLoop;
                break;

            case Mode.Memory:
                audioSource_arr[2].clip = memorySfxClip_List[index];
                audioSource_arr[2].Play();
                audioSource_arr[2].loop = bLoop;
                break;

            case Mode.Bomb:
                audioSource_arr[2].clip = bombSfxClip_List[index];
                audioSource_arr[2].Play();
                audioSource_arr[2].loop = bLoop;
                break;

            case Mode.None:
                audioSource_arr[2].clip = commonSfxClip_List[index];
                audioSource_arr[2].Play();
                audioSource_arr[2].loop = bLoop;
                break;
        }
    }

    public void SetCommonAudioClip_SFX(int index)
    {
        audioSource_arr[2].PlayOneShot(commonSfxClip_List[index]);
    }

    public void Stop_SFX()
    {
        audioSource_arr[2].Stop();
    }

    public void Pause_SFX(bool pause)
    {
        if (pause)
            audioSource_arr[2].Pause();
        else
            audioSource_arr[2].UnPause();

    }

    public IEnumerator BGM_Fade(int index)
    {
        float startVolume = audioSource_arr[1].volume;

        while (audioSource_arr[1].volume > 0)
        {
            audioSource_arr[1].volume -= startVolume + Time.deltaTime / fadeTime;

            yield return null;
        }
        audioSource_arr[1].Stop();
        audioSource_arr[1].volume = 0;

        yield return null;

        audioSource_arr[1].clip = bgmClip_List[index];
        audioSource_arr[1].Play();
        audioSource_arr[1].loop = true;

        while (audioSource_arr[1].volume >= startVolume)
        {
            audioSource_arr[1].volume += Time.deltaTime / fadeTime;

            yield return null;
        }
    }

}
