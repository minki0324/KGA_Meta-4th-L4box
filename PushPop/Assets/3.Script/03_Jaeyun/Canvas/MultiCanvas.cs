using System.Collections;
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

    [Header("Ready Info")]
    [SerializeField] private TMP_Text warningLog = null;

    [Header("Profile Info")]
    public GameObject MaskImage = null;
    public GameObject SelectButton = null;
    public GameObject ChangeButton = null;

    [Header("Multi Game")]
    public GameObject MultiGame = null;
    [SerializeField] private MultiManager multiManager = null;

    [Header("Panel")]
    public GameObject HelpPanel = null;
    public GameObject GameReadyPanel = null;

    [Header("Game Ready Panel")]
    public TMP_Text GameReadyPanelText = null; // warning, ready 공용


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
        AudioManager.instance.SetCommonAudioClip_SFX(0);
        AudioManager.instance.SetAudioClip_BGM(5);

        // todo... gamereadypanel에 뜨도록 수정
        if (!ReadyProfileSetting.IsSelect)
        { // 플레이어 선택을 안했을 시
            ProfileManager.Instance.PrintErrorLog(warningLog, "플레이어를 선택해주세요.");
            return;
        }

        MultiGame.SetActive(true);
        HelpButton.SetActive(false);
        BackButton.SetActive(false);
        Ready.SetActive(false);
        multiManager.GameStart();
    }

    private IEnumerator NonePlayerSetting()
    { // panel touch 시 바로 꺼지게 만들까 말까 ... todo
        GameReadyPanelText.text = "플레이어를 선택해주세요.";
        GameReadyPanel.SetActive(true);
        yield return new WaitForSeconds(3f);
    }
    #endregion
    #region Side Panel
    public void MultiBackButton()
    { // 뒤로가기
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.SelectPlayer = Player.Player1;
        GameManager.Instance.GameMode = GameMode.None;
        GameManager.Instance.InGame = false;

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

    public void HelpPanelButton()
    { // 도움말
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        HelpPanel.SetActive(true);
    }

    public void HelpPanelBackButton()
    { // 도움말 닫기
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        HelpPanel.SetActive(false);
    }
    #endregion
}
