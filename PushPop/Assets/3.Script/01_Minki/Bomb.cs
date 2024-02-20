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
    [Header("1P Player")]
    [SerializeField] private Image playerImage1P = null;
    [SerializeField] private TMP_Text playerName1P = null;
    public List<GameObject> popList1P = new List<GameObject>();
    [SerializeField] private TMP_Text inGameText1P = null;
    [SerializeField] private Image inGameImage1P = null;
    private bool Quit1P = false;

    [Header("2P Player")]
    public Image playerImage2P = null;
    public Image tempPlayerImage2P = null;
    public TMP_Text playerName2P = null;
    public TMP_Text tempPlayerName2P = null;
    public bool isImageSelect = false;
    public bool isUpdate = false;
    public bool isSelect2P = false;
    public List<GameObject> popList2P = new List<GameObject>();
    [SerializeField] private TMP_Text inGameText2P = null;
    [SerializeField] private Image inGameImage2P = null;
    private bool Quit2P = false;

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
    public GameObject help_Canvas = null;
    public GameObject main_Canvas = null;

    [Header("ErrorLog")]
    [SerializeField] private GameObject nameLog = null;
    [SerializeField] private GameObject imageLog = null;
    [SerializeField] private GameObject need2P = null;

    [Header("BombGame")]
    [SerializeField] private bool isStart = false;
    [SerializeField] private Turn turn = new Turn();
    [SerializeField] private SpriteAtlas atlas = null;
    [SerializeField] private Sprite[] sprites = null;
    [SerializeField] private float upperTimer = 12f;
    [SerializeField] private float bottomTimer = 60f;
    [SerializeField] private Vector2[] upperPos = new Vector2[2];
    [SerializeField] private Vector2[] bottomPos = new Vector2[2];
    [SerializeField] private GameObject upperBubble;
    [SerializeField] private GameObject bottomBubble;
    [SerializeField] private Transform[] Frame;
    [SerializeField] private Image waterfall;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Sprite[] upperBubbleSprite;
    [SerializeField] private Animator endAnimation;
    [SerializeField] private GameObject result;

    [Header("Other Component")]
    [SerializeField] private Button[] quitBtn;
    //waterfall 회전 변수들
    private bool rotateDirection = true; // true면 회전 방향이 +, false면 회전 방향이 -
    private float rotationZ = 0f; // 현재 Z 축 회전 각도

    private Coroutine log;
    private Coroutine waterfall_co;

    #region Unity Callback
    private void Awake()
    {
        // 초기 Sprite 배열 세팅
        Sprite[] tempSprites = new Sprite[atlas.spriteCount];
        sprites = tempSprites;
        atlas.GetSprites(sprites);

        // 버튼 사이즈 설정
        PushPop.Instance.buttonSize = new Vector2(90f, 90f);
        PushPop.Instance.percentage = 0.8f;

        GameManager.Instance.GameStart();
    }

    private void OnEnable()
    {
        PlayerSet1P();
        profile2PInput.onValidateInput += ValidateInput;

        if (!help_Canvas.gameObject.activeSelf)
        {
            help_Canvas.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (!isStart) return;
        else if(bottomTimer <= 0.1f)
        { // 게임 끝
            EndGame();
            if(waterfall_co != null)
            { // upperbubble 코루틴이 돌아가고 있다면 스탑
                StopCoroutine(waterfall_co);
            }
            return;
        }
        bottomTimer -= Time.deltaTime;
        
        timerText.text = $"남은시간\n{(int)bottomTimer}";
    }

    private void OnDisable()
    {
        profile2PInput.onValidateInput -= ValidateInput;
        isSelect2P = false;

        if (help_Canvas.activeSelf)
        {
            help_Canvas.SetActive(false);
        }
        
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
            InitSetting();
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

    public void RepeatGameLogic()
    { // 게임 로직 반복 Method
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
        isStart = true;
        TurnSetting();
        waterfall_co = StartCoroutine(Waterfall_co());

        // Profile Setting
        inGameText1P.text = GameManager.Instance.ProfileName;
        inGameText2P.text = GameManager.Instance.ProfileName2P;
        if(GameManager.Instance.IsImageMode)
        { // 1P 가 사진 찍기를 선택한 Player일 경우
            Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(GameManager.Instance.UID, GameManager.Instance.ProfileIndex);
            Sprite profileSprite = GameManager.Instance.TextureToSprite(profileTexture);
            inGameImage1P.sprite = profileSprite;
        }
        else if(!GameManager.Instance.IsImageMode)
        { // 1P 가 이미지 고르기를 선택한 Player일 경우
            inGameImage1P.sprite = GameManager.Instance.ProfileImages[GameManager.Instance.DefaultImage];
        }

        if (GameManager.Instance.IsimageMode2P)
        { // 2P 가 사진 찍기를 선택한 Player일 경우
            Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(GameManager.Instance.UID, GameManager.Instance.ProfileIndex2P);
            Sprite profileSprite = GameManager.Instance.TextureToSprite(profileTexture);
            inGameImage2P.sprite = profileSprite;
        }
        else if (!GameManager.Instance.IsimageMode2P)
        { // 2P 가 이미지 고르기를 선택한 Player일 경우
            inGameImage2P.sprite = GameManager.Instance.ProfileImages[GameManager.Instance.DefaultImage2P];
        }
    }

    private void PosSetting()
    { // 턴 넘어갔을 때 각 포지션들 설정하는 Method
        if(turn.Equals(Turn.Turn1P))
        { // 1P 턴
            popList2P.Clear();
            Destroy(Frame[1].transform.GetChild(0).gameObject);    // 지금 오브젝트 풀링의 List를 받아올 수 없는 구조라서 일단 Destroy로 했음, 추후 수정해야함
            SetSpriteImage(Frame[1], popList2P);
            Frame[1].transform.GetChild(1).transform.localPosition = bottomPos[1]; // Destroy한 객체는 다음 프레임에 삭제됨
            upperBubble.transform.localPosition = upperPos[0];
            bottomBubble.transform.localPosition = bottomPos[1];
        }
        else if(turn.Equals(Turn.Turn2P))
        { // 2P 턴
            popList1P.Clear();
            Destroy(Frame[0].transform.GetChild(0).gameObject);
            SetSpriteImage(Frame[0], popList1P);
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
        EndGame();
    }

    private void EndGame()
    { // 게임이 종료됐을 때 Method
        isStart = false;
        endAnimation.transform.gameObject.SetActive(true);
        if(turn.Equals(Turn.Turn1P))
        {
            endAnimation.transform.localPosition = bottomPos[0];
        }
        else if (turn.Equals(Turn.Turn2P))
        {
            endAnimation.transform.localPosition = bottomPos[1];
        }
        endAnimation.SetTrigger("EndGame");
        StartCoroutine(Result_Co());
    }

    private IEnumerator Result_Co()
    { // 결과창 출력 코루틴
        yield return new WaitForSeconds(1.2f);
        result.SetActive(true);
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
    }

    private void ResetGame()
    { // 오브젝트들 삭제하는 메소드
        endAnimation.transform.gameObject.SetActive(false);
        popList1P.Clear();
        popList2P.Clear();
        Destroy(Frame[0].transform.GetChild(0).gameObject);
        Destroy(Frame[1].transform.GetChild(0).gameObject);

        // Bool값 초기화
        Quit1P = false;
        Quit2P = false;

        if(waterfall_co != null)
        {
            StopCoroutine(waterfall_co);
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

    public void QuitBtn(int _player)
    { // 매개변수 0은 1P / 1은 2P
        if(_player.Equals(0))
        {
            Quit1P = true;
            quitBtn[0].interactable = false;
        }
        else if (_player.Equals(1))
        {
            Quit2P = true;
            quitBtn[1].interactable = false;
        }

        if(Quit1P && Quit2P)
        {
            QuitGame();
            result.SetActive(false);
            GamePanel.SetActive(false);
            MainPanel.SetActive(true);
        }
    }

    public void BackBtn_Clicked()
    {
        main_Canvas.SetActive(true);
        GamePanel.SetActive(false);
        CreateImagePanel.SetActive(false);
        CurrentProfile.SetActive(false);
        SelectProfile.SetActive(false);
        gameObject.SetActive(false);
    }
    #endregion
}
