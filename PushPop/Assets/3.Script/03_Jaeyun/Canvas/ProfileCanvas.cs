using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileCanvas : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private MainCanvas mainCanvas = null;
    [SerializeField] private MultiCanvas multiCanvas = null;

    [Header("Profile Panel")]
    public GameObject BlockPanel = null;
    public GameObject ProfilePanel = null;
    public GameObject Select = null;
    public GameObject CreateName = null;
    public GameObject CreateImage = null;
    public GameObject ProfileIconSelect = null;
    public GameObject CaptureCheck = null;
    public GameObject CurrentProfile = null;

    [Header("Select")]
    public ScrollRect SelectScrollView = null;
    public Transform SelectScrollViewContent = null;

    [Header("Create Name")]
    [SerializeField] private InputFieldCheck inputFieldCheck = null;

    [Header("Create Image")]
    [SerializeField] private CameraManager cameraManager = null;
    public Image CaptureImage = null;

    [Header("ProfileIcon Select")]
    [SerializeField] private SelectProfileIcon selectProfileIcon = null;

    [Header("Current Profile")]
    public Image ProfileIamge = null;
    public TMP_Text ProfileText = null;

    [Header("Exit Button")]
    public GameObject ExitButton = null;

    [Header("Delete Panel")]
    public GameObject DeletePanel = null;

    private void OnEnable()
    {
        BlockPanel.SetActive(false);
    }

    private void Start()
    {
        ProfileManager.Instance.LoadOrCreateGUID();
        ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);

        // audio PlayerPrefs setting ... todo
        AudioManager.instance.SetAudioClip_BGM(0);
    }

    #region Select
    public void ProfileCreateButton()
    { // ������ ���� â - ������ ����
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.isUpdate = false;

        CreateName.SetActive(true);
        Select.SetActive(false);
    }

    public void ProfileSelectDeleteButton()
    { // ������ ���� â - ������ ����
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        if (ProfileManager.Instance.ProfilePanelList.Count > 0)
        {
            bool active = ProfileManager.Instance.ProfilePanelList[0].GetComponent<ProfileInfo>().DeleteButton.activeSelf;
            for (int i = 0; i < ProfileManager.Instance.ProfilePanelList.Count; i++)
            {
                ProfileManager.Instance.ProfilePanelList[i].GetComponent<ProfileInfo>().DeleteButton.SetActive(!active);
            }
        }
    }
    #endregion
    #region Profile Icon Select
    public void ProfileIconSelectOkButton()
    { // ������ ������ ���� - ����
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);

        if (ProfileManager.Instance.ImageSet(true, true, ProfileManager.Instance.TempProfileName, ProfileManager.Instance.TempImageIndex, selectProfileIcon.WarningLog))
        {
            ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);

            if (!ProfileManager.Instance.isUpdate)
            { // ������ ���� ��
                Select.SetActive(true);
            }
            else
            { // ������ ���� ��
                CurrentProfile.SetActive(true);
            }

            SQL_Manager.instance.PrintProfileImage(ProfileManager.Instance.IsImageMode1P, ProfileIamge, ProfileManager.Instance.FirstPlayerIndex);
            ProfileManager.Instance.ProfileImageCaching();
            ProfileIconSelect.SetActive(false);
        }
        else
        {
            Debug.Log("������ ������ ���� ��� ����");
            return;
        }
    }
    public void ProfileIconSelectBackButton()
    { // ������ ������ ���� - �ڷΰ���
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        if (ProfileManager.Instance.WarningCoroutine != null)
        { // warning print coroutine stop
            ProfileManager.Instance.StopCoroutine(ProfileManager.Instance.WarningCoroutine);
        }

        inputFieldCheck.WarningLog.gameObject.SetActive(false);
        CreateImage.SetActive(true);
        ProfileIconSelect.SetActive(false);
    }
    #endregion
    #region Create Image
    public void CreateImageTakePictureButton()
    { // ������ �̹��� ��� - ���� ���
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        CreateImage.SetActive(false);

        // Camera
        CaptureImage.sprite = null;
        cameraManager.CameraOpen(CaptureImage);

        CaptureCheck.SetActive(true);
    }

    public void CreateImageSelectImageButton()
    { // ������ �̹��� ��� - �̹��� ����
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        ProfileIconSelect.SetActive(true);
        CreateImage.SetActive(false);
    }

    public void CreateImageBackButton()
    { // ������ �̹��� ��� - �ڷΰ���
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        CreateName.SetActive(true);
        CreateImage.SetActive(false);
    }
    #endregion
    #region Capture Check
    public void CaptureImageSelect()
    { // ���� ���� �� - Ȯ��
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);

        CaptureImage.sprite = null;

        if (!ProfileManager.Instance.isUpdate)
        { // ������ ���� ��
            Select.SetActive(true);
            CaptureCheck.SetActive(false);
        }
        else
        { // ������ ���� ��
            CurrentProfile.SetActive(true);
            CaptureCheck.SetActive(false);
        }
    }

    public void AgainTakePicture()
    { // ���� ���� �� - �ٽ� ���
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        CaptureImage.sprite = null;
        SQL_Manager.instance.SQL_DeleteProfile(ProfileManager.Instance.TempUserIndex);
        cameraManager.CameraOpen(CaptureImage);
    }
    #endregion
    #region Create Name
    public void CreateNameConfirmationButton()
    { // �̸� �Է��ϱ� - �̸� �Է�
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        if (inputFieldCheck.ProfileNameCheck())
        { // inputField�� �Էµ� �ܾ ��Ӿ ���� ��
            CreateImage.SetActive(true);
            CreateName.SetActive(false);
        }
    }

    public void CreateNameBackButton()
    { // �̸� �Է��ϱ� - �ڷΰ���
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);

        if (ProfileManager.Instance.WarningCoroutine != null)
        { // warning print coroutine stop
            ProfileManager.Instance.StopCoroutine(ProfileManager.Instance.WarningCoroutine);
        }

        if (!ProfileManager.Instance.isUpdate)
        { // ������ ���� ��
            Select.SetActive(true);
        }
        else
        { // ������ ���� ��
            CurrentProfile.SetActive(true);
        }

        inputFieldCheck.WarningLog.gameObject.SetActive(false);
        CreateName.SetActive(false);
    }
    #endregion
    #region Current Profile
    public void CurrentProfileSelectButton()
    { // ���õ� ������ - ������ ����
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        if (!ProfileManager.Instance.isProfileSelected)
        {
            ProfileManager.Instance.isProfileSelected = true;
        }
        GameManager.Instance.GameMode = GameMode.None;

        mainCanvas.TitleText.SetActive(true);
        mainCanvas.OptionButton.SetActive(true);
        mainCanvas.ProfileButton.SetActive(true);
        mainCanvas.HomeButton.SetActive(true);
        mainCanvas.PushpushButton.SetActive(true);
        mainCanvas.SpeedButton.SetActive(true);
        mainCanvas.MemoryButton.SetActive(true);
        mainCanvas.MultiButton.SetActive(true);
        mainCanvas.NetworkButton.SetActive(true);

        // profile sprite setting
        mainCanvas.CaptureImage.sprite = ProfileManager.Instance.ProfileImageCaching();

        CurrentProfile.SetActive(false);
        ExitButton.SetActive(false);
        Select.SetActive(true);
        gameObject.SetActive(false);
    }

    public void CurrentProfileChangeButton()
    { // ���õ� ������ - ����
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.isUpdate = true;

        CurrentProfile.SetActive(false);
        CreateName.SetActive(true);
    }

    public void CurrentProfileReturnButton()
    { // ���õ� ������ - �ڷΰ���
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        SelectScrollView.normalizedPosition = new Vector2(1f, 1f);
        ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);

        // temp init
        ProfileManager.Instance.TempProfileName = string.Empty;
        ProfileManager.Instance.TempUserIndex = -1;
        ProfileManager.Instance.TempImageIndex = 0;

        BlockPanel.SetActive(false);
        CurrentProfile.SetActive(false);
        Select.SetActive(true);
        ExitButton.SetActive(true);
    }
    #endregion
    #region Delete Panel
    public void ProfileDeleteButton()
    { // ������ ���� - ����
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        ProfileManager.Instance.DeleteProfile(ProfileManager.Instance.TempUserIndex);
        ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);

        DeletePanel.SetActive(false);
    }

    public void ProfileDeleteReturnButton()
    { // ������ ���� - ���
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        DeletePanel.SetActive(false);
    }
    #endregion
    public void GameQuitButton()
    { // ���� ����
        AudioManager.instance.SetCommonAudioClip_SFX(3);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
