using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Profile ���� �ൿ ó�� Class
/// </summary>
public class ProfileManager : MonoBehaviour
{
    public static ProfileManager Instance;

    [Header("UID")]
    [Space(5)]
    private string uniqueID = string.Empty; // PlayerPrefs�� ����Ǵ� ���� GUID;
    public int UID = 0; // ��⺰ ���� ��ȣ > SQL�� ����

    [Header("1P Infomation")]
    [Space(5)]
    public string ProfileName1P = string.Empty; // 1P Profile Name
    public int FirstPlayerIndex = 0; // 1P Profile Index
    public int DefaultImage1P = 0; // 1P Profile DefaultImage Index
    public bool IsIconMode1P = false; // false = �������, true = �̹��� ����
    public Sprite CacheProfileImage = null;

    [Header("2P Player")]
    [Space(5)]
    public string ProfileName2P = string.Empty; // 2P Profile Name
    public int SecondPlayerIndex = 0; // 2P Profile Index
    public int DefaultImage2P = 0; // 2P Profile DefaultImage Index
    public bool IsIconMode2P = false; // false = �������, true = �̹��� ����

    [Header("Profile Component")]
    [Space(5)]
    public Sprite[] ProfileImages = null; // ProfileImage Sprites
    public Sprite NoneBackground = null; // Profile None Sprite
    public GameObject ProfilePanel = null; // Profile Panel
    public List<GameObject> ProfilePanelList = new List<GameObject>(); // Profile Panel List

    [Header("Other Request")]
    [Space(5)]
    private string imagePath = string.Empty; // Image Saving Path
    public bool isUpdate = false; // Profile �߰� �� false, ���� �� true
    public bool isImageSelect = false; // Profile Image Icon Select ?
    public bool isProfileSelected = false; // is Profile Select ?

    [Header("Temp Info")]
    [Space(5)]
    public int TempImageIndex = 0; // profile ��� �� �̹��� ���⿡�� ������ index
    public int TempUserIndex = -1; // Profile ����� �� ����� �ӽ� ProfileIndex
    public string TempProfileName = string.Empty; // Profile ����� �� ����� �ӽ� ProfileName

    public Coroutine WarningCoroutine = null;

    #region Unity Callback
    private void Awake()
    {
        if (Instance == null)
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
        Debug.Log(uniqueID);
        SQL_Manager.instance.SQL_AddUser(uniqueID);
    }

    /// <summary>
    /// SQL Manager�� �����Ͽ� �ű� Profile�� ����ϰų�, Update �ϴ� Method
    /// </summary>
    /// <param name="_profileIndex"></param>
    public void AddProfile(int _profileIndex, bool _isIconMode)
    { // Profile ���� �� ����
        int iconMode = _isIconMode ? 1 : 0; // take picture or default image

        if (!isUpdate)
        { // profile ���� ��
            if (!string.IsNullOrWhiteSpace(TempProfileName))
            {
                TempUserIndex = SQL_Manager.instance.SQL_AddProfile(TempProfileName, iconMode); // profile ����, primary key �߰���
            }
        }
        else
        { // profile ���� ��
            SQL_Manager.instance.SQL_UpdateMode(iconMode, UID, TempUserIndex);
            ProfileName1P = TempProfileName; // ������ Update �ؼ� �־���
        }
    }

    public void DeleteProfile(int _index)
    {
        SQL_Manager.instance.SQL_DeleteProfile(_index);
    }

