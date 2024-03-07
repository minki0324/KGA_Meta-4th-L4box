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
    { // ������ - ������ ����, ����
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
    { // ��� - ���� ����
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        if (!ReadyProfileSetting.IsSelect)
        { // �÷��̾� ������ ������ ��
            ProfileManager.Instance.PrintErrorLog(warningLog, "�÷��̾ �������ּ���.");
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
    { // ����
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        HelpPanel.SetActive(true);
    }

    public void MultiBackButton()
    { // �ڷΰ���
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
