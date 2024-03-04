using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using System;

/// <summary>
/// Profile Canvas ���� Class
/// </summary>
public class NewProfileCanvas : MonoBehaviour, IPointerClickHandler
{
    [Header("Other Component")] [Space(5)]
    [SerializeField] private CameraManager cameraManager; // �ܺ� Camera ���� Script
    [SerializeField] private Image CaptureImage;

    [Header("Game Lobby")] [Space(5)]
    [SerializeField] private GameObject MainButtonPanel; // Main Game Lobby�� ��ư�� Panel
    [SerializeField] private GameObject GameModePanel; // Main Game Lobby�� GameMode Panel

    [Header("Main Panel")] [Space(5)]
    public Button ExitBtn; // ���� ��ư

    [Header("Select Profile Panel")] [Space(5)]
    public GameObject SelectProfilePanel; // Select Profile Panel
    public Transform profileParent; // Profile List�� ����� �θ� Transform
    [SerializeField] private ScrollRect profileScrollView; // Profile List�� ScrollView

    [Header("Create Name Panel")] [Space(5)]
    public GameObject CreateNamePanel; // Create Name Panel
    [SerializeField] private TMP_InputField inputProfileName; // Name ���� InputField
    [SerializeField] private TMP_Text nameErrorLog; // Name ���� ����� Text

    [Header("Create Image Panel")] [Space(5)]
    [SerializeField] private GameObject createImagePanel; // Create Image Panel
    [SerializeField] private GameObject iconPanel; // �̹��� Icon Panel
    [SerializeField] private GameObject checkPanel; // ���� �Կ� Panel
    public int imageIndex; // Icon�� Index

    [Header("Current Profile Panel")] [Space(5)]
    public GameObject CurrentProfilePanel; // Current Profile panel
    public TMP_Text SelectProfileText; // ���õ� Profile�� Name ����ϴ� Text
    public Image SelectProfileImage; // ���õ� Profile�� Image ����ϴ� Image

    [Header("Delete Panel")] [Space(5)]
    public GameObject DeletePanel; // Delete Panel

    public bool isLobby = false; // �������� �����ϰ� �κ� ������ �ƴ��� Ȯ���ϴ� Bool��

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
    { // DeletePanel �� Btn ���� Method
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.DeleteProfile(ProfileManager.Instance.tempIndex);
        ProfileManager.Instance.PrintProfileList(profileParent, ProfileManager.Instance.ProfileIndex1P);
        DeletePanel.SetActive(false);
        Enable_ExitBtn(true);
    }

    public void SendProfile(bool _player)
    { // Profile Add �ϱ� �� InputField�� ����� �̸��� ������ �������ִ� Btn ���� Method 
        // �� ���ǵ��� ����ϸ� (��Ӿ�, �ʼ�, ���ڼ� ���� ��) ��� ������ Name���� �����ϰ� Profile Manager�� �� �÷��̾��� name ������ �߰�

        for (int i = 0; i < DataManager2.instance.vulgarism_Arr.Length; i++)
        { // ��Ӿ� üũ
            if (inputProfileName.text.Contains(DataManager2.instance.vulgarism_Arr[i]))
            {
                if (DataManager2.instance.vulgarism_Arr[i] != string.Empty)
                {
                    if(DialogManager.instance.log_co != null)
                    {
                        StopCoroutine(DialogManager.instance.log_co);
                    }
                    DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(nameErrorLog, "��Ӿ�� ���Խ�ų �� �����ϴ�."));
                    inputProfileName.text = String.Empty;  
                    return;
                }
            }
        }

