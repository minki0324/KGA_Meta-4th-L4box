using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using System;

/// <summary>
/// Profile Canvas °ü·Ã Class
/// </summary>
public class NewProfileCanvas : MonoBehaviour, IPointerClickHandler
{
    [Header("Other Component")] [Space(5)]
    [SerializeField] private CameraManager cameraManager; // ¿ÜºÎ Camera ¿¬µ¿ Script
    [SerializeField] private Image CaptureImage;

    [Header("Game Lobby")] [Space(5)]
    [SerializeField] private GameObject MainButtonPanel; // Main Game LobbyÀÇ ¹öÆ°µé Panel
    [SerializeField] private GameObject GameModePanel; // Main Game LobbyÀÇ GameMode Panel

    [Header("Main Panel")] [Space(5)]
    public Button ExitBtn; // Á¾·á ¹öÆ°

    [Header("Select Profile Panel")] [Space(5)]
    public GameObject SelectProfilePanel; // Select Profile Panel
    public Transform profileParent; // Profile Listµé »ó¼ÓÇÒ ºÎ¸ð Transform
    [SerializeField] private ScrollRect profileScrollView; // Profile ListÀÇ ScrollView

    [Header("Create Name Panel")] [Space(5)]
    public GameObject CreateNamePanel; // Create Name Panel
    [SerializeField] private TMP_InputField inputProfileName; // Name Àû´Â InputField
    [SerializeField] private TMP_Text nameErrorLog; // Name ¿À·ù Ãâ·ÂÇÒ Text

    [Header("Create Image Panel")] [Space(5)]
    [SerializeField] private GameObject createImagePanel; // Create Image Panel
    [SerializeField] private GameObject iconPanel; // ÀÌ¹ÌÁö Icon Panel
    [SerializeField] private GameObject checkPanel; // »çÁø ÃÔ¿µ Panel
    public int imageIndex; // IconÀÇ Index

    [Header("Current Profile Panel")] [Space(5)]
    public GameObject CurrentProfilePanel; // Current Profile panel
    public TMP_Text SelectProfileText; // ¼±ÅÃµÈ ProfileÀÇ Name Ãâ·ÂÇÏ´Â Text
    public Image SelectProfileImage; // ¼±ÅÃµÈ ProfileÀÇ Image Ãâ·ÂÇÏ´Â Image

    [Header("Delete Panel")] [Space(5)]
    public GameObject DeletePanel; // Delete Panel

    public bool isLobby = false; // ÇÁ·ÎÇÊÀ» ¼±ÅÃÇÏ°í ·Îºñ¿¡ µé¾î°¬´ÂÁö ¾Æ´ÑÁö È®ÀÎÇÏ´Â Bool°ª

    #region Unity Callback
    private void OnEnable()
    {
        inputProfileName.onValidateInput += ValidateInput;
    }

    void Start()
    {
        ProfileManager.Instance.LoadOrCreateGUID();
        ProfileManager.Instance.PrintProfileList(profileParent, null);
    }

