using System.Collections;
using TMPro;
using UnityEngine;

public class MultiCanvas : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private MainCanvas mainCanvas;
    [SerializeField] private ProfileCanvas profileCanvas;

    [Header("Side Panel")]
    public GameObject BackButton;
    public GameObject HelpButton;

    [Header("Ready")]
    public GameObject Ready;
    public ReadyProfileSetting ReadyProfileSetting;
    public GameObject ProfileSelectText;

    [Header("Profile Info")]
    public GameObject MaskImage;
    public GameObject SelectButton;
    public GameObject ChangeButton;

    [Header("Multi Game")]
    public GameObject MultiGame;
    [SerializeField] private MultiManager multiManager;

    [Header("Panel")]
    public GameObject HelpPanel;
    public GameObject GameReadyPanel;

    [Header("Game Ready Panel")]
    public TMP_Text GameReadyPanelText; // warning, ready 공용

    private void OnDisable()
    {
        ShutdownInit();
    }

    private void ShutdownInit()
    { // shutdown 시 초기화되는 목록
        if (!GameManager.Instance.IsShutdown) return;
        LoadingPanel.Instance.gameObject.SetActive(true);
        StopAllCoroutines();
        profileCanvas.gameObject.SetActive(false);
        HelpPanel.SetActive(false);
    }
    #region Ready
    public void ProfileSelectButton()
    { // 프로필 - 프로필 선택, 변경
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.SelectPlayer = Player.Player2;
        ProfileManager.Instance.PrintProfileList(profileCanvas.SelectScrollViewContent);

        SelectButton.SetActive(false);
        ChangeButton.SetActive(true);
        profileCanvas.gameObject.SetActive(true);
        profileCanvas.BackButton.SetActive(true);
        profileCanvas.BlockPanel.SetActive(true);
    }

    public void GameStartButton()
    { // 대기 - 게임 시작
        AudioManager.Instance.SetCommonAudioClip_SFX(0);

        if (!ProfileManager.Instance.IsSelect)
        { // 플레이어 선택을 안했을 시
            StartCoroutine(NonePlayerSetting_Co());
            return;
        }
        else
        {
            AudioManager.Instance.SetAudioClip_BGM(5);
            StopAllCoroutines();
            GameManager.Instance.IsGameClear = false;
            SQL_Manager.instance.SQL_ProfileListSet();
            LoadingPanel.Instance.gameObject.SetActive(true);
            MultiGame.SetActive(true);
            HelpButton.SetActive(false);
            BackButton.SetActive(false);
            Ready.SetActive(false);
            multiManager.GameStart();
        }
    }

    private IEnumerator NonePlayerSetting_Co()
    {
        GameReadyPanelText.text = "플레이어를 선택해주세요.";
        GameReadyPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
    }
    #endregion
    #region Side Panel
    public void MultiBackButton()
    { // 뒤로가기
        AudioManager.Instance.SetAudioClip_BGM(0);
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        LoadingPanel.Instance.gameObject.SetActive(true);

        ProfileManager.Instance.SelectPlayer = Player.Player1;
        GameManager.Instance.GameMode = GameMode.Lobby;
        GameManager.Instance.InGame = false;
        GameManager.Instance.ShutdownCoroutineStop();

        StartCoroutine(NonePlayerSetting_Co());
        ProfileManager.Instance.IsSelect = false;
        ProfileSelectText.SetActive(true);
        MaskImage.SetActive(false);
        ReadyProfileSetting.ProfileName2P.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(true);
        GameReadyPanel.SetActive(false);
        ChangeButton.SetActive(false);
        SelectButton.SetActive(true);
        gameObject.SetActive(false);
    }

    public void HelpPanelButton()
    { // 도움말
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        HelpPanel.SetActive(true);
    }

    public void HelpPanelBackButton()
    { // 도움말 닫기
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        HelpPanel.SetActive(false);
    }
    #endregion
}
