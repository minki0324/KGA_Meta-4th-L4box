using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public enum Mode // GameMode
{
    PushPush = 0,
    Speed,
    Memory,
    Bomb,
    None
}

public class PushPushObject
{
    public int spriteName;
    public int childIndex;
    public int[] childSpriteIndex;
    public Vector2[] childPosition;

    public PushPushObject(int spriteName, int childIndex, int[] childSpriteIndex, Vector2[] childPosition)
    {
        this.spriteName = spriteName;
        this.childIndex = childIndex;
        this.childSpriteIndex = childSpriteIndex;
        this.childPosition = childPosition;
    }
}

public class PuzzleObject
{
    public GameObject puzzleObject;
    public Sprite puzzleSprite;
    public Vector2 puzzleArea;
    public Vector2 puzzleCenter;

    public PuzzleObject(GameObject puzzleObject, Sprite puzzleSprite, Vector2 puzzleArea, Vector2 puzzleCenter)
    {
        this.puzzleObject = puzzleObject;
        this.puzzleSprite = puzzleSprite;
        this.puzzleArea = puzzleArea;
        this.puzzleCenter = puzzleCenter;
    }
}

public class GameManager : MonoBehaviour, IGameMode
{
    public static GameManager Instance = null;
    public Mode gameMode;

    [Header("GameScript")]
    [SerializeField] private CustomPushpopManager pushpushScript;
    public Bomb bombScript;

    // Bubble
    [Header("Bubble Info")]
    [SerializeField] private GameObject bubbleCanvas = null;
    [SerializeField] private GameObject bubblePrefab = null;
    public List<GameObject> bubbleObject = new List<GameObject>();
    [SerializeField] private float bubbleSize;

    [SerializeField] private Transform[] pos = new Transform[4]; // pushpush, speed, bomb1, bomb2
    public List<Transform> bubblePos = new List<Transform>(); // Mode에 따라 달라짐

    [Header("PushPop Info")]
    public Sprite moldIcon = null;
    public int PushPopStage = 0;
    public Vector2 BoardSize;
    public Vector2 BoardSizeGameObject;
    public int buttonActive = 0;

    [Header("Puzzle Info")]
    public Vector2 puzzleSize;
    public Vector2 finalCenter;
    public List<PuzzleObject> puzzleClass = new List<PuzzleObject>();
    [SerializeField] private PuzzleLozic puzzleLogic;
    public List<PushPushObject> pushlist = new List<PushPushObject>();

    [Header("Score")]
    private Coroutine timer = null;
    public int Score = 0;
    public float TimeScore = 0;

    [Header("User Infomation")]
    public int UID;
    public string ProfileName;
    public int ProfileIndex;
    public int DefaultImage;
    public Sprite[] ProfileImages;
    public bool IsImageMode = true; // false = 사진찍기, true = 이미지 선택

    [Header("Speed Mode")]
    public float count = 0.25f;
    public Coroutine pushpushCreate_Co = null;

    [Header("2P Player")]
    public string ProfileName2P = string.Empty;
    public int ProfileIndex2P = 0;
    public int DefaultImage2P = 0;
    public bool IsimageMode2P = true;

    [Header("Speed Print")]
    [SerializeField] private TMP_Text[] printName;
    [SerializeField] private TMP_Text[] printTimer;
    [SerializeField] private Image[] printImage;
    [SerializeField] private TMP_Text printNamePersonal;
    [SerializeField] private TMP_Text printTimerPersonal;
    [SerializeField] private Image printImagePersonal;

    [Header("Other")]
    [SerializeField] private SpriteAtlas atlas;
    public Sprite noneSprite;
    public bool backButtonClick = false;
    [SerializeField] private Sprite[] btnSprites;