    private void OnDisable()
    {
        inputProfileName.onValidateInput -= ValidateInput;
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
                ProfileManager.Instance.isImageSelect = false;
            }
        }
    }
    #endregion

    #region Other Method
    public void DeleteProfile()
    { // DeletePanel ¼Ó Btn ¿¬µ¿ Method
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.DeleteProfile(ProfileManager.Instance.tempIndex);
        ProfileManager.Instance.PrintProfileList(profileParent, ProfileManager.Instance.ProfileIndex1P);
        DeletePanel.SetActive(false);
        Enable_ExitBtn(true);
    }

    public void SendProfile(bool _player)
    { // Profile Add ÇÏ±â Àü InputField¿¡ ÀúÀåµÈ ÀÌ¸§À» º¯¼ö¿¡ ÀúÀåÇØÁÖ´Â Btn ¿¬µ¿ Method 
        // °¢ Á¶°ÇµéÀ» Åë°úÇÏ¸é (ºñ¼Ó¾î, ÃÊ¼º, ±ÛÀÚ¼ö Á¦ÇÑ µî) µî·Ï °¡´ÉÇÑ NameÀ¸·Î ÆÇÁ¤ÇÏ°í Profile ManagerÀÇ °¢ ÇÃ·¹ÀÌ¾îÀÇ name º¯¼ö¿¡ Ãß°¡

        for (int i = 0; i < DataManager2.instance.vulgarism_Arr.Length; i++)
        { // ºñ¼Ó¾î Ã¼Å©
            if (inputProfileName.text.Contains(DataManager2.instance.vulgarism_Arr[i]))
            {
                if (DataManager2.instance.vulgarism_Arr[i] != string.Empty)
                {
                    if(DialogManager.instance.log_co != null)
                    {
                        StopCoroutine(DialogManager.instance.log_co);
                    }
                    DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(nameErrorLog, "ºñ¼Ó¾î´Â Æ÷ÇÔ½ÃÅ³ ¼ö ¾ø½À´Ï´Ù."));
                    inputProfileName.text = String.Empty;  
                    return;
                }
            }
        }

        for (int i = 0; i < inputProfileName.text.Length; i++)
        { // ÃÊ¼º Ã¼Å©
            if (Regex.IsMatch(inputProfileName.text[i].ToString(), @"[^0-9a-zA-Z°¡-ÆR]"))
            {
                if (DialogManager.instance.log_co != null)
                {
                    StopCoroutine(DialogManager.instance.log_co);
                }
                DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(nameErrorLog, "ÃÊ¼º ÀÔ·ÂÀº ºÒ°¡´ÉÇÕ´Ï´Ù."));
                inputProfileName.text = String.Empty;  
                return;
            }
        }

        if (inputProfileName.text != string.Empty && inputProfileName.text.Length > 1)
        { // ±ÛÀÚ¼ö Ã¼Å©
            ProfileManager.Instance.tempName = inputProfileName.text;
            inputProfileName.text = string.Empty;
            CreateNamePanel.SetActive(false);
            createImagePanel.SetActive(true);
        }
        else
        { 
            if (DialogManager.instance.log_co != null)
            {
                StopCoroutine(DialogManager.instance.log_co);
            }
            DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(nameErrorLog, "2~6±ÛÀÚÀÇ ÀÌ¸§À» ÀÔ·ÂÇØÁÖ¼¼¿ä."));
        }
    }

    private char ValidateInput(string text, int charIndex, char addedChar)
    { // Profile ¿µ¾î, ¼ýÀÚ ÀÔ·Â ¸øÇÏµµ·Ï ¼³Á¤
        // ÀÔ·ÂµÈ ¹®ÀÚ°¡ ¿µ¾î ¾ËÆÄºª, ¼ýÀÚÀÎ °æ¿ì ÀÔ·ÂÀ» ¸·À½
        if ((addedChar >= 'a' && addedChar <= 'z') || (addedChar >= 'A' && addedChar <= 'Z') || (addedChar >= '0' && addedChar <= '9'))
        {
            if (DialogManager.instance.log_co != null)
            {
                DialogManager.instance.StopCoroutine(DialogManager.instance.log_co);
            }
            DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(nameErrorLog, "ÇÑ±Û·Î ÀÔ·Â ÇØÁÖ¼¼¿ä."));
            return '\0'; // ÀÔ·Â ¸·À½
        }

        // ´Ù¸¥ ¹®ÀÚ´Â Çã¿ë
        return addedChar;
    }

    public void ImageSetting(bool _mode)
    { // »çÁøÂï±âÀÎÁö, ÀÌ¹ÌÁö °í¸£±âÀÎÁö È®ÀÎÇÏ°í, 1PÀÇ Image¸¦ ¼¼ÆÃÇÏ´Â Btn ¿¬µ¿ Method
        if(ProfileManager.Instance.ImageSet(_mode, true, ProfileManager.Instance.tempName, imageIndex, nameErrorLog))
        { // ÇÁ·ÎÇÊ ¼¼ÆÃ ¿Ï·á ÇßÀ» ¶§
          // ·Îºñ¿¡¼­´Â º»ÀÎÀÇ Profile List¸¦ Á¦¿ÜÇÏ°í Ãâ·ÂÇØ¾ßÇÔ
            if (isLobby) ProfileManager.Instance.PrintProfileList(profileParent, ProfileManager.Instance.ProfileIndex1P);
            else ProfileManager.Instance.PrintProfileList(profileParent, null);

            // Panelµé Active
            if (_mode)
            { // ÀÌ¹ÌÁö °í¸£±â ¹öÆ° ´­·¶À» ¶§
                iconPanel.SetActive(false);
                createImagePanel.SetActive(false);
                SelectProfilePanel.SetActive(true);
                if (ProfileManager.Instance.isUpdate)
                {
                    ProfileManager.Instance.isUpdate = false;
                }
                if (!isLobby)
                {
                    ExitBtn.gameObject.SetActive(true);
                }
            }
            else
            { // »çÁø Âï±â ¹öÆ° ´­·¶À» ¶§
                checkPanel.SetActive(true);
                createImagePanel.SetActive(false);
            }
        }
        else
        { // ¼¼ÆÃ µµÁß ¿À·ù°¡ ³ª¿ÔÀ» ¶§
            Debug.Log("µî·Ï ½ÇÆÐ");
            return;
        }
    }

    public void SelectImage(int index)
    {
        imageIndex = index;
        ProfileManager.Instance.isImageSelect = true;
    }

    public void Enable_ExitBtn(bool _enable)
    {
        if (_enable)
        {
            ExitBtn.interactable = true;
        }
        else
        {
            ExitBtn.interactable = false;
        }
    }

    public void TakeAgainPicture()
    { // »çÁø Âï°í³­ µÚ ´Ù½Ã Âï±â Btn ¿¬µ¿ Method
        SQL_Manager.instance.SQL_DeleteProfile(ProfileManager.Instance.tempIndex);
        cameraManager.CameraOpen(CaptureImage);
    }

    #region Active
    public void SelectProfilePanelCreateBtn()
    { // Select Profile Panel ¼Ó CreateBtn ¿¬µ¿ Method
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.isUpdate = false;
        CreateNamePanel.SetActive(true);
        if(!isLobby)
        { // ¸ÞÀÎ ·Îºñ¿¡¼­´Â Á¾·á ¹öÆ°ÀÌ È°¼ºÈ­ µÇ¸é ¾ÈµÊ
            ExitBtn.gameObject.SetActive(false);
        }
    }

    public void SelectProfilePanelDeleteBtn()
    { // SelectProfilePanel ¼Ó Btn ¿¬µ¿ Method
        // Profile ListÀÇ Count¸¸Å­ Profile PanelÀÇ »èÁ¦ Ã¼Å©¹Ú½º ÄÑÁÖ±â
        if (ProfileManager.Instance.ProfilePanelList.Count > 0)
        { 
            bool active = ProfileManager.Instance.ProfilePanelList[0].GetComponent<NewProfile_Infomation>().DelBtn.activeSelf;
            for (int i = 0; i < ProfileManager.Instance.ProfilePanelList.Count; i++)
            {
                ProfileManager.Instance.ProfilePanelList[i].GetComponent<NewProfile_Infomation>().DelBtn.SetActive(!active);
            }
            if(!isLobby)
            { // ¸ÞÀÎ ·Îºñ¿¡¼­´Â Á¾·á ¹öÆ°ÀÌ È°¼ºÈ­ µÇ¸é ¾ÈµÊ
                ExitBtn.interactable = active;
            }
        }
    }

    public void CreateNamePanelBackBtn()
    { // Create Name Panel ¼Ó BackBtn ¿¬µ¿ Method
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        CreateNamePanel.SetActive(false);

        // ·Îºñ¿¡¼­´Â º»ÀÎÀÇ Profile List¸¦ Á¦¿ÜÇÏ°í Ãâ·ÂÇØ¾ßÇÔ
        if(isLobby) ProfileManager.Instance.PrintProfileList(profileParent, ProfileManager.Instance.ProfileIndex1P);
        else ProfileManager.Instance.PrintProfileList(profileParent, null);
        
        if (ProfileManager.Instance.isUpdate)
        { //¼öÁ¤¸ðµåÀÌ¸é µÚ·Î°¡±â ´­·¶À» ¶§ ´­¸° ÇÁ·ÎÇÊ ¶ç¿ì±â
            CurrentProfilePanel.SetActive(true);
        }
        else
        {
            if (ProfileManager.Instance.isProfileSelected && !isLobby)
            {
                CurrentProfilePanel.SetActive(true);
            }
            else
            {
                profileScrollView.normalizedPosition = new Vector2(1f, 1f);
                SelectProfilePanel.SetActive(true);
                if(!isLobby)
                { // ¸ÞÀÎ ·Îºñ¿¡¼± Á¾·á ¹öÆ°ÀÌ È°¼ºÈ­ µÇ¸é ¾ÈµÊ
                    ExitBtn.gameObject.SetActive(true);
                }
            }
        }
    }

    public void CreateImagePanelIconBackBtn()
    { // Create Image Panel ¼Ó Icon PanelÀÇ BackBtn ¿¬µ¿ Method
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        iconPanel.SetActive(false);

        // ·Îºñ¿¡¼­´Â º»ÀÎÀÇ Profile List¸¦ Á¦¿ÜÇÏ°í Ãâ·ÂÇØ¾ßÇÔ
        if (isLobby) ProfileManager.Instance.PrintProfileList(profileParent, ProfileManager.Instance.ProfileIndex1P);
        else ProfileManager.Instance.PrintProfileList(profileParent, null);
    }

    public void CreateImagePanelPictureConfirmBtn()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        CaptureImage.sprite = null;
        checkPanel.SetActive(false);
        createImagePanel.SetActive(false);
        if (isLobby) ProfileManager.Instance.PrintProfileList(profileParent, ProfileManager.Instance.ProfileIndex1P);
        else ProfileManager.Instance.PrintProfileList(profileParent, null);
    }

    public void CurrentProfilePanelSelectBtn()
    { // Current Profile Panel ¼Ó SelectBtn ¿¬µ¿ Method
        if (!ProfileManager.Instance.isProfileSelected)
        {
            ProfileManager.Instance.isProfileSelected = true;
        }
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        isLobby = true;
        GameModePanel.SetActive(true);
        MainButtonPanel.SetActive(true);
        CurrentProfilePanel.SetActive(false);
        gameObject.SetActive(false);
    }

    public void CurrentProfilePanelChangeBtn()
    { // Current Profile Panel ¼Ó ChangeBtn ¿¬µ¿ Method
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        ProfileManager.Instance.isUpdate = true;

        CurrentProfilePanel.SetActive(false);
        CreateNamePanel.SetActive(true);
    }

    public void CurrentProfilePanelReturnBtn()
    { // Current Profile Panel ¼Ó ReturnBtn ¿¬µ¿ Method
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        profileScrollView.normalizedPosition = new Vector2(1f, 1f);
        CurrentProfilePanel.SetActive(false);
        SelectProfilePanel.SetActive(true);
        if(!isLobby)
        { // ¸ÞÀÎ ·Îºñ¿¡¼± Á¾·á ¹öÆ°ÀÌ È°¼ºÈ­ µÇ¸é ¾ÈµÊ
            ExitBtn.gameObject.SetActive(true);
        }
    }
    #endregion
    #endregion
}
