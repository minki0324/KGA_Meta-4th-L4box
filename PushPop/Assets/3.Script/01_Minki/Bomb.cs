using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.PlayerLoop.PreUpdate;
using UnityEngine.EventSystems;
using UnityEditor.Tilemaps;

/// <summary>
/// 2인 모드(폭탄 돌리기) 관련 Class
/// </summary>
public class Bomb : MonoBehaviour, IPointerClickHandler
{
    [Header("1P Player")]
    [SerializeField] private Image playerImage1P = null;
    [SerializeField] private TMP_Text playerName1P = null;

    [Header("2P Player")]
    public Image playerImage2P = null;
    public Image tempPlayerImage2P = null;
    public TMP_Text playerName2P = null;
    public TMP_Text tempPlayerName2P = null;
    public bool isImageSelect = false;
    public bool isUpdate = false;
    public bool isSelect2P = false;

    [Header("Profile Obj")]
    [SerializeField] private GameObject profilePanel = null;
    [SerializeField] private Transform profileParent = null;
    [SerializeField] private List<GameObject> profileList = new List<GameObject>();
    [SerializeField] private string profile2PName = null;
    [SerializeField] private TMP_InputField profile2PInput = null;
    [SerializeField] private GameObject iconPanel = null;
    [SerializeField] private GameObject checkPanel = null;
    private int imageIndex = 0;
    private string imagePath = string.Empty;   // Camera Image Save Path

    [Header("Panel")]
    public GameObject MainPanel = null;
    public GameObject GamePanel = null;
    public GameObject CreateImagePanel = null;    
    public GameObject SelectProfile = null;
    public GameObject CurrentProfile = null;

    [Header("ErrorLog")]
    [SerializeField] private GameObject nameLog = null;
    [SerializeField] private GameObject imageLog = null;
    [SerializeField] private GameObject need2P = null;

    [Header("BombGame")]

    private Coroutine log;

    #region Unity Callback
    private void OnEnable()
    {
        PlayerSet1P();
        profile2PInput.onValidateInput += ValidateInput;
    }

