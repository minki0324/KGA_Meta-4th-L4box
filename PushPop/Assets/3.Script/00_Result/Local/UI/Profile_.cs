using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text.RegularExpressions;


/// <summary>
/// Profile 관련 행동 처리 Class
/// </summary>
public class Profile_ : MonoBehaviour, IPointerClickHandler
{
    [Header("Profile_Panel")]
    public GameObject SelectProfilePanel;
    public GameObject CreateNamePanel;
    public GameObject CreateImagePanel;
    public GameObject CurrnetProfilePanel;
    public GameObject IconPanel;
    public GameObject CheckPanel;
    public GameObject PicturePanel;

    [Header("Button")]
    [SerializeField] private Button profileCreateBtn;

    [Header("GUID")]
    private string uniqueID;

    [Header("Other Object")]
    [SerializeField] private GameObject profilePanel;
    [SerializeField] private TMP_Text profileLog;
    [SerializeField] private TMP_Text nameLog;
    [SerializeField] private TMP_InputField _profileNameAdd;
    [SerializeField] private Transform panelParent;
    [SerializeField] private List<GameObject> panelList;
    public GameObject DeletePanel;
    public Image ProfileImage;

    [Header("Image")]
    public int _imageIndex = -1;
    private string _imagePath = string.Empty;   // Camera Image Save Path

    [Header("Text")]
    public TMP_Text SelectName;
    private string _profileName = string.Empty;

    [Header("bool")]
    public bool _isImageSelect = false;
    public bool _isUpdate = false;
    
    #region Unity Callback
    private void OnEnable()
    {
        SelectProfilePanel.SetActive(true);
        _profileNameAdd.onValidateInput += ValidateInput;
        _profileNameAdd.characterLimit = 6;
//        _profileNameAdd.onValueChanged.AddListener(
//    (word) => _profileNameAdd.text = Regex.Replace(word, @"[^0-9a-zA-Z가-힣]", "")
//);
    }

    private void Start()
    {
        // GUID를 확인하고, GUID에 맞는 UID index를 설정
        LoadOrCreateGUID();

        Debug.Log("Device GUID: " + uniqueID);

        // 한글 입력만 가능하도록 이벤트 추가
    }

