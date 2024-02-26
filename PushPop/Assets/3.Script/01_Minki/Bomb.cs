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
/// 2인 모드(폭탄 돌리기) 관련 Class
/// </summary>
public class Bomb : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject changeBtn; // Back버튼을 모든 모드 통합으로 써서 ... 스크립트로 끄려면 참조할수밖에 없읍,,
    [SerializeField] private GameObject selectBtn;// Back버튼을 모든 모드 통합으로 써서 ... 스크립트로 끄려면 참조할수밖에 없읍,,

    [Header("1P Player")]
    [SerializeField] private Image playerImage1P = null;    // 로비에서 보이는 1P Image
    [SerializeField] private TMP_Text playerName1P = null;  // 로비에서 보이는 1P Name
    public List<GameObject> popList1P = new List<GameObject>(); // 1P의 Pushpop List
    [SerializeField] private Image inGameImage1P = null;    // 게임 화면에서 보이는 1P Image
    [SerializeField] private TMP_Text inGameText1P = null;  // 게임 화면에서 보이는 1P Name
    private bool Quit1P = false;    // 게임 화면에서 뒤로가기 버튼 1P

    [Header("2P Player")]
    public Image playerImage2P = null;    // 로비에서 보이는 2P Image
    public Image tempPlayerImage2P = null;  // 프로필 선택 판넬에서 보이는 2P Image
    public TMP_Text playerName2P = null;    // 로비에서 보이는 2P Name
    public TMP_Text tempPlayerName2P = null;    // 프로필 선택 판넬에서 보이는 2P Name
    public bool isImageSelect = false;  // 이미지 고르기에서 아이콘을 선택 했는지 
    public bool isUpdate = false;   // 프로필 수정모드인지
    public bool isSelect2P = false;     // 2P 프로필이 선택 됐는지
    public List<GameObject> popList2P = new List<GameObject>(); // 1P의 Pushpop List
    [SerializeField] private Image inGameImage2P = null;    // 게임 화면에서 보이는 2P Image
    [SerializeField] private TMP_Text inGameText2P = null;  // 게임 화면에서 보이는 2P Name
    private bool Quit2P = false;    // 게임 화면에서 뒤로가기 버튼 2P

    [Header("Profile Obj")]
    [SerializeField] private GameObject profilePanel = null;    // Profile Panel
    [SerializeField] private Transform profileParent = null;    // Profile Panel Parent
    [SerializeField] private List<GameObject> profileList = new List<GameObject>(); // Profile Panel List
    [SerializeField] private string profile2PName = null;   // Profile Add시 게임매니저나 SQL매니저에게 보내줄 string 값
    [SerializeField] private TMP_InputField profile2PInput = null;  // Profile Add하는 Inputfield
    [SerializeField] private GameObject iconPanel = null;   // 이미지 고르기 했을 때 나오는 아이콘 판넬
    [SerializeField] private GameObject checkPanel = null;  // 사진 찍기 했을 때 체크 판넬
    private int imageIndex = 0; // 이미지 고르기의 Icon Index
    private string imagePath = string.Empty;   // Camera Image Save Path

    [Header("Panel")]
    public GameObject MainPanel = null; // 게임 로비
    public GameObject GamePanel = null; // 게임 화면
    public GameObject CreateNamePanel = null;   //이름 입력 판넬
    public GameObject CreateImagePanel = null;  // 사진 찍기 판넬
    public GameObject SelectProfile = null; // 선택 판넬
    public GameObject CurrentProfile = null;    // 최종 프로필 판넬
    public GameObject deletePanel = null; // 삭제 판넬
    public Help_Canvas help_Canvas = null; // 추후 꺼주고 켜주고 하는 로직만 냅두고 삭제해도 될 듯 ?
    public GameObject main_Canvas = null;   // 메인 캔버스
    public GameObject WarningPanel = null;

    [Header("ErrorLog")]
    [SerializeField] private TMP_Text nameLog = null; // 한글만 입력해주세요 에러로그
    [SerializeField] private TMP_Text imageLog = null;    // 이미지를 선택해주세요 에러로그
    [SerializeField] private TMP_Text need2P = null;  // 2P를 선택해주세요 에러로그

    [Header("BombGame")]
    [SerializeField] private bool isStart = false;  // 게임 시작 했는지 판단하는 Bool값
    [SerializeField] private Turn turn = new Turn();    // Turn enum
    [SerializeField] private SpriteAtlas atlas = null;  // spriteatlas
    [SerializeField] private Sprite[] sprites = null;   // atlas의 sprite들 배열
    [SerializeField] private float upperTimer = 12f;    // 위 방울의 제한시간
    [SerializeField] private float bottomTimer = 60f;   // 전체 게임의 제한시간
    [SerializeField] private Vector2[] upperPos = new Vector2[2];   // 위 방울의 Pos
    [SerializeField] private Vector2[] bottomPos = new Vector2[2];  // 아래 방울의 Pos
    [SerializeField] private GameObject upperBubble;    // 위 방울 오브젝트
    [SerializeField] private GameObject bottomBubble;   // 아래 방울 오브젝트
    [SerializeField] private Transform[] Frame; // Pushpop Board가 소환될 Parent
    [SerializeField] private Image waterfall;   // upperBubble안에 들어있는 물 이미지
    [SerializeField] private TMP_Text timerText;    // 전체 제한시간 출력 text
    [SerializeField] private Sprite[] upperBubbleSprite;    // upperBubble에 들어있는 물 이미지들의 배열
    [SerializeField] private Animator endAnimation; // 게임 종료했을 때 나올 Animation
    [SerializeField] private GameObject result; // 결과창 Panel
    [SerializeField] private GameObject readyPanel; // 처음 준비 모드 패널
    [SerializeField] private TMP_Text readyText; // 처음 준비 모드 텍스트 

    [Header("Other Component")]
    [SerializeField] private Button profileBtn;
    [SerializeField] private Button gameStartBtn;
    [SerializeField] private Sprite quitNormal_Sprite; //quit 버튼 안눌렸을 때 스프라이트
    [SerializeField] private Sprite quitPressed_Sprite; //quit 버튼 눌렷을 때 스프라이트
    [SerializeField] private Button[] quitBtn;  // 양쪽의 나가기 버튼
    public Profile_Information player2PInfo = null;

    [Header("Versus")]
    [SerializeField] private TMP_Text[] winTexts;
    [SerializeField] private TMP_Text winText;
    [SerializeField] private TMP_Text[] loseTexts;
    [SerializeField] private TMP_Text loseText;
    [SerializeField] private Image[] winProfileImages;
    [SerializeField] private Image winProfileImage;
    [SerializeField] private Image[] loseProfileImages;
    [SerializeField] private Image loseProfileImage;

    //waterfall 회전 변수들
    private bool rotateDirection = true; // true면 회전 방향이 +, false면 회전 방향이 -
    private float rotationZ = 0f; // 현재 Z 축 회전 각도

    private Coroutine log;  // 에러로그 코루틴
    private Coroutine waterfall_co; // 물 차오르는 코루틴
    private Coroutine readyGame_co; // 처음 시작 코루틴

    private bool b10minleft = false;    //10초 남앗나 체크하는 변수

    #region Unity Callback
    private void Awake()
    {
        // 초기 Sprite 배열 세팅
        Sprite[] tempSprites = new Sprite[atlas.spriteCount];
        sprites = tempSprites;
        atlas.GetSprites(sprites);

        // 버튼 사이즈 설정
        PushPop.Instance.buttonSize = new Vector2(80f, 80f);
        PushPop.Instance.percentage = 0.67f;

        GameManager.Instance.GameStart();
    }

    private void OnEnable()
    {
        AudioManager.instance.SetAudioClip_BGM(1);
        PlayerSet1P();
        profile2PInput.onValidateInput += ValidateInput;

        if (!help_Canvas.gameObject.activeSelf)
        {
            help_Canvas.gameObject.SetActive(true);
        }

        if(WarningPanel.activeSelf)
        {
            WarningPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (!isStart) return;
        else if (bottomTimer <= 0.1f)
        { // 게임 끝
            EndGame();
            if (waterfall_co != null)
            { // upperbubble 코루틴이 돌아가고 있다면 스탑
                StopCoroutine(waterfall_co);
            }
            return;
        }

        //10초미만 오디오
        if (bottomTimer <= 10)
        {
            if (!b10minleft)
            {
                b10minleft = true;
                AudioManager.instance.SetAudioClip_SFX(3, true);
            }
        }

        bottomTimer -= Time.deltaTime;
        
        timerText.text = $"남은시간\n{(int)bottomTimer}";
    }

    private void OnDisable()
    {
        profile2PInput.onValidateInput -= ValidateInput;
        isSelect2P = false;
        isStart = false;
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
        // 프로필 이미지 출력
        SQL_Manager.instance.PrintProfileImage(GameManager.Instance.IsImageMode, playerImage1P, GameManager.Instance.ProfileIndex);
      
        // 프로필 이름 출력
        playerName1P.text = GameManager.Instance.ProfileName;
    }

    public void Choice2P()
    { // 2Player를 선택하는 Btn 연동 Method
        // 기존 로직대로면 뒤로가기를 선택했을 때도 정보가 바뀌도록 로직이 설계되어있어, 프로필을 선택했을 때만 정보를 바꾸도록 수정함
        player2PInfo.Receive_Infomation();

        // 2P 프로필 이미지 출력
        SQL_Manager.instance.PrintProfileImage(GameManager.Instance.IsimageMode2P, playerImage2P, GameManager.Instance.ProfileIndex2P);
       
        // 2P 프로필 이름 출력
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
                    if (DialogManager.instance.log_co != null)
                    {
                        StopCoroutine(DialogManager.instance.log_co);
                    }
                    DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(imageLog, "이미지를 선택해주세요."));
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
                if (!isImageSelect)
                { // 선택한 이미지가 없을 때
                    if (DialogManager.instance.log_co != null)
                    {
                        StopCoroutine(DialogManager.instance.log_co);
                    }
                    DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(imageLog, "이미지를 선택해주세요."));
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

            // 각 infomation 프로필 이름 출력
            info.Profile_name.text = SQL_Manager.instance.Profile_list[i].name;

            // 각 infomation 프로필 이미지 출력
            SQL_Manager.instance.PrintProfileImage(SQL_Manager.instance.Profile_list[i].imageMode, info.ProfileImage, SQL_Manager.instance.Profile_list[i].index);
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
            else if (GameManager.Instance.ProfileIndex2P == SQL_Manager.instance.Profile_list[i].index)
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
        player2PInfo.Receive_Infomation();
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
            if (DialogManager.instance.log_co != null)
            {
                StopCoroutine(DialogManager.instance.log_co);
            }
            DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(nameLog, "한글 입력만 가능합니다."));
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
            AudioManager.instance.SetCommonAudioClip_SFX(0);
            AudioManager.instance.SetAudioClip_BGM(5);

            MainPanel.gameObject.SetActive(false);
            help_Canvas.gameObject.SetActive(false);
            GamePanel.SetActive(true);
            help_Canvas.gameObject.SetActive(false);
            WarningPanel.SetActive(false);
            ButtonSetting();
            InitSetting();
        }
        else
        { // 2P 선택이 되지 않은 경우 ErrorLog를 출력하고 return
            if (DialogManager.instance.log_co != null)
            {
                StopCoroutine(DialogManager.instance.log_co);
            }
            DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(need2P, "2P를 선택해주세요."));
            return;
        }
    }

    /// <summary>
    /// Multi게임 로직 반복 Method
    /// </summary>
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
        // 새로운 Sprite 생성 및 버블 위치 세팅
        PosSetting();

        // 버튼 interactable 설정하여 누구의 턴인지 설정
        TurnSetting();

        // upperBubble 초기화 및 코루틴 재실행
        if(waterfall_co != null)
        {
            StopCoroutine(waterfall_co);
        }
        upperTimer = 12f;
        waterfall_co = StartCoroutine(Waterfall_co());
    }

    public void InitSetting()
    { // 게임 초기 시작시 셋팅하는 Method
        // Timer 선언
        upperTimer = 12f;
        bottomTimer = 60f;
        // 상단의 Bubble의 위치를 랜덤으로 부여하여 해당 위치에 따라 어떤 플레이어가 먼저 시작할지 턴을 부여
        int randomPos = Random.Range(0, 2);
        upperBubble.transform.localPosition = upperPos[randomPos];
        if(randomPos == 0)
        {
            bottomBubble.transform.localPosition = bottomPos[1];
            turn = Turn.Turn1P; // 1P 먼저 시작
        }
        else
        {
            bottomBubble.transform.localPosition = bottomPos[0];
            turn = Turn.Turn2P; // 2P 먼저 시작
        }

        // Sprite 배열로 각 플레이어들에게 랜덤한 Sprite 부여 및 Pushpop 생성
        SetSpriteImage(Frame[0], popList1P);
        SetSpriteImage(Frame[1], popList2P);
        Frame[0].transform.GetChild(0).transform.localPosition = bottomPos[0];
        Frame[1].transform.GetChild(0).transform.localPosition = bottomPos[1];
        

        // 버튼 interactable 설정하여 누구의 턴인지 설정
        TurnSetting();

        // Profile Setting
        inGameText1P.text = GameManager.Instance.ProfileName;
        inGameText2P.text = GameManager.Instance.ProfileName2P;
        SQL_Manager.instance.PrintProfileImage(GameManager.Instance.IsImageMode, inGameImage1P, GameManager.Instance.ProfileIndex);
        SQL_Manager.instance.PrintProfileImage(GameManager.Instance.IsimageMode2P, inGameImage2P, GameManager.Instance.ProfileIndex2P);

        readyGame_co = StartCoroutine(ReadyGame_Co());
    }

    private void PosSetting()
    { // 턴 넘어갔을 때 각 포지션들 설정하는 Method
        AudioManager.instance.SetAudioClip_SFX(2, false);

        if(turn.Equals(Turn.Turn1P))
        { // 1P 턴
            // 버튼 리스트 초기화 및 삭제 (추후 풀링으로 구현한다면 setactive false로 바꾸면 될듯)
            popList2P.Clear();
            Destroy(Frame[1].transform.GetChild(0).gameObject);    // 지금 오브젝트 풀링의 List를 받아올 수 없는 구조라서 일단 Destroy로 했음, 추후 수정해야함

            // Sprite 배열로 각 플레이어들에게 랜덤한 Sprite 부여 및 Pushpop 생성
            SetSpriteImage(Frame[1], popList2P);

            // 새로운 sprite, popButton 포지션 설정
            Frame[1].transform.GetChild(1).transform.localPosition = bottomPos[1]; // Destroy한 객체는 다음 프레임에 삭제됨
            upperBubble.transform.localPosition = upperPos[0];
            bottomBubble.transform.localPosition = bottomPos[1];
        }
        else if(turn.Equals(Turn.Turn2P))
        { // 2P 턴
            // 버튼 리스트 초기화 및 삭제 (추후 풀링으로 구현한다면 setactive false로 바꾸면 될듯)
            popList1P.Clear();
            Destroy(Frame[0].transform.GetChild(0).gameObject);

            // Sprite 배열로 각 플레이어들에게 랜덤한 Sprite 부여 및 Pushpop 생성
            SetSpriteImage(Frame[0], popList1P);

            // 새로운 sprite, popButton 포지션 설정
            Frame[0].transform.GetChild(1).transform.localPosition = bottomPos[0];
            upperBubble.transform.localPosition = upperPos[1];
            bottomBubble.transform.localPosition = bottomPos[0];
        }
    }

    private void TurnSetting()
    { // 턴 세팅하는 Method
        if(turn.Equals(Turn.Turn1P))
        { // 1P 턴
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
        { // 2P 턴
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
    { // 매개변수를 이용해 각 Player의 Sprite와 PopBtn 세팅하는 Method
        int randomIndex = Random.Range(0, sprites.Length);

        Sprite sprite = sprites[randomIndex];
        PushPop.Instance.boardSprite = sprite;
        // Sprite 이름에서 "(Clone)" 부분을 제거
        string spriteName = sprite.name.Replace("(Clone)", "").Trim();

        // 이름에서 숫자 부분만 추출하여 int로 변환
        // 이 부분 Pushpop에서 생성하는 부분이랑 많이 꼬여있음...
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
            PushPop.Instance.pushPopBoardObject[0].transform.SetParent(_parent, false); // worldPositionStays를 false로 설정하여 로컬 위치 유지
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
    { // 밑에 큰 방울을 터치할 때마다 상단 방울의 시간이 줄어듬
        AudioManager.instance.SetCommonAudioClip_SFX(5);
        upperTimer -= 0.1f;
    }

    private IEnumerator Waterfall_co()
    { // 시간에 따라 bubble속의 waterfall의 sprite를 결정하고 uppertimer가 0보다 작아지면 게임 종료
        waterfall.sprite = upperBubbleSprite[0]; // 방울의 sprite를 초기 상태로 변경

        while (upperTimer > 0)
        { // upperTimer가 0보다 클 때까지 반복
            upperTimer -= Time.deltaTime; // upperTimer 감소

            // upperTimer 값에 따라 waterfall의 sprite 변경
            if (upperTimer < 2) waterfall.sprite = upperBubbleSprite[5];
            else if (upperTimer < 4) waterfall.sprite = upperBubbleSprite[4];
            else if (upperTimer < 6) waterfall.sprite = upperBubbleSprite[3];
            else if (upperTimer < 8) waterfall.sprite = upperBubbleSprite[2];
            else if (upperTimer < 10) waterfall.sprite = upperBubbleSprite[1];

            // Z 축 회전 처리
            if (rotateDirection)
            {
                rotationZ += Time.deltaTime * 30; // 속도 조절
                if (rotationZ > 15)
                {
                    rotateDirection = !rotateDirection;
                }
            }
            else
            {
                rotationZ -= Time.deltaTime * 30; // 속도 조절
                if (rotationZ < -15)
                {
                    rotateDirection = !rotateDirection;
                }
            }

            waterfall.transform.rotation = Quaternion.Euler(0, 0, rotationZ);

            yield return null; // 다음 프레임까지 대기
        }

        // while문을 벗어났다면 upperTimer가 0보다 작아졌다는 것을 의미하기에 게임 종료
        AudioManager.instance.SetAudioClip_SFX(0, false);
        EndGame();
    }

    private IEnumerator ReadyGame_Co()
    {
        readyPanel.SetActive(true);
        DialogManager.instance.Print_Dialog(readyText, "준비 ~");
        yield return new WaitForSeconds(2f);

        DialogManager.instance.Print_Dialog(readyText, "시작 ~");
        yield return new WaitForSeconds(0.8f);
        readyPanel.SetActive(false);

        // upperBubble 코루틴 실행
        waterfall_co = StartCoroutine(Waterfall_co());
        isStart = true;
        readyGame_co = null;
    }

    private void EndGame()
    { // 게임이 종료됐을 때 Method
        isStart = false;
        b10minleft = false;

        bool result = false;

        // turn은 푸쉬팝을 누르고 있는 사람을 뜻하기 때문에 본인의 턴에 게임이 종료 됐다면 패배
        if(turn == Turn.Turn1P) result = false;
        else if (turn == Turn.Turn2P) result = true;

        Ranking.instance.SetBombVersus(GameManager.Instance.ProfileIndex, GameManager.Instance.ProfileName, GameManager.Instance.ProfileIndex2P, GameManager.Instance.ProfileName2P, result);
       
        // 종료 애니메이션 켜주고 애니메이션 나올 위치 설정
        endAnimation.transform.gameObject.SetActive(true);
        AudioManager.instance.SetAudioClip_SFX(1, false);

        if (turn.Equals(Turn.Turn1P)) endAnimation.transform.localPosition = bottomPos[0];
        else if (turn.Equals(Turn.Turn2P)) endAnimation.transform.localPosition = bottomPos[1];

        endAnimation.SetTrigger("EndGame");
        StartCoroutine(Result_Co());
    }

    private IEnumerator Result_Co()
    { // 결과창 출력 코루틴
        // 애니메이션 출력 기다림
        yield return new WaitForSeconds(1.2f);

        // 결과창 출력
        AudioManager.instance.SetCommonAudioClip_SFX(7);
        result.SetActive(true);
        Ranking.instance.LoadVersusResult_Personal(winText, loseText, winProfileImage, loseProfileImage);
        
        yield return new WaitForSeconds(0.1f);
        
        // 오브젝트들 삭제
        ResetGame();
        yield return null;
    }

    private void QuitGame()
    { // 중간에 게임 나갔을 때 Method
        isStart = false;
        for(int i = 0; i < quitBtn.Length; i++)
        {
            quitBtn[i].interactable = true;
        }
       
        // 오브젝트 삭제
        ResetGame();
        AudioManager.instance.SetAudioClip_SFX(1, false);
        help_Canvas.gameObject.SetActive(true);
    }

    private void ResetGame()
    { // 오브젝트들 삭제하는 메소드
        // 종료 애니메이션 비활성화
        endAnimation.transform.gameObject.SetActive(false);
        
        // 리스트 초기화 및 Sprite 삭제
        popList1P.Clear();
        popList2P.Clear();
        Destroy(Frame[0].transform.GetChild(0).gameObject);
        Destroy(Frame[1].transform.GetChild(0).gameObject);

        // Bool값 초기화
        Quit1P = false;
        Quit2P = false;

        if(waterfall_co != null || readyGame_co != null)
        {
            StopCoroutine(waterfall_co);
            StopCoroutine(readyGame_co);
        }
    }

    public void PrintVersus()
    {
        Ranking.instance.LoadVersusResult(winTexts, loseTexts, winProfileImages, loseProfileImages);
    }
    #endregion
    private IEnumerator PrintLog_co(GameObject errorlog)
    { // ErrorLog 출력 Coroutine
        errorlog.SetActive(true);

        yield return new WaitForSeconds(3f);

        errorlog.SetActive(false);
        log = null;
    }

    public void QuitBtn(int _player)
    { // 매개변수 0은 1P / 1은 2P Btn연동 Method
        if(_player.Equals(0))
        {
            Check_quitBtn_1P();
        }
        else if (_player.Equals(1))
        {
            Check_quitBtn_2P();
        }

        if(Quit1P && Quit2P)
        { // 두 버튼 모두 나가기를 눌렀을 때
            Time.timeScale = 0;
            quitBtn[0].interactable = false;
            quitBtn[1].interactable = false;
            WarningPanel.SetActive(true);
        }
    }

    //시작시 quit 버튼들 세팅
    private void ButtonSetting()
    {
        Quit1P = false;
        quitBtn[0].GetComponent<Image>().sprite = quitNormal_Sprite;
        quitBtn[0].interactable = true;

        Quit2P = false;
        quitBtn[1].GetComponent<Image>().sprite = quitNormal_Sprite;
        quitBtn[1].interactable = true;
    }

    //quitBtn[0]번 상태 변경 메소드
    private void Check_quitBtn_1P()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        if(!Quit1P)
        {
            Quit1P = true;          
            quitBtn[0].GetComponent<Image>().sprite = quitPressed_Sprite;
        }
        else
        {
            Quit1P = false;       
            quitBtn[0].GetComponent<Image>().sprite = quitNormal_Sprite;
        }
    }

    //quitBtn[1]번 상태 변경 메소드
    private void Check_quitBtn_2P()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        if (!Quit2P)
        {
            Quit2P = true;
            quitBtn[1].GetComponent<Image>().sprite = quitPressed_Sprite;
        }
        else
        {
            Quit2P = false;      
            quitBtn[1].GetComponent<Image>().sprite = quitNormal_Sprite;
        }
    }

    //프로필 이름 입력창 뒤로가기 버튼
    public void ProfileInputName_BackBtn_Clicked()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        profile2PInput.text = string.Empty;
        SelectProfile.SetActive(true);
        //CurrentProfile.SetActive(true);
        CreateNamePanel.SetActive(false);
    }

    //프로필 선택/ 프로필 변경 버튼 
    public void Select2PBtn_Clicked()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        SelectProfile.SetActive(true);
        PrintProfileList();
        help_Canvas.Button_Disable();
        gameStartBtn.interactable = false;
    }

    //나가기 전 경고패널 나가기 버튼
    public void GoOutBtn_Clicked()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        QuitGame();
        result.SetActive(false);
        GamePanel.SetActive(false);
        MainPanel.SetActive(true);
        WarningPanel.SetActive(false);
        Time.timeScale = 1;
    }

    //나가기 전 경고패널 취소 버튼
    public void CancelBtn_Clicked()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        Time.timeScale = 1;
        WarningPanel.SetActive(false);
        ButtonSetting();
    }

    //좌측 하단 나가기 버튼
    public void BackBtn_Clicked()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        AudioManager.instance.SetAudioClip_BGM(0);

        GameManager.Instance.gameMode = Mode.None;
        main_Canvas.SetActive(true);
        selectBtn.SetActive(true);
        changeBtn.SetActive(false);
        playerImage2P.gameObject.SetActive(false);
        playerName2P.gameObject.SetActive(false);
        GamePanel.SetActive(false);
        CreateImagePanel.SetActive(false);
        CurrentProfile.SetActive(false);
        SelectProfile.SetActive(false);
        gameObject.SetActive(false);
    }

    public void Enable_Objects()
    {
        gameStartBtn.interactable = true;
        profileBtn.interactable = true;
    }

    public void Disable_Objects()
    {
        gameStartBtn.interactable = false;
        profileBtn.interactable = false;
    }
    #endregion
}