    private void OnDisable()
    {
        profile2PInput.onValidateInput -= ValidateInput;
        isSelect2P = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerPress != null && eventData.pointerPress.GetComponent<Button>() != null)
        {
            return;
        }
        else
        {
            if (iconPanel.activeSelf)
            {
                isImageSelect = false;
            }
        }
    }
    #endregion

    #region Other Method
    #region Profile
    private void PlayerSet1P()
    { // 본인의 프로필을 출력하는 Method
        if (GameManager.Instance.IsImageMode)
        { // 이미지 고르기 선택한 플레이어일 때
            playerImage1P.sprite = GameManager.Instance.ProfileImages[GameManager.Instance.DefaultImage];
        }
        else if(!GameManager.Instance.IsImageMode) 
        { // 사진 찍기를 선택한 플레이어일 때
            Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(GameManager.Instance.UID, GameManager.Instance.ProfileIndex);
            Sprite profileSprite = GameManager.Instance.TextureToSprite(profileTexture);
            playerImage1P.sprite = profileSprite;
        }
        playerName1P.text = GameManager.Instance.ProfileName;
    }

    public void Choice2P()
    { // 2Player를 선택하는 Btn 연동 Method
        if(GameManager.Instance.IsimageMode2P)
        { // 이미지 고르기 선택한 플레이어일 때
            playerImage2P.sprite = GameManager.Instance.ProfileImages[GameManager.Instance.DefaultImage2P];
        }
        else if(!GameManager.Instance.IsimageMode2P)
        {// 사진 찍기를 선택한 플레이어일 때
            Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(GameManager.Instance.UID, GameManager.Instance.ProfileIndex2P);
            Sprite profileSprite = GameManager.Instance.TextureToSprite(profileTexture);
            playerImage2P.sprite = profileSprite;
        }
        playerName2P.text = GameManager.Instance.ProfileName2P;
        isSelect2P = true;
    }

    public void ImageSet(int index)
    { // Profile에 넣을 Image 셋팅하는 Btn 연동 Method
        if (!isUpdate)
        { // 첫 등록일 때
            if (index.Equals(0))
            { // 사진 찍기 버튼 눌렀을 때
                imagePath = $"{Application.persistentDataPath}/Profile/{GameManager.Instance.UID}_{GameManager.Instance.ProfileIndex2P}.png";
                SQL_Manager.instance.SQL_AddProfileImage($"{imagePath}", GameManager.Instance.UID, GameManager.Instance.ProfileIndex2P);

                PrintProfileList();
                checkPanel.SetActive(false);
                CreateImagePanel.SetActive(false);
            }
            else if (index.Equals(1))
            { // 이미지 고르기 버튼 눌렀을 때
                if (!isImageSelect)
                { // 이미지 선택을 안했을 때
                    if (log != null)
                    {
                        StopCoroutine(log);
                    }
                    log = StartCoroutine(PrintLog_co(imageLog));
                }
                else
                { // 선택한 이미지가 있을 때
                    GameManager.Instance.IsimageMode2P = true;
                    AddProfile();
                    SQL_Manager.instance.SQL_AddProfileImage(imageIndex, GameManager.Instance.UID, GameManager.Instance.ProfileIndex2P);

                    PrintProfileList();
                    iconPanel.SetActive(false);
                    CreateImagePanel.SetActive(false);
                    SelectProfile.SetActive(true);
                }
            }
        }
        else if (isUpdate)
        { // 수정모드일 때
            if (index.Equals(0))
            { // 사진찍기 모드 눌렀을 때

            }
            else if (index.Equals(1))
            { // 이미지 고르기 버튼 눌렀을 때
                if (isImageSelect)
                { // 선택한 이미지가 없을 때
                    if (log != null)
                    {
                        StopCoroutine(log);
                    }
                    log = StartCoroutine(PrintLog_co(imageLog));
                }
                else
                { // 선택한 이미지가 있을 때
                    GameManager.Instance.IsimageMode2P = true;
                    AddProfile();
                    SQL_Manager.instance.SQL_UpdateProfile(GameManager.Instance.ProfileIndex2P, profile2PName, GameManager.Instance.UID, imageIndex);

                    PrintProfileList();
                    iconPanel.SetActive(false);
                    CreateImagePanel.SetActive(false);
                    SelectProfile.SetActive(true);
                    isUpdate = false;
                }
            }
        }
    }

    // Profile List 셋팅 후 Image, Name을 출력하는 Method (2P Mode, 1P로 설정해놓은 프로필은 출력 X) 
    public void PrintProfileList()
    {
        // DB에 UID별로 저장되어있는 Profile들을 SQL_Manager에 List Up 해놓음
        SQL_Manager.instance.SQL_ProfileListSet();

        for (int i = 0; i < profileList.Count; i++)
        { // 출력 전 기존에 출력되어 있는 List가 있다면 초기화
            Destroy(profileList[i].gameObject);
        }
        profileList.Clear();

        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        { // SQL_Manager에 Query문을 이용하여 UID에 담긴 Profile만큼 List를 셋팅하고, 해당 List의 Count 만큼 Profile Panel 생성
            GameObject panel = Instantiate(profilePanel);
            panel.transform.SetParent(profileParent);
            profileList.Add(panel);
        }

        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        { // Panel의 Index 별로 Profile_Information 컴포넌트를 가져와서 name과 image를 Mode에 맞게 셋팅
            Profile_Information info = profileList[i].GetComponent<Profile_Information>();
            info.Profile_name.text = SQL_Manager.instance.Profile_list[i].name;
            if (SQL_Manager.instance.Profile_list[i].imageMode)
            { // 이미지 선택으로 설정 했을 경우
                info.ProfileImage.sprite = GameManager.Instance.ProfileImages[SQL_Manager.instance.Profile_list[i].defaultImage];
            }
            else
            { // 사진 찍기로 설정 했을 경우
                Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(GameManager.Instance.UID, SQL_Manager.instance.Profile_list[i].index);
                Sprite profileSprite = GameManager.Instance.TextureToSprite(profileTexture);
                info.ProfileImage.sprite = profileSprite;
            }
        }

        for(int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        { // 세팅을 마친 후 본인의 프로필은 삭제
            if(GameManager.Instance.ProfileIndex == SQL_Manager.instance.Profile_list[i].index)
            { // index = index로 매칭 시켜놓은 로직 중간에 예외처리로 삭제하면 index오류가 날 것을 우려하여 세팅을 다 마친 후 본인 프로필 삭제
                Profile tempProfile = SQL_Manager.instance.Profile_list[i];
                SQL_Manager.instance.Profile_list.Remove(tempProfile);
                GameObject list = profileList[i];
                profileList.Remove(list);
                Destroy(list);
            }
        }
    }
    
    public void Update_Profile()
    { // Update Mode Bool값 설정 Btn 연동 Method
        isUpdate = true;
    }

    public void DeleteProfile()
    { // 프로필 삭제 Btn 연동 Method (2P)
        SQL_Manager.instance.SQL_DeleteProfile(GameManager.Instance.ProfileName2P, GameManager.Instance.ProfileIndex2P);
    }

    public void AddProfile()
    { // Bomb 모드에서 2P 프로필 선택 시 Profile 등록하는 Btn 연동 Method
        int imageMode = -1;
        switch (GameManager.Instance.IsimageMode2P)
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
            if (!string.IsNullOrWhiteSpace(profile2PName))
            {
                GameManager.Instance.ProfileIndex2P = SQL_Manager.instance.SQL_AddProfile(profile2PName, imageMode);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(profile2PName))
                {

                }
            }
        }
        else if (isUpdate)
        { // 수정 중일 때
            SQL_Manager.instance.SQL_UpdateMode(imageMode, GameManager.Instance.UID, GameManager.Instance.ProfileIndex2P);
        }
    }

    public void SendProfile()
    { // Profile Add 하기 전 InputField에 저장된 이름을 변수에 저장해주는 Btn 연동 Method
        profile2PName = profile2PInput.text;
        profile2PInput.text = string.Empty;
    }

    public void SelectImage(int index)
    { // Profile Image Index를 저장하는 Method
        imageIndex = index;
        isImageSelect = true;
    }

    public void DeleteBtnOpen()
    { // 삭제 버튼 List만큼 출력 Btn 연동 Method
        if (profileList.Count > 0)
        { // 프로필 리스트에 담긴 ProfilePanel의 삭제 버튼을 껐다 켜줌
            bool active = profileList[0].GetComponent<Profile_Information>().DelBtn.activeSelf;
            for (int i = 0; i < profileList.Count; i++)
            {
                profileList[i].GetComponent<Profile_Information>().DelBtn.SetActive(!active);
            }
        }
        else
        { // 삭제할 프로필이 없는 경우
            return;
        }
    }

    private char ValidateInput(string text, int charIndex, char addedChar)
    { // Profile 영어, 숫자 입력 못하도록 설정
        // 입력된 문자가 영어 알파벳, 숫자인 경우 입력을 막음
        if ((addedChar >= 'a' && addedChar <= 'z') || (addedChar >= 'A' && addedChar <= 'Z') || (addedChar >= '0' && addedChar <= '9'))
        {
            if (log != null)
            {
                StopCoroutine(log);
            }
            log = StartCoroutine(PrintLog_co(nameLog));
            return '\0'; // 입력 막음
        }

        // 다른 문자는 허용
        return addedChar;
    }
    #endregion

    #region Game Logic
    public void StartGame()
    { // 게임 시작 버튼 눌렀을 때 2P 설정이 되어 있는지 확인 후 GamePanel 켜주는 Btn 연동 Method
        if(isSelect2P)
        {
            MainPanel.gameObject.SetActive(false);
            GamePanel.SetActive(true);
        }
        else
        { // 2P 선택이 되지 않은 경우 ErrorLog를 출력하고 return
            if(log != null)
            {
                StopCoroutine(log);
            }
            log = StartCoroutine(PrintLog_co(need2P));
            return;
        }
    }
    #endregion
    private IEnumerator PrintLog_co(GameObject errorlog)
    { // ErrorLog 출력 Coroutine
        errorlog.SetActive(true);

        yield return new WaitForSeconds(3f);

        errorlog.SetActive(false);
        log = null;
    }
    #endregion
}