    public void PrintProfileList(Transform parent)
    { // scroll view output
        // DB�� UID���� ����Ǿ��ִ� Profile���� SQL_Manager�� List Up �س���
        SQL_Manager.instance.SQL_ProfileListSet();

        for (int i = 0; i < ProfilePanelList.Count; i++)
        { // ��� �� ������ ��µǾ� �ִ� List�� �ִٸ� �ʱ�ȭ
            Destroy(ProfilePanelList[i].gameObject);
        }
        ProfilePanelList.Clear();

        for (int i = 0; i < SQL_Manager.instance.ProfileList.Count; i++)
        { // SQL_Manager�� Query���� �̿��Ͽ� UID�� ��� Profile��ŭ List�� �����ϰ�, �ش� List�� Count ��ŭ Profile Panel ����
            GameObject panel = Instantiate(ProfilePanel);
            panel.transform.SetParent(parent);
            ProfilePanelList.Add(panel);
        }

        if (GameManager.Instance.gameMode.Equals(Mode.Multi))
        { // Multi Mode���� Profile ���� ��
            DuplicateProfileDelete();
        }

        for (int i = 0; i < SQL_Manager.instance.ProfileList.Count; i++)
        { // Panel�� Index ���� Profile_Information ������Ʈ�� �����ͼ� name�� image�� Mode�� �°� ����
            ProfileInfo info = ProfilePanelList[i].GetComponent<ProfileInfo>();

            // �� infomation ������ �̸� ���
            info.ProfileName.text = SQL_Manager.instance.ProfileList[i].name;

            // �� information ������ �̹��� ���
            SQL_Manager.instance.PrintProfileImage(SQL_Manager.instance.ProfileList[i].imageMode, info.ProfileImage, SQL_Manager.instance.ProfileList[i].index);
        }
    }

    private void DuplicateProfileDelete()
    { // ������ ������ �����ϰ� ���
        for (int i = 0; i < SQL_Manager.instance.ProfileList.Count; i++)
        {  // index = index�� ��Ī ���ѳ��� ���� �߰��� ����ó���� �����ϸ� index������ �� ���� ����Ͽ� ������ �� ��ģ �� ���� ������ ����
            if (FirstPlayerIndex.Equals(SQL_Manager.instance.ProfileList[i].index))
            {
                Profile tempProfile = SQL_Manager.instance.ProfileList[i];
                SQL_Manager.instance.ProfileList.Remove(tempProfile);
                GameObject list = ProfilePanelList[i];
                ProfilePanelList.Remove(list);
                Destroy(list);
                return;
            }
        }
    }

    public bool ImageSet(bool _isIconMode, bool _isFirstPlayer, string _profileName, int _defaultImageIndex, TMP_Text _nameLog = null)
    { // Profile�� ���� Image Setting
        // _player bool���� ���� 1P�� �����ϴ��� 2P�� �����ϴ��� ����
        int profileIndex = GameManager.Instance.gameMode.Equals(Mode.Multi) ? FirstPlayerIndex : SecondPlayerIndex;

        if (!_isIconMode)
        { // ���� ��� ��ư Ŭ�� ��
            AddProfileImage();

            return true;
        }
        else
        { // �̹��� ���� ��ư Ŭ�� ��
            if (!isImageSelect)
            { // �̹��� ������ ������ ��
                if (WarningCoroutine != null)
                {
                    StopCoroutine(WarningCoroutine);
                }
                PrintErrorLog(_nameLog, "�̹����� �������ּ���.");

                return false;
            }
            else
            { // ������ �̹����� ���� ��
              //SetImageMode(_isFirstPlayer, true);
                AddProfile(profileIndex, _isIconMode); // defaultimage add

                // ���޹��� profile Index�� Profile Image ����

                if (!isUpdate)
                { // profile ���� ��
                    SQL_Manager.instance.SQL_AddProfileImage(TempImageIndex, UID, TempUserIndex);
                }
                else
                { // profile ���� ��
                    SQL_Manager.instance.SQL_UpdateProfile(TempUserIndex, TempProfileName, UID, TempImageIndex); // ���޹��� profile Index�� Profile Image ����
                }

                return true;
            }
        }

        /*
        if (!isUpdate)
        { // profile ���� ��
            if (!_isIconMode)
            { // ���� ��� ��ư ������ ��
                //SetImageMode(_isFirstPlayer, false);
                AddProfileImage();

                return true;
            }
            else
            { // �̹��� ���� ��ư ������ ��
                if (!isImageSelect)
                { // �̹��� ������ ������ ��
                    if (WarningCoroutine != null)
                    {
                        StopCoroutine(WarningCoroutine);
                    }
                    PrintErrorLog(_nameLog, "�̹����� �������ּ���.");
                    
                    return false;
                }
                else
                { // ������ �̹����� ���� ��
                    //SetImageMode(_isFirstPlayer, true);
                    AddProfile(profileIndex, _isIconMode); // defaultimage add

                    // ���޹��� profile Index�� Profile Image ����
                    SQL_Manager.instance.SQL_AddProfileImage(TempImageIndex, UID, TempUserIndex);
                    return true;
                }
            }
        }
        else
        { // profile ���� ��
            if (!_isIconMode)
            { // ������� ��� ������ ��
                //SetImageMode(_isFirstPlayer, false);
                AddProfileImage();

                return true;
            }
            else if (_isIconMode)
            { // �̹��� ���� ��ư ������ ��
                if (!isImageSelect)
                { // ������ �̹����� ���� ��
                    if (WarningCoroutine != null)
                    {
                        StopCoroutine(WarningCoroutine);
                    }
                    PrintErrorLog(_nameLog, "�̹����� �������ּ���.");

                    return false;
                }
                else
                { // ������ �̹����� ���� ��
                    //SetImageMode(_isFirstPlayer, true);
                    // 1P���� 2P���� üũ �� Profile Image�� ����
                    profileIndex = _isFirstPlayer ? FirstPlayerIndex : SecondPlayerIndex; // isfirstplayer gamemode�� ���� ����
                    AddProfile(profileIndex, _isIconMode); // �������� ����ϰ� Index ����
                    SQL_Manager.instance.SQL_UpdateProfile(profileIndex, TempProfileName, UID, TempImageIndex); // ���޹��� profile Index�� Profile Image ����

                    return true;
                }
            }
        }
        */
    }

