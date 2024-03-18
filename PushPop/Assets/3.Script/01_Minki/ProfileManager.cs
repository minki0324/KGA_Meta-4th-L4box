using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfo
{
    public Sprite profileImage;
    public string profileName;
    public int playerIndex;
    public int defaultImageIndex;
    public bool imageMode;

    public PlayerInfo(string _profileName, int _playerIndex, int _defaultImageIndex, bool _imageMode)
    { // sprite�� ĳ�� �� �߰�
        profileName = _profileName;
        playerIndex = _playerIndex;
        defaultImageIndex = _defaultImageIndex; // image mode true�� �� -1�� ����
        imageMode = _imageMode;
    }
}

/// <summary>
/// Profile ���� �ൿ ó�� Class
/// </summary>
public class ProfileManager : MonoBehaviour
{
    public static ProfileManager Instance;
    public List<Profile> profileList = new List<Profile>();
    public Profile myProfile;
    [Header("UID")]
    [Space(5)]
    private string uniqueID = string.Empty; // PlayerPrefs�� ����Ǵ� ���� GUID;
    public int UID = 0; // ��⺰ ���� ��ȣ > SQL�� ����

    [Header("Player Info")]
    public Player SelectPlayer = Player.Player1;
    public PlayerInfo[] PlayerInfo = new PlayerInfo[2]; // Player1, Player2

    [Header("1P Info")]
    [Space(5)]
    public string ProfileName1P = string.Empty; // 1P Profile Name
    public int FirstPlayerIndex = 0; // 1P Profile Index
    public int DefaultImage1P = 0; // 1P Profile DefaultImage Index
    public bool IsImageMode1P = false; // false = �������, true = �̹��� ����
    public Sprite CacheProfileImage1P = null;

    [Header("2P Info")]
    [Space(5)]
    public string ProfileName2P = string.Empty; // 2P Profile Name
    public int SecondPlayerIndex = 0; // 2P Profile Index
    public int DefaultImage2P = 0; // 2P Profile DefaultImage Index
    public bool IsImageMode2P = false; // false = �������, true = �̹��� ����
    public Sprite CacheProfileImage2P = null;
    public bool IsSelect = false;

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
    public bool isImageSelect = false; // Profile Image Icon ���� �� true, �ƴ� �� false
    public bool isProfileSelected = false; // is Profile Select ?

    [Header("Temp Info")]
    [Space(5)]
    public int TempImageIndex = 0; // profile ��� �� �̹��� ���⿡�� ������ index
    public int TempUserIndex = -1; // Profile ����� �� ����� �ӽ� ProfileIndex
    public string TempProfileName = string.Empty; // Profile ����� �� ����� �ӽ� ProfileName
    public bool TempImageMode = true; // profile �̹��� ��� �� true, ���� ��� �� false

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

    /*private void Start()
    {
        LoadOrCreateGUID();
        PrintProfileList(canvas.profileParent);
    }*/
    #endregion

    #region Other Method
    public void LoadOrCreateGUID()
    {
        try
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
            //DebugLog.instance.Adding_Message(uniqueID);
            SQL_Manager.instance.SQL_AddUser(uniqueID);
        }
       catch(Exception e)
        {
            DebugLog.instance.Adding_Message(e.Message);
        }
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
            PlayerInfo[(int)SelectPlayer].profileName = TempProfileName; // ������ Update �ؼ� �־���
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

        if (GameManager.Instance.GameMode.Equals(GameMode.Multi))
        { // Multi Mode���� Profile ���� ��
            DuplicateProfileDelete();
        }

        for (int i = 0; i < SQL_Manager.instance.ProfileList.Count; i++)
        { // Panel�� Index ���� Profile_Information ������Ʈ�� �����ͼ� name�� image�� Mode�� �°� ����
            ProfileInfo info = ProfilePanelList[i].GetComponent<ProfileInfo>();

            // ������ ���
            SQL_Manager.instance.PrintProfileImage(info.ProfileImage, SQL_Manager.instance.ProfileList[i].imageMode, SQL_Manager.instance.ProfileList[i].index);
            info.ProfileName.text = SQL_Manager.instance.ProfileList[i].name;
        }
    }

    private void DuplicateProfileDelete()
    { // ������ ������ �����ϰ� ���
        for (int i = 0; i < SQL_Manager.instance.ProfileList.Count; i++)
        {  // index = index�� ��Ī ���ѳ��� ���� �߰��� ����ó���� �����ϸ� index������ �� ���� ����Ͽ� ������ �� ��ģ �� ���� ������ ����
            if (PlayerInfo[(int)Player.Player1].playerIndex.Equals(SQL_Manager.instance.ProfileList[i].index))
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
        int profileIndex = GameManager.Instance.GameMode.Equals(GameMode.Multi) ? FirstPlayerIndex : SecondPlayerIndex;

        if (!_isIconMode)
        { // ���� ��� ��ư Ŭ�� ��
            AddProfileImage();

            return true;
        }
        else
        { // �̹��� ���� ��ư Ŭ�� ��
            if (!isImageSelect)
            { // �̹��� ������ ������ ��
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
    }

    private void SetImageMode(bool isFirstPlayer, bool isImageMode)
    {
        if (isFirstPlayer)
        {
            IsImageMode1P = isImageMode;
        }
        else
        {
            IsImageMode2P = isImageMode;
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
    /// <param name="_profileImage"></param>
    public Sprite ProfileImageCaching()
    { // ������ ������ �̹��� ĳ��
        Sprite profileSprite = null;
        if (TempImageMode)
        { // �̹��� ���� ���
            profileSprite = ProfileImages[TempImageIndex]; // ����� Index�� �̹����� ������ Sprite�� �־���
            return profileSprite;
        }
        else
        { // ���� ��� ���
            Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(UID, TempUserIndex);
            profileSprite = TextureToSprite(profileTexture);
            return profileSprite;
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
        StopAllCoroutines();
        StartCoroutine(PrintWarningDialog_Co(_warningLog, _logText));
    }

    private IEnumerator PrintWarningDialog_Co(TMP_Text _warningLog, string _logText)
    {
        _warningLog.gameObject.SetActive(true);
        _warningLog.text = _logText;

        yield return new WaitForSeconds(3f);

        _warningLog.gameObject.SetActive(false);
    }
    #endregion
}
