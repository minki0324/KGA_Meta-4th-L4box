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
/// Profile ���� �ൿ ó�� Class
/// </summary>
public class ProfileManager : MonoBehaviour
{
    public static ProfileManager Instance;

    [Header("UID")] [Space(5)]
    private string uniqueID = string.Empty; // PlayerPrefs�� ����Ǵ� ���� GUID;
    public int UID = 0; // ��⺰ ���� ��ȣ > SQL�� ����

    [Header("1P Infomation")] [Space(5)]
    public string ProfileName1P = string.Empty; // 1P Profile Name
    public int ProfileIndex1P = 0; // 1P Profile Index
    public int DefaultImage1P = 0; // 1P Profile DefaultImage Index
    public bool IsImageMode1P = false; // false = �������, true = �̹��� ����
    public Sprite CacheProfileImage = null;

    [Header("2P Player")] [Space(5)]
    public string ProfileName2P = string.Empty; // 2P Profile Name
    public int ProfileIndex2P = 0; // 2P Profile Index
    public int DefaultImage2P = 0; // 2P Profile DefaultImage Index
    public bool IsimageMode2P = false; // false = �������, true = �̹��� ����

    [Header("Profile Component")] [Space(5)]
    public Sprite[] ProfileImages = null; // ProfileImage Sprites
    public Sprite NoneBackground = null; // Profile None Sprite
    public GameObject ProfilePanel = null; // Profile Panel
    public List<GameObject> ProfilePanelList = new List<GameObject>(); // Profile Panel List

    [Header("Other Request")] [Space(5)]
    private string imagePath = string.Empty; // Image Saving Path
    public bool isUpdate = false; // Profile Update ?
    public bool isImageSelect = false; // Profile Image Icon Select ?
    public bool isProfileSelected = false; // is Profile Select ?

    [Header("Temp Infomation")] [Space(5)]
    public int tempIndex = 0; // Profile ����� �� ����� �ӽ� ProfileIndex
    public string tempName = string.Empty; // Profile ����� �� ����� �ӽ� ProfileName

    #region Unity Callback
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    #endregion

    #region Other Method
    public void LoadOrCreateGUID()
    {
        if (PlayerPrefs.HasKey("DeviceGUID"))
        { // ���� GUID�� �ִ� ��� �ش� GUID�� ������ �����
            uniqueID = PlayerPrefs.GetString("DeviceGUID");
        }
        else
        { // ù ���ӽ� GUID�� �ο��ϰ� �ش� GUID�� ������ �����
            uniqueID = Guid.NewGuid().ToString();

            // PlayerPrefs�� GUID�� ����
            PlayerPrefs.SetString("DeviceGUID", uniqueID);
            PlayerPrefs.Save();
        }
        // GUID�� ������ DB�� �����Ͽ� UID�� �ο�����
        DebugLog.instance.Adding_Message(uniqueID);
        SQL_Manager.instance.SQL_AddUser(uniqueID);
    }

    /// <summary>
    /// SQL Manager�� �����Ͽ� �ű� Profile�� ����ϰų�, Update �ϴ� Method
    /// </summary>
    /// <param name="_profileName"></param>
    /// <param name="_profileIndex"></param>
    public void AddProfile(string _profileName, int _profileIndex, bool _imageMode)
    {
        int imageMode = -1;
        switch (_imageMode)
        {
            case false: //  ���� ��⸦ �������� ��
                imageMode = 0;
                break;
            case true:  //  Default �̹����� �������� ��
                imageMode = 1;
                break;
        }
        if (!isUpdate)
        { // ù ����� ��
            if (!string.IsNullOrWhiteSpace(_profileName))
            {
                tempIndex = SQL_Manager.instance.SQL_AddProfile(_profileName, imageMode);
            }
            else if (string.IsNullOrWhiteSpace(_profileName))
            {

            }
        }
        else if (isUpdate)
        { // ���� ���� ��
            SQL_Manager.instance.SQL_UpdateMode(imageMode, UID, _profileIndex);
        }
    }

    public void DeleteProfile(int _index)
    {
        SQL_Manager.instance.SQL_DeleteProfile(_index);
    }

    public void PrintProfileList(Transform parent, int? _profileIndex1P)
    {
        // DB�� UID���� ����Ǿ��ִ� Profile���� SQL_Manager�� List Up �س���
        SQL_Manager.instance.SQL_ProfileListSet();

        for (int i = 0; i < ProfilePanelList.Count; i++)
        { // ��� �� ������ ��µǾ� �ִ� List�� �ִٸ� �ʱ�ȭ
            Destroy(ProfilePanelList[i].gameObject);
        }
        ProfilePanelList.Clear();

        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        { // SQL_Manager�� Query���� �̿��Ͽ� UID�� ��� Profile��ŭ List�� �����ϰ�, �ش� List�� Count ��ŭ Profile Panel ����
            GameObject panel = Instantiate(ProfilePanel);
            panel.transform.SetParent(parent);
            ProfilePanelList.Add(panel);
        }

        if (_profileIndex1P != null)
        {
            DuplicateProfileDelete((int)_profileIndex1P);
        }

        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        { // Panel�� Index ���� Profile_Information ������Ʈ�� �����ͼ� name�� image�� Mode�� �°� ����
            NewProfile_Infomation info = ProfilePanelList[i].GetComponent<NewProfile_Infomation>();

            // �� infomation ������ �̸� ���
            info.Profile_name.text = SQL_Manager.instance.Profile_list[i].name;

            // �� information ������ �̹��� ���
            SQL_Manager.instance.PrintProfileImage(SQL_Manager.instance.Profile_list[i].imageMode, info.ProfileImage, SQL_Manager.instance.Profile_list[i].index);
        }
    }

