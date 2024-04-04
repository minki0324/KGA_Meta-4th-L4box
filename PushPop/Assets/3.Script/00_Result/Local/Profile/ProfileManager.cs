using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    { // sprite는 캐싱 후 추가
        profileName = _profileName;
        playerIndex = _playerIndex;
        defaultImageIndex = _defaultImageIndex; // image mode true일 때 -1로 저장
        imageMode = _imageMode;
    }
}

/// <summary>
/// Profile 관련 행동 처리 Class
/// </summary>
public class ProfileManager : MonoBehaviour
{
    public static ProfileManager Instance = null;
    public Profile myProfile;

    [Header("UID")]
    private string uniqueID = string.Empty; // PlayerPrefs에 저장되는 고유 GUID;
    [HideInInspector] public int UID = 0; // 기기별 고유 번호 > SQL과 연동

    [Header("Player Info")]
    [HideInInspector] public Player SelectPlayer = Player.Player1;
    [HideInInspector] public bool IsSelect = false;
    public PlayerInfo[] PlayerInfo = new PlayerInfo[2]; // Player1, Player2

    [Header("Profile Component")]
    public Sprite[] ProfileImages; // ProfileImage Sprites
    public Sprite NoneBackground = null; // Profile None Sprite
    public GameObject ProfilePanel; // Profile Panel
    [HideInInspector] public List<GameObject> ProfilePanelList = new List<GameObject>(); // Profile Panel List

    [Header("Other Request")]
    private string imagePath = string.Empty; // Image Saving Path1
    [HideInInspector] public bool isUpdate = false; // Profile 추가 시 false, 수정 시 true
    [HideInInspector] public bool isImageSelect = false; // Profile Image Icon 선택 시 true, 아닐 시 false
    [HideInInspector] public bool isProfileSelected = false; // is Profile Select ?

    [Header("Temp Info")]
    [HideInInspector] public int TempImageIndex = 0; // profile 등록 시 이미지 고르기에서 선택한 index
    [HideInInspector] public int TempUserIndex = -1; // Profile 등록할 때 사용할 임시 ProfileIndex
    [HideInInspector] public string TempProfileName = string.Empty; // Profile 등록할 때 사용할 임시 ProfileName
    [HideInInspector] public bool TempImageMode = true; // profile 이미지 등록 시 true, 사진 찍기 시 false

