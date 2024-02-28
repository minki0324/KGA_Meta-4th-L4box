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

    [Header("2P Player")] [Space(5)]
    public string ProfileName2P = string.Empty; // 2P Profile Name
    public int ProfileIndex2P = 0; // 2P Profile Index
    public int DefaultImage2P = 0; // 2P Profile DefaultImage Index
    public bool IsimageMode2P = false; // false = �������, true = �̹��� ����

    [Header("Profile Component")] [Space(5)]
    public Sprite[] ProfileImages = null; // ProfileImage Sprites
    public GameObject ProfilePanel = null; // Profile Panel
    public List<GameObject> ProfilePanelList = new List<GameObject>(); // Profile Panel List

    [Header("Other Request")] [Space(5)]
    private string imagePath = string.Empty; // Image Saving Path
    public bool isUpdate = false; // Profile Update ?
    public bool isImageSelect = false; // Profile Image Icon Select ?
    public bool isProfileSelected = false; // is Profile Select ?

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
        Debug.Log(uniqueID);
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
                _profileIndex = SQL_Manager.instance.SQL_AddProfile(_profileName, imageMode);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(_profileName))
                {
                    
                }
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

    public void PrintProfileList(Transform parent, int? _profileIndex1P, int? _profileIndex2P)
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

        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        { // Panel�� Index ���� Profile_Information ������Ʈ�� �����ͼ� name�� image�� Mode�� �°� ����
            NewProfile_Infomation info = ProfilePanelList[i].GetComponent<NewProfile_Infomation>();

            // �� infomation ������ �̸� ���
            info.Profile_name.text = SQL_Manager.instance.Profile_list[i].name;

            // �� information ������ �̹��� ���
            SQL_Manager.instance.PrintProfileImage(SQL_Manager.instance.Profile_list[i].imageMode, info.ProfileImage, SQL_Manager.instance.Profile_list[i].index);
        }
        if(GameManager.Instance.gameMode == Mode.Bomb)
        {
            for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
            { // ������ ��ģ �� ������ �������� ����
                if (_profileIndex1P == SQL_Manager.instance.Profile_list[i].index)
                { // index = index�� ��Ī ���ѳ��� ���� �߰��� ����ó���� �����ϸ� index������ �� ���� ����Ͽ� ������ �� ��ģ �� ���� ������ ����
                    Profile tempProfile = SQL_Manager.instance.Profile_list[i];
                    SQL_Manager.instance.Profile_list.Remove(tempProfile);
                    GameObject list = ProfilePanelList[i];
                    ProfilePanelList.Remove(list);
                    Destroy(list);
                }
                else if (_profileIndex2P == SQL_Manager.instance.Profile_list[i].index)
                { // index = index�� ��Ī ���ѳ��� ���� �߰��� ����ó���� �����ϸ� index������ �� ���� ����Ͽ� ������ �� ��ģ �� ���� ������ ����
                    Profile tempProfile = SQL_Manager.instance.Profile_list[i];
                    SQL_Manager.instance.Profile_list.Remove(tempProfile);
                    GameObject list = ProfilePanelList[i];
                    ProfilePanelList.Remove(list);
                    Destroy(list);
                }
            }
        }
    }

    public bool ImageSet(bool _mode,  bool _player, TMP_Text _nameLog, string _profileName, int _defaultImageIndex)
    { // Profile�� ���� Image �����ϴ� Btn ���� Method
        // _player bool���� ���� 1P�� �����ϴ��� 2P�� �����ϴ��� ����
        int index = 0;
        if (_player.Equals(true)) index = ProfileIndex1P;
        else if (_player.Equals(false)) index = ProfileIndex2P;

        if (!isUpdate)
        { // ù ����� ��
            if (!_mode)
            { // ���� ��� ��ư ������ ��
                imagePath = $"{Application.persistentDataPath}/Profile/{UID}_{index}.png";
                SQL_Manager.instance.SQL_AddProfileImage($"{imagePath}", UID, index);
                return true;
            }
            else if (_mode)
            { // �̹��� ���� ��ư ������ ��
                if (!isImageSelect)
                { // �̹��� ������ ������ ��
                    if (DialogManager.instance.log_co != null)
                    {
                        DialogManager.instance.StopCoroutine(DialogManager.instance.log_co);
                    }
                    DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(_nameLog, "�̹����� �������ּ���."));
                    return false;
                }
                else
                { // ������ �̹����� ���� ��
                    AddProfile(_profileName, index, _mode);
                    SQL_Manager.instance.SQL_AddProfileImage(_defaultImageIndex, UID, index);
                    return true;
                }
            }
        }
        else if (isUpdate)
        { // ��������� ��
            if (_mode)
            { // ������� ��� ������ ��
                return true;
            }
            else if (!_mode)
            { // �̹��� ���� ��ư ������ ��
                if (!isImageSelect)
                { // ������ �̹����� ���� ��
                    if (DialogManager.instance.log_co != null)
                    {
                        DialogManager.instance.StopCoroutine(DialogManager.instance.log_co);
                    }
                    DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(_nameLog, "�̹����� �������ּ���."));
                    return false;
                }
                else
                { // ������ �̹����� ���� ��
                    if (_player.Equals(true)) IsImageMode1P = true;
                    else if (_player.Equals(false)) IsimageMode2P = true;

                    AddProfile(_profileName, index, _mode);
                    SQL_Manager.instance.SQL_UpdateProfile(index, _profileName, UID, _defaultImageIndex);
                    
                    isUpdate = false;
                    return true;
                }
            }
        }
        return false;
    }


    #endregion

}