        for (int i = 0; i < inputProfileName.text.Length; i++)
        { // �ʼ� üũ
            if (Regex.IsMatch(inputProfileName.text[i].ToString(), @"[^0-9a-zA-Z��-�R]"))
            {
                if (DialogManager.instance.log_co != null)
                {
                    StopCoroutine(DialogManager.instance.log_co);
                }
                DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(nameErrorLog, "�ʼ� �Է��� �Ұ����մϴ�."));
                inputProfileName.text = String.Empty;  
                return;
            }
        }

        if (inputProfileName.text != string.Empty && inputProfileName.text.Length > 1)
        { // ���ڼ� üũ
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
            DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(nameErrorLog, "2~6������ �̸��� �Է����ּ���."));
        }
    }

    private char ValidateInput(string text, int charIndex, char addedChar)
    { // Profile ����, ���� �Է� ���ϵ��� ����
        // �Էµ� ���ڰ� ���� ���ĺ�, ������ ��� �Է��� ����
        if ((addedChar >= 'a' && addedChar <= 'z') || (addedChar >= 'A' && addedChar <= 'Z') || (addedChar >= '0' && addedChar <= '9'))
        {
            if (DialogManager.instance.log_co != null)
            {
                DialogManager.instance.StopCoroutine(DialogManager.instance.log_co);
            }
            DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(nameErrorLog, "�ѱ۷� �Է� ���ּ���."));
            return '\0'; // �Է� ����
        }

        // �ٸ� ���ڴ� ���
        return addedChar;
    }

    public void ImageSetting(bool _mode)
    { // �����������, �̹��� �������� Ȯ���ϰ�, 1P�� Image�� �����ϴ� Btn ���� Method
        if(ProfileManager.Instance.ImageSet(_mode, true, ProfileManager.Instance.tempName, imageIndex, nameErrorLog))
        { // ������ ���� �Ϸ� ���� ��
          // �κ񿡼��� ������ Profile List�� �����ϰ� ����ؾ���
            if (isLobby) ProfileManager.Instance.PrintProfileList(profileParent, ProfileManager.Instance.ProfileIndex1P);
            else ProfileManager.Instance.PrintProfileList(profileParent, null);

            // Panel�� Active
            if (_mode)
            { // �̹��� ���� ��ư ������ ��
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
            { // ���� ��� ��ư ������ ��
                checkPanel.SetActive(true);
                createImagePanel.SetActive(false);
            }
        }
        else
        { // ���� ���� ������ ������ ��
            Debug.Log("��� ����");
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
    { // ���� ��� �� �ٽ� ��� Btn ���� Method
        SQL_Manager.instance.SQL_DeleteProfile(ProfileManager.Instance.tempIndex);
        cameraManager.CameraOpen(CaptureImage);
    }

    #region Active
    public void SelectProfilePanelCreateBtn()
    { // Select Profile Panel �� CreateBtn ���� Method
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.isUpdate = false;
        CreateNamePanel.SetActive(true);
        if(!isLobby)
        { // ���� �κ񿡼��� ���� ��ư�� Ȱ��ȭ �Ǹ� �ȵ�
            ExitBtn.gameObject.SetActive(false);
        }
    }

    public void SelectProfilePanelDeleteBtn()
    { // SelectProfilePanel �� Btn ���� Method
        // Profile List�� Count��ŭ Profile Panel�� ���� üũ�ڽ� ���ֱ�
        if (ProfileManager.Instance.ProfilePanelList.Count > 0)
        { 
            bool active = ProfileManager.Instance.ProfilePanelList[0].GetComponent<NewProfile_Infomation>().DelBtn.activeSelf;
            for (int i = 0; i < ProfileManager.Instance.ProfilePanelList.Count; i++)
            {
                ProfileManager.Instance.ProfilePanelList[i].GetComponent<NewProfile_Infomation>().DelBtn.SetActive(!active);
            }
            if(!isLobby)
            { // ���� �κ񿡼��� ���� ��ư�� Ȱ��ȭ �Ǹ� �ȵ�
                ExitBtn.interactable = active;
            }
        }
    }

    public void CreateNamePanelBackBtn()
    { // Create Name Panel �� BackBtn ���� Method
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        CreateNamePanel.SetActive(false);

        // �κ񿡼��� ������ Profile List�� �����ϰ� ����ؾ���
        if(isLobby) ProfileManager.Instance.PrintProfileList(profileParent, ProfileManager.Instance.ProfileIndex1P);
        else ProfileManager.Instance.PrintProfileList(profileParent, null);
        
        if (ProfileManager.Instance.isUpdate)
        { //��������̸� �ڷΰ��� ������ �� ���� ������ ����
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
                { // ���� �κ񿡼� ���� ��ư�� Ȱ��ȭ �Ǹ� �ȵ�
                    ExitBtn.gameObject.SetActive(true);
                }
            }
        }
    }

    public void CreateImagePanelIconBackBtn()
    { // Create Image Panel �� Icon Panel�� BackBtn ���� Method
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        iconPanel.SetActive(false);

        // �κ񿡼��� ������ Profile List�� �����ϰ� ����ؾ���
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
    { // Current Profile Panel �� SelectBtn ���� Method
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
    { // Current Profile Panel �� ChangeBtn ���� Method
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        ProfileManager.Instance.isUpdate = true;

        CurrentProfilePanel.SetActive(false);
        CreateNamePanel.SetActive(true);
    }

    public void CurrentProfilePanelReturnBtn()
    { // Current Profile Panel �� ReturnBtn ���� Method
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        profileScrollView.normalizedPosition = new Vector2(1f, 1f);
        CurrentProfilePanel.SetActive(false);
        SelectProfilePanel.SetActive(true);
        if(!isLobby)
        { // ���� �κ񿡼� ���� ��ư�� Ȱ��ȭ �Ǹ� �ȵ�
            ExitBtn.gameObject.SetActive(true);
        }
    }
    #endregion
    #endregion
}