    public int boardName = 0; // mold name
    public int currentTime = 0;
    Speed_Timer speedTimer = null;
    public Coroutine speedCreate = null;


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
    }
    #endregion

    #region Other Method
    public void GameModeSetting(int _gameMode)
    { // game mode setting button click method
        gameMode = (Mode)_gameMode;
    }

    public void GameStageSetting(int _stage)
    { // game stage button click method
        PushPopStage = _stage;
    }

    public void GameStart()
    { // game Start 시 호출되는 method
        /*if (gameMode.Equals(0))
        {
            for (int i = 0; i < pos[(int)gameMode].transform.childCount; i++)
            { // 지정된 position
                bubblePos.Add(pos[(int)gameMode].GetChild(i));
            }
        }*/

        // score 초기화
        Score = 0;
        TimeScore = 0;
        buttonActive = 0;
        count = 0.25f;

        timer = StartCoroutine(GameReady_Co()); // Game 시작 전 대기

        switch (gameMode)
        {
            case Mode.PushPush:
                PushPushMode();
                break;
            case Mode.Speed:
                SpeedMode();
                break;
            case Mode.Memory:
                break;
            case Mode.Bomb:
                BombMode();
                break;
        }
    }

    public IEnumerator GameReady_Co()
    {
        // game start 문구 띄워주기, panel 다 막아버리면 될듯?
        AudioManager.instance.SetCommonAudioClip_SFX(1);
        // 준비~~
        yield return new WaitForSeconds(2f);

        AudioManager.instance.SetCommonAudioClip_SFX(2);
        // 시작!!
        yield return new WaitForSeconds(1f);
    }

    public void GameClear()
    { // Game End 시 호출하는 method
        // button active check
        if (gameMode.Equals(Mode.Bomb))
        {
            if (bombScript.popList1P.Count.Equals(0) || bombScript.popList2P.Count.Equals(0))
            {
                bombScript.RepeatGameLogic();
                return;
            }
        }
        else
        {
            switch (gameMode)
            {
                case Mode.PushPush:
                    if (PushPop.Instance.pushPopButton.Count == 0)
                    {
                        AudioManager.instance.SetAudioClip_SFX(4, false);
                        
                        //담고
                        int[] spriteIndexs = new int[pushpushScript.puzzleBoard.transform.childCount];
                        Vector2[] childPos = new Vector2[pushpushScript.puzzleBoard.transform.childCount];
                        for (int i = 0; i < pushpushScript.puzzleBoard.transform.childCount; i++)
                        {
                            PushPopButton pop = pushpushScript.puzzleBoard.transform.GetChild(i).GetComponent<PushPopButton>();
                            spriteIndexs[i] = pop.spriteIndex;
                            childPos[i] = pop.gameObject.transform.localPosition;
                        }

                        PushPushObject newPush = new PushPushObject(puzzleLogic.currentPuzzle.PuzzleID, pushpushScript.StackPops.Count, spriteIndexs, childPos);
                        string json = JsonUtility.ToJson(newPush);
                        SQL_Manager.instance.SQL_AddPushpush(json, ProfileIndex);

                        pushpushScript.result.SetActive(true);

                        // PushPushList 세팅
                        List<PushPushObject> pushlist = SQL_Manager.instance.SQL_SetPushPush(ProfileIndex);
                        if (pushlist == null)
                        {
                            Debug.Log("널");
                        }

                        //출력
                        pushpushScript.resultText.text = Mold_Dictionary.instance.icon_Dictionry[puzzleLogic.currentPuzzle.PuzzleID];

                        // List를 자동으로 먼저한 순서대로 담기게 해놨음
                        pushpushScript.resultImage.sprite = atlas.GetSprite(pushlist[0].spriteName.ToString());

                        // 기존 PushPush에서 사용했던 크기로 먼저 세팅
                        pushpushScript.resultImage.SetNativeSize();
                        pushpushScript.resultImage.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                        for (int i = 0; i < pushlist[0].childIndex; i++)
                        { // PushPushObject Class에 저장되어있는 Btn의 index
                            // 저장된 만큼버튼 생성 및 부모설정
                            GameObject pop = Instantiate(PushPop.Instance.pushPopButtonPrefab, pushpushScript.resultImage.transform);

                            // 버튼의 색깔 Index에 맞게 Sprite 변경
                            pop.GetComponent<Image>().sprite = btnSprites[pushlist[0].childSpriteIndex[i]];

                            // Scale과 Position 세팅
                            pop.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                            pop.transform.localPosition = pushlist[0].childPosition[i];
                        }

                        // 세팅이 끝났으면 컬렉션 Bubble의 크기만큼 스케일 조정
                        pushpushScript.resultImage.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

                        for (int i = 0; i < pushpushScript.puzzleBoard.transform.childCount; i++)
                        {
                            pushpushScript.puzzleBoard.transform.GetChild(i).GetComponent<Button>().interactable = true;
                        }

                        bubblePos.Clear(); // bubble transform mode에 따라 달라짐
                        PushPop.Instance.PushPopClear();
                    }
                    break;

                case Mode.Speed:
                    if (speedTimer == null)
                    {
                        speedTimer = FindObjectOfType<Speed_Timer>();
                    }

                    if (buttonActive == 0)
                    {
                        // button active false
                        for (int i = 0; i < PushPop.Instance.buttonCanvas.childCount; i++)
                        {
                            PushPop.Instance.buttonCanvas.GetChild(i).gameObject.SetActive(false);
                        }

                        speedTimer.time_Slider.value += count;
                    }
                    
                    if (buttonActive == 0 && speedTimer.time_Slider.value.Equals(1f) || speedTimer.currentTime.Equals(60))
                    {
                        // Game Clear
                        bubblePos.Clear(); // bubble transform mode에 따라 달라짐
                        PushPop.Instance.PushPopClear();
                        currentTime = speedTimer.currentTime;
                        speedTimer.StopCoroutine(speedTimer.timer);

                        Ranking.instance.SetTimer(ProfileName, ProfileIndex, int.Parse(PushPop.Instance.boardSprite.name), speedTimer.currentTime);
                        speedTimer.resultPanel.SetActive(true);
                        speedTimer.Result();
                    }
                    else if (buttonActive == 0)
                    {
                        pushpushCreate_Co = StartCoroutine(SpeedCreate_Co());
                    }
                    break;
                case Mode.Bomb:

                    break;
            }

            if (timer != null)
            {
                StopCoroutine(timer); // timer coroutine stop;
            }
        }
    }

    private IEnumerator SpeedCreate_Co()
    {
        BoardSize = new Vector2(700f, 700f);

        // animation
        Animator pushAni = PushPop.Instance.pushPopAni.GetComponent<Animator>();
        pushAni.SetTrigger("Turning");
        

        yield return new WaitForSeconds(0.5f);
        AudioManager.instance.SetAudioClip_SFX(0, false);
        yield return new WaitForSeconds(1f);

        PushPop.Instance.pushTurn = !PushPop.Instance.pushTurn;
        bubblePos.Clear();
        PushPop.Instance.PushPopClear();

        // pushpop 생성, PushPop.Instance.pushTurn == false일 때 Rotate 180 돌려준 뒤에 add
        PushPop.Instance.CreatePushPopBoard(PushPop.Instance.pushPopCanvas);
        PushPop.Instance.CreateGrid(PushPop.Instance.pushPopBoardObject[0]);
        PushPop.Instance.PushPopButtonSetting(PushPop.Instance.buttonCanvas);
        buttonActive = PushPop.Instance.activePos.Count;
    }

    public void PushPushMode()
    {
        BoardSize = new Vector2(520f, 400f); // scale
        // puzzle 생성
        if (puzzleLogic == null)
        {
            puzzleLogic = FindObjectOfType<PuzzleLozic>();
        }
        if (!puzzleLogic.gameObject.activeSelf)
        {
            puzzleLogic.gameObject.SetActive(true);
        }
        puzzleLogic.SettingPuzzle();
        //초기화
        bubbleObject.Clear();
        // puzzle position
        for (int i = 0; i < puzzleClass.Count; i++)
        {
            CreateBubble(puzzleClass[i].puzzleArea, puzzleClass[i].puzzleCenter, puzzleClass[i].puzzleObject);
        }

    }

    public void SpeedMode()
    { // speed mode start
        Ranking.instance.SettingPreviousScore();
        StartCoroutine(GameReady_Co());
        // position count 한 개, 위치 가운데, scale 조정
        bubbleSize = 300f; // speed mode bubble size setting
        BoardSize = new Vector2(300f, 300f); // scale

        // bubble position
        GameObject board = Instantiate(PushPop.Instance.boardPrefabUI, PushPop.Instance.pushPopCanvas); // image
        board.GetComponent<Image>().sprite = PushPop.Instance.boardSprite;
        board.GetComponent<RectTransform>().sizeDelta = BoardSize;
        PushPop.Instance.pushPopBoardObject.Add(board);
        CreateBubble(BoardSize, board.transform.localPosition, board);
    }

    public void SpeedModePushPopCreate()
    {
        for (int i = 0; i < PushPop.Instance.pushPopBoardObject.Count; i++)
        {
            Destroy(PushPop.Instance.pushPopBoardObject[i]);
        }
        PushPop.Instance.pushPopBoardObject.Clear();

        speedCreate = StartCoroutine(SpeedBoardStartCreate_Co());
    }

    private IEnumerator SpeedBoardStartCreate_Co()
    { // bubble 터졌을 때
        // pushpop 생성
        BoardSize = new Vector2(700f, 700f);
        BoardSizeGameObject = new Vector2(700f, 700f);
        PushPop.Instance.CreatePushPopBoard(PushPop.Instance.pushPopCanvas);

        yield return new WaitForSeconds(1.5f);

        BoardSize = new Vector2(700f, 700f);
        PushPop.Instance.CreateGrid(PushPop.Instance.pushPopBoardObject[0]);
        PushPop.Instance.PushPopButtonSetting(PushPop.Instance.buttonCanvas);
        buttonActive = PushPop.Instance.activePos.Count;
    }

    public void MemoryMode()
    {
        // pushpop button처럼 생성
    }

    public void BombMode()
    {
        BoardSize = new Vector2(600f, 600f);
        // 상단 배치

        // 게임 보드에 배치

    }

    private void CreateBubble(Vector2 _size, Vector2 _pos, GameObject _puzzle)
    { // bubble size, pos, parent 상속 setting method
        GameObject bubbleObject = Instantiate(bubblePrefab, _puzzle.transform);
        Bubble bubble = bubbleObject.GetComponent<Bubble>();

        this.bubbleObject.Add(bubbleObject);
        bubble.BubbleSetting(_size, _pos, _puzzle.transform);
        _puzzle.GetComponent<Image>().raycastTarget = false;
        // _puzzle.GetComponent<RectTransform>().sizeDelta = BoardSize;
        //_puzzle.SetParent(bubble.transform);
        /*bubble.touchCount = 1;*/
        bubble.touchCount = Random.Range(10, 21); // 2 ~ 9회, Mode별로 다르게 설정 ... todo touch count 바꿔줄 것
    }

    /// <summary>
    /// 변경할 gameobject를 매개변수로 받아서 그 안의 Image Component를 통해 프로필 이미지를 출력
    /// </summary>
    /// <param name="printobj"></param>
    public void ProfileImagePrint(GameObject printobj)
    {
        Image image = printobj.GetComponent<Image>();

        if (IsImageMode) // 이미지 선택모드
        {   // 저장된 Index의 이미지를 프로필 Sprite에 넣어줌
            image.sprite = ProfileImages[DefaultImage];
        }
        else if (!IsImageMode) // 사진찍기 모드
        {
            Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(UID, ProfileIndex);
            Sprite profileSprite = TextureToSprite(profileTexture);
            image.sprite = profileSprite;
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

    public void PrintSpeed(int _spriteName)
    {
        Ranking.instance.LoadTimer(printTimer, printImage, printName, _spriteName);
        Ranking.instance.LoadTimer_Personal(printNamePersonal, printTimerPersonal, printImagePersonal, _spriteName);
    }

    public void RankClear()
    {
        for (int i = 0; i < printTimer.Length; i++)
        {
            printTimer[i].text = string.Empty;
            printName[i].text = string.Empty;
            printImage[i].sprite = noneSprite;
        }
    }
}
