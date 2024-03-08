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
    public Image ProfileImage = null;
    public TMP_Text ProfileText = null;

    [Header("Exit Button")]
    public GameObject ExitButton = null;

    [Header("Delete Panel")]
    public GameObject DeletePanel = null;

    private void Start()
    {
        ProfileManager.Instance.LoadOrCreateGUID();
        ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);

        // audio PlayerPrefs setting ... todo
        AudioManager.instance.SetAudioClip_BGM(0);
    }

    #region Select
    public void ProfileCreateButton()
    { // 프로필 선택 창 - 프로필 생성
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.isUpdate = false;

        CreateName.SetActive(true);
        Select.SetActive(false);
    }

    public void ProfileSelectDeleteButton()
    { // 프로필 선택 창 - 프로필 삭제
        AudioManager.instance.SetCommonAudioClip_SFX(3);

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
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);

        if (ProfileManager.Instance.ImageSet(true, true, ProfileManager.Instance.TempProfileName, ProfileManager.Instance.TempImageIndex, selectProfileIcon.WarningLog))
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
        }
        else
        {
            Debug.Log("프로필 아이콘 선택 등록 실패");
            return;
        }
    }
    public void ProfileIconSelectBackButton()
    { // 프로필 아이콘 선택 - 뒤로가기
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        StopAllCoroutines();

        inputFieldCheck.WarningLog.gameObject.SetActive(false);
        CreateImage.SetActive(true);
        ProfileIconSelect.SetActive(false);
    }
    #endregion
    #region Create Image
    public void CreateImageTakePictureButton()
    { // 프로필 이미지 등록 - 사진 찍기
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        CreateImage.SetActive(false);
        ProfileManager.Instance.TempImageMode = false;

        // Camera
        CaptureImage.sprite = null;
        cameraManager.CameraOpen(CaptureImage);

        CaptureCheck.SetActive(true);
    }

    public void CreateImageSelectImageButton()
    { // 프로필 이미지 등록 - 이미지 고르기
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        
        ProfileManager.Instance.TempImageMode = true;

        ProfileIconSelect.SetActive(true);
        CreateImage.SetActive(false);
    }

    public void CreateImageBackButton()
    { // 프로필 이미지 등록 - 뒤로가기
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        CreateName.SetActive(true);
        CreateImage.SetActive(false);
    }
    #endregion
    #region Capture Check
    public void CaptureImageSelect()
    { // 사진 찍은 후 - 확인
        AudioManager.instance.SetCommonAudioClip_SFX(3);
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
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        CaptureImage.sprite = null;
        SQL_Manager.instance.SQL_DeleteProfile(ProfileManager.Instance.TempUserIndex);
        cameraManager.CameraOpen(CaptureImage);
    }
    #endregion
    #region Create Name
    public void CreateNameConfirmationButton()
    { // 이름 입력하기 - 이름 입력
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        if (inputFieldCheck.ProfileNameCheck())
        { // inputField에 입력된 단어에 비속어가 없을 때
            CreateImage.SetActive(true);
            CreateName.SetActive(false);
        }
    }

    public void CreateNameBackButton()
    { // 이름 입력하기 - 뒤로가기
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);
        StopAllCoroutines();

        if (!ProfileManager.Instance.isUpdate)
        { // 프로필 생성 시
            Select.SetActive(true);
        }
        else
        { // 프로필 수정 시
            CurrentProfile.SetActive(true);
        }

        inputFieldCheck.WarningLog.gameObject.SetActive(false);
        CreateName.SetActive(false);
    }
    #endregion
    #region Current Profile
    public void CurrentProfileSelectButton()
    { // 선택된 프로필 - 프로필 선택
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        if (!ProfileManager.Instance.isProfileSelected)
        {
            ProfileManager.Instance.isProfileSelected = true;
        }

        if (GameManager.Instance.GameMode.Equals(GameMode.Multi))
        {
            multiCanvas.ReadyProfileSetting.SelectPlayerInfoSetting(); // 2P Profile Setting
            multiCanvas.ProfileSelectText.SetActive(false);
            multiCanvas.MaskImage.SetActive(true);
            multiCanvas.ReadyProfileSetting.ProfileName2P.gameObject.SetActive(true);
        }
        else
        { // Gamemode.None
            mainCanvas.TitleText.SetActive(true);
            mainCanvas.OptionButton.SetActive(true);
            mainCanvas.ProfileButton.SetActive(true);
            mainCanvas.HomeButton.SetActive(true);
            mainCanvas.PushpushButton.SetActive(true);
            mainCanvas.SpeedButton.SetActive(true);
            mainCanvas.MemoryButton.SetActive(true);
            mainCanvas.MultiButton.SetActive(true);
            mainCanvas.NetworkButton.SetActive(true);

            mainCanvas.CaptureImage.sprite = ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileImage;
        }

        BlockPanel.SetActive(false);
        CurrentProfile.SetActive(false);
        ExitButton.SetActive(false);
        Select.SetActive(true);
        gameObject.SetActive(false);
    }

    public void CurrentProfileChangeButton()
    { // 선택된 프로필 - 수정
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.isUpdate = true;

        CurrentProfile.SetActive(false);
        CreateName.SetActive(true);
    }

    public void CurrentProfileReturnButton()
    { // 선택된 프로필 - 뒤로가기
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        SelectScrollView.normalizedPosition = new Vector2(1f, 1f);
        ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);

        if (!GameManager.Instance.GameMode.Equals(GameMode.Multi))
        {
            BlockPanel.SetActive(false);
            ExitButton.SetActive(true);
        }

        CurrentProfile.SetActive(false);
        Select.SetActive(true);
    }
    #endregion
    #region Delete Panel
    public void ProfileDeleteButton()
    { // 프로필 삭제 - 삭제
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        ProfileManager.Instance.DeleteProfile(ProfileManager.Instance.TempUserIndex);
        ProfileManager.Instance.PrintProfileList(SelectScrollViewContent);

        DeletePanel.SetActive(false);
    }

    public void ProfileDeleteReturnButton()
    { // 프로필 삭제 - 취소
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        DeletePanel.SetActive(false);
    }
    #endregion
    public void GameQuitButton()
    { // 게임 종료
        AudioManager.instance.SetCommonAudioClip_SFX(3);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
