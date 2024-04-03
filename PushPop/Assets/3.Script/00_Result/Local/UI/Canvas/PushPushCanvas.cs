using TMPro;
using UnityEngine;

public class PushPushCanvas : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private MainCanvas mainCanvas;

    [Header("Side Panel")]
    public GameObject BackButton;
    public GameObject HelpButton;

    [Header("PushPush Game")]
    [SerializeField] private PushPushManager pushpushManager;
    [SerializeField] private SelectListSetting selectListSetting;
    [SerializeField] private GameObject pushpushGame;

    [Header("Select Panel")]
    [SerializeField] private GameObject selectBoardPanel;
    public GameObject SelectCategoryPanel;

    [Header("Panel")]
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private GameObject helpPanel;
    public GameObject GameReadyPanel;
    public TMP_Text GameReadyPanelText;

    private void OnDisable()
    {
        ShutdownInit();
    }

    private void ShutdownInit()
    { // shutdown �� �ʱ�ȭ�Ǵ� ���
        if (!GameManager.Instance.IsShutdown) return;
        LoadingPanel.Instance.gameObject.SetActive(true);
        selectBoardPanel.SetActive(false);
        helpPanel.SetActive(false);
    }
    public void GameStartButton()
    { // ���� ���� ��ư
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
    { // �ڷΰ��� ��ư
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        if (SelectCategoryPanel.activeSelf)
        { // ī�װ� ���� ���� ��
            AudioManager.Instance.SetAudioClip_BGM(0);
            LoadingPanel.Instance.gameObject.SetActive(true);
            GameManager.Instance.GameMode = GameMode.Lobby;
            GameManager.Instance.InGame = false;
            GameManager.Instance.ShutdownCoroutineStop();

            mainCanvas.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        else if (pushpushGame.activeSelf)
        { // ���� ���� ��
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
    { // ����
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        helpPanel.SetActive(true);
    }

    public void HelpPanelBackButton()
    {
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        helpPanel.SetActive(false);
    }
}
