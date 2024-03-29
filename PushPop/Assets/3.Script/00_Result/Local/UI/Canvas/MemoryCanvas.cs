using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MemoryCanvas : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject mainCanvas = null;


    [Header("Side Panel")]
    public GameObject HelpButton = null;

    [Header("Ready")]
    public GameObject Ready = null;

    [Header("Memory Game")]
    public GameObject MemoryGame = null;
    [SerializeField] private MemoryManager memoryManager = null;

    [Header("Panel")]
    public GameObject WarningPanel = null;
    public GameObject GameReadyPanel = null;
    public GameObject HelpPanel = null;

    [Header("Game Ready Panel")]
    public TMP_Text GameReadyPanelText = null;

    [Header("Continue")]
    public Button[] stagebtns;
    public GameObject ContinuePanel = null;

    private void OnDisable()
    {
        ShutdownInit();
    }

    private void ShutdownInit()
    { // shutdown �� �ʱ�ȭ�Ǵ� ���
        if (!GameManager.Instance.IsShutdown) return;
        LoadingPanel.Instance.gameObject.SetActive(true);
        ContinuePanel.SetActive(false);
        HelpPanel.SetActive(false);
    }
    #region Ready
    public void GameStartButton()
    { // ��� - ���� ����
        AudioManager.Instance.SetAudioClip_BGM(4);
        AudioManager.Instance.SetCommonAudioClip_SFX(0);

        GameManager.Instance.IsGameClear = false;

        LoadingPanel.Instance.gameObject.SetActive(true);
        ContinuePanel.SetActive(false);
        MemoryGame.SetActive(true);
        HelpButton.SetActive(false);
        Ready.SetActive(false);
        memoryManager.BackButton.SetActive(true);
        memoryManager.GameStart();
    }

    public void ContinueBtn()
    { // ��� �̵� ��ư
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        ContinuePanel.SetActive(true);
        try
        {
            memoryManager.saveStage = GameManager.Instance.myMeomoryStageInfo.saveStageIndex;
        }
        catch (System.Exception)
        {
            memoryManager.saveStage = 1;
        }
        int activebtnCount = memoryManager.saveStage / 5;
        for (int i = 0; i < activebtnCount; i++)
        {
            stagebtns[i].interactable = true;
        }
    }

    public void ExitContinuePanel()
    { // ��� �̵� ������ ��ư
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        ContinuePanel.SetActive(false);
        //��ư��� disable
        //currentStage = 0
        for (int i = 0; i < stagebtns.Length; i++)
        {
            if (stagebtns[i].interactable)
            {
                stagebtns[i].interactable = false;
            }
        }
        memoryManager.CurrentStage = 1;
    }
    public void SelectContinueStage(int stage)
    { // stage button
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        memoryManager.CurrentStage = stage;
    }
    #endregion
    #region Side Panel
    public void MemoryBackButton()
    { // �ڷΰ���
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        if (Ready.activeSelf)
        { // ��� ȭ���� ��
            AudioManager.Instance.SetAudioClip_BGM(0);
            LoadingPanel.Instance.gameObject.SetActive(true);

            GameManager.Instance.GameMode = GameMode.Lobby;
            GameManager.Instance.InGame = false;
            GameManager.Instance.ShutdownCoroutineStop();

            mainCanvas.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        { // ���� ���� ��
            Time.timeScale = 0f;
            WarningPanel.SetActive(true);
        }
    }

    public void HelpPanelButton()
    { // ����
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        HelpPanel.SetActive(true);
    }

    public void HelpPanelBackButton()
    {
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        HelpPanel.SetActive(false);
    }
    #endregion
}
