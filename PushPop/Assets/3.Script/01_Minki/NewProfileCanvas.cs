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
    { // SelectProfilePanel 속 Btn 연동 Method
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
    { // DeletePanel 속 Btn 연동 Method
        ProfileManager.Instance.DeleteProfile(ProfileManager.Instance.ProfileIndex1P);
        ProfileManager.Instance.PrintProfileList(profileParent, null, null);
        DeletePanel.SetActive(false);
    }

    
    public void SendProfile()
    { // Profile Add 하기 전 InputField에 저장된 이름을 변수에 저장해주는 Btn 연동 Method
        bool bPossibleName = true;

        for (int i = 0; i < inputProfileName.text.Length; i++)
        {
            if (Regex.IsMatch(inputProfileName.text[i].ToString(), @"[^0-9a-zA-Z가-힣]"))
            {
                if (DialogManager.instance.log_co != null)
                {
                    DialogManager.instance.StopCoroutine(DialogManager.instance.log_co);
                }
                DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(nameErrorLog, "초성 입력은 불가능합니다."));
                inputProfileName.text = string.Empty;    //다지우는애
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
                DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(nameErrorLog, "2~6글자의 이름을 입력해주세요."));
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
            //수정모드이면 뒤로가기 눌렀을 때 눌린 프로필 띄우기
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
    { // Profile 영어, 숫자 입력 못하도록 설정
        // 입력된 문자가 영어 알파벳, 숫자인 경우 입력을 막음
        if ((addedChar >= 'a' && addedChar <= 'z') || (addedChar >= 'A' && addedChar <= 'Z') || (addedChar >= '0' && addedChar <= '9'))
        {
            if (DialogManager.instance.log_co != null)
            {
                DialogManager.instance.StopCoroutine(DialogManager.instance.log_co);
            }
            DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(nameErrorLog, "한글로 입력 해주세요."));
            return '\0'; // 입력 막음
        }

        // 다른 문자는 허용
        return addedChar;
    }

    public void ImageSetting(bool _mode)
    { // 사진찍기인지, 이미지 고르기인지 확인하고, 1P의 Image를 세팅하는 Btn 연동 Method

        if(ProfileManager.Instance.ImageSet(_mode, true, nameErrorLog, ProfileManager.Instance.ProfileName1P, imageIndex))
        { // 프로필 세팅 완료 했을 때
            ProfileManager.Instance.PrintProfileList(profileParent, null, null);
            // Panel들 Active
            if (_mode)
            { // 이미지 고르기 버튼 눌렀을 때
                iconPanel.SetActive(false);
                createImagePanel.SetActive(false);
                SelectProfilePanel.SetActive(true);
                if (ProfileManager.Instance.isUpdate)
                {
                    ProfileManager.Instance.isUpdate = false;
                }
            }
            else
            { // 사진 찍기 버튼 눌렀을 때
                checkPanel.SetActive(false);
                createImagePanel.SetActive(false);
            }
        }
        else
        { // 세팅 도중 오류가 나왔을 때
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
