using TMPro;
using UnityEngine;

public class PushPushCanvas : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private MainCanvas mainCanvas = null;

    [Header("Side Panel")]
    public GameObject BackButton = null;
    public GameObject HelpButton = null;

    [Header("PushPush Game")]
    [SerializeField] private PushPushManager pushpushManager = null;
    [SerializeField] private SelectListSetting selectListSetting = null;
    [SerializeField] private GameObject pushpushGame = null;

    [Header("Select Panel")]
    [SerializeField] private GameObject selectBoardPanel = null;
    public GameObject SelectCategoryPanel = null;

    [Header("Panel")]
    [SerializeField] private GameObject warningPanel = null;
    [SerializeField] private GameObject helpPanel = null;
    public GameObject GameReadyPanel = null;
    public TMP_Text GameReadyPanelText = null;

    private void OnDisable()
    {
        ShutdownInit();
    }

    private void ShutdownInit()
    {
        if (!GameManager.Instance.IsShutdown) return;
        LoadingPanel.Instance.gameObject.SetActive(true);
        selectBoardPanel.SetActive(false);
        helpPanel.SetActive(false);
    }
    public void GameStartButton()
    {
        AudioManager.Instance.SetAudioClip_BGM(3);
        AudioManager.Instance.SetCommonAudioClip_SFX(0);
        LoadingPanel.Instance.gameObject.SetActive(true);

        GameManager.Instance.IsGameClear = false;
        SelectCategoryPanel.SetActive(false);
        selectBoardPanel.SetActive(false);
        HelpButton.SetActive(false);
        pushpushGame.SetActive(true);
        pushpushManager.GameStart();
    }

    public void PushPushBackButton()
    { // 뒤로가기 버튼
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        if (SelectCategoryPanel.activeSelf)
        { // 카테고리 선택 중일 때
            AudioManager.Instance.SetAudioClip_BGM(0);
            LoadingPanel.Instance.gameObject.SetActive(true);
            GameManager.Instance.GameMode = GameMode.Lobby;
            GameManager.Instance.InGame = false;
            GameManager.Instance.ShutdownCoroutineStop();

            mainCanvas.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        else if (pushpushGame.activeSelf)
        { // 게임 중일 때
            Time.timeScale = 0f;
            warningPanel.SetActive(true);
        }
    }
    public void ReadyPanelExitButton()
    { // Ready panel
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        selectListSetting.Ready.SetActive(false);
    }

    public void HelpPanelButton()
    { // 도움말
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        helpPanel.SetActive(true);
    }

    public void HelpPanelBackButton()
    {
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        helpPanel.SetActive(false);
    }
}
