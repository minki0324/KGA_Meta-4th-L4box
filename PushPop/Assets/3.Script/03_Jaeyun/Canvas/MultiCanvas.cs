using TMPro;
using UnityEngine;

public class MultiCanvas : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private MainCanvas mainCanvas = null;
    [SerializeField] private ProfileCanvas profileCanvas = null;

    [Header("Side Panel")]
    public GameObject BackButton = null;
    public GameObject HelpButton = null;

    [Header("Ready")]
    public GameObject Ready = null;
    public ReadyProfileSetting ReadyProfileSetting = null;
    public GameObject ProfileSelectText = null;
    [SerializeField] private TMP_Text warningLog = null;

    [Header("Profile Info")]
    public GameObject MaskImage = null;
    public GameObject SelectButton = null;
    public GameObject ChangeButton = null;

    [Header("Multi Game")]
    public GameObject MultiGame = null;

    [Header("Panel")]
    public GameObject ResultPanel = null;
    public GameObject WarningPanel = null;
    public GameObject GameReadyPanel = null;
    public GameObject HelpPanel = null;

    #region Ready
    public void ProfileSelectButton()
    { // 프로필 - 프로필 선택, 변경
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.SelectPlayer = Player.Player2;
        ReadyProfileSetting.IsSelect = true;
        ProfileManager.Instance.PrintProfileList(profileCanvas.SelectScrollViewContent);

        SelectButton.SetActive(false);
        ChangeButton.SetActive(true);
        profileCanvas.gameObject.SetActive(true);
        profileCanvas.BlockPanel.SetActive(true);
    }

    public void GameStartButton()
    { // 대기 - 게임 시작
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        if (!ReadyProfileSetting.IsSelect)
        { // 플레이어 선택을 안했을 시
            ProfileManager.Instance.PrintErrorLog(warningLog, "플레이어를 선택해주세요.");
            return;
        }

        MultiGame.SetActive(true);
        HelpButton.SetActive(false);
        BackButton.SetActive(false);
        Ready.SetActive(false);
    }
    #endregion
    #region Side Panel
    public void HelpPanelButton()
    { // 도움말
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        HelpPanel.SetActive(true);
    }

    public void MultiBackButton()
    { // 뒤로가기
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.SelectPlayer = Player.Player1;
        GameManager.Instance.GameMode = GameMode.None;

        if (GameManager.Instance.ShutdownCoroutine != null)
        {
            GameManager.Instance.StopCoroutine(GameManager.Instance.ShutdownCoroutine);
        }

        ReadyProfileSetting.IsSelect = false;
        ProfileSelectText.SetActive(true);
        MaskImage.SetActive(false);
        ReadyProfileSetting.ProfileName2P.gameObject.SetActive(false);
        warningLog.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(true);
        ChangeButton.SetActive(false);
        SelectButton.SetActive(true);
        gameObject.SetActive(false);
    }
    #endregion
}
