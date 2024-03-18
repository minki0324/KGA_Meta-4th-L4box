using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProfileCanvas : MonoBehaviour, IPointerClickHandler
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
    public Image ProfileImage = null;
    public TMP_Text ProfileText = null;

    [Header("Exit Button")]
    public GameObject ExitButton = null;
    public GameObject BackButton = null;

    [Header("Delete Panel")]
    public GameObject DeletePanel = null;

    private void Start()
    {
        ProfileManager.Instance.LoadOrCreateGUID();
        ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerPress != null && eventData.pointerPress.GetComponent<Button>() != null)
        {
            return;
        }
        else
        {
            if (ProfileIconSelect.activeSelf)
            {
                ProfileManager.Instance.isImageSelect = false;
            }
        }
    }

    #region Select
    public void ProfileCreateButton()
    { // ������ ���� â - ������ ����
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.isUpdate = false;

        CreateName.SetActive(true);
        Select.SetActive(false);
    }

    public void ProfileSelectDeleteButton()
    { // ������ ���� â - ������ ����
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        if (ProfileManager.Instance.ProfilePanelList.Count > 0)
        { // ������ ����Ʈ ���
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
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
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
                // Profile Update
                ProfileManager.Instance.PlayerInfo[(int)ProfileManager.Instance.SelectPlayer].profileImage = ProfileManager.Instance.ProfileImageCaching();
                SQL_Manager.instance.PrintProfileImage(ProfileImage, true, ProfileManager.Instance.TempUserIndex);
                ProfileText.text = ProfileManager.Instance.PlayerInfo[(int)ProfileManager.Instance.SelectPlayer].profileName;
                
                CurrentProfile.SetActive(true);
            }
            
            ProfileIconSelect.SetActive(false);
            ProfileManager.Instance.isImageSelect = false;
        }
        else
        {
            Debug.Log("������ ������ ���� ��� ����");
            return;
        }
    }
    public void ProfileIconSelectBackButton()
    { // ������ ������ ���� - �ڷΰ���
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        StopAllCoroutines();

        inputFieldCheck.WarningLog.gameObject.SetActive(false);
        CreateImage.SetActive(true);
        ProfileIconSelect.SetActive(false);
    }
    #endregion
    #region Create Image
    public void CreateImageTakePictureButton()
    { // ������ �̹��� ��� - ���� ���
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        CreateImage.SetActive(false);
        ProfileManager.Instance.TempImageMode = false;

        // Camera
        CaptureImage.sprite = null;
        cameraManager.CameraOpen(CaptureImage);

        CaptureCheck.SetActive(true);
    }

    public void CreateImageSelectImageButton()
    { // ������ �̹��� ��� - �̹��� ����
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        
        ProfileManager.Instance.TempImageMode = true;

        ProfileIconSelect.SetActive(true);
        CreateImage.SetActive(false);
    }

    public void CreateImageBackButton()
    { // ������ �̹��� ��� - �ڷΰ���
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        CreateName.SetActive(true);
        CreateImage.SetActive(false);
    }
    #endregion
    #region Capture Check
    public void CaptureImageSelect()
    { // ���� ���� �� - Ȯ��
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);

        CaptureImage.sprite = null;

        if (!ProfileManager.Instance.isUpdate)
        { // ������ ���� ��
            Select.SetActive(true);
            CaptureCheck.SetActive(false);
        }
        else
        { // ������ ���� ��
            // Profile Update
            ProfileManager.Instance.PlayerInfo[(int)ProfileManager.Instance.SelectPlayer].profileImage = ProfileManager.Instance.ProfileImageCaching();
            SQL_Manager.instance.PrintProfileImage(ProfileImage, false, ProfileManager.Instance.TempUserIndex);
            ProfileText.text = ProfileManager.Instance.PlayerInfo[(int)ProfileManager.Instance.SelectPlayer].profileName;

            CurrentProfile.SetActive(true);
            CaptureCheck.SetActive(false);
        }
    }

    public void AgainTakePicture()
    { // ���� ���� �� - �ٽ� ���
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        CaptureImage.sprite = null;
        SQL_Manager.instance.SQL_DeleteProfile(ProfileManager.Instance.TempUserIndex);
        cameraManager.CameraOpen(CaptureImage);
    }
    #endregion
    #region Create Name
    public void CreateNameConfirmationButton()
    { // �̸� �Է��ϱ� - �̸� �Է�
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        if (inputFieldCheck.ProfileNameCheck())
        { // inputField�� �Էµ� �ܾ ��Ӿ ���� ��
            CreateImage.SetActive(true);
            CreateName.SetActive(false);
        }
    }

    public void CreateNameBackButton()
    { // �̸� �Է��ϱ� - �ڷΰ���
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);
        StopAllCoroutines();

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
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        if (!ProfileManager.Instance.isProfileSelected)
        {
            ProfileManager.Instance.isProfileSelected = true;
        }

        if (GameManager.Instance.GameMode.Equals(GameMode.Multi))
        {
            multiCanvas.ReadyProfileSetting.SelectPlayerInfoSetting(); // 2P Profile Setting
            ProfileManager.Instance.IsSelect = true;
            multiCanvas.ProfileSelectText.SetActive(false);
            multiCanvas.MaskImage.SetActive(true);
            multiCanvas.ReadyProfileSetting.ProfileName2P.gameObject.SetActive(true);
        }
        else
        { // Gamemode.None
            mainCanvas.TitleText.SetActive(true);
            mainCanvas.OptionButton.SetActive(true);
            mainCanvas.ProfileButton.SetActive(true);
            mainCanvas.PushpushButton.SetActive(true);
            mainCanvas.SpeedButton.SetActive(true);
            mainCanvas.MemoryButton.SetActive(true);
            mainCanvas.MultiButton.SetActive(true);
            mainCanvas.NetworkButton.SetActive(true);

            mainCanvas.CaptureImage.sprite = ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileImage;
        }
        ProfileManager.Instance.isImageSelect = false;
        for(int i = 0; i < SQL_Manager.instance.ProfileList.Count; i++)
        {
            if(SQL_Manager.instance.ProfileList[i].index.Equals(ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex))
            {
                ProfileManager.Instance.myProfile = SQL_Manager.instance.ProfileList[i];
            }
        }
        BlockPanel.SetActive(false);
        CurrentProfile.SetActive(false);
        ExitButton.SetActive(false);
        BackButton.SetActive(true);
        Select.SetActive(true);
        gameObject.SetActive(false);
    }

    public void CurrentProfileChangeButton()
    { // ���õ� ������ - ����
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.isUpdate = true;

        CurrentProfile.SetActive(false);
        CreateName.SetActive(true);
    }

    public void CurrentProfileReturnButton()
    { // ���õ� ������ - �ڷΰ���
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        SelectScrollView.normalizedPosition = new Vector2(1f, 1f);
        ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);

        if (GameManager.Instance.GameMode.Equals(GameMode.None) && !mainCanvas.ProfileButton.activeSelf)
        {
            BlockPanel.SetActive(false);
            ExitButton.SetActive(true);
        }
        else
        {
            BackButton.SetActive(true);
        }

        CurrentProfile.SetActive(false);
        Select.SetActive(true);
    }
    #endregion
    #region Delete Panel
    public void ProfileDeleteButton()
    { // ������ ���� - ����
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        ProfileManager.Instance.DeleteProfile(ProfileManager.Instance.TempUserIndex);
        ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);

        DeletePanel.SetActive(false);
    }

    public void ProfileDeleteReturnButton()
    { // ������ ���� - ���
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        DeletePanel.SetActive(false);
    }
    #endregion
    public void GameQuitButton()
    { // ���� ����
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ProfileBackButton()
    {
        gameObject.SetActive(false);
    }
}
