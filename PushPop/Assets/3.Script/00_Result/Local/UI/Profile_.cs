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
        // ï¿½ï¿½ï¿½ï¿½ï¿? ï¿½ï¿½ï¿½ï¿½ IDï¿½ï¿½ ï¿½Ò·ï¿½ï¿½ï¿½ï¿½Å³ï¿½ ï¿½ï¿½ï¿½ï¿½
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
    // ê²Œì„ ì²? ?‹œ?‘?‹œ GUIDë¥? ì§??‹ˆê³? ?ˆ?Š”ì§? ?™•?¸ ?›„ ?—†?‹¤ë©? ?ƒ?„±?•˜ê³? ?ˆ?‹¤ë©? PlayerPrefs?— ????¥
    // SQL_Manager?— ?•´?‹¹ GUIDê°? DB?— ?ˆ?Š”ì§? ì²´í¬ ?›„ ?—†?‹¤ë©? ?ƒ?„±, ?ˆ?‹¤ë©? GUID?— ë§ëŠ” UIDë¥? ?Ÿ°????„ì¤‘ì— ????¥?•˜?—¬ ê°ì¢… ? •ë³´ë?? DB??? ?—°ê²?
    private void LoadOrCreateGUID()
    {
        // ï¿½ï¿½ï¿½ï¿½ï¿? GUID ï¿½Ò·ï¿½ï¿½ï¿½ï¿½ï¿½
        if (PlayerPrefs.HasKey("DeviceGUID"))
        {
            _uniqueID = PlayerPrefs.GetString("DeviceGUID");
        }
        else
        {
            // ï¿½ï¿½ï¿½Î¿ï¿½ GUID ï¿½ï¿½ï¿½ï¿½
            _uniqueID = Guid.NewGuid().ToString();

            // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ GUID ï¿½ï¿½ï¿½ï¿½
            PlayerPrefs.SetString("DeviceGUID", _uniqueID);
            PlayerPrefs.Save();
        }
        SQL_Manager.instance.SQL_AddUser(_uniqueID);
        PrintProfileList();
    }

    // ?”„ë¡œí•„ ?´ë¯¸ì??ë¥? ?‚¬ì§„ìœ¼ë¡? ?• ì§?, ?´ë¯¸ì?? ?„ ?ƒ?œ¼ë¡? ?• ì§? ? •?•œ ?›„ ?•´?‹¹ ? •ë³´ë?? DB?— ê³µìœ ?•˜ê³? ?”„ë¡œí•„ ?ƒ?„±
    public void AddProfile()
    {
        if (!string.IsNullOrWhiteSpace(_profileName))
        {
            int imageMode = -1;
            switch(GameManager.instance._isImageMode)
            {
                case false: //  ?‚¬ì§? ì°ê¸°ë¥? ?„ ?ƒ?–ˆ?„ ?•Œ
                    imageMode = 0;
                    break;
                case true:  //  Default ?´ë¯¸ì??ë¥? ?„ ?ƒ?–ˆ?„ ?•Œ
                    imageMode = 1;
                    break;
            }
            GameManager.instance.Profile_Index = SQL_Manager.instance.SQL_AddProfile(_profileName, imageMode);
        }
        else
        {
            if (string.IsNullOrWhiteSpace(_profileName))
            {
                Debug.Log("ï¿½Ã¹Ù¸ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Ğ³ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Ô·ï¿½ï¿½ï¿½ï¿½Ö¼ï¿½ï¿½ï¿½.");
            }
        }
    }

    // ?”„ë¡œí•„ ë¦¬ìŠ¤?Š¸ ì¶œë ¥ Btn ?—°?™ Method
    public void PrintProfileList()
    {
        SQL_Manager.instance.SQL_ProfileListSet();

        // ï¿½Ú·Î°ï¿½ï¿½ï¿½ ï¿½ï¿½Æ° ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Ì¹ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½Ç¾ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿? ï¿½Ê±ï¿½È­
        for (int i = 0; i < _panelList.Count; i++)
        {
            Destroy(_panelList[i].gameObject);
        }
        _panelList.Clear();

        // Listï¿½ï¿½ Countï¿½ï¿½ï¿? Panelï¿½ï¿½ï¿½ï¿½
        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        {
            GameObject panel = Instantiate(_profilePanel);
            panel.transform.SetParent(_panelParent);
            _panelList.Add(panel);
        }

        // Profile Indexï¿½ï¿½ ï¿½Â°ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ name ï¿½ï¿½ï¿?
        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        {
            Profile_Information info = _panelList[i].GetComponent<Profile_Information>();
            info.Profile_name.text = SQL_Manager.instance.Profile_list[i].name;
            if (SQL_Manager.instance.Profile_list[i].imageMode) // ?´ë¯¸ì??ë¥? ?„ ?ƒ?•œ Profile?¼ ê²½ìš°
            {
                info.ProfileImage.sprite = GameManager.instance.ProfileImages[SQL_Manager.instance.Profile_list[i].defaultImage];
            }
            else // ?‚¬ì§„ì°ê¸°ë?? ?„ ?ƒ?•œ Profile?¼ ê²½ìš°
            {
                Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(GameManager.instance.UID, SQL_Manager.instance.Profile_list[i].index);
                Sprite profileSprite = GameManager.instance.TextureToSprite(profileTexture);
                info.ProfileImage.sprite = profileSprite;
            }
        }
    }

    // ?”„ë¡œí•„ ?‚­? œ Btn ?—°?™ Method
    public void DeleteProfile()
    {
        SQL_Manager.instance.SQL_DeleteProfile(GameManager.instance.Profile_name, GameManager.instance.Profile_Index);
    }

    // Profile ?ˆ˜? • Btn ?—°?™ Method
    public void Update_Profile()
    {
        _isUpdate = true;
    }

    //ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Ú·Î°ï¿½ï¿½ï¿½Btn ï¿½ï¿½ï¿½ï¿½ Method
    public void Back_Profile()
    {
        SelectProfilePanel.SetActive(false);
        CreateImagePanel.SetActive(false);
        CreateNamePanel.SetActive(false);
        CurrnetProfilePanel.SetActive(false);
        gameObject.SetActive(false);
    }

    // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Ì¹ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Ï´ï¿½ Btn ï¿½ï¿½ï¿½ï¿½ Method (ï¿½Å°ï¿½ ï¿½ï¿½ï¿½ï¿½ 0 ï¿½Ì¸ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½Ô¿ï¿½ Btn, ï¿½Å°ï¿½ ï¿½ï¿½ï¿½ï¿½ 1 ï¿½Ì¸ï¿½ Image Select Btn)
    public void ImageSet(int index)
    {
        if (!_isUpdate)
        {   // ?ˆ˜? •?´ ?•„?‹ ?•Œ (ì²? ?“±ë¡ì¼ ?•Œ)
            if (index.Equals(0))
            { // ?‚¬ì§? ì°ê¸° ë²„íŠ¼ ?ˆŒ????„ ?•Œ
                _imagePath = $"{Application.persistentDataPath}/Profile/{GameManager.instance.UID}_{GameManager.instance.Profile_Index}.png";
                SQL_Manager.instance.SQL_AddProfileImage($"{_imagePath}", GameManager.instance.UID, GameManager.instance.Profile_Index);

                PrintProfileList();
                CheckPanel.SetActive(false);
                CreateImagePanel.SetActive(false);
            }
            else if (index.Equals(1))
            { // ?´ë¯¸ì?? ê³ ë¥´ê¸? ë²„íŠ¼ ?ˆŒ????„ ?•Œ
                if (!_isImageSelect)
                { // ?„ ?ƒ?œ ?´ë¯¸ì??ê°? ?—†?„ ?•Œ
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
        { // ?”„ë¡œí•„ ?ˆ˜? •?œ¼ë¡? ?“¤?–´?™”?„ ?•Œ
            if(index.Equals(0))
            { // ?‚¬ì§? ì°ê¸° ë²„íŠ¼ ?ˆŒ????„ ?•Œ

            }
            else if(index.Equals(1))
            { // ?´ë¯¸ì?? ê³ ë¥´ê¸? ë²„íŠ¼ ?ˆŒ????„ ?•Œ
                if (!_isImageSelect)
                { // ?„ ?ƒ?œ ?´ë¯¸ì??ê°? ?—†?„ ?•Œ
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

    // ï¿½ï¿½ï¿½ï¿½ btn ï¿½Ñ´ï¿½ Method
    public void DeleteBtnOpen()
    {
        bool active = _panelList[0].GetComponent<Profile_Information>().DelBtn.activeSelf;
        for (int i = 0; i < _panelList.Count; i++)
        {
            _panelList[i].GetComponent<Profile_Information>().DelBtn.SetActive(!active);
        }
    }

    // Profile Add?•  name?„ ????¥?•´?†“?Š” Btn ?—°?™ Method
    public void SendProfile()
    {
        _profileName = _profileNameAdd.text;
        _profileNameAdd.text = string.Empty;

        /*SQL_Manager.instance.SQL_ProfileListSet();
        GameManager.instance.Profile_Index = SQL_Manager.instance.Profile_list[SQL_Manager.instance.Profile_list.Count - 1].index+1;*/
    }

    // Profile Image ?¸?±?Š¤ ë²ˆí˜¸ ? „?‹¬ Btn ?—°?™ Method
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
