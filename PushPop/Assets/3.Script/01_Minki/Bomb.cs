using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public enum Turn
{
    Turn1P = 0,
    Turn2P
}

/// <summary>
/// 2�� ���(��ź ������) ���� Class
/// </summary>
public class Bomb : MonoBehaviour, IPointerClickHandler
{
    [Header("1P Player")]
    [SerializeField] private Image playerImage1P = null;
    [SerializeField] private TMP_Text playerName1P = null;
    public List<GameObject> popList1P = new List<GameObject>();

    [Header("2P Player")]
    public Image playerImage2P = null;
    public Image tempPlayerImage2P = null;
    public TMP_Text playerName2P = null;
    public TMP_Text tempPlayerName2P = null;
    public bool isImageSelect = false;
    public bool isUpdate = false;
    public bool isSelect2P = false;
    public List<GameObject> popList2P = new List<GameObject>();

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
    [SerializeField] private bool isStart = false;
    [SerializeField] private Turn turn = new Turn();
    [SerializeField] private SpriteAtlas atlas = null;
    [SerializeField] private Sprite[] sprites = null;
    [SerializeField] private float upperTimer = 10f;
    [SerializeField] private float bottomTimer = 60f;
    [SerializeField] private Vector2[] upperPos = new Vector2[2];
    [SerializeField] private Vector2[] bottomPos = new Vector2[2];
    [SerializeField] private GameObject upperBubble;
    [SerializeField] private GameObject bottomBubble;
    [SerializeField] private Transform[] Frame;
    [SerializeField] private Image waterfall;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Sprite[] upperBubbleSprite;

    private Coroutine log;
    private Coroutine waterfall_co;

    #region Unity Callback
    private void Awake()
    {
        // �ʱ� Sprite �迭 ����
        Sprite[] tempSprites = new Sprite[atlas.spriteCount];
        sprites = tempSprites;
        atlas.GetSprites(sprites);

        // ��ư ������ ����
        PushPop.Instance.buttonSize = new Vector2(90f, 90f);
        PushPop.Instance.percentage = 0.85f;
    }

    private void OnEnable()
    {
        PlayerSet1P();
        profile2PInput.onValidateInput += ValidateInput;
    }

