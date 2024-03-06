using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private ProfileCanvas profileCanvas = null;
    [SerializeField] private PushPushCanvas pushpushCanvas = null;
    [SerializeField] private SpeedCanvas speedCanvas = null;
    [SerializeField] private MemoryCanvas memoryCanvas = null;
    [SerializeField] private MultiCanvas multiCanvas = null;

    [Header("Side Panel")]
    public GameObject TitleText = null;
    public GameObject OptionButton = null;
    public GameObject ProfileButton = null;
    public GameObject HomeButton = null;

    [Header("Profile")]
    public Image CaptureImage = null;

    [Header("GameMode Panel")]
    public GameObject PushpushButton = null;
    public GameObject SpeedButton = null;
    public GameObject MemoryButton = null;
    public GameObject MultiButton = null;
    public GameObject NetworkButton = null;

    [Header("Panel")]
    [SerializeField] private GameObject timeSettingPanel = null;
    [SerializeField] private GameObject optionPanel = null;

    [Header("TimeSetting Panel")]
    [SerializeField] private TMP_Text timeText = null;

    #region Button Click Method
    public void GameModeButton(int _gameMode)
    { // Time Set Start
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        GameManager.Instance.GameMode = (GameMode)_gameMode;
        GameManager.Instance.ShutdownTimer = 3f;

        timeText.text = $"{string.Format("{0:0}", GameManager.Instance.ShutdownTimer)}분";
        timeSettingPanel.SetActive(true);
    }

    public void ProfileImageButton()
    { // Profile 이미지 클릭 시 프로필 선택으로 돌아감
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        profileCanvas.gameObject.SetActive(true);
        profileCanvas.ExitButton.SetActive(true);

        TitleText.SetActive(false);
        OptionButton.SetActive(false);
        ProfileButton.SetActive(false);
        HomeButton.SetActive(false);
        PushpushButton.SetActive(false);
        SpeedButton.SetActive(false);
        MemoryButton.SetActive(false);
        MultiButton.SetActive(false);
        NetworkButton.SetActive(false);
    }

    public void MusicOptionButton()
    { // Music Option 버튼, 소리 조절
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        optionPanel.SetActive(true);
    }

    public void HomeSiteButton()
    { // 좌측 하단 버튼, 사이트로 이동
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        Application.OpenURL("https://www.l4box.com/");
    }
    #endregion
    #region TimeSetting Panel
    public void TimeSelectButton(int _time)
    { // 게임 시간 조절 - 지정 시간
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        GameManager.Instance.ShutdownTimer = _time;
        timeText.text = $"{string.Format("{0:0}", _time)}분";
    }

    public void TimeSettingButton(bool _isPlus)
    { // 게임 시간 조절 - 조절 시간
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        if (_isPlus)
        { // Increase button
            if (GameManager.Instance.ShutdownTimer >= 15f) return;
            GameManager.Instance.ShutdownTimer += 1f;
        }
        else
        { // Decrease button
            if (GameManager.Instance.ShutdownTimer <= 1f) return;
            GameManager.Instance.ShutdownTimer -= 1f;
        }

        timeText.text = $"{string.Format("{0:0}", GameManager.Instance.ShutdownTimer)}분";
    }

    public void TimeSettingEndButton()
    { // 게임 시간 조절 - 확인
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        AudioManager.instance.SetAudioClip_BGM(1);

        GameManager.Instance.ShutdownTimerStart();
        switch (GameManager.Instance.GameMode)
        {
            case GameMode.PushPush:
                pushpushCanvas.gameObject.SetActive(true);
                break;
            case GameMode.Speed:
                speedCanvas.gameObject.SetActive(true);
                break;
            case GameMode.Memory:
                memoryCanvas.gameObject.SetActive(true);
                break;
            case GameMode.Multi:
                multiCanvas.gameObject.SetActive(true);
                break;
        }

        timeSettingPanel.SetActive(false);
        gameObject.SetActive(false);
    }

    public void TimeSettingBackButton()
    { // 게임 시간 조절 - 나가기
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        GameManager.Instance.GameMode = GameMode.None;
        timeSettingPanel.SetActive(false);
    }
    #endregion
    #region Music Option Setting Button
    public void GameQuitButton()
    { // 옵션 - 게임 종료
        AudioManager.instance.SetCommonAudioClip_SFX(3);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    #endregion
}
