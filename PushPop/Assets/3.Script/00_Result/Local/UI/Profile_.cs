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
            if(IconPanel.activeSelf)
            {
                _isImageSelect = false;
            }
        }
    }
    #endregion

    #region Other Method
    // �������� �� GUID�� �ִ��� Ȯ���ϰ�, ������ ����
    // GUID�� DB�� �����Ͽ� UID�� ���� ������ �����ϴ� Method
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
        PrintProfile();
    }

    // ������ ���� Btn ���� Method
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
                Debug.Log("�ùٸ� ������ �г����� �Է����ּ���.");
            }
        }
    }

    // ������ ��� Btn ���� Method
    public void PrintProfile()
    {
        SQL_Manager.instance.SQL_ProfileListSet();

        // �ڷΰ��� ��ư ������ �̹� ���� �Ǿ����� ��� �ʱ�ȭ
        for (int i = 0; i < Panel_List.Count; i++)
        {
            Destroy(Panel_List[i].gameObject);
        }
        Panel_List.Clear();

        // List�� Count��� Panel����
        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        {
            GameObject panel = Instantiate(Profile_Panel);
            panel.transform.SetParent(Panel_Parent);
            Panel_List.Add(panel);
        }

        // Profile Index�� �°� ������ name ���
        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        {
            Profile_Information info = Panel_List[i].GetComponent<Profile_Information>();
            info.Profile_name.text = SQL_Manager.instance.Profile_list[i].name;
            Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(GameManager.instance.UID, SQL_Manager.instance.Profile_list[i].index);
            Sprite profileSprite = TextureToSprite(profileTexture);
            info.ProfileImage.sprite = profileSprite;
        }
    }

    // ������ ���� Btn ���� Method
    public void DeleteProfile()
    {
        SQL_Manager.instance.SQL_DeleteProfile(GameManager.instance.Profile_name, GameManager.instance.Profile_Index);
    }

    // ������ ���� Btn ���� Method
    public void Update_Profile()
    {

    }

    //������ �ڷΰ���Btn ���� Method
    public void Back_Profile()
    {
        selectProfile_Panel.SetActive(false);
        createImage_Panel.SetActive(false);
        createName_Panel.SetActive(false);
        currnetProfile_Panel.SetActive(false);
        gameObject.SetActive(false);
    }

    // ������ �̹��� �����ϴ� Btn ���� Method (�Ű� ���� 0 �̸� ���� �Կ� Btn, �Ű� ���� 1 �̸� Image Select Btn)
    public void ImageSet(int index)
    {
        // ��ΰ� �����ϴ��� Ȯ���ϰ� ���ٸ� ����
        if (!Directory.Exists(_imagePath))
        {
            Directory.CreateDirectory(_imagePath);
        }

        // ���࿡ �����Կ��� ���� ��
        if(index == 0)
        {
            /*SQL_Manager.instance.SQL_AddProfileImage($"{_imagePath}/{GameManager.instance.UID}_{GameManager.instance.Profile_Index}.png", GameManager.instance.UID, GameManager.instance.Profile_Index);*/
        }
        // ���� �Կ� ���� �⺻ �̹��� ������ ����� ��
        else if(index == 1)
        {
            if (!_isImageSelect)
            {
                // �̹����� ����ּ��� �˾� ���� ����
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

    // ���� btn �Ѵ� Method
    public void DeleteBtnOpen()
    {
        bool active = Panel_List[0].GetComponent<Profile_Information>().DelBtn.activeSelf;
        for (int i =0; i < Panel_List.Count; i++)
        {
            Panel_List[i].GetComponent<Profile_Information>().DelBtn.SetActive(!active);
        }
    }

    // ������ ���� �� �� ���� Method
    public void Next_Scene()
    {
        SceneManager.LoadScene(1);
    }

    // SQL���� �޾ƿ� Texture�� Sprite�������� ��ȯ�ϴ� Method
    private Sprite TextureToSprite(Texture2D texture)
    {
        // Texture2D�� Sprite�� ��ȯ�մϴ�.
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

        return sprite;
    }

    // Profile Add�Ҷ� ���� Text�� ��������� �Ҵ�
    public void SendProfile()
    {
        _profileName = _profileNameAdd.text;
        _profileNameAdd.text = string.Empty;
    }

    // Profile Image �������� �� Btn ���� Method
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
