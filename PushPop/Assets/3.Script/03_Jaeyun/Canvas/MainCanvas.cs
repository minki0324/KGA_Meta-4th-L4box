using System.Collections;
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

    public bool isChangeProfile = false;

    [Header("Side Panel")]
    public GameObject TitleText = null;
    public GameObject OptionButton = null;
    public GameObject ProfileButton = null;

    [Header("Profile")]
    public Image CaptureImage = null;

    [Header("GameMode Panel")]
    public GameObject PushpushButton = null;
    public GameObject SpeedButton = null;
    public GameObject MemoryButton = null;
    public GameObject MultiButton = null;
    public GameObject NetworkButton = null;

    [Header("Panel")]
    [SerializeField] private GameObject shutdownPanel = null;
    [SerializeField] private GameObject timeSettingPanel = null;
    [SerializeField] private GameObject optionPanel = null;

    [Header("TimeSetting Panel")]
    [SerializeField] private TMP_Text timeText = null;

    private void OnEnable()
    {
        StartCoroutine(Init());
    }
    private void Start()
    {
        GameManager.Instance.Shutdown += ShutDownPanelSetting;
    }

    private IEnumerator Init()
    {
        yield return null;
        if (GameManager.Instance.GameMode.Equals(GameMode.Lobby))
        {
            CaptureImage.sprite = ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileImage;
        }
    }
    #region Button Click Method
    public void GameModeButton(int _gameMode)
    { // Time Set Start
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        GameManager.Instance.GameMode = (GameMode)_gameMode;
        GameManager.Instance.ShutdownTimer = 3f;

        timeText.text = $"{string.Format("{0:0}", GameManager.Instance.ShutdownTimer)}분";
        timeSettingPanel.SetActive(true);
    }

    public void ProfileImageButton()
    { // Profile 이미지 클릭 시 프로필 선택으로 돌아감
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.PrintProfileList(profileCanvas.SelectScrollViewContent);
        isChangeProfile = true;

        profileCanvas.BlockPanel.SetActive(true);
        profileCanvas.BackButton.SetActive(true);
        profileCanvas.gameObject.SetActive(true);
    }

    public void MusicOptionButton()
    { // Music Option 버튼, 소리 조절
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        optionPanel.SetActive(true);
    }
    #endregion
    #region TimeSetting Panel
    public void TimeSelectButton(int _time)
    { // 게임 시간 조절 - 지정 시간
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        GameManager.Instance.ShutdownTimer = _time;
        timeText.text = $"{string.Format("{0:0}", _time)}분";
    }

    public void TimeSettingButton(bool _isPlus)
    { // 게임 시간 조절 - 조절 시간
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

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
        AudioManager.Instance.SetAudioClip_BGM(1);
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        LoadingPanel.Instance.gameObject.SetActive(true);

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

        GameManager.Instance.ShutdownTimer *= 60f;
        GameManager.Instance.InGame = true;
        GameManager.Instance.IsGameClear = true;
        GameManager.Instance.ShutdownAlarmStart();

        timeSettingPanel.SetActive(false);
        gameObject.SetActive(false);
    }

    public void TimeSettingBackButton()
    { // 게임 시간 조절 - 나가기
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        GameManager.Instance.GameMode = GameMode.Lobby;
        timeSettingPanel.SetActive(false);
    }

    private void ShutDownPanelSetting()
    {
        pushpushCanvas.gameObject.SetActive(false);
        speedCanvas.gameObject.SetActive(false);
        memoryCanvas.gameObject.SetActive(false);
        multiCanvas.gameObject.SetActive(false);

        shutdownPanel.SetActive(true);
        gameObject.SetActive(true);
    }
    #endregion
    #region Music Option Setting Button
    public void GameQuitButton()
    { // 옵션 - 게임 종료
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void MusicOptionBackButton()
    { // 옵션 - 창 닫기
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        optionPanel.SetActive(false);
    }
    #endregion
}
