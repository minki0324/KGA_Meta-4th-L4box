using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.PlayerLoop.PreUpdate;
using UnityEngine.EventSystems;
using UnityEditor.Tilemaps;

/// <summary>
/// 2�� ���(��ź ������) ���� Class
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
    { // ������ �������� ����ϴ� Method
        if (GameManager.Instance.IsImageMode)
        { // �̹��� ���� ������ �÷��̾��� ��
            playerImage1P.sprite = GameManager.Instance.ProfileImages[GameManager.Instance.DefaultImage];
        }
        else if(!GameManager.Instance.IsImageMode) 
        { // ���� ��⸦ ������ �÷��̾��� ��
            Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(GameManager.Instance.UID, GameManager.Instance.ProfileIndex);
            Sprite profileSprite = GameManager.Instance.TextureToSprite(profileTexture);
            playerImage1P.sprite = profileSprite;
        }
        playerName1P.text = GameManager.Instance.ProfileName;
    }

    public void Choice2P()
    { // 2Player�� �����ϴ� Btn ���� Method
        if(GameManager.Instance.IsimageMode2P)
        { // �̹��� ���� ������ �÷��̾��� ��
            playerImage2P.sprite = GameManager.Instance.ProfileImages[GameManager.Instance.DefaultImage2P];
        }
        else if(!GameManager.Instance.IsimageMode2P)
        {// ���� ��⸦ ������ �÷��̾��� ��
            Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(GameManager.Instance.UID, GameManager.Instance.ProfileIndex2P);
            Sprite profileSprite = GameManager.Instance.TextureToSprite(profileTexture);
            playerImage2P.sprite = profileSprite;
        }
        playerName2P.text = GameManager.Instance.ProfileName2P;
        isSelect2P = true;
    }

    public void ImageSet(int index)
    { // Profile�� ���� Image �����ϴ� Btn ���� Method
        if (!isUpdate)
        { // ù ����� ��
            if (index.Equals(0))
            { // ���� ��� ��ư ������ ��
                imagePath = $"{Application.persistentDataPath}/Profile/{GameManager.Instance.UID}_{GameManager.Instance.ProfileIndex2P}.png";
                SQL_Manager.instance.SQL_AddProfileImage($"{imagePath}", GameManager.Instance.UID, GameManager.Instance.ProfileIndex2P);

                PrintProfileList();
                checkPanel.SetActive(false);
                CreateImagePanel.SetActive(false);
            }
            else if (index.Equals(1))
            { // �̹��� ���� ��ư ������ ��
                if (!isImageSelect)
                { // �̹��� ������ ������ ��
                    if (log != null)
                    {
                        StopCoroutine(log);
                    }
                    log = StartCoroutine(PrintLog_co(imageLog));
                }
                else
                { // ������ �̹����� ���� ��
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
        { // ��������� ��
            if (index.Equals(0))
            { // ������� ��� ������ ��

            }
            else if (index.Equals(1))
            { // �̹��� ���� ��ư ������ ��
                if (isImageSelect)
                { // ������ �̹����� ���� ��
                    if (log != null)
                    {
                        StopCoroutine(log);
                    }
                    log = StartCoroutine(PrintLog_co(imageLog));
                }
                else
                { // ������ �̹����� ���� ��
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

    // Profile List ���� �� Image, Name�� ����ϴ� Method (2P Mode, 1P�� �����س��� �������� ��� X) 
    public void PrintProfileList()
    {
        // DB�� UID���� ����Ǿ��ִ� Profile���� SQL_Manager�� List Up �س���
        SQL_Manager.instance.SQL_ProfileListSet();

        for (int i = 0; i < profileList.Count; i++)
        { // ��� �� ������ ��µǾ� �ִ� List�� �ִٸ� �ʱ�ȭ
            Destroy(profileList[i].gameObject);
        }
        profileList.Clear();

        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        { // SQL_Manager�� Query���� �̿��Ͽ� UID�� ��� Profile��ŭ List�� �����ϰ�, �ش� List�� Count ��ŭ Profile Panel ����
            GameObject panel = Instantiate(profilePanel);
            panel.transform.SetParent(profileParent);
            profileList.Add(panel);
        }

        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        { // Panel�� Index ���� Profile_Information ������Ʈ�� �����ͼ� name�� image�� Mode�� �°� ����
            Profile_Information info = profileList[i].GetComponent<Profile_Information>();
            info.Profile_name.text = SQL_Manager.instance.Profile_list[i].name;
            if (SQL_Manager.instance.Profile_list[i].imageMode)
            { // �̹��� �������� ���� ���� ���
                info.ProfileImage.sprite = GameManager.Instance.ProfileImages[SQL_Manager.instance.Profile_list[i].defaultImage];
            }
            else
            { // ���� ���� ���� ���� ���
                Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(GameManager.Instance.UID, SQL_Manager.instance.Profile_list[i].index);
                Sprite profileSprite = GameManager.Instance.TextureToSprite(profileTexture);
                info.ProfileImage.sprite = profileSprite;
            }
        }

        for(int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        { // ������ ��ģ �� ������ �������� ����
            if(GameManager.Instance.ProfileIndex == SQL_Manager.instance.Profile_list[i].index)
            { // index = index�� ��Ī ���ѳ��� ���� �߰��� ����ó���� �����ϸ� index������ �� ���� ����Ͽ� ������ �� ��ģ �� ���� ������ ����
                Profile tempProfile = SQL_Manager.instance.Profile_list[i];
                SQL_Manager.instance.Profile_list.Remove(tempProfile);
                GameObject list = profileList[i];
                profileList.Remove(list);
                Destroy(list);
            }
        }
    }
    
    public void Update_Profile()
    { // Update Mode Bool�� ���� Btn ���� Method
        isUpdate = true;
    }

    public void DeleteProfile()
    { // ������ ���� Btn ���� Method (2P)
        SQL_Manager.instance.SQL_DeleteProfile(GameManager.Instance.ProfileName2P, GameManager.Instance.ProfileIndex2P);
    }

    public void AddProfile()
    { // Bomb ��忡�� 2P ������ ���� �� Profile ����ϴ� Btn ���� Method
        int imageMode = -1;
        switch (GameManager.Instance.IsimageMode2P)
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
        { // ���� ���� ��
            SQL_Manager.instance.SQL_UpdateMode(imageMode, GameManager.Instance.UID, GameManager.Instance.ProfileIndex2P);
        }
    }

    public void SendProfile()
    { // Profile Add �ϱ� �� InputField�� ����� �̸��� ������ �������ִ� Btn ���� Method
        profile2PName = profile2PInput.text;
        profile2PInput.text = string.Empty;
    }

    public void SelectImage(int index)
    { // Profile Image Index�� �����ϴ� Method
        imageIndex = index;
        isImageSelect = true;
    }

    public void DeleteBtnOpen()
    { // ���� ��ư List��ŭ ��� Btn ���� Method
        if (profileList.Count > 0)
        { // ������ ����Ʈ�� ��� ProfilePanel�� ���� ��ư�� ���� ����
            bool active = profileList[0].GetComponent<Profile_Information>().DelBtn.activeSelf;
            for (int i = 0; i < profileList.Count; i++)
            {
                profileList[i].GetComponent<Profile_Information>().DelBtn.SetActive(!active);
            }
        }
        else
        { // ������ �������� ���� ���
            return;
        }
    }

    private char ValidateInput(string text, int charIndex, char addedChar)
    { // Profile ����, ���� �Է� ���ϵ��� ����
        // �Էµ� ���ڰ� ���� ���ĺ�, ������ ��� �Է��� ����
        if ((addedChar >= 'a' && addedChar <= 'z') || (addedChar >= 'A' && addedChar <= 'Z') || (addedChar >= '0' && addedChar <= '9'))
        {
            if (log != null)
            {
                StopCoroutine(log);
            }
            log = StartCoroutine(PrintLog_co(nameLog));
            return '\0'; // �Է� ����
        }

        // �ٸ� ���ڴ� ���
        return addedChar;
    }
    #endregion

    #region Game Logic
    public void StartGame()
    { // ���� ���� ��ư ������ �� 2P ������ �Ǿ� �ִ��� Ȯ�� �� GamePanel ���ִ� Btn ���� Method
        if(isSelect2P)
        {
            MainPanel.gameObject.SetActive(false);
            GamePanel.SetActive(true);
        }
        else
        { // 2P ������ ���� ���� ��� ErrorLog�� ����ϰ� return
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
    { // ErrorLog ��� Coroutine
        errorlog.SetActive(true);

        yield return new WaitForSeconds(3f);

        errorlog.SetActive(false);
        log = null;
    }
    #endregion
}
