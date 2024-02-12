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
    [SerializeField] private GameObject _errorLog;
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
        // ����� ���� ID�� �ҷ����ų� ����
        LoadOrCreateGUID();

        Debug.Log("Device GUID: " + _uniqueID);
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
    // 게임 첫 시작시 GUID를 지니고 있는지 확인 후 없다면 생성하고 있다면 PlayerPrefs에 저장
    // SQL_Manager에 해당 GUID가 DB에 있는지 체크 후 없다면 생성, 있다면 GUID에 맞는 UID를 런타임중에 저장하여 각종 정보를 DB와 연결
    private void LoadOrCreateGUID()
    {
        // ����� GUID �ҷ�����
        if (PlayerPrefs.HasKey("DeviceGUID"))
        {
            _uniqueID = PlayerPrefs.GetString("DeviceGUID");
        }
        else
        {
            // ���ο� GUID ����
            _uniqueID = Guid.NewGuid().ToString();

            // ������ GUID ����
            PlayerPrefs.SetString("DeviceGUID", _uniqueID);
            PlayerPrefs.Save();
        }
        SQL_Manager.instance.SQL_AddUser(_uniqueID);
        PrintProfileList();
    }

    // 프로필 이미지를 사진으로 할지, 이미지 선택으로 할지 정한 후 해당 정보를 DB에 공유하고 프로필 생성
    public void AddProfile()
    {
        if (!string.IsNullOrWhiteSpace(_profileName))
        {
            int imageMode = -1;
            switch(GameManager.instance._isImageMode)
            {
                case false: //  사진 찍기를 선택했을 때
                    imageMode = 0;
                    break;
                case true:  //  Default 이미지를 선택했을 때
                    imageMode = 1;
                    break;
            }
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

    // 프로필 리스트 출력 Btn 연동 Method
    public void PrintProfileList()
    {
        SQL_Manager.instance.SQL_ProfileListSet();

        // �ڷΰ��� ��ư ������ �̹� ���� �Ǿ����� ��� �ʱ�ȭ
        for (int i = 0; i < _panelList.Count; i++)
        {
            Destroy(_panelList[i].gameObject);
        }
        _panelList.Clear();

        // List�� Count��� Panel����
        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        {
            GameObject panel = Instantiate(_profilePanel);
            panel.transform.SetParent(_panelParent);
            _panelList.Add(panel);
        }

        // Profile Index�� �°� ������ name ���
        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        {
            Profile_Information info = _panelList[i].GetComponent<Profile_Information>();
            info.Profile_name.text = SQL_Manager.instance.Profile_list[i].name;
            if (SQL_Manager.instance.Profile_list[i].imageMode) // 이미지를 선택한 Profile일 경우
            {
                info.ProfileImage.sprite = GameManager.instance.ProfileImages[SQL_Manager.instance.Profile_list[i].defaultImage];
            }
            else // 사진찍기를 선택한 Profile일 경우
            {
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

    // Profile 수정 Btn 연동 Method
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

    // ������ �̹��� �����ϴ� Btn ���� Method (�Ű� ���� 0 �̸� ���� �Կ� Btn, �Ű� ���� 1 �̸� Image Select Btn)
    public void ImageSet(int index)
    {
        if (!_isUpdate)
        {   // 수정이 아닐 때 (첫 등록일 때)
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
                { // 선택된 이미지가 없을 때
                    if (log != null)
                    {
                        StopCoroutine(log);
                    }
                    log = StartCoroutine(PrintLog_co());
                }
                else
                { 
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
        { // 프로필 수정으로 들어왔을 때
            if(index.Equals(0))
            { // 사진 찍기 버튼 눌렀을 때

            }
            else if(index.Equals(1))
            { // 이미지 고르기 버튼 눌렀을 때
                if (!_isImageSelect)
                { // 선택된 이미지가 없을 때
                    if (log != null)
                    {
                        StopCoroutine(log);
                    }
                    log = StartCoroutine(PrintLog_co());
                }
                else
                {
                    GameManager.instance._isImageMode = true;
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

    // ���� btn �Ѵ� Method
    public void DeleteBtnOpen()
    {
        bool active = _panelList[0].GetComponent<Profile_Information>().DelBtn.activeSelf;
        for (int i = 0; i < _panelList.Count; i++)
        {
            _panelList[i].GetComponent<Profile_Information>().DelBtn.SetActive(!active);
        }
    }

    // Profile Add할 name을 저장해놓는 Btn 연동 Method
    public void SendProfile()
    {
        _profileName = _profileNameAdd.text;
        _profileNameAdd.text = string.Empty;

        /*SQL_Manager.instance.SQL_ProfileListSet();
        GameManager.instance.Profile_Index = SQL_Manager.instance.Profile_list[SQL_Manager.instance.Profile_list.Count - 1].index+1;*/
    }

    // Profile Image 인덱스 번호 전달 Btn 연동 Method
    public void SelectImage(int index)
    {
        _imageIndex = index;
        _isImageSelect = true;
    }

    private IEnumerator PrintLog_co()
    {
        _errorLog.SetActive(true);

        yield return new WaitForSeconds(3f);

        _errorLog.SetActive(false);
        log = null;
    }
    #endregion
}