    private void OnDisable()
    {
        _profileNameAdd.onValidateInput -= ValidateInput;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerPress != null && eventData.pointerPress.GetComponent<Button>() != null)
        {
            return;
        }
        else
        {
            if (IconPanel.activeSelf)
            {
                _isImageSelect = false;
            }
        }
    }
    #endregion

    #region Other Method
    // 게임 접속시 PlayerPrefs에 GUID를 검색하고, 기존 GUID가 있다면 해당 GUID 기반으로 DB의 UID 연동
    // 첫 접속시 PlayerPrefs에 GUID를 부여하고 해당 GUID로 DB에 User를 등록하면서 UID 연동
    private void LoadOrCreateGUID()
    {
        if (PlayerPrefs.HasKey("DeviceGUID"))
        { // 기존 GUID가 있는 경우 해당 GUID를 변수에 담아줌
            uniqueID = PlayerPrefs.GetString("DeviceGUID");
        }
        else
        { // 첫 접속시 GUID를 부여하고 해당 GUID를 변수에 담아줌
            uniqueID = Guid.NewGuid().ToString();

            // PlayerPrefs에 GUID를 저장
            PlayerPrefs.SetString("DeviceGUID", uniqueID);
            PlayerPrefs.Save();
        }
        // GUID를 가지고 DB와 연동하여 UID를 부여받음
        SQL_Manager.instance.SQL_AddUser(uniqueID);

        // 해당 UID가 가지고 있는 Profile들을 출력
        PrintProfileList();
    }

    
    public void AddProfile()
    {
        int imageMode = -1;
        switch (GameManager.Instance.IsImageMode)
        {
            case false: //  사진 찍기를 선택했을 때
                imageMode = 0;
                break;
            case true:  //  Default 이미지를 선택했을 때
                imageMode = 1;
                break;
        }
        if (!_isUpdate)
        { // 첫 등록일 때
            if (!string.IsNullOrWhiteSpace(_profileName))
            {
                GameManager.Instance.ProfileIndex = SQL_Manager.instance.SQL_AddProfile(_profileName, imageMode);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(_profileName))
                {
                    Debug.Log("�ùٸ� ������ �г����� �Է����ּ���.");
                }
            }
        }
        else if(_isUpdate)
        { // 수정 중일 때
            SQL_Manager.instance.SQL_UpdateMode(imageMode, GameManager.Instance.UID, GameManager.Instance.ProfileIndex);
        }
    }

    // Profile List 셋팅 후 Image, Name을 출력하는 Method
    public void PrintProfileList()
    {
        _profileNameAdd.text = string.Empty;

        // DB에 UID별로 저장되어있는 Profile들을 SQL_Manager에 List Up 해놓음
        SQL_Manager.instance.SQL_ProfileListSet();

        for (int i = 0; i < panelList.Count; i++)
        { // 출력 전 기존에 출력되어 있는 List가 있다면 초기화
            Destroy(panelList[i].gameObject);
        }
        panelList.Clear();
        
        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        { // SQL_Manager에 Query문을 이용하여 UID에 담긴 Profile만큼 List를 셋팅하고, 해당 List의 Count 만큼 Profile Panel 생성
            GameObject panel = Instantiate(profilePanel);
            panel.transform.SetParent(panelParent);
            panelList.Add(panel);
        }

        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        { // Panel의 Index 별로 Profile_Information 컴포넌트를 가져와서 name과 image를 Mode에 맞게 셋팅
            Profile_Information info = panelList[i].GetComponent<Profile_Information>();

            // 각 infomation 프로필 이름 출력
            info.Profile_name.text = SQL_Manager.instance.Profile_list[i].name;

            // 각 information 프로필 이미지 출력
            SQL_Manager.instance.PrintProfileImage(SQL_Manager.instance.Profile_list[i].imageMode, info.ProfileImage, SQL_Manager.instance.Profile_list[i].index);
        }
    }

    // 프로필 삭제 Btn 연동 Method
    public void DeleteProfile()
    {
        SQL_Manager.instance.SQL_DeleteProfile(GameManager.Instance.ProfileName, GameManager.Instance.ProfileIndex);
    }

    // Update Mode Bool값 설정 Btn 연동 Method
    public void Update_Profile()
    {
        _isUpdate = true;
    }

    //������ �ڷΰ���Btn ���� Method
    public void Back_Profile()
    {
        SelectProfilePanel.SetActive(false);
        CreateImagePanel.SetActive(false);
        CreateNamePanel.SetActive(false);
        CurrnetProfilePanel.SetActive(false);
        gameObject.SetActive(false);
    }

    public void ImageSet(int index)
    { // Profile에 넣을 Image 셋팅하는 Btn 연동 Method
        if (!_isUpdate)
        { // 첫 등록일 때
            if (index.Equals(0))
            { // 사진 찍기 버튼 눌렀을 때
                _imagePath = $"{Application.persistentDataPath}/Profile/{GameManager.Instance.UID}_{GameManager.Instance.ProfileIndex}.png";
                SQL_Manager.instance.SQL_AddProfileImage($"{_imagePath}", GameManager.Instance.UID, GameManager.Instance.ProfileIndex);

                PrintProfileList();
                CheckPanel.SetActive(false);
                CreateImagePanel.SetActive(false);
            }
            else if (index.Equals(1))
            { // 이미지 고르기 버튼 눌렀을 때
                if (!_isImageSelect)
                { // 이미지 선택을 안했을 때
                    if (DialogManager.instance.log_co != null)
                    {
                        StopCoroutine(DialogManager.instance.log_co);
                    }
                    DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(nameLog, "이미지를 선택해주세요."));
                }
                else
                { // 선택한 이미지가 있을 때
                    GameManager.Instance.IsImageMode = true;
                    AddProfile();
                    SQL_Manager.instance.SQL_AddProfileImage(_imageIndex, GameManager.Instance.UID, GameManager.Instance.ProfileIndex);

                    PrintProfileList();
                    IconPanel.SetActive(false);
                    CreateImagePanel.SetActive(false);
                    SelectProfilePanel.SetActive(true);
                }
            }
        }
        else if (_isUpdate)
        { // 수정모드일 때
            if(index.Equals(0))
            { // 사진찍기 모드 눌렀을 때

            }
            else if(index.Equals(1))
            { // 이미지 고르기 버튼 눌렀을 때
                if (!_isImageSelect)
                { // 선택한 이미지가 없을 때
                    if (DialogManager.instance.log_co != null)
                    {
                        StopCoroutine(DialogManager.instance.log_co);
                    }
                    DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(nameLog, "이미지를 선택해주세요."));
                }
                else
                { // 선택한 이미지가 있을 때
                    GameManager.Instance.IsImageMode = true;
                    AddProfile();
                    SQL_Manager.instance.SQL_UpdateProfile(GameManager.Instance.ProfileIndex, _profileName, GameManager.Instance.UID, _imageIndex);

                    PrintProfileList();
                    IconPanel.SetActive(false);
                    CreateImagePanel.SetActive(false);
                    SelectProfilePanel.SetActive(true);
                    _isUpdate = false;
                }
            }
        }
    }

    // 삭제 버튼 List만큼 출력
    public void DeleteBtnOpen()
    {
        if (panelList.Count > 0)
        {
            bool active = panelList[0].GetComponent<Profile_Information>().DelBtn.activeSelf;
            for (int i = 0; i < panelList.Count; i++)
            {
                panelList[i].GetComponent<Profile_Information>().DelBtn.SetActive(!active);
            }
        }
        else
        {
            // 삭제할 프로필 로그 출력
            return;
        }
    }

    // Profile Add 하기 전 InputField에 저장된 이름을 변수에 저장해주는 Btn 연동 Method
    public void SendProfile()
    {
        bool bPossibleName = true;

        for(int i = 0; i < _profileNameAdd.text.Length; i++)
        {
            if (Regex.IsMatch(_profileNameAdd.text[i].ToString(), @"[^0-9a-zA-Z가-힣]"))
            {
                if (DialogManager.instance.log_co != null)
                {
                    StopCoroutine(DialogManager.instance.log_co);
                }
                DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(nameLog, "초성 입력은 불가능합니다."));
                //_profileNameAdd.text = Regex.Replace(_profileNameAdd.text, @"[^0-9a-zA-Z가-힣]", string.Empty); //초성만 지우는애
                _profileNameAdd.text = String.Empty;    //다지우는애
                bPossibleName = false;
            }
        }

        if (bPossibleName)
        {
            if (_profileNameAdd.text != string.Empty && _profileNameAdd.text.Length > 1)
            {
                _profileName = _profileNameAdd.text;
                _profileNameAdd.text = string.Empty;
                CreateNamePanel.SetActive(false);
                CreateImagePanel.SetActive(true);
            }
            else
            {
                if (DialogManager.instance.log_co != null)
                {
                    StopCoroutine(DialogManager.instance.log_co);
                }
                DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(nameLog, "2~6글자의 이름을 입력해주세요."));
            }
        }
    }

    // Profile Image Index를 저장하는 Method
    public void SelectImage(int index)
    {
        _imageIndex = index;
        _isImageSelect = true;
    }

    // Profile 영어, 숫자 입력 못하도록 설정
    private char ValidateInput(string text, int charIndex, char addedChar)
    {
        // 입력된 문자가 영어 알파벳, 숫자인 경우 입력을 막음
        if ((addedChar >= 'a' && addedChar <= 'z') || (addedChar >= 'A' && addedChar <= 'Z') || (addedChar >= '0' && addedChar <= '9'))
        {
            if (DialogManager.instance.log_co != null)
            {
                StopCoroutine(DialogManager.instance.log_co);
            }
            DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(nameLog, "한글로 입력 해주세요."));
            return '\0'; // 입력 막음
        }

        // 다른 문자는 허용
        return addedChar;
    }


    public void BackBtn_CreateNamePanel_Clicked()
    {

        AudioManager.instance.SetCommonAudioClip_SFX(3);
        CreateNamePanel.SetActive(false);
        PrintProfileList();
        SelectProfilePanel.SetActive(true);
    }

    public void BackBtn_IconPanel_Clicked()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        IconPanel.SetActive(false);
        
        
        PrintProfileList();
    }

    public void SelectBtn_CurrentProfilePanel_Clicked()
    {
         
    }

    public void ChangeBtn_CurrentProfilePanel_Clicked()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        CurrnetProfilePanel.SetActive(false);
        CreateNamePanel.SetActive(true);
    }

    public void ReturnBtn_CurrentProfilePanel_Clicked()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        CurrnetProfilePanel.SetActive(false);
        SelectProfilePanel.SetActive(true);
    }

    #endregion
}