    private void Update()
    {
        if (!isStart) return;
        if(upperTimer <= 0.1f)
        { // ���� ��
            Debug.Log("���� ��");
            isStart = false;
            return;
        }
        else if(bottomTimer <= 0.1f)
        { // ���� ��
            Debug.Log("���� ��");
            isStart = false;
            return;
        }
        bottomTimer -= Time.deltaTime;
        upperTimer -= Time.deltaTime;
        timerText.text = $"�����ð�\n{(int)bottomTimer}";
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
            InitSetting();
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

    public void RepeatGameLogic()
    {
        switch(turn)
        {
            case Turn.Turn1P:
                turn = Turn.Turn2P;
                break;
            case Turn.Turn2P:
                turn = Turn.Turn1P;
                break;
        }
        PosSetting();
        TurnSetting();
    }

    private void InitSetting()
    { // ���� �ʱ� ���۽� �����ϴ� Method
        GameManager.Instance.GameStart();
        // ����� Bubble�� ��ġ�� �������� �ο��Ͽ� �ش� ��ġ�� ���� � �÷��̾ ���� �������� ���� �ο�
        int randomPos = Random.Range(0, 2);
        upperBubble.transform.localPosition = upperPos[randomPos];
        if(randomPos == 0)
        {
            bottomBubble.transform.localPosition = bottomPos[1];
            turn = Turn.Turn1P; // 1P ���� ����
        }
        else
        {
            bottomBubble.transform.localPosition = bottomPos[0];
            turn = Turn.Turn2P; // 2P ���� ����
        }

        // Sprite �迭�� �� �÷��̾�鿡�� ������ Sprite �ο� �� Pushpop ����
        SetSpriteImage(Frame[0], popList1P);
        SetSpriteImage(Frame[1], popList2P);
        Frame[0].transform.GetChild(0).transform.localPosition = bottomPos[0];
        Frame[1].transform.GetChild(0).transform.localPosition = bottomPos[1];
        isStart = true;
        TurnSetting();
    }

    private void PosSetting()
    {
        if(turn.Equals(Turn.Turn1P))
        { // 1P ��
            popList2P.Clear();
            Destroy(Frame[1].transform.GetChild(0).gameObject);    // ���� ������Ʈ Ǯ���� List�� �޾ƿ� �� ���� ������ �ϴ� Destroy�� ����, ���� �����ؾ���
            SetSpriteImage(Frame[1], popList2P);
            Frame[1].transform.GetChild(1).transform.localPosition = bottomPos[1]; // Destroy�� ��ü�� ���� �����ӿ� ������
            upperBubble.transform.localPosition = upperPos[0];
            bottomBubble.transform.localPosition = bottomPos[1];
        }
        else if(turn.Equals(Turn.Turn2P))
        { // 2P ��
            popList1P.Clear();
            Destroy(Frame[0].transform.GetChild(0).gameObject);
            SetSpriteImage(Frame[0], popList1P);
            Frame[0].transform.GetChild(1).transform.localPosition = bottomPos[0];
            upperBubble.transform.localPosition = upperPos[1];
            bottomBubble.transform.localPosition = bottomPos[0];
        }
    }

    private void TurnSetting()
    { // �� �����ϴ� Method
        if(turn.Equals(Turn.Turn1P))
        { // 1P ��
            for (int i = 0; i < popList2P.Count; i++)
            {
                popList2P[i].GetComponent<Button>().interactable = false;
            }
            for(int i = 0; i < popList1P.Count; i++)
            {
                popList1P[i].GetComponent<Button>().interactable = true;
                popList1P[i].GetComponent<PushPopButton>().player = 0;
            }
        }
        else if(turn.Equals(Turn.Turn2P))
        { // 2P ��
            for (int i = 0; i < popList1P.Count; i++)
            {
                popList1P[i].GetComponent<Button>().interactable = false;
            }
            for (int i = 0; i < popList2P.Count; i++)
            {
                popList2P[i].GetComponent<Button>().interactable = true;
                popList2P[i].GetComponent<PushPopButton>().player = 1;
            }
        }
    }

    private void SetSpriteImage(Transform _parent, List<GameObject> _popList)
    { // �Ű������� �̿��� �� Player�� Sprite�� PopBtn �����ϴ� Method
        int randomIndex = Random.Range(0, sprites.Length);

        Sprite sprite = sprites[randomIndex];
        PushPop.Instance.boardSprite = sprite;
        // Sprite �̸����� "(Clone)" �κ��� ����
        string spriteName = sprite.name.Replace("(Clone)", "").Trim();

        // �̸����� ���� �κи� �����Ͽ� int�� ��ȯ
        if (int.TryParse(spriteName, out int spriteNumber))
        {
            GameManager.Instance.PushPopStage = spriteNumber;
            PushPop.Instance.CreatePushPopBoard(_parent);
            PushPop.Instance.CreateGrid(PushPop.Instance.pushPopBoardObject[0]);
            PushPop.Instance.PushPopButtonSetting(PushPop.Instance.PopParent.transform);
            for(int i = 0; i < PushPop.Instance.PopParent.transform.childCount; i++)
            {
                GameObject pop = PushPop.Instance.PopParent.transform.GetChild(i).gameObject;
                _popList.Add(pop);
            }
            PushPop.Instance.pushPopBoardObject[0].transform.SetParent(_parent, false); // worldPositionStays�� false�� �����Ͽ� ���� ��ġ ����
            GameObject temp = PushPop.Instance.pushPopBoardObject[0];
            PushPop.Instance.pushPopBoardObject.Remove(temp);
            Destroy(temp);
            for(int i = 0; i < PushPop.Instance.activePos.Count; i++)
            {
                PushPop.Instance.activePos[i].SetActive(false);
            }
            PushPop.Instance.activePos.Clear();
            PushPop.Instance.pushPopButton.Clear();
            PushPop.Instance.pushPopBoardObject.Clear();
            PushPop.Instance.pushPopBoardUIObject.Clear();
        }
    }

    public void BottomBubbleTouch()
    { // �ؿ� ū ����� ��ġ�� ������ ��� ����� �ð��� �پ��
        upperTimer -= 0.1f;
    }

    private IEnumerator Waterfall_co()
    {
        yield return null;
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
