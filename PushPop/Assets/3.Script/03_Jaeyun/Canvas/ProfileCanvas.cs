using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProfileCanvas : MonoBehaviour, IPointerClickHandler
{
    [Header("Canvas")]
    [SerializeField] private MainCanvas mainCanvas = null;
    [SerializeField] private MultiCanvas multiCanvas = null;

    [SerializeField] private MemoryManager memoryManager = null;

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
    public Button ResetButton = null;
    private Coroutine resetCoroutine = null;
    public GameObject ProfileLoadingPanel = null;

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
    public bool isChangeProfile = false;

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
            if (ProfileIconSelect.activeSelf)
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
        }
    }

    private void ShutdownInit()
    {
        if (!GameManager.Instance.IsShutdown) return;
        Select.SetActive(true);
        CreateName.SetActive(false);
        CreateImage.SetActive(false);
        ProfileIconSelect.SetActive(false);
        CaptureCheck.SetActive(false);
        CurrentProfile.SetActive(false);
        DeletePanel.SetActive(false);
        BlockPanel.SetActive(true);
        BackButton.SetActive(true);
    }
    #region Select
    public void ProfileCreateButton()
    { // 프로필 선택 창 - 프로필 생성
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.isUpdate = false;

        BackButton.SetActive(false);
        CreateName.SetActive(true);
        Select.SetActive(false);
    }

    public void ProfileSelectDeleteButton()
    { // 프로필 선택 창 - 프로필 삭제
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        if (ProfileManager.Instance.ProfilePanelList.Count > 0)
        { // 프로필 리스트 출력
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
    { // 프로필 아이콘 선택 - 선택
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);

        if (ProfileManager.Instance.ImageSet(true, selectProfileIcon.WarningLog, SelectScrollViewContent))
        {
            ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);

            if (!ProfileManager.Instance.isUpdate)
            { // 프로필 생성 시
                Select.SetActive(true);
            }
            else
            { // 프로필 수정 시
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
            Debug.Log("프로필 아이콘 선택 등록 실패");
            return;
        }
    }
    public void ProfileIconSelectBackButton()
    { // 프로필 아이콘 선택 - 뒤로가기
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        StopAllCoroutines();

        BackButton.SetActive(true);
        inputFieldCheck.WarningLog.gameObject.SetActive(false);
        CreateImage.SetActive(true);
        ProfileIconSelect.SetActive(false);
    }
    #endregion
    #region Create Image
    public void CreateImageTakePictureButton()
    { // 프로필 이미지 등록 - 사진 찍기
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        CreateImage.SetActive(false);
        ProfileManager.Instance.TempImageMode = false;

        // Camera
        CaptureImage.sprite = null;
        cameraManager.CameraOpen(CaptureImage);

        CaptureCheck.SetActive(true);
    }

    public void CreateImageSelectImageButton()
    { // 프로필 이미지 등록 - 이미지 고르기
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        
        ProfileManager.Instance.TempImageMode = true;

        ProfileManager.Instance.isImageSelect = false;
        ProfileIconSelect.SetActive(true);
        CreateImage.SetActive(false);
    }

    public void CreateImageBackButton()
    { // 프로필 이미지 등록 - 뒤로가기
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        CreateName.SetActive(true);
        CreateImage.SetActive(false);
    }
    #endregion
    #region Capture Check
    public void CaptureImageSelect()
    { // 사진 찍은 후 - 확인
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);

        CaptureImage.sprite = null;

        if (!ProfileManager.Instance.isUpdate)
        { // 프로필 생성 시
            Select.SetActive(true);
            CaptureCheck.SetActive(false);
        }
        else
        { // 프로필 수정 시
            // Profile Update
            ProfileManager.Instance.PlayerInfo[(int)ProfileManager.Instance.SelectPlayer].profileImage = ProfileManager.Instance.ProfileImageCaching();
            SQL_Manager.instance.PrintProfileImage(ProfileImage, false, ProfileManager.Instance.TempUserIndex);
            ProfileText.text = ProfileManager.Instance.PlayerInfo[(int)ProfileManager.Instance.SelectPlayer].profileName;

            CurrentProfile.SetActive(true);
            CaptureCheck.SetActive(false);
        }
    }

    public void AgainTakePicture()
    { // 사진 찍은 후 - 다시 찍기
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        CaptureImage.sprite = null;
        SQL_Manager.instance.SQL_DeleteProfile(ProfileManager.Instance.TempUserIndex);
        cameraManager.CameraOpen(CaptureImage);
    }
    #endregion
    #region Create Name
    public void CreateNameConfirmationButton()
    { // 이름 입력하기 - 이름 입력
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        if (inputFieldCheck.ProfileNameCheck())
        { // inputField에 입력된 단어에 비속어가 없을 때
            CreateImage.SetActive(true);
            CreateName.SetActive(false);
        }
    }

    public void CreateNameBackButton()
    { // 이름 입력하기 - 뒤로가기
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);
        StopAllCoroutines();

        if (!GameManager.Instance.GameMode.Equals(GameMode.Title))
        {
            BackButton.SetActive(true);
        }

        if (!ProfileManager.Instance.isUpdate)
        { // 프로필 생성 시
            Select.SetActive(true);
        }
        else
        { // 프로필 수정 시
            CurrentProfile.SetActive(true);
            BackButton.SetActive(false);
        }
        
        inputFieldCheck.WarningLog.gameObject.SetActive(false);
        CreateName.SetActive(false);
    }
    #endregion
    #region Current Profile
    public void CurrentProfileSelectButton()
    { // 선택된 프로필 - 프로필 선택
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
        ExitButton.SetActive(false);
        BackButton.SetActive(true);
        Select.SetActive(true);
        gameObject.SetActive(false);

        try
        {
        GameManager.Instance.myMeomoryStageInfo = SavePoint.Instance.completedStageList.FirstOrDefault(stage => stage.profileName == ProfileManager.Instance.myProfile.name);
        }
        catch (System.Exception)
        {
            memoryManager.CurrentStage= 1;
        }
        mainCanvas.isChangeProfile = false;
    }

    public void CurrentProfileChangeButton()
    { // 선택된 프로필 - 수정
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.isUpdate = true;

        CurrentProfile.SetActive(false);
        CreateName.SetActive(true);
    }

    public void CurrentProfileReturnButton()
    { // 선택된 프로필 - 뒤로가기
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        SelectScrollView.normalizedPosition = new Vector2(1f, 1f);
        ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);

        if (GameManager.Instance.GameMode.Equals(GameMode.Title))
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
    { // 프로필 삭제 - 삭제
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        ProfileManager.Instance.DeleteProfile(ProfileManager.Instance.TempUserIndex);
        ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);

        DeletePanel.SetActive(false);
    }

    public void ProfileDeleteReturnButton()
    { // 프로필 삭제 - 취소
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        DeletePanel.SetActive(false);
    }
    #endregion
    public void GameQuitButton()
    { // 게임 종료
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

    public void ProfileResetButton()
    {
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        if (resetCoroutine != null) return;
        resetCoroutine = StartCoroutine(ProfileResetButton_co());
    }

    public IEnumerator ProfileResetButton_co()
    {
        ResetButton.interactable = false;
        ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);
        yield return new WaitForSeconds(5f);
        ResetButton.interactable = true;
        resetCoroutine = null;
    }
}