    private void SetImageMode(bool isFirstPlayer, bool isImageMode)
    {
        if (isFirstPlayer)
        {
            IsIconMode1P = isImageMode;
        }
        else
        {
            IsIconMode2P = isImageMode;
        }
    }

    private void AddProfileImage()
    {
        imagePath = $"{Application.persistentDataPath}/Profile/{UID}_{TempUserIndex}.png";

        if (!isUpdate)
        { // ù ����� ��
            SQL_Manager.instance.SQL_AddProfileImage(imagePath, UID, TempUserIndex);
        }
        else
        { // ������� �� ��
            SQL_Manager.instance.SQL_UpdateProfile(TempUserIndex, TempProfileName, UID, imagePath);
        }
    }

    /// <summary>
    /// ������ gameobject�� �Ű������� �޾Ƽ� �� ���� Image Component�� ���� ������ �̹����� ���
    /// </summary>
    /// <param name="printobj"></param>
    public void ProfileImagePrint(GameObject printobj)
    {
        Image image = printobj.GetComponent<Image>();

        if (IsIconMode1P) // �̹��� ���ø��
        {   // ����� Index�� �̹����� ������ Sprite�� �־���
            CacheProfileImage = ProfileImages[DefaultImage1P];
            image.sprite = CacheProfileImage;
        }
        else if (!IsIconMode1P) // ������� ���
        {
            Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(UID, FirstPlayerIndex);
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

    // Jaeyun Profile Canvas
    #region Warning Log Output
    public void PrintErrorLog(TMP_Text _warningLog, string _logText)
    { // InputField Check �� Error Log ���� method
        if (WarningCoroutine != null)
        {
            StopCoroutine(WarningCoroutine);
        }
        WarningCoroutine = StartCoroutine(PrintWarningDialog_Co(_warningLog, _logText));
    }

    private IEnumerator PrintWarningDialog_Co(TMP_Text _warningLog, string _logText)
    {
        _warningLog.gameObject.SetActive(true);
        _warningLog.text = _logText;

        yield return new WaitForSeconds(3f);

        _warningLog.gameObject.SetActive(false);
        WarningCoroutine = null;
    }
    #endregion
}
