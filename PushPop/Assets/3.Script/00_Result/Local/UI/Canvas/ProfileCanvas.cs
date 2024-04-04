using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProfileCanvas : MonoBehaviour, IPointerClickHandler
{
    [Header("Other Component")]
    [SerializeField] private SavePoint savePoint;
    [SerializeField] private MemoryManager memoryManager;
    
    [Header("Canvas")]
    [SerializeField] private MainCanvas mainCanvas;
    [SerializeField] private MultiCanvas multiCanvas;

    [Header("Profile Panel")]
    [SerializeField] private GameObject createName;
    [SerializeField] private GameObject createImage;
    [SerializeField] private GameObject profileIconSelect;
    [SerializeField] private GameObject captureCheck;
    public GameObject BlockPanel;
    public GameObject Select;
    public GameObject CurrentProfile;

    [Header("Select")]
    public GameObject ProfileLoadingPanel;
    [SerializeField] private ScrollRect selectScrollView;
    public Button CreateButton;
    public Button DeleteButton;
    public Transform SelectScrollViewContent;

    [Header("Create Name")]
    [SerializeField] private InputFieldCheck inputFieldCheck;

    [Header("Create Image")]
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private Image captureImage;

    [Header("ProfileIcon Select")]
    [SerializeField] private SelectProfileIcon selectProfileIcon;

    [Header("Current Profile")]
    public Image ProfileImage;
    public TMP_Text ProfileText;

    [Header("Exit Button")]
    [SerializeField] private GameObject quitButton;
    public GameObject BackButton;

    [Header("Delete Panel")]
    public GameObject DeletePanel;

    private void OnEnable()
    {
        StartCoroutine(Init());
    }

    private void Start()
    {
        ProfileManager.Instance.LoadOrCreateGUID();
        ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);
    }

    private void OnDisable()
    {
        ShutdownInit();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerPress != null && eventData.pointerPress.GetComponent<Button>() != null)
        {
            return;
        }
        else
        {
            if (profileIconSelect.activeSelf)
            {
                ProfileManager.Instance.isImageSelect = false;
            }
        }
    }

    private IEnumerator Init()
    {
        yield return null;
        if (GameManager.Instance.GameMode.Equals(GameMode.Lobby) && !mainCanvas.isChangeProfile)
        {
            quitButton.SetActive(false);
            gameObject.SetActive(false);
        }
        else if (GameManager.Instance.GameMode.Equals(GameMode.Title))
        {
            mainCanvas.TitleText.SetActive(false);
            mainCanvas.OptionButton.SetActive(false);
            mainCanvas.ProfileButton.SetActive(false);
            mainCanvas.PushpushButton.SetActive(false);
            mainCanvas.SpeedButton.SetActive(false);
            mainCanvas.MemoryButton.SetActive(false);
            mainCanvas.MultiButton.SetActive(false);
            mainCanvas.NetworkButton.SetActive(false);
            mainCanvas.PracticeButton.SetActive(false);
        }
    }

    private void ShutdownInit()
    { // shutdown �� �ʱ�ȭ�Ǵ� ���
        if (!GameManager.Instance.IsShutdown) return;
        Select.SetActive(true);
        createName.SetActive(false);
        createImage.SetActive(false);
        profileIconSelect.SetActive(false);
        captureCheck.SetActive(false);
        CurrentProfile.SetActive(false);
        DeletePanel.SetActive(false);
        BlockPanel.SetActive(true);
        BackButton.SetActive(true);
    }
    #region Select
    public void ProfileCreateButton()
    { // ������ ���� â - ������ ����
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.isUpdate = false;
        ProfileManager.Instance.isImageUpdate = false;

        BackButton.SetActive(false);
        createName.SetActive(true);
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
                ProfileManager.Instance.ProfilePanelList[i].GetComponent<Button>().enabled = active;
                ProfileManager.Instance.ProfilePanelList[i].GetComponent<ProfileInfo>().DeleteButton.SetActive(!active);
            }
        }
    }
    #endregion
    #region Profile Icon Select
    public void ProfileIconSelectOkButton()
    { // ������ ������ ���� - ����
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        if (ProfileManager.Instance.ImageSet(true, selectProfileIcon.WarningLog))
        {
            ProfileManager.Instance.isImageSelect = false;
            ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);

            if (!ProfileManager.Instance.isUpdate)
            { // ������ ���� ��
                Select.SetActive(true);
                if (!GameManager.Instance.GameMode.Equals(GameMode.Title))
                {
                    BackButton.SetActive(true);
                }
            }
            else
            { // ������ ���� ��
                // Profile Update
                ProfileManager.Instance.PlayerInfo[(int)ProfileManager.Instance.SelectPlayer].profileImage = ProfileManager.Instance.ProfileImageCaching();
                SQL_Manager.instance.PrintProfileImage(ProfileImage, true, ProfileManager.Instance.TempUserIndex);
                ProfileText.text = ProfileManager.Instance.PlayerInfo[(int)ProfileManager.Instance.SelectPlayer].profileName;
                
                CurrentProfile.SetActive(true);
            }
            
            profileIconSelect.SetActive(false);
        }
        else
        {
            return;
        }
    }
    public void ProfileIconSelectBackButton()
    { // ������ ������ ���� - �ڷΰ���
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        StopAllCoroutines();

        BackButton.SetActive(true);
        inputFieldCheck.WarningLog.gameObject.SetActive(false);
        createImage.SetActive(true);
        profileIconSelect.SetActive(false);
    }
    #endregion
    #region Create Image
    public void CreateImageTakePictureButton()
    { // ������ �̹��� ��� - ���� ���
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        createImage.SetActive(false);
        ProfileManager.Instance.TempImageMode = false;

        // Camera
        captureImage.sprite = null;
        cameraManager.CameraOpen(captureImage);

        captureCheck.SetActive(true);
    }

    public void CreateImageSelectImageButton()
    { // ������ �̹��� ��� - �̹��� ����
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        
        ProfileManager.Instance.TempImageMode = true;
        ProfileManager.Instance.isImageSelect = false;
        profileIconSelect.SetActive(true);
        createImage.SetActive(false);
    }

    public void CreateImageBackButton()
    { // ������ �̹��� ��� - �ڷΰ���
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        createName.SetActive(true);
        createImage.SetActive(false);
    }
    #endregion
    #region Capture Check
    public void CaptureImageSelect()
    { // ���� ���� �� - Ȯ��
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        captureImage.sprite = null;

        if (!ProfileManager.Instance.isUpdate)
        { // ������ ���� ��
            Select.SetActive(true);
            captureCheck.SetActive(false);
        }
        else
        { // ������ ���� ��
            // Profile Update
            ProfileManager.Instance.PlayerInfo[(int)ProfileManager.Instance.SelectPlayer].profileImage = ProfileManager.Instance.ProfileImageCaching();
            SQL_Manager.instance.PrintProfileImage(ProfileImage, false, ProfileManager.Instance.TempUserIndex);
            ProfileText.text = ProfileManager.Instance.PlayerInfo[(int)ProfileManager.Instance.SelectPlayer].profileName;

            CurrentProfile.SetActive(true);
            captureCheck.SetActive(false);
        }

        ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);
    }

    public void AgainTakePicture()
    { // ���� ���� �� - �ٽ� ���
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        captureImage.sprite = null;
        SQL_Manager.instance.SQL_DeleteProfile(ProfileManager.Instance.TempUserIndex);
        cameraManager.CameraOpen(captureImage);
    }
    #endregion
    #region Create Name
    public void CreateNameConfirmationButton()
    { // �̸� �Է��ϱ� - �̸� �Է�
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        if (inputFieldCheck.ProfileNameCheck())
        { // inputField�� �Էµ� �ܾ ��Ӿ ���� ��
            createImage.SetActive(true);
            createName.SetActive(false);
        }
    }

    public void CreateNameBackButton()
    { // �̸� �Է��ϱ� - �ڷΰ���
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.isImageUpdate = true;
        ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);
        StopAllCoroutines();

        if (!GameManager.Instance.GameMode.Equals(GameMode.Title))
        {
            BackButton.SetActive(true);
        }

        if (!ProfileManager.Instance.isUpdate)
        { // ������ ���� ��
            Select.SetActive(true);
        }
        else
        { // ������ ���� ��
            CurrentProfile.SetActive(true);
            BackButton.SetActive(false);
        }
        
        inputFieldCheck.WarningLog.gameObject.SetActive(false);
        createName.SetActive(false);
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

        Player player = ProfileManager.Instance.SelectPlayer;
        ProfileManager.Instance.UID = SQL_Manager.instance.UID;
        ProfileManager.Instance.PlayerInfo[(int)player] = new PlayerInfo(
            ProfileManager.Instance.TempProfileName,
            ProfileManager.Instance.TempUserIndex,
            ProfileManager.Instance.TempImageIndex,
            ProfileManager.Instance.TempImageMode
            );
        ProfileManager.Instance.PlayerInfo[(int)player].profileImage = ProfileManager.Instance.ProfileImageCaching();

        if (GameManager.Instance.GameMode.Equals(GameMode.Multi))
        {
            multiCanvas.ReadyProfileSetting.SelectPlayerInfoSetting(); // 2P Profile Setting
            ProfileManager.Instance.IsSelect = true;
            multiCanvas.ProfileSelectText.SetActive(false);
            multiCanvas.MaskImage.SetActive(true);
            multiCanvas.ReadyProfileSetting.ProfileName2P.gameObject.SetActive(true);
        }
        else if (GameManager.Instance.GameMode.Equals(GameMode.Title))
        {
            GameManager.Instance.GameMode = GameMode.Lobby;
            LoadingPanel.Instance.gameObject.SetActive(true);
            mainCanvas.TitleText.SetActive(true);
            mainCanvas.OptionButton.SetActive(true);
            mainCanvas.ProfileButton.SetActive(true);
            mainCanvas.PushpushButton.SetActive(true);
            mainCanvas.SpeedButton.SetActive(true);
            mainCanvas.MemoryButton.SetActive(true);
            mainCanvas.MultiButton.SetActive(true);
            mainCanvas.NetworkButton.SetActive(true);
            mainCanvas.PracticeButton.SetActive(true);
        }

        mainCanvas.CaptureImage.sprite = ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileImage;
        ProfileManager.Instance.isImageSelect = false;
        for (int i = 0; i < SQL_Manager.instance.ProfileList.Count; i++)
        {
            if (SQL_Manager.instance.ProfileList[i].index.Equals(ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex))
            {
                ProfileManager.Instance.myProfile = SQL_Manager.instance.ProfileList[i];
            }
        }
        BlockPanel.SetActive(false);
        CurrentProfile.SetActive(false);
        quitButton.SetActive(false);
        BackButton.SetActive(true);
        Select.SetActive(true);
        gameObject.SetActive(false);

        try
        {
            GameManager.Instance.myMeomoryStageInfo = savePoint.completedStageList.FirstOrDefault(stage => stage.profileName == ProfileManager.Instance.myProfile.name);
        }
        catch (System.Exception)
        {
            memoryManager.CurrentStage= 1;
        }
        mainCanvas.isChangeProfile = false;
    }

    public void CurrentProfileChangeButton()
    { // ���õ� ������ - ����
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.isUpdate = true;

        CurrentProfile.SetActive(false);
        createName.SetActive(true);
    }

    public void CurrentProfileReturnButton()
    { // ���õ� ������ - �ڷΰ���
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        selectScrollView.normalizedPosition = new Vector2(1f, 1f);
        ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);

        if (GameManager.Instance.GameMode.Equals(GameMode.Title))
        {
            BlockPanel.SetActive(false);
            quitButton.SetActive(true);
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
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        gameObject.SetActive(false);
    }
}
