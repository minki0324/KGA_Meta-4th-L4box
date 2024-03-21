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
    public AudioMixer audioMixer;

    [Header("0 : 배경 / 1 : 효과음")]
    public AudioSource[] audioSource_arr;
    [SerializeField] private List<AudioClip> bgmClip_List;
    
    private List<List<AudioClip>> sfxClipList = new List<List<AudioClip>>();
    [SerializeField] private List<AudioClip> pushSfxClip_List;
    [SerializeField] private List<AudioClip> speedSfxClip_List;
    [SerializeField] private List<AudioClip> memorySfxClip_List;
    [SerializeField] private List<AudioClip> multiSfxClip_List;
    [SerializeField] private List<AudioClip> talkSfxClip_List;
    [SerializeField] private List<AudioClip> commonSfxClip_List;

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
        audioSource_arr[1].clip = bgmClip_List[0];

        sfxClipList.Add(pushSfxClip_List);
        sfxClipList.Add(speedSfxClip_List);
        sfxClipList.Add(memorySfxClip_List);
        sfxClipList.Add(multiSfxClip_List);
        sfxClipList.Add(talkSfxClip_List);

        // 시작 시 Audio Setting
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            Debug.Log(PlayerPrefs.GetFloat("BGMVolume"));
            audioMixer.SetFloat("BGM", PlayerPrefs.GetFloat("BGMVolume"));
        }
        else
        {
            audioMixer.SetFloat("BGM", -12f);
            PlayerPrefs.SetFloat("BGMVolume", -12f);
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            audioMixer.SetFloat("SFX", PlayerPrefs.GetFloat("SFXVolume"));
        }
        else
        {
            audioMixer.SetFloat("SFX", -12f);
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
            audioSource_arr[sfxIndex].PlayOneShot(sfxClipList[(int)GameManager.Instance.GameMode][index]);
            /*switch (GameManager.Instance.GameMode)
            {
                case GameMode.PushPush:
                    audioSource_arr[sfxIndex].PlayOneShot(pushSfxClip_List[index]);
                    break;
                case GameMode.Speed:
                    audioSource_arr[sfxIndex].PlayOneShot(speedSfxClip_List[index]);
                    break;
                case GameMode.Memory:
                    audioSource_arr[sfxIndex].PlayOneShot(memorySfxClip_List[index]);
                    break;
                case GameMode.Multi:
                    audioSource_arr[sfxIndex].PlayOneShot(multiSfxClip_List[index]);
                    break;
            }*/
        }
        else
        {
            audioSource_arr[sfxIndex].PlayOneShot(sfxClipList[(int)GameManager.Instance.GameMode][index]);
            audioSource_arr[sfxIndex].Play();
            audioSource_arr[sfxIndex].loop = bLoop;
            /*switch (GameManager.Instance.GameMode)
            {
                case GameMode.PushPush:
                    audioSource_arr[sfxIndex].clip = pushSfxClip_List[index];
                    audioSource_arr[sfxIndex].Play();
                    audioSource_arr[sfxIndex].loop = bLoop;
                    break;
                case GameMode.Speed:
                    audioSource_arr[sfxIndex].clip = speedSfxClip_List[index];
                    audioSource_arr[sfxIndex].Play();
                    audioSource_arr[sfxIndex].loop = bLoop;
                    break;
                case GameMode.Memory:
                    audioSource_arr[sfxIndex].clip = memorySfxClip_List[index];
                    audioSource_arr[sfxIndex].Play();
                    audioSource_arr[sfxIndex].loop = bLoop;
                    break;
                case GameMode.Multi:
                    audioSource_arr[sfxIndex].clip = multiSfxClip_List[index];
                    audioSource_arr[sfxIndex].Play();
                    audioSource_arr[sfxIndex].loop = bLoop;
                    break;
            }*/
        }
    }


    /*public void SetTalkTalkAudioClic_SFX(int index, bool bLoop)
    {
        if (!bLoop)
        {
            audioSource_arr[sfxIndex].PlayOneShot(talkSfxClip_List[index]);
        }
        else
        {
            audioSource_arr[sfxIndex].clip = talkSfxClip_List[index];
            audioSource_arr[sfxIndex].Play();
            audioSource_arr[sfxIndex].loop = bLoop;
        }

    }*/

    public void SetCommonAudioClip_SFX(int index)
    {
        audioSource_arr[sfxIndex].PlayOneShot(commonSfxClip_List[index]);
    }

    public void Stop_SFX()
    {
        audioSource_arr[sfxIndex].Stop();
    }

    public void Pause_SFX(bool pause)
    {
        if (pause)
            audioSource_arr[sfxIndex].Pause();
        else
            audioSource_arr[sfxIndex].UnPause();
    }

    public IEnumerator BGM_Fade(int index)
    {
        bBgmChanging = true;
        while (true)
        {
            if(audioSource_arr[bgmIndex].volume <= 0.5f)
            {
                audioSource_arr[bgmIndex].Stop();
                audioSource_arr[bgmIndex].volume = 0.5f;

                audioSource_arr[bgmIndex].clip = bgmClip_List[index];
                audioSource_arr[bgmIndex].Play();
                audioSource_arr[bgmIndex].loop = true;
                break;
            }

            audioSource_arr[bgmIndex].volume -= (Time.deltaTime / fadeTime);
            yield return null;
        }
        
        while (true)
        {
            if(audioSource_arr[bgmIndex].volume >= 1f)
            {
                audioSource_arr[bgmIndex].volume = 1;
                bBgmChanging = false;
                break;
            }

            audioSource_arr[bgmIndex].volume += Time.deltaTime / fadeTime;
            yield return null;
        }

        yield break;
    }
}
