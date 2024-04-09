using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public enum MusicOption
{
    BGM = 0,
    SFX,
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;


    [Header("AudioMixer")]
    public AudioMixer AudioMixer;

    [Header("0 : 배경 / 1 : 효과음")]
    public AudioSource[] AudioSourceArray;
    [SerializeField] private List<AudioClip> bgmClipList;
    
    private List<List<AudioClip>> sfxClipList = new List<List<AudioClip>>();
    [SerializeField] private List<AudioClip> pushSfxClipList;
    [SerializeField] private List<AudioClip> speedSfxClipList;
    [SerializeField] private List<AudioClip> memorySfxClipList;
    [SerializeField] private List<AudioClip> multiSfxClipList;
    [SerializeField] private List<AudioClip> talkSfxClipList;
    [SerializeField] private List<AudioClip> commonSfxClipList;

    public float startVolume;
    public float fadeTime = 5000f;

    public bool bBgmChanging = false;
    public int playingBgm = 0;
    private Coroutine stopMusic = null;

    int bgmIndex = (int)MusicOption.BGM;
    int sfxIndex = (int)MusicOption.SFX;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        Init();
        SetAudioClip_BGM(0);
    }

    private void Init()
    {
        playingBgm = 0;
        AudioSourceArray[1].clip = bgmClipList[0];

        sfxClipList.Add(pushSfxClipList);
        sfxClipList.Add(speedSfxClipList);
        sfxClipList.Add(memorySfxClipList);
        sfxClipList.Add(multiSfxClipList);
        //sfxClipList.Add(talkSfxClip_List);

        // 시작 시 Audio Setting
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            AudioMixer.SetFloat("BGM", PlayerPrefs.GetFloat("BGMVolume"));
        }
        else
        {
            AudioMixer.SetFloat("BGM", -12f);
            PlayerPrefs.SetFloat("BGMVolume", -12f);
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            AudioMixer.SetFloat("SFX", PlayerPrefs.GetFloat("SFXVolume"));
        }
        else
        {
            AudioMixer.SetFloat("SFX", -12f);
            PlayerPrefs.SetFloat("SFXVolume", -12f);
        }
    }

    public void SetAudioClip_BGM(int index)
    {
        if (bBgmChanging)
        {
            StopCoroutine(stopMusic);

            playingBgm = index;
            stopMusic = StartCoroutine(BGM_Fade(playingBgm));
        }
        else
        {
            //bgm 코루틴이 실행중이 아니면
            playingBgm = index;
            stopMusic = StartCoroutine(BGM_Fade(playingBgm));
        }
    }

    public void SetAudioClip_SFX(int index, bool bLoop)
    {
        if (!bLoop)
        {
            AudioSourceArray[sfxIndex].PlayOneShot(sfxClipList[(int)GameManager.Instance.GameMode][index]);
        }
        else
        {
            AudioSourceArray[sfxIndex].clip = sfxClipList[(int)GameManager.Instance.GameMode][index];
            AudioSourceArray[sfxIndex].Play();
            AudioSourceArray[sfxIndex].loop = bLoop;
        }
    }


    public void SetTalkTalkAudioClic_SFX(int index, bool bLoop)
    {
        if (!bLoop)
        {
            AudioSourceArray[sfxIndex].PlayOneShot(talkSfxClipList[index]);
        }
        else
        {
            AudioSourceArray[sfxIndex].clip = talkSfxClipList[index];
            AudioSourceArray[sfxIndex].Play();
            AudioSourceArray[sfxIndex].loop = bLoop;
        }
    }

    public void SetCommonAudioClip_SFX(int index)
    {
        AudioSourceArray[sfxIndex].PlayOneShot(commonSfxClipList[index]);
    }

    public void Stop_SFX()
    {
        AudioSourceArray[sfxIndex].Stop();
    }

    public void Pause_SFX(bool pause)
    {
        if (pause)
            AudioSourceArray[sfxIndex].Pause();
        else
            AudioSourceArray[sfxIndex].UnPause();
    }

    public IEnumerator BGM_Fade(int index)
    {
        bBgmChanging = true;
        while (true)
        {
            if(AudioSourceArray[bgmIndex].volume <= 0.5f)
            {
                AudioSourceArray[bgmIndex].Stop();
                AudioSourceArray[bgmIndex].volume = 0.5f;

                AudioSourceArray[bgmIndex].clip = bgmClipList[index];
                AudioSourceArray[bgmIndex].Play();
                AudioSourceArray[bgmIndex].loop = true;
                break;
            }

            AudioSourceArray[bgmIndex].volume -= (Time.deltaTime / fadeTime);
            yield return null;
        }
        
        while (true)
        {
            if(AudioSourceArray[bgmIndex].volume >= 1f)
            {
                AudioSourceArray[bgmIndex].volume = 1;
                bBgmChanging = false;
                break;
            }

            AudioSourceArray[bgmIndex].volume += Time.deltaTime / fadeTime;
            yield return null;
        }

        yield break;
    }
}
