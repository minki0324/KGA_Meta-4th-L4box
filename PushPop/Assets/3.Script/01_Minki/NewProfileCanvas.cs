using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using System;

/// <summary>
/// Profile Canvas 관련 Class
/// </summary>
public class NewProfileCanvas : MonoBehaviour, IPointerClickHandler
{
    [Header("Other Component")] [Space(5)]
    [SerializeField] private CameraManager cameraManager; // 외부 Camera 연동 Script
    [SerializeField] private Image CaptureImage;

    [Header("Game Lobby")] [Space(5)]
    [SerializeField] private GameObject MainButtonPanel; // Main Game Lobby의 버튼들 Panel
    [SerializeField] private GameObject GameModePanel; // Main Game Lobby의 GameMode Panel

    [Header("Main Panel")] [Space(5)]
    public Button ExitBtn; // 종료 버튼

    [Header("Select Profile Panel")] [Space(5)]
    public GameObject SelectProfilePanel; // Select Profile Panel
    public Transform profileParent; // Profile List들 상속할 부모 Transform
    [SerializeField] private ScrollRect profileScrollView; // Profile List의 ScrollView

    [Header("Create Name Panel")] [Space(5)]
    public GameObject CreateNamePanel; // Create Name Panel
    [SerializeField] private TMP_InputField inputProfileName; // Name 적는 InputField
    [SerializeField] private TMP_Text nameErrorLog; // Name 오류 출력할 Text

    [Header("Create Image Panel")] [Space(5)]
    [SerializeField] private GameObject createImagePanel; // Create Image Panel
    [SerializeField] private GameObject iconPanel; // 이미지 Icon Panel
    [SerializeField] private GameObject checkPanel; // 사진 촬영 Panel
    public int imageIndex; // Icon의 Index

    [Header("Current Profile Panel")] [Space(5)]
    public GameObject CurrentProfilePanel; // Current Profile panel
    public TMP_Text SelectProfileText; // 선택된 Profile의 Name 출력하는 Text
    public Image SelectProfileImage; // 선택된 Profile의 Image 출력하는 Image

    [Header("Delete Panel")] [Space(5)]
    public GameObject DeletePanel; // Delete Panel

    public bool isLobby = false; // 프로필을 선택하고 로비에 들어갔는지 아닌지 확인하는 Bool값

    #region Unity Callback
    private void OnEnable()
    {
        inputProfileName.onValidateInput += ValidateInput;
    }

    void Start()
    {
        ProfileManager.Instance.LoadOrCreateGUID();
        ProfileManager.Instance.PrintProfileList(profileParent);
    }

