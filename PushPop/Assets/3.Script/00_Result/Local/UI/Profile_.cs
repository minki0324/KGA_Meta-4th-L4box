using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Profile_ : MonoBehaviour, IPointerClickHandler 
{
    [Header("Profile_Panel")]
    public GameObject SelectProfilePanel;
    public GameObject CreateNamePanel;
    public GameObject createImage_Panel;
    public GameObject currnetProfile_Panel;
    public GameObject IconPanel;

    [Header("Button")]
    [SerializeField] private Button Profile_Create;

    [Header("GUID")]
    private string _uniqueID;

    [Header("Other Object")]
    [SerializeField] private GameObject Profile_Panel;
    [SerializeField] private GameObject _errorLog;
    [SerializeField] private TMP_InputField _profileNameAdd;
    [SerializeField] private Transform Panel_Parent;
    [SerializeField] private List<GameObject> Panel_List;
    public TMP_Text Select_Name;
    public GameObject _deletePanel;

    [Header("Image")]
    public int _imageIndex = -1;
    private string _imagePath = string.Empty;
    [SerializeField] private Sprite[] _profileSelectImage;
    [SerializeField] private string _selectImage = string.Empty;

    [Header("Text")]
    private string _profileName = string.Empty;

    [Header("bool")]
    public bool _isImageSelect = false;

    private Coroutine log;

    #region Unity Callback
    private void Start()
    {
        _imagePath = Application.persistentDataPath + "/PushPop_";
        // 기기의 고유 ID를 불러오거나 생성
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
            if(IconPanel.activeSelf)
            {
                _isImageSelect = false;
            }
        }
    }
    #endregion

    #region Other Method
    // 접속했을 때 GUID가 있는지 확인하고, 없으면 생성
    // GUID를 DB와 연동하여 UID에 따른 프로필 연동하는 Method
    private void LoadOrCreateGUID()
    {
        // 저장된 GUID 불러오기
        if (PlayerPrefs.HasKey("DeviceGUID"))
        {
            _uniqueID = PlayerPrefs.GetString("DeviceGUID");
        }
        else
        {
            // 새로운 GUID 생성
            _uniqueID = Guid.NewGuid().ToString();

            // 생성된 GUID 저장
            PlayerPrefs.SetString("DeviceGUID", _uniqueID);
            PlayerPrefs.Save();
        }
        SQL_Manager.instance.Add_User(_uniqueID);
        PrintProfile();
    }

    // 프로필 생성 Btn 연동 Method
    public void Add_Profile()
    {
        if (!string.IsNullOrWhiteSpace(_profileName))
        {
            SQL_Manager.instance.SQL_AddProfile(_profileName);
        }
        else
        {
            if (string.IsNullOrWhiteSpace(_profileName))
            {
                Debug.Log("올바른 프로필 닉네임을 입력해주세요.");
            }
        }
    }

    // 프로필 출력 Btn 연동 Method
    public void PrintProfile()
    {
        SQL_Manager.instance.SQL_ProfileListSet();

        // 뒤로가기 버튼 등으로 이미 생성 되어있을 경우 초기화
        for (int i = 0; i < Panel_List.Count; i++)
        {
            Destroy(Panel_List[i].gameObject);
        }
        Panel_List.Clear();

        // List의 Count대로 Panel생성
        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        {
            GameObject panel = Instantiate(Profile_Panel);
            panel.transform.SetParent(Panel_Parent);
            Panel_List.Add(panel);
        }

        // Profile Index에 맞게 프로필 name 출력
        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        {
            Profile_Information info = Panel_List[i].GetComponent<Profile_Information>();
            info.Profile_name.text = SQL_Manager.instance.Profile_list[i].name;
            Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(GameManager.instance.UID, SQL_Manager.instance.Profile_list[i].index);
            Sprite profileSprite = TextureToSprite(profileTexture);
            info.ProfileImage.sprite = profileSprite;
        }
    }

    // 프로필 삭제 Btn 연동 Method
    public void DeleteProfile()
    {
        SQL_Manager.instance.SQL_DeleteProfile(GameManager.instance.Profile_name, GameManager.instance.Profile_Index);
    }

    // 프로필 수정 Btn 연동 Method
    public void Update_Profile()
    {

    }

    // 프로필 이미지 저장하는 Btn 연동 Method (매개 변수 0 이면 사진 촬영 Btn, 매개 변수 1 이면 Image Select Btn)
    public void ImageSet(int index)
    {
        // 경로가 존재하는지 확인하고 없다면 생성
        if (!Directory.Exists(_imagePath))
        {
            Directory.CreateDirectory(_imagePath);
        }

        // 만약에 사진촬영을 했을 때
        if(index == 0)
        {
            /*SQL_Manager.instance.SQL_AddProfileImage($"{_imagePath}/{GameManager.instance.UID}_{GameManager.instance.Profile_Index}.png", GameManager.instance.UID, GameManager.instance.Profile_Index);*/
        }
        // 사진 촬영 말고 기본 이미지 선택을 골랐을 때
        else if(index == 1)
        {
            if (!_isImageSelect)
            {
                // 이미지를 골라주세요 팝업 문구 생성
                if(log != null)
                {
                    StopCoroutine(log);
                }
                log = StartCoroutine(PrintLog_co());
            }
            else
            {
                _selectImage = _profileSelectImage[_imageIndex].name;
                Add_Profile();
                SQL_Manager.instance.SQL_ProfileListSet();
                GameManager.instance.Profile_Index = SQL_Manager.instance.Profile_list[SQL_Manager.instance.Profile_list.Count - 1].index;
                SQL_Manager.instance.SQL_AddProfileImage($"{_imagePath}/{_selectImage}.png", GameManager.instance.UID, GameManager.instance.Profile_Index);
                PrintProfile();
                IconPanel.SetActive(false);
                createImage_Panel.SetActive(false);
            }
        }

    }

    // 삭제 btn 켜는 Method
    public void DeleteBtnOpen()
    {
        bool active = Panel_List[0].GetComponent<Profile_Information>().DelBtn.activeSelf;
        for (int i =0; i < Panel_List.Count; i++)
        {
            Panel_List[i].GetComponent<Profile_Information>().DelBtn.SetActive(!active);
        }
    }

    // 프로필 선택 후 씬 변경 Method
    public void Next_Scene()
    {
        SceneManager.LoadScene(1);
    }

    // SQL에서 받아온 Texture를 Sprite형식으로 변환하는 Method
    private Sprite TextureToSprite(Texture2D texture)
    {
        // Texture2D를 Sprite로 변환합니다.
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

        return sprite;
    }

    // Profile Add할때 적힌 Text를 멤버변수에 할당
    public void SendProfile()
    {
        _profileName = _profileNameAdd.text;
        _profileNameAdd.text = string.Empty;
    }

    // Profile Image 선택했을 때 Btn 연동 Method
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
