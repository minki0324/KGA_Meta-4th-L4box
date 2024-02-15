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
        // �����? ���� ID�� �ҷ����ų� ����
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
    // 게임 �? ?��?��?�� GUID�? �??���? ?��?���? ?��?�� ?�� ?��?���? ?��?��?���? ?��?���? PlayerPrefs?�� ????��
    // SQL_Manager?�� ?��?�� GUID�? DB?�� ?��?���? 체크 ?�� ?��?���? ?��?��, ?��?���? GUID?�� 맞는 UID�? ?��????��중에 ????��?��?�� 각종 ?��보�?? DB??? ?���?
    private void LoadOrCreateGUID()
    {
        // �����? GUID �ҷ�����
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

    // ?��로필 ?��미�??�? ?��진으�? ?���?, ?��미�?? ?��?��?���? ?���? ?��?�� ?�� ?��?�� ?��보�?? DB?�� 공유?���? ?��로필 ?��?��
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

    // ?��로필 리스?�� 출력 Btn ?��?�� Method
    public void PrintProfileList()
    {
        SQL_Manager.instance.SQL_ProfileListSet();

        // �ڷΰ��� ��ư ������ �̹� ���� �Ǿ����� ���? �ʱ�ȭ
        for (int i = 0; i < _panelList.Count; i++)
        {
            Destroy(_panelList[i].gameObject);
        }
        _panelList.Clear();

        // List�� Count���? Panel����
        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        {
            GameObject panel = Instantiate(_profilePanel);
            panel.transform.SetParent(_panelParent);
            _panelList.Add(panel);
        }

        // Profile Index�� �°� ������ name ���?
        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        {
            Profile_Information info = _panelList[i].GetComponent<Profile_Information>();
            info.Profile_name.text = SQL_Manager.instance.Profile_list[i].name;
            if (SQL_Manager.instance.Profile_list[i].imageMode) // ?��미�??�? ?��?��?�� Profile?�� 경우
            {
                info.ProfileImage.sprite = GameManager.Instance.ProfileImages[SQL_Manager.instance.Profile_list[i].defaultImage];
            }
            else // ?��진찍기�?? ?��?��?�� Profile?�� 경우
            {
                Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(GameManager.Instance.UID, SQL_Manager.instance.Profile_list[i].index);
                Sprite profileSprite = GameManager.Instance.TextureToSprite(profileTexture);
                info.ProfileImage.sprite = profileSprite;
            }
        }
    }

    // ?��로필 ?��?�� Btn ?��?�� Method
    public void DeleteProfile()
    {
        SQL_Manager.instance.SQL_DeleteProfile(GameManager.Instance.ProfileName, GameManager.Instance.ProfileIndex);
    }

    // Profile ?��?�� Btn ?��?�� Method
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
        {   // ?��?��?�� ?��?�� ?�� (�? ?��록일 ?��)
            if (index.Equals(0))
            { // ?���? 찍기 버튼 ?��????�� ?��
                _imagePath = $"{Application.persistentDataPath}/Profile/{GameManager.Instance.UID}_{GameManager.Instance.ProfileIndex}.png";
                SQL_Manager.instance.SQL_AddProfileImage($"{_imagePath}", GameManager.Instance.UID, GameManager.Instance.ProfileIndex);

                PrintProfileList();
                CheckPanel.SetActive(false);
                CreateImagePanel.SetActive(false);
            }
            else if (index.Equals(1))
            { // ?��미�?? 고르�? 버튼 ?��????�� ?��
                if (!_isImageSelect)
                { // ?��?��?�� ?��미�??�? ?��?�� ?��
                    if (log != null)
                    {
                        StopCoroutine(log);
                    }
                    log = StartCoroutine(PrintLog_co());
                }
                else
                { 
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
        { // ?��로필 ?��?��?���? ?��?��?��?�� ?��
            if(index.Equals(0))
            { // ?���? 찍기 버튼 ?��????�� ?��

            }
            else if(index.Equals(1))
            { // ?��미�?? 고르�? 버튼 ?��????�� ?��
                if (!_isImageSelect)
                { // ?��?��?�� ?��미�??�? ?��?�� ?��
                    if (log != null)
                    {
                        StopCoroutine(log);
                    }
                    log = StartCoroutine(PrintLog_co());
                }
                else
                {
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

    // ���� btn �Ѵ� Method
    public void DeleteBtnOpen()
    {
        bool active = _panelList[0].GetComponent<Profile_Information>().DelBtn.activeSelf;
        for (int i = 0; i < _panelList.Count; i++)
        {
            _panelList[i].GetComponent<Profile_Information>().DelBtn.SetActive(!active);
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

    private IEnumerator PrintLog_co()
    {
        _errorLog.SetActive(true);

        yield return new WaitForSeconds(3f);

        _errorLog.SetActive(false);
        log = null;
    }
    #endregion
}
