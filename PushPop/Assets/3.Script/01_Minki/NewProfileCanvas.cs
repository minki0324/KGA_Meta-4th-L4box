using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class NewProfileCanvas : MonoBehaviour, IPointerClickHandler
{
    [Header("Select Profile Panel")] [Space(5)]
    public GameObject SelectProfilePanel;
    [SerializeField] private Transform profileParent;
    [SerializeField] private ScrollRect profileScrollView;

    [Header("Create Name Panel")] [Space(5)]
    public GameObject CreateNamePanel;
    [SerializeField] private TMP_InputField inputProfileName;
    [SerializeField] private TMP_Text nameErrorLog;

    [Header("Create Image Panel")] [Space(5)]
    [SerializeField] private GameObject createImagePanel;
    [SerializeField] private GameObject iconPanel;
    [SerializeField] private GameObject checkPanel;
    
    private int imageIndex;

    [Header("Current Profile Panel")] [Space(5)]
    public GameObject CurrnetProfilePanel;
    public TMP_Text SelectProfileText;
    public Image SelectProfileImage;

    [Header("Delete Panel")] [Space(5)]
    public GameObject DeletePanel;

    #region Unity Callback
    private void OnEnable()
    {
        inputProfileName.onValidateInput += ValidateInput;
    }

    void Start()
    {
        ProfileManager.Instance.PrintProfileList(profileParent, null, null);
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
    public void DeleteBtnOpen()
    { // SelectProfilePanel º” Btn ø¨µø Method
        if (ProfileManager.Instance.ProfilePanelList.Count > 0)
        { 
            bool active = ProfileManager.Instance.ProfilePanelList[0].GetComponent<NewProfile_Infomation>().DelBtn.activeSelf;
            for (int i = 0; i < ProfileManager.Instance.ProfilePanelList.Count; i++)
            {
                ProfileManager.Instance.ProfilePanelList[i].GetComponent<NewProfile_Infomation>().DelBtn.SetActive(!active);
            }
        }
    }

    public void DeleteProfile()
    { // DeletePanel º” Btn ø¨µø Method
        ProfileManager.Instance.DeleteProfile(ProfileManager.Instance.ProfileIndex1P);
        ProfileManager.Instance.PrintProfileList(profileParent, null, null);
        DeletePanel.SetActive(false);
    }

    
    public void SendProfile()
    { // Profile Add «œ±‚ ¿¸ InputFieldø° ¿˙¿Âµ» ¿Ã∏ß¿ª ∫Øºˆø° ¿˙¿Â«ÿ¡÷¥¬ Btn ø¨µø Method
        bool bPossibleName = true;

        for (int i = 0; i < inputProfileName.text.Length; i++)
        {
            if (Regex.IsMatch(inputProfileName.text[i].ToString(), @"[^0-9a-zA-Z∞°-∆R]"))
            {
                if (DialogManager.instance.log_co != null)
                {
                    DialogManager.instance.StopCoroutine(DialogManager.instance.log_co);
                }
                DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(nameErrorLog, "√ º∫ ¿‘∑¬¿∫ ∫“∞°¥…«’¥œ¥Ÿ."));
                inputProfileName.text = string.Empty;    //¥Ÿ¡ˆøÏ¥¬æ÷
                bPossibleName = false;
            }
        }
        if (bPossibleName)
        {
            if (inputProfileName.text != string.Empty && inputProfileName.text.Length > 1)
            {
                ProfileManager.Instance.ProfileName1P = inputProfileName.text;
                inputProfileName.text = string.Empty;
                CreateNamePanel.SetActive(false);
                createImagePanel.SetActive(true);
            }
            else
            { 
                if (DialogManager.instance.log_co != null)
                {
                    DialogManager.instance.StopCoroutine(DialogManager.instance.log_co);
                }
                DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(nameErrorLog, "2~6±€¿⁄¿« ¿Ã∏ß¿ª ¿‘∑¬«ÿ¡÷ººø‰."));
            }
        }
    }

    public void CreateNamePanelBackBtn()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        CreateNamePanel.SetActive(false);
        ProfileManager.Instance.PrintProfileList(profileParent, null, null);
        if (ProfileManager.Instance.isUpdate)
        {
            //ºˆ¡§∏µÂ¿Ã∏È µ⁄∑Œ∞°±‚ ¥≠∑∂¿ª ∂ß ¥≠∏∞ «¡∑Œ«  ∂ÁøÏ±‚
            CurrnetProfilePanel.SetActive(true);
        }
        else
        {
            if (ProfileManager.Instance.isProfileSelected)
            {
                CurrnetProfilePanel.SetActive(true);
            }
            else
            {
                profileScrollView.normalizedPosition = new Vector2(1f, 1f);
                SelectProfilePanel.SetActive(true);

            }
        }
    }

    private char ValidateInput(string text, int charIndex, char addedChar)
    { // Profile øµæÓ, º˝¿⁄ ¿‘∑¬ ∏¯«œµµ∑œ º≥¡§
        // ¿‘∑¬µ» πÆ¿⁄∞° øµæÓ æÀ∆ƒ∫™, º˝¿⁄¿Œ ∞ÊøÏ ¿‘∑¬¿ª ∏∑¿Ω
        if ((addedChar >= 'a' && addedChar <= 'z') || (addedChar >= 'A' && addedChar <= 'Z') || (addedChar >= '0' && addedChar <= '9'))
        {
            if (DialogManager.instance.log_co != null)
            {
                DialogManager.instance.StopCoroutine(DialogManager.instance.log_co);
            }
            DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(nameErrorLog, "«—±€∑Œ ¿‘∑¬ «ÿ¡÷ººø‰."));
            return '\0'; // ¿‘∑¬ ∏∑¿Ω
        }

        // ¥Ÿ∏• πÆ¿⁄¥¬ «„øÎ
        return addedChar;
    }

    public void ImageSetting(bool _mode)
    { // ªÁ¡¯¬Ô±‚¿Œ¡ˆ, ¿ÃπÃ¡ˆ ∞Ì∏£±‚¿Œ¡ˆ »Æ¿Œ«œ∞Ì, 1P¿« Image∏¶ ºº∆√«œ¥¬ Btn ø¨µø Method

        if(ProfileManager.Instance.ImageSet(_mode, true, nameErrorLog, ProfileManager.Instance.ProfileName1P, imageIndex))
        { // «¡∑Œ«  ºº∆√ øœ∑· «ﬂ¿ª ∂ß
            ProfileManager.Instance.PrintProfileList(profileParent, null, null);
            // PanelµÈ Active
            if (_mode)
            { // ¿ÃπÃ¡ˆ ∞Ì∏£±‚ πˆ∆∞ ¥≠∑∂¿ª ∂ß
                iconPanel.SetActive(false);
                createImagePanel.SetActive(false);
                SelectProfilePanel.SetActive(true);
                if (ProfileManager.Instance.isUpdate)
                {
                    ProfileManager.Instance.isUpdate = false;
                }
            }
            else
            { // ªÁ¡¯ ¬Ô±‚ πˆ∆∞ ¥≠∑∂¿ª ∂ß
                checkPanel.SetActive(false);
                createImagePanel.SetActive(false);
            }
        }
        else
        { // ºº∆√ µµ¡ﬂ ø¿∑˘∞° ≥™ø‘¿ª ∂ß
            return;
        }
    }

    public void SelectImage(int index)
    {
        imageIndex = index;
        ProfileManager.Instance.isImageSelect = true;
    }
    #endregion
}