    private ProfileCanvas profileCanvas;
    public bool isImageUpdate = true; // 프로필 생성 시작 시 false, 이미지 등록 완료되었을 시 true

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
        profileCanvas = FindObjectOfType<ProfileCanvas>();
    }
    #endregion

    #region Other Method
    public void LoadOrCreateGUID()
    {
        try
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
            //DebugLog.instance.Adding_Message(uniqueID);
            SQL_Manager.instance.SQL_AddUser(uniqueID);
        }
        catch (Exception e)
        {
            Debug.Log("LoadOrCreateGUID : " + e.Message);
        }
    }

    /// <summary>
    /// SQL Manager와 연동하여 신규 Profile을 등록하거나, Update 하는 Method
    /// </summary>
    /// <param name="_profileIndex"></param>
    public void AddProfile(bool _isIconMode)
    { // Profile 생성 및 수정
        int iconMode = _isIconMode ? 1 : 0; // take picture or default image

        if (!isUpdate)
        { // profile 생성 시
            if (!string.IsNullOrWhiteSpace(TempProfileName))
            {
                TempUserIndex = SQL_Manager.instance.SQL_AddProfile(TempProfileName, iconMode); // profile 생성, primary key 추가됨
            }
        }
        else
        { // profile 수정 시
            SQL_Manager.instance.SQL_UpdateMode(iconMode, UID, TempUserIndex);
            PlayerInfo[(int)SelectPlayer].profileName = TempProfileName; // 본인을 Update 해서 넣어줌
        }
    }

    public void DeleteProfile(int _index)
    {
        SQL_Manager.instance.SQL_DeleteProfile(_index);
    }

    public void PrintProfileList(Transform parent)
    {
        StartCoroutine(PrintProfileList_Co(parent));
    }

    public IEnumerator PrintProfileList_Co(Transform parent)
    { // scroll view output
        profileCanvas.CreateButton.enabled = false;
        profileCanvas.DeleteButton.enabled = false;
        profileCanvas.ProfileLoadingPanel.SetActive(true);
        while (true)
        {
            if (isImageUpdate) break;
            yield return null;
        }
        // DB에 UID별로 저장되어있는 Profile들을 SQL_Manager에 List Up 해놓음
        SQL_Manager.instance.SQL_ProfileListSet();

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < ProfilePanelList.Count; i++)
        { // 출력 전 기존에 출력되어 있는 List가 있다면 초기화
            Destroy(ProfilePanelList[i].gameObject);
        }
        ProfilePanelList.Clear();

        for (int i = 0; i < SQL_Manager.instance.ProfileList.Count; i++)
        { // SQL_Manager에 Query문을 이용하여 UID에 담긴 Profile만큼 List를 셋팅하고, 해당 List의 Count 만큼 Profile Panel 생성
            GameObject panel = Instantiate(ProfilePanel);
            panel.transform.SetParent(parent);
            ProfilePanelList.Add(panel);
        }

        if (GameManager.Instance.GameMode.Equals(GameMode.Multi) || GameManager.Instance.GameMode.Equals(GameMode.Lobby))
        { // Multi Mode에서 Profile 수정 시
            DuplicateProfileDelete();
        }

        for (int i = 0; i < SQL_Manager.instance.ProfileList.Count; i++)
        { // Panel의 Index 별로 Profile_Information 컴포넌트를 가져와서 name과 image를 Mode에 맞게 셋팅
            ProfileInfo info = ProfilePanelList[i].GetComponent<ProfileInfo>();

            // 프로필 출력
            SQL_Manager.instance.PrintProfileImage(info.ProfileImage, SQL_Manager.instance.ProfileList[i].imageMode, SQL_Manager.instance.ProfileList[i].index);
            info.ProfileName.text = SQL_Manager.instance.ProfileList[i].name;
        }

        profileCanvas.ProfileLoadingPanel.SetActive(false);
        profileCanvas.CreateButton.enabled = true;
        profileCanvas.DeleteButton.enabled = true;
    }

    private void DuplicateProfileDelete()
    { // 본인의 프로필 제외하고 출력
        for (int i = 0; i < SQL_Manager.instance.ProfileList.Count; i++)
        {  // index = index로 매칭 시켜놓은 로직 중간에 예외처리로 삭제하면 index오류가 날 것을 우려하여 세팅을 다 마친 후 본인 프로필 삭제
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

    public bool ImageSet(bool _isIconMode, TMP_Text _nameLog = null)
    { // Profile에 넣을 Image Setting
        // _player bool값에 따라 1P를 설정하는지 2P를 설정하는지 결정
        if (!_isIconMode)
        { // 사진 찍기 버튼 클릭 시
            AddProfileImage();
            return true;
        }
        else
        { // 이미지 고르기 버튼 클릭 시
            if (!isImageSelect)
            { // 이미지 선택을 안했을 때
                PrintErrorLog(_nameLog, "이미지를 선택해주세요.");

                return false;
            }
            else
            { // 선택한 이미지가 있을 때
              //SetImageMode(_isFirstPlayer, true);
                AddProfile(_isIconMode); // defaultimage add

                // 전달받은 profile Index로 Profile Image 설정

                if (!isUpdate)
                { // profile 생성 시
                    SQL_Manager.instance.SQL_AddProfileImage(TempImageIndex, UID, TempUserIndex);
                }
                else
                { // profile 수정 시
                    SQL_Manager.instance.SQL_UpdateProfile(TempUserIndex, TempProfileName, UID, TempImageIndex); // 전달받은 profile Index로 Profile Image 수정
                }

                isImageUpdate = true;
                return true;
            }
        }
    }
    private void AddProfileImage()
    {
        imagePath = $"{Application.persistentDataPath}/Profile/{UID}_{TempUserIndex}.png";
        Debug.Log(imagePath);
        if (!isUpdate)
        { // 첫 등록일 때
            Task.Run(() =>
            {
                SQL_Manager.instance.SQL_AddProfileImage(imagePath, UID, TempUserIndex);
                isImageUpdate = true;
            });
        }
        else
        { // 수정모드 일 때
            Task.Run(() =>
            {
                SQL_Manager.instance.SQL_UpdateProfile(TempUserIndex, TempProfileName, UID, imagePath);
                isImageUpdate = true;
            });
        }
    }

    /// <summary>
    /// 변경할 gameobject를 매개변수로 받아서 그 안의 Image Component를 통해 프로필 이미지를 출력
    /// </summary>
    /// <param name="_profileImage"></param>
    public Sprite ProfileImageCaching()
    { // 선택한 프로필 이미지 캐싱
        Sprite profileSprite = null;
        if (TempImageMode)
        { // 이미지 선택 모드
            profileSprite = ProfileImages[TempImageIndex]; // 저장된 Index의 이미지를 프로필 Sprite에 넣어줌
            return profileSprite;
        }
        else
        { // 사진 찍기 모드
            Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(UID, TempUserIndex);
            profileSprite = TextureToSprite(profileTexture);
            return profileSprite;
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
    #region Warning Log Output
    public void PrintErrorLog(TMP_Text _warningLog, string _logText)
    { // InputField Check 후 Error Log 띄우는 method
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