    private void OnDisable()
    {
        inputProfileName.onValidateInput -= ValidateInput;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerPress != null && eventData.pointerPress.GetComponent<Button>() != null)
        {
            return;
        }
        else
        {
            if (iconPanel.activeSelf)
            {
                ProfileManager.Instance.isImageSelect = false;
            }
        }
    }
    #endregion

    #region Other Method
    public void DeleteProfile()
    { // DeletePanel 속 Btn 연동 Method
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.DeleteProfile(ProfileManager.Instance.TempUserIndex);
        ProfileManager.Instance.PrintProfileList(profileParent);
        DeletePanel.SetActive(false);
        Enable_ExitBtn(true);
    }

    public void SendProfile(bool _player)
    { // Profile Add 하기 전 InputField에 저장된 이름을 변수에 저장해주는 Btn 연동 Method 
        // 각 조건들을 통과하면 (비속어, 초성, 글자수 제한 등) 등록 가능한 Name으로 판정하고 Profile Manager의 각 플레이어의 name 변수에 추가

        for (int i = 0; i < DataManager.instance.vulgarism_Arr.Length; i++)
        { // 비속어 체크
            if (inputProfileName.text.Contains(DataManager.instance.vulgarism_Arr[i]))
            {
                if (DataManager.instance.vulgarism_Arr[i] != string.Empty)
                {
                    PrintErrorLog(nameErrorLog, "비속어는 포함시킬 수 없습니다.");
                    inputProfileName.text = String.Empty;  
                    return;
                }
            }
        }

        for (int i = 0; i < inputProfileName.text.Length; i++)
        { // 초성 체크
            if (Regex.IsMatch(inputProfileName.text[i].ToString(), @"[^0-9a-zA-Z가-힣]"))
            {
                PrintErrorLog(nameErrorLog, "초성 입력은 불가능합니다.");
                inputProfileName.text = String.Empty;  
                return;
            }
        }

        if (inputProfileName.text != string.Empty && inputProfileName.text.Length > 1)
        { // 글자수 체크
            ProfileManager.Instance.TempProfileName = inputProfileName.text;
            inputProfileName.text = string.Empty;
            CreateNamePanel.SetActive(false);
            createImagePanel.SetActive(true);
        }
        else
        {
            PrintErrorLog(nameErrorLog, "2~6글자의 이름을 입력해주세요.");
        }
    }

    private char ValidateInput(string text, int charIndex, char addedChar)
    { // Profile 영어, 숫자 입력 못하도록 설정
        // 입력된 문자가 영어 알파벳, 숫자인 경우 입력을 막음
        if ((addedChar >= 'a' && addedChar <= 'z') || (addedChar >= 'A' && addedChar <= 'Z') || (addedChar >= '0' && addedChar <= '9'))
        {
            PrintErrorLog(nameErrorLog, "한글로 입력 해주세요.");
            return '\0'; // 입력 막음
        }

        // 다른 문자는 허용
        return addedChar;
    }

    public void ImageSetting(bool _mode)
    { // 사진찍기인지, 이미지 고르기인지 확인하고, 1P의 Image를 세팅하는 Btn 연동 Method
        if(ProfileManager.Instance.ImageSet(_mode, true, ProfileManager.Instance.TempProfileName, imageIndex, nameErrorLog))
        { // 프로필 세팅 완료 했을 때
          // 로비에서는 본인의 Profile List를 제외하고 출력해야함
            ProfileManager.Instance.PrintProfileList(profileParent);

            // Panel들 Active
            if (_mode)
            { // 이미지 고르기 버튼 눌렀을 때
                iconPanel.SetActive(false);
                createImagePanel.SetActive(false);
                SelectProfilePanel.SetActive(true);
                if (ProfileManager.Instance.isUpdate)
                {
                    ProfileManager.Instance.isUpdate = false;
                }
                if (!isLobby)
                {
                    ExitBtn.gameObject.SetActive(true);
                }
            }
            else
            { // 사진 찍기 버튼 눌렀을 때
                checkPanel.SetActive(true);
                createImagePanel.SetActive(false);
            }
        }
        else
        { // 세팅 도중 오류가 나왔을 때
            Debug.Log("등록 실패");
            return;
        }
    }

    public void SelectImage(int index)
    {
        imageIndex = index;
        ProfileManager.Instance.isImageSelect = true;
    }

    public void Enable_ExitBtn(bool _enable)
    {
        if (_enable)
        {
            ExitBtn.interactable = true;
        }
        else
        {
            ExitBtn.interactable = false;
        }
    }

    private void PrintErrorLog(TMP_Text logObj, string log)
    {
        if (DialogManager.instance.log_co != null)
        {
            StopCoroutine(DialogManager.instance.log_co);
        }
        DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(logObj, log));
    }

    public void TakeAgainPicture()
    { // 사진 찍고난 뒤 다시 찍기 Btn 연동 Method
        SQL_Manager.instance.SQL_DeleteProfile(ProfileManager.Instance.TempUserIndex);
        cameraManager.CameraOpen(CaptureImage);
    }

    #region Active
    public void SelectProfilePanelCreateBtn()
    { // Select Profile Panel 속 CreateBtn 연동 Method
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.isUpdate = false;
        CreateNamePanel.SetActive(true);
        if(!isLobby)
        { // 메인 로비에서는 종료 버튼이 활성화 되면 안됨
            ExitBtn.gameObject.SetActive(false);
        }
    }

    public void SelectProfilePanelDeleteBtn()
    { // SelectProfilePanel 속 Btn 연동 Method
        // Profile List의 Count만큼 Profile Panel의 삭제 체크박스 켜주기
        if (ProfileManager.Instance.ProfilePanelList.Count > 0)
        { 
            bool active = ProfileManager.Instance.ProfilePanelList[0].GetComponent<ProfileInfo>().DeleteButton.activeSelf;
            for (int i = 0; i < ProfileManager.Instance.ProfilePanelList.Count; i++)
            {
                ProfileManager.Instance.ProfilePanelList[i].GetComponent<ProfileInfo>().DeleteButton.SetActive(!active);
            }
            if(!isLobby)
            { // 메인 로비에서는 종료 버튼이 활성화 되면 안됨
                ExitBtn.interactable = active;
            }
        }
    }

    public void CreateNamePanelBackBtn()
    { // Create Name Panel 속 BackBtn 연동 Method
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        CreateNamePanel.SetActive(false);

        // 로비에서는 본인의 Profile List를 제외하고 출력해야함
        ProfileManager.Instance.PrintProfileList(profileParent);
        
        if (ProfileManager.Instance.isUpdate)
        { //수정모드이면 뒤로가기 눌렀을 때 눌린 프로필 띄우기
            CurrentProfilePanel.SetActive(true);
        }
        else
        {
            if (ProfileManager.Instance.isProfileSelected && !isLobby)
            {
                CurrentProfilePanel.SetActive(true);
            }
            else
            {
                profileScrollView.normalizedPosition = new Vector2(1f, 1f);
                SelectProfilePanel.SetActive(true);
                if(!isLobby)
                { // 메인 로비에선 종료 버튼이 활성화 되면 안됨
                    ExitBtn.gameObject.SetActive(true);
                }
            }
        }
    }

    public void CreateImagePanelIconBackBtn()
    { // Create Image Panel 속 Icon Panel의 BackBtn 연동 Method
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        iconPanel.SetActive(false);

        // 로비에서는 본인의 Profile List를 제외하고 출력해야함
        ProfileManager.Instance.PrintProfileList(profileParent);
    }

    public void CreateImagePanelPictureConfirmBtn()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        CaptureImage.sprite = null;
        checkPanel.SetActive(false);
        createImagePanel.SetActive(false);
        ProfileManager.Instance.PrintProfileList(profileParent);
    }

    public void CurrentProfilePanelSelectBtn()
    { // Current Profile Panel 속 SelectBtn 연동 Method
        if (!ProfileManager.Instance.isProfileSelected)
        {
            ProfileManager.Instance.isProfileSelected = true;
        }
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        isLobby = true;
        GameModePanel.SetActive(true);
        MainButtonPanel.SetActive(true);
        CurrentProfilePanel.SetActive(false);
        gameObject.SetActive(false);
    }

    public void CurrentProfilePanelChangeBtn()
    { // Current Profile Panel 속 ChangeBtn 연동 Method
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        ProfileManager.Instance.isUpdate = true;

        CurrentProfilePanel.SetActive(false);
        CreateNamePanel.SetActive(true);
    }

    public void CurrentProfilePanelReturnBtn()
    { // Current Profile Panel 속 ReturnBtn 연동 Method
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        profileScrollView.normalizedPosition = new Vector2(1f, 1f);
        CurrentProfilePanel.SetActive(false);
        SelectProfilePanel.SetActive(true);
        if(!isLobby)
        { // 메인 로비에선 종료 버튼이 활성화 되면 안됨
            ExitBtn.gameObject.SetActive(true);
        }
    }
    #endregion
    #endregion
}
