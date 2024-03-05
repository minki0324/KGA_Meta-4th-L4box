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
/// Profile 관련 행동 처리 Class
/// </summary>
public class ProfileManager : MonoBehaviour
{
    public static ProfileManager Instance;

    [Header("UID")] [Space(5)]
    private string uniqueID = string.Empty; // PlayerPrefs에 저장되는 고유 GUID;
    public int UID = 0; // 기기별 고유 번호 > SQL과 연동

    [Header("1P Infomation")] [Space(5)]
    public string ProfileName1P = string.Empty; // 1P Profile Name
    public int ProfileIndex1P = 0; // 1P Profile Index
    public int DefaultImage1P = 0; // 1P Profile DefaultImage Index
    public bool IsImageMode1P = false; // false = 사진찍기, true = 이미지 선택
    public Sprite CacheProfileImage = null;

    [Header("2P Player")] [Space(5)]
    public string ProfileName2P = string.Empty; // 2P Profile Name
    public int ProfileIndex2P = 0; // 2P Profile Index
    public int DefaultImage2P = 0; // 2P Profile DefaultImage Index
    public bool IsimageMode2P = false; // false = 사진찍기, true = 이미지 선택

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
    public int tempIndex = 0; // Profile 등록할 때 사용할 임시 ProfileIndex
    public string tempName = string.Empty; // Profile 등록할 때 사용할 임시 ProfileName

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
        { // 기존 GUID가 있는 경우 해당 GUID를 변수에 담아줌
            uniqueID = PlayerPrefs.GetString("DeviceGUID");
        }
        else
        { // 첫 접속시 GUID를 부여하고 해당 GUID를 변수에 담아줌
            uniqueID = Guid.NewGuid().ToString();

            // PlayerPrefs에 GUID를 저장
            PlayerPrefs.SetString("DeviceGUID", uniqueID);
            PlayerPrefs.Save();
        }
        // GUID를 가지고 DB와 연동하여 UID를 부여받음
        DebugLog.instance.Adding_Message(uniqueID);
        SQL_Manager.instance.SQL_AddUser(uniqueID);
    }

    /// <summary>
    /// SQL Manager와 연동하여 신규 Profile을 등록하거나, Update 하는 Method
    /// </summary>
    /// <param name="_profileName"></param>
    /// <param name="_profileIndex"></param>
    public void AddProfile(string _profileName, int _profileIndex, bool _imageMode)
    {
        int imageMode = -1;
        switch (_imageMode)
        {
            case false: //  사진 찍기를 선택했을 때
                imageMode = 0;
                break;
            case true:  //  Default 이미지를 선택했을 때
                imageMode = 1;
                break;
        }
        if (!isUpdate)
        { // 첫 등록일 때
            if (!string.IsNullOrWhiteSpace(_profileName))
            {
                tempIndex = SQL_Manager.instance.SQL_AddProfile(_profileName, imageMode);
            }
            else if (string.IsNullOrWhiteSpace(_profileName))
            {

            }
        }
        else if (isUpdate)
        { // 수정 중일 때
            SQL_Manager.instance.SQL_UpdateMode(imageMode, UID, _profileIndex);
        }
    }

    public void DeleteProfile(int _index)
    {
        SQL_Manager.instance.SQL_DeleteProfile(_index);
    }

    public void PrintProfileList(Transform parent, int? _profileIndex1P)
    {
        // DB에 UID별로 저장되어있는 Profile들을 SQL_Manager에 List Up 해놓음
        SQL_Manager.instance.SQL_ProfileListSet();

        for (int i = 0; i < ProfilePanelList.Count; i++)
        { // 출력 전 기존에 출력되어 있는 List가 있다면 초기화
            Destroy(ProfilePanelList[i].gameObject);
        }
        ProfilePanelList.Clear();

        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        { // SQL_Manager에 Query문을 이용하여 UID에 담긴 Profile만큼 List를 셋팅하고, 해당 List의 Count 만큼 Profile Panel 생성
            GameObject panel = Instantiate(ProfilePanel);
            panel.transform.SetParent(parent);
            ProfilePanelList.Add(panel);
        }

        if (_profileIndex1P != null)
        {
            DuplicateProfileDelete((int)_profileIndex1P);
        }

        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        { // Panel의 Index 별로 Profile_Information 컴포넌트를 가져와서 name과 image를 Mode에 맞게 셋팅
            NewProfile_Infomation info = ProfilePanelList[i].GetComponent<NewProfile_Infomation>();

            // 각 infomation 프로필 이름 출력
            info.Profile_name.text = SQL_Manager.instance.Profile_list[i].name;

            // 각 information 프로필 이미지 출력
            SQL_Manager.instance.PrintProfileImage(SQL_Manager.instance.Profile_list[i].imageMode, info.ProfileImage, SQL_Manager.instance.Profile_list[i].index);
        }
    }

    private void DuplicateProfileDelete(int _profileIndex)
    { // 프로필이 선택 된 상황에서 본인의 프로필은 제외하고 출력해야 할 때
        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        {  // index = index로 매칭 시켜놓은 로직 중간에 예외처리로 삭제하면 index오류가 날 것을 우려하여 세팅을 다 마친 후 본인 프로필 삭제
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
    { // Profile에 넣을 Image 셋팅하는 Btn 연동 Method
        // _player bool값에 따라 1P를 설정하는지 2P를 설정하는지 결정
        int profileIndex = 0;
        if (!isUpdate)
        { // 첫 등록일 때
            if (!isIconMode)
            { // 사진 찍기 버튼 눌렀을 때
                SetImageMode(isFirstPlayer, false);

                profileIndex = tempIndex;

                TakePicture(isUpdate, profileIndex);
                return true;
            }
            else if (isIconMode)
            { // 이미지 고르기 버튼 눌렀을 때
                if (!isImageSelect)
                { // 이미지 선택을 안했을 때
                    if (DialogManager.instance.log_co != null) DialogManager.instance.StopCoroutine(DialogManager.instance.log_co);

                    DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(_nameLog, "이미지를 선택해주세요."));
                    return false;
                }
                else
                { // 선택한 이미지가 있을 때
                    SetImageMode(isFirstPlayer, true);

                    // 프로필을 등록하고 Index 설정
                    AddProfile(_profileName, profileIndex, isIconMode);

                    profileIndex = tempIndex;

                    // 전달받은 profile Index로 Profile Image 설정
                    SQL_Manager.instance.SQL_AddProfileImage(_defaultImageIndex, UID, profileIndex);
                    return true;
                }
            }
        }
        else if (isUpdate)
        { // 수정모드일 때
            if (!isIconMode)
            { // 사진찍기 모드 눌렀을 때
                SetImageMode(isFirstPlayer, false);

                // 1P인지 2P인지 체크 후 Profile Image에 전달
                profileIndex = isFirstPlayer ? ProfileIndex1P : ProfileIndex2P;

                TakePicture(isUpdate, profileIndex);

                return true;
            }
            else if (isIconMode)
            { // 이미지 고르기 버튼 눌렀을 때
                if (!isImageSelect)
                { // 선택한 이미지가 없을 때
                    if (DialogManager.instance.log_co != null) DialogManager.instance.StopCoroutine(DialogManager.instance.log_co);

                    DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(_nameLog, "이미지를 선택해주세요."));
                    return false;
                }
                else
                { // 선택한 이미지가 있을 때
                    SetImageMode(isFirstPlayer, true);

                    // 1P인지 2P인지 체크 후 Profile Image에 전달
                    profileIndex = isFirstPlayer ? ProfileIndex1P : ProfileIndex2P;

                    // 프로필을 등록하고 Index 설정
                    AddProfile(_profileName, profileIndex, isIconMode);

                    // 전달받은 profile Index로 Profile Image 수정
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
        { // 첫 등록일 때
            SQL_Manager.instance.SQL_AddProfileImage($"{imagePath}", UID, _profileIndex);
        }
        else
        { // 수정모드 일 때
            SQL_Manager.instance.SQL_UpdateProfile(_profileIndex, tempName, UID, imagePath);
        }
    }

    /// <summary>
    /// 변경할 gameobject를 매개변수로 받아서 그 안의 Image Component를 통해 프로필 이미지를 출력
    /// </summary>
    /// <param name="printobj"></param>
    public void ProfileImagePrint(GameObject printobj)
    {
        Image image = printobj.GetComponent<Image>();

        if (IsImageMode1P) // 이미지 선택모드
        {   // 저장된 Index의 이미지를 프로필 Sprite에 넣어줌
            CacheProfileImage = ProfileImages[DefaultImage1P];
            image.sprite = CacheProfileImage;
        }
        else if (!IsImageMode1P) // 사진찍기 모드
        {
            Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(UID, ProfileIndex1P);
            Sprite profileSprite = TextureToSprite(profileTexture);
            CacheProfileImage = profileSprite;
            image.sprite = CacheProfileImage;
        }
    }

    /// <summary>
    /// SQL_Manager에서 Texture2D로 변환한 이미지파일을 Sprite로 한번 더 변환하는 Method
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
