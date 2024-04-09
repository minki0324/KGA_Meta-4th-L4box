using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private ProfileCanvas profileCanvas;
    [SerializeField] private PushPushCanvas pushpushCanvas;
    [SerializeField] private SpeedCanvas speedCanvas;
    [SerializeField] private MemoryCanvas memoryCanvas;
    [SerializeField] private MultiCanvas multiCanvas;
    [SerializeField] private PracticeCanvas practiveCanvas;

    public bool isChangeProfile = false;

    [Header("Side Panel")]
    public GameObject TitleText;
    public GameObject OptionButton;
    public GameObject ProfileButton;

    [Header("Profile")]
    public Image CaptureImage;

    [Header("GameMode Panel")]
    public GameObject PushpushButton;
    public GameObject SpeedButton;
    public GameObject MemoryButton;
    public GameObject MultiButton;
    public GameObject NetworkButton;
    public GameObject PracticeButton;

    [Header("Panel")]
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private GameObject shutdownPanel;
    [SerializeField] private GameObject timeSettingPanel;
    [SerializeField] private GameObject optionPanel;

    [Header("TimeSetting Panel")]
    [SerializeField] private TMP_Text timeText;

    private void OnEnable()
    {
        StartCoroutine(Init());
    }
    private void Start()
    {
        GameManager.Instance.Shutdown += ShutDownPanelSetting;
    }

    private void OnDestroy()
    {
        GameManager.Instance.Shutdown -= ShutDownPanelSetting;
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

        timeText.text = $"{string.Format("{0:0}", GameManager.Instance.ShutdownTimer)}��";
        timeSettingPanel.SetActive(true);
    }

    public void ProfileImageButton()
    { // Profile �̹��� Ŭ�� �� ������ �������� ���ư�
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.PrintProfileList(profileCanvas.SelectScrollViewContent);
        isChangeProfile = true;

        profileCanvas.BlockPanel.SetActive(true);
        profileCanvas.BackButton.SetActive(true);
        profileCanvas.gameObject.SetActive(true);
    }

    public void MusicOptionButton()
    { // Music Option ��ư, �Ҹ� ����
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        optionPanel.SetActive(true);
    }
    #endregion
    #region TimeSetting Panel
    public void TimeSelectButton(int _time)
    { // ���� �ð� ���� - ���� �ð�
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        GameManager.Instance.ShutdownTimer = _time;
        timeText.text = $"{string.Format("{0:0}", _time)}��";
    }

    public void TimeSettingButton(bool _isPlus)
    { // ���� �ð� ���� - ���� �ð�
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

        timeText.text = $"{string.Format("{0:0}", GameManager.Instance.ShutdownTimer)}��";
    }

    public void TimeSettingEndButton()
    { // ���� �ð� ���� - Ȯ��
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
    { // ���� �ð� ���� - ������
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
    { // �ɼ� - ���� ����
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void MusicOptionBackButton()
    { // �ɼ� - â �ݱ�
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        optionPanel.SetActive(false);
    }
    #endregion
    #region Practice mode
    public void PracticeModeButton()
    { // ���� ��� ��ư
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        warningPanel.SetActive(true);
    }

    public void PracticeOkButton()
    { // ���� ��� - Ȯ��
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        GameManager.Instance.GameMode = GameMode.Speed; // practice�� ���� ���
        LoadingPanel.Instance.gameObject.SetActive(true);
        practiveCanvas.gameObject.SetActive(true);
        warningPanel.SetActive(false);
        gameObject.SetActive(false);
    }

    public void PracticeCancelButton()
    { // ���� ��� - ������
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        warningPanel.SetActive(false);
    }
    #endregion
}