    private void DuplicateProfileDelete(int _profileIndex)
    { // �������� ���� �� ��Ȳ���� ������ �������� �����ϰ� ����ؾ� �� ��
        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        {  // index = index�� ��Ī ���ѳ��� ���� �߰��� ����ó���� �����ϸ� index������ �� ���� ����Ͽ� ������ �� ��ģ �� ���� ������ ����
            if (_profileIndex == SQL_Manager.instance.Profile_list[i].index)
            {
                Profile tempProfile = SQL_Manager.instance.Profile_list[i];
                SQL_Manager.instance.Profile_list.Remove(tempProfile);
                GameObject list = ProfilePanelList[i];
                ProfilePanelList.Remove(list);
                Destroy(list);
                return;
            }
        }
    }

    public bool ImageSet(bool isIconMode, bool isFirstPlayer, string _profileName, int _defaultImageIndex, TMP_Text _nameLog = null)
    { // Profile�� ���� Image �����ϴ� Btn ���� Method
        // _player bool���� ���� 1P�� �����ϴ��� 2P�� �����ϴ��� ����
        int profileIndex = 0;
        if (!isUpdate)
        { // ù ����� ��
            if (!isIconMode)
            { // ���� ��� ��ư ������ ��
                SetImageMode(isFirstPlayer, false);

                profileIndex = tempIndex;

                TakePicture(isUpdate, profileIndex);
                return true;
            }
            else if (isIconMode)
            { // �̹��� ���� ��ư ������ ��
                if (!isImageSelect)
                { // �̹��� ������ ������ ��
                    if (DialogManager.instance.log_co != null) DialogManager.instance.StopCoroutine(DialogManager.instance.log_co);

                    DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(_nameLog, "�̹����� �������ּ���."));
                    return false;
                }
                else
                { // ������ �̹����� ���� ��
                    SetImageMode(isFirstPlayer, true);

                    // �������� ����ϰ� Index ����
                    AddProfile(_profileName, profileIndex, isIconMode);

                    profileIndex = tempIndex;

                    // ���޹��� profile Index�� Profile Image ����
                    SQL_Manager.instance.SQL_AddProfileImage(_defaultImageIndex, UID, profileIndex);
                    return true;
                }
            }
        }
        else if (isUpdate)
        { // ��������� ��
            if (!isIconMode)
            { // ������� ��� ������ ��
                SetImageMode(isFirstPlayer, false);

                // 1P���� 2P���� üũ �� Profile Image�� ����
                profileIndex = isFirstPlayer ? ProfileIndex1P : ProfileIndex2P;

                TakePicture(isUpdate, profileIndex);

                return true;
            }
            else if (isIconMode)
            { // �̹��� ���� ��ư ������ ��
                if (!isImageSelect)
                { // ������ �̹����� ���� ��
                    if (DialogManager.instance.log_co != null) DialogManager.instance.StopCoroutine(DialogManager.instance.log_co);

                    DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(_nameLog, "�̹����� �������ּ���."));
                    return false;
                }
                else
                { // ������ �̹����� ���� ��
                    SetImageMode(isFirstPlayer, true);

                    // 1P���� 2P���� üũ �� Profile Image�� ����
                    profileIndex = isFirstPlayer ? ProfileIndex1P : ProfileIndex2P;

                    // �������� ����ϰ� Index ����
                    AddProfile(_profileName, profileIndex, isIconMode);

                    // ���޹��� profile Index�� Profile Image ����
                    SQL_Manager.instance.SQL_UpdateProfile(profileIndex, _profileName, UID, _defaultImageIndex);
                    return true;
                }
            }
        }
        return false;
    }

    private void SetImageMode(bool isFirstPlayer, bool isImageMode)
    {
        if (isFirstPlayer)
        {
            IsImageMode1P = isImageMode;
        }
        else
        {
            IsimageMode2P = isImageMode;
        }
    }

    private void TakePicture(bool _update, int _profileIndex)
    {
        imagePath = $"{Application.persistentDataPath}/Profile/{UID}_{_profileIndex}.png";
        if(!_update)
        { // ù ����� ��
            SQL_Manager.instance.SQL_AddProfileImage($"{imagePath}", UID, _profileIndex);
        }
        else
        { // ������� �� ��
            SQL_Manager.instance.SQL_UpdateProfile(_profileIndex, tempName, UID, imagePath);
        }
    }

    /// <summary>
    /// ������ gameobject�� �Ű������� �޾Ƽ� �� ���� Image Component�� ���� ������ �̹����� ���
    /// </summary>
    /// <param name="printobj"></param>
    public void ProfileImagePrint(GameObject printobj)
    {
        Image image = printobj.GetComponent<Image>();

        if (IsImageMode1P) // �̹��� ���ø��
        {   // ����� Index�� �̹����� ������ Sprite�� �־���
            CacheProfileImage = ProfileImages[DefaultImage1P];
            image.sprite = CacheProfileImage;
        }
        else if (!IsImageMode1P) // ������� ���
        {
            Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(UID, ProfileIndex1P);
            Sprite profileSprite = TextureToSprite(profileTexture);
            CacheProfileImage = profileSprite;
            image.sprite = CacheProfileImage;
        }
    }

    /// <summary>
    /// SQL_Manager���� Texture2D�� ��ȯ�� �̹��������� Sprite�� �ѹ� �� ��ȯ�ϴ� Method
    /// </summary>
    /// <param name="texture"></param>
    /// <returns></returns>
    public Sprite TextureToSprite(Texture2D texture)
    {
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

        return sprite;
    }
    #endregion

}
