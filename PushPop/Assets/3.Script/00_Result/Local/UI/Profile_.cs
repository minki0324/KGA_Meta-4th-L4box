using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Profile_ : MonoBehaviour, IPointerClickHandler
{
    [Header("Profile_Panel")]
    public GameObject SelectProfilePanel;
    public GameObject CreateNamePanel;
    public GameObject CreateImagePanel;
    public GameObject CurrnetProfilePanel;
    public GameObject IconPanel;
    public GameObject CheckPanel;

    [Header("Button")]
    [SerializeField] private Button _profileCreateBtn;

    [Header("GUID")]
    private string _uniqueID;

    [Header("Other Object")]
    [SerializeField] private GameObject _profilePanel;
    [SerializeField] private GameObject _profileLog;
    [SerializeField] private GameObject _nameLog;
    [SerializeField] private TMP_InputField _profileNameAdd;
    [SerializeField] private Transform _panelParent;
    [SerializeField] private List<GameObject> _panelList;
    public GameObject _deletePanel;
    public Image _profileImage;

    [Header("Image")]
    public int _imageIndex = -1;
    private string _imagePath = string.Empty;

    [Header("Text")]
    public TMP_Text SelectName;
    private string _profileName = string.Empty;
    public string PreviousName = string.Empty;

    [Header("bool")]
    public bool _isImageSelect = false;
    public bool _isUpdate = false;
    
    private Coroutine log;

    #region Unity Callback
    private void OnEnable()
    {
        SelectProfilePanel.SetActive(true);
    }

    private void Start()
    {
        // GUID를 확인하고, GUID에 맞는 UID index를 설정
        LoadOrCreateGUID();

        Debug.Log("Device GUID: " + _uniqueID);

        // 한글 입력만 가능하도록 이벤트 추가
        _profileNameAdd.onValidateInput += ValidateInput;
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
            _uniqueID = PlayerPrefs.GetString("DeviceGUID");
        }
        else
        { // 첫 접속시 GUID를 부여하고 해당 GUID를 변수에 담아줌
            _uniqueID = Guid.NewGuid().ToString();

            // PlayerPrefs에 GUID를 저장
            PlayerPrefs.SetString("DeviceGUID", _uniqueID);
            PlayerPrefs.Save();
        }
        // GUID를 가지고 DB와 연동하여 UID를 부여받음
        SQL_Manager.instance.SQL_AddUser(_uniqueID);

        // 해당 UID가 가지고 있는 Profile들을 출력
        PrintProfileList();
    }

    
    public void AddProfile()
    {
        int imageMode = -1;
        switch (GameManager.instance._isImageMode)
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
                GameManager.instance.Profile_Index = SQL_Manager.instance.SQL_AddProfile(_profileName, imageMode);
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
            SQL_Manager.instance.SQL_UpdateMode(imageMode, GameManager.instance.UID, GameManager.instance.Profile_Index);
        }
    }

    // Profile List 셋팅 후 Image, Name을 출력하는 Method
    public void PrintProfileList()
    {
        // DB에 UID별로 저장되어있는 Profile들을 SQL_Manager에 List Up 해놓음
        SQL_Manager.instance.SQL_ProfileListSet();

        for (int i = 0; i < _panelList.Count; i++)
        { // 출력 전 기존에 출력되어 있는 List가 있다면 초기화
            Destroy(_panelList[i].gameObject);
        }
        _panelList.Clear();
        
        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        { // SQL_Manager에 Query문을 이용하여 UID에 담긴 Profile만큼 List를 셋팅하고, 해당 List의 Count 만큼 Profile Panel 생성
            GameObject panel = Instantiate(_profilePanel);
            panel.transform.SetParent(_panelParent);
            _panelList.Add(panel);
        }

        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        { // Panel의 Index 별로 Profile_Information 컴포넌트를 가져와서 name과 image를 Mode에 맞게 셋팅
            Profile_Information info = _panelList[i].GetComponent<Profile_Information>();
            info.Profile_name.text = SQL_Manager.instance.Profile_list[i].name;
            if (SQL_Manager.instance.Profile_list[i].imageMode)
            { // 이미지 선택으로 설정 했을 경우
                info.ProfileImage.sprite = GameManager.instance.ProfileImages[SQL_Manager.instance.Profile_list[i].defaultImage];
            }
            else 
            { // 사진 찍기로 설정 했을 경우
                Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(GameManager.instance.UID, SQL_Manager.instance.Profile_list[i].index);
                Sprite profileSprite = GameManager.instance.TextureToSprite(profileTexture);
                info.ProfileImage.sprite = profileSprite;
            }
        }
    }

    // 프로필 삭제 Btn 연동 Method
    public void DeleteProfile()
    {
        SQL_Manager.instance.SQL_DeleteProfile(GameManager.instance.Profile_name, GameManager.instance.Profile_Index);
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
                _imagePath = $"{Application.persistentDataPath}/Profile/{GameManager.instance.UID}_{GameManager.instance.Profile_Index}.png";
                SQL_Manager.instance.SQL_AddProfileImage($"{_imagePath}", GameManager.instance.UID, GameManager.instance.Profile_Index);

                PrintProfileList();
                CheckPanel.SetActive(false);
                CreateImagePanel.SetActive(false);
            }
            else if (index.Equals(1))
            { // 이미지 고르기 버튼 눌렀을 때
                if (!_isImageSelect)
                { // 이미지 선택을 안했을 때
                    if (log != null)
                    {
                        StopCoroutine(log);
                    }
                    log = StartCoroutine(PrintLog_co(_profileLog));
                }
                else
                { // 선택한 이미지가 있을 때
                    GameManager.instance._isImageMode = true;
                    AddProfile();
                    SQL_Manager.instance.SQL_AddProfileImage(_imageIndex, GameManager.instance.UID, GameManager.instance.Profile_Index);

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
                    if (log != null)
                    {
                        StopCoroutine(log);
                    }
                    log = StartCoroutine(PrintLog_co(_profileLog));
                }
                else
                { // 선택한 이미지가 있을 때
                    GameManager.instance._isImageMode = true;
                    AddProfile();
                    SQL_Manager.instance.SQL_UpdateProfile(GameManager.instance.Profile_Index, _profileName, GameManager.instance.UID, _imageIndex);

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
        if(_panelList.Count > 0)
        {
            bool active = _panelList[0].GetComponent<Profile_Information>().DelBtn.activeSelf;
            for (int i = 0; i < _panelList.Count; i++)
            {
                _panelList[i].GetComponent<Profile_Information>().DelBtn.SetActive(!active);
            }
        }
        else
        {
            // 삭제할 프로필 로그 출력
            return;
        }
    }

    // Profile Add?�� name?�� ????��?��?��?�� Btn ?��?�� Method
    public void SendProfile()
    {
        _profileName = _profileNameAdd.text;
        _profileNameAdd.text = string.Empty;

        /*SQL_Manager.instance.SQL_ProfileListSet();
        GameManager.instance.Profile_Index = SQL_Manager.instance.Profile_list[SQL_Manager.instance.Profile_list.Count - 1].index+1;*/
    }

    // Profile Image ?��?��?�� 번호 ?��?�� Btn ?��?�� Method
    public void SelectImage(int index)
    {
        _imageIndex = index;
        _isImageSelect = true;
    }

    private IEnumerator PrintLog_co(GameObject errorlog)
    {
        errorlog.SetActive(true);

        yield return new WaitForSeconds(3f);

        errorlog.SetActive(false);
        log = null;
    }

    // Profile 영어 입력 못하도록 설정
    private char ValidateInput(string text, int charIndex, char addedChar)
    {
        // 입력된 문자가 영어 알파벳, 숫자인 경우 입력을 막음
        if ((addedChar >= 'a' && addedChar <= 'z') || (addedChar >= 'A' && addedChar <= 'Z') || (addedChar >= '0' && addedChar <= '9'))
        {
            if (log != null)
            {
                StopCoroutine(log);
            }
            log = StartCoroutine(PrintLog_co(_nameLog));
            return '\0'; // 입력 막음
        }

        // 다른 문자는 허용
        return addedChar;
    }
    #endregion
}
