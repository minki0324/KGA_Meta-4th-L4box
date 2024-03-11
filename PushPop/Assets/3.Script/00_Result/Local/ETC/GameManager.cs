using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public enum GameMode // GameMode
{
    PushPush = 0,
    Speed,
    Memory,
    Multi,
    None
}


#region Other Class
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
#endregion

public class GameManager : MonoBehaviour, IGameMode_
{
    public static GameManager Instance = null;

    public GameMode GameMode = GameMode.None;

    [Header("ShutDown")]
    public float ShutdownTimer = 0f;
    public bool InGame = false; // Shutdown setting 시 ture
    public bool IsShutdown = false; // Shutdown End 시 ture

    [Header("Game Script")]
    public MultiManager multiGame = null;
    public Speed_Timer speedTimer = null;
    public PushPushManager pushPush;
    public MemoryManager MemoryManager = null;

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
    public List<PushPushObject> pushlist = new List<PushPushObject>();

    [Header("Score")]
    private Coroutine timer = null;
    public int Score = 0;
    public float TimeScore = 0;

    [Header("Speed Mode")]
    public float count = 0.25f;
    public Coroutine pushpushCreate_Co = null;
    public Coroutine slider_Co = null;

    [Header("Speed Print")]
    [SerializeField] private TMP_Text[] printName;
    [SerializeField] private TMP_Text[] printTimer;
    [SerializeField] private Image[] printImage;
    [SerializeField] private TMP_Text printNamePersonal;
    [SerializeField] private TMP_Text printTimerPersonal;
    [SerializeField] private Image printImagePersonal;

    [Header("Other")]
    [SerializeField] private SpriteAtlas atlas;
    public bool backButtonClick = false;
    [SerializeField] private Sprite[] btnSprites;
    public bool isStart = false;
  
    public int boardName = 0; // mold name
    public int currentTime = 0;
    public Coroutine speedCreate = null;

    public Difficulty Difficulty = Difficulty.Easy;

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
    private void Update()
    {
        if (InGame && !IsShutdown)
        {
            ShutdownTimerStart();
        }
    }
    #endregion

    #region Other Method
    public void GameModeSetting(int _gameMode)
    { // game mode setting button click method
        GameMode = (GameMode)_gameMode;
    }

    public void GameStageSetting(int _stage)
    { // game stage button click method
        PushPopStage = _stage;
    }

    public void ShutdownTimerStart()
    { // Game Time Setting End 시 InGame = true 되면서 호출
        ShutdownTimer -= Time.deltaTime;
        if (ShutdownTimer <= 0f)
        {
            IsShutdown = true; // Shutdown panel active 이후에 false로
            ShutdownTimer = 0f;
            return;
        }
    }

    public void GameStart()
    { 
        // score 초기화
        Score = 0;
        TimeScore = 0;
        buttonActive = 0;

        switch (GameMode)
        {
            case GameMode.PushPush:
                PushPushMode();
                break;
            case GameMode.Speed:
                SpeedMode();
                break;
            case GameMode.Memory:
                break;
            case GameMode.Multi:
                BombMode();
                break;
        }
    }

    public IEnumerator GameReady_Co(GameObject _panel, TMP_Text text)
    {

        if(GameMode == GameMode.PushPush)
        {
            //게임 시작 버튼 소리가 안들려서 잠깐 시간 좀 띄울게..
            yield return new WaitForSeconds(0.5f);
        }
        
        // game start 문구 띄워주기, panel 다 막아버리면 될듯?
        for (int i = 0; i < PushPop.Instance.pushPopBoardObject.Count; i++)
        {
            Destroy(PushPop.Instance.pushPopBoardObject[i]);
        }
        PushPop.Instance.pushPopBoardObject.Clear();
        AudioManager.instance.SetCommonAudioClip_SFX(1);
        _panel.SetActive(true);
        DialogManager.instance.Print_Dialog(text, "준비 ~");
        yield return new WaitForSeconds(2f);

        if(GameMode == GameMode.Speed)
        {
            SpeedModePushPopCreate();
            speedTimer.TimerObj.SetActive(true);
            speedTimer.TimerStart();
            speedTimer.time_Slider.gameObject.SetActive(true);
        }
        AudioManager.instance.SetCommonAudioClip_SFX(2);
        DialogManager.instance.Print_Dialog(text, "시작 ~");

        yield return new WaitForSeconds(1f);
        _panel.SetActive(false);


        switch(GameMode)
        {
            case GameMode.PushPush:
                PushPushMode();
                break;
            case GameMode.Speed:
                break;
            case GameMode.Memory:
                MemoryManager.Instance.CreatBoard();
                break;
            case GameMode.Multi:
                break;
            default:
                break;
        }
        
    }

    public void GameClear()
    { // Game End 시 호출하는 method
        // button active check
        if (GameMode.Equals(GameMode.Multi))
        {
            if (multiGame.popButtonList1P.Count.Equals(0) || multiGame.popButtonList2P.Count.Equals(0))
            {
                multiGame.RepeatGameLogic();
                return;
            }
        }
        else
        {
            switch (GameMode)
            {
                case GameMode.PushPush:
                    if (PushPop.Instance.pushPopButton.Count == pushPush.pushCount)
                    {
                        pushPush.OnButtonAllPush();
                        PushPop.Instance.pushPopButton.Clear();
                        AudioManager.instance.SetAudioClip_SFX(4, false);
                        CustomPushpopManager pushpushScript = pushPush.custom;
                        //담고
                        int[] spriteIndexs = new int[pushpushScript.puzzleBoard.transform.childCount];
                        Vector2[] childPos = new Vector2[pushpushScript.puzzleBoard.transform.childCount];
                        for (int i = 0; i < pushpushScript.puzzleBoard.transform.childCount; i++)
                        {
                            PushPopButton pop = pushpushScript.puzzleBoard.transform.GetChild(i).GetComponent<PushPopButton>();
                            spriteIndexs[i] = pop.spriteIndex;
                            childPos[i] = pop.gameObject.transform.localPosition;
                        }

                        PushPushObject newPush = new PushPushObject(pushPush.puzzle.currentPuzzle.PuzzleID, pushpushScript.StackPops.Count, spriteIndexs, childPos);
                        string json = JsonUtility.ToJson(newPush);
                        SQL_Manager.instance.SQL_AddPushpush(json, ProfileManager.Instance.FirstPlayerIndex);

                        pushpushScript.result.SetActive(true);

                        // PushPushList 세팅
                        List<PushPushObject> pushlist = SQL_Manager.instance.SQL_SetPushPush(ProfileManager.Instance.FirstPlayerIndex);
                        if (pushlist == null)
                        {
                            Debug.Log("널");
                        }

                        //출력
                        pushpushScript.resultText.text = DataManager.instance.iconDict[pushPush.puzzle.currentPuzzle.PuzzleID];

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

                case GameMode.Speed:
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

                        slider_Co = StartCoroutine(SpeedSlider_Co(speedTimer.time_Slider, count, 1.4f));
                    }
                    if (buttonActive == 0 && speedTimer.time_Slider.value + count >= 0.9f || speedTimer.currentTime.Equals((int)speedTimer.difficult))
                    {
                        // Game Clear
                        bubblePos.Clear(); // bubble transform mode에 따라 달라짐
                        PushPop.Instance.PushPopClear();
                        currentTime = speedTimer.currentTime;
                        speedTimer.StopCoroutine(speedTimer.timer);
                        speedTimer.TimerObj.SetActive(false);

                        Ranking.Instance.SetTimer(ProfileManager.Instance.ProfileName1P, ProfileManager.Instance.FirstPlayerIndex, int.Parse(PushPop.Instance.boardSprite.name), speedTimer.currentTime);
                        speedTimer.resultPanel.SetActive(true);
                        speedTimer.Result();
                    }
                    else if (buttonActive == 0)
                    {
                        pushpushCreate_Co = StartCoroutine(SpeedCreate_Co());
                    }
                    break;
                case GameMode.Multi:

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
        if (pushPush.puzzle == null)
        {
            pushPush.puzzle = FindObjectOfType<PuzzleLozic>();
        }
        if (!pushPush.puzzle.gameObject.activeSelf)
        {
            pushPush.puzzle.gameObject.SetActive(true);
        }
        pushPush.puzzle.SettingPuzzle();
        //초기화
        bubbleObject.Clear();

        // puzzle position
        for (int i = 0; i < puzzleClass.Count; i++)
        {
            CreateBubble(puzzleClass[i].puzzleArea, puzzleClass[i].puzzleCenter, puzzleClass[i].puzzleObject, puzzleClass[i]);
        }

    }

    public void SpeedMode()
    { // speed mode start
        PushPop.Instance.buttonSize = new Vector2(80f, 80f);
        PushPop.Instance.percentage = 0.67f;
        Ranking.Instance.SettingPreviousScore();
        // position count 한 개, 위치 가운데, scale 조정
        bubbleSize = 300f; // speed mode bubble size setting
        BoardSize = new Vector2(300f, 300f); // scale

        // bubble position
        GameObject board = Instantiate(PushPop.Instance.boardPrefabUI, PushPop.Instance.pushPopCanvas); // image
        board.GetComponent<Image>().sprite = PushPop.Instance.boardSprite;
        board.GetComponent<RectTransform>().sizeDelta = BoardSize;
        PushPop.Instance.pushPopBoardObject.Add(board);
        CreateBubble(BoardSize, board.transform.localPosition, board, null);
    }

    public void SpeedModePushPopCreate()
    {
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

    public void SpeedOnBubbleDestroy()
    {
        if(isStart)
        {
            StartCoroutine(GameReady_Co(speedTimer.readyPanel, speedTimer.readyText));
        }
    }

    public void MemoryMode()
    {
        // pushpop button처럼 생성
    }

    public void BombMode()
    {
        BoardSize = new Vector2(500f, 500f);
    }

    private void CreateBubble(Vector2 _size, Vector2 _pos, GameObject _puzzle, PuzzleObject _puzzleInfo)
    { // bubble size, pos, parent 상속 setting method
        GameObject bubbleObject = Instantiate(bubblePrefab, _puzzle.transform);
        Bubble bubble = bubbleObject.GetComponent<Bubble>();

        this.bubbleObject.Add(bubbleObject);
        bubble.BubbleSetting(_size, _pos, _puzzle.transform, _puzzleInfo);
        _puzzle.GetComponent<Image>().raycastTarget = false;
        // _puzzle.GetComponent<RectTransform>().sizeDelta = BoardSize;
        //_puzzle.SetParent(bubble.transform);
        //bubble.touchCount = 1;
        bubble.touchCount = Random.Range(10, 21); // 2 ~ 9회, Mode별로 다르게 설정 ... todo touch count 바꿔줄 것
    }
    #endregion

    public void PrintSpeed(int _spriteName)
    {
        Ranking.Instance.LoadTimer(printTimer, printImage, printName, _spriteName);
        Ranking.Instance.LoadTimer_Personal(printNamePersonal, printTimerPersonal, printImagePersonal, _spriteName);
    }

    public void RankClear()
    {
        for (int i = 0; i < printTimer.Length; i++)
        {
            printTimer[i].text = string.Empty;
            printName[i].text = string.Empty;
            printImage[i].sprite = ProfileManager.Instance.NoneBackground;
        }
    }

    private IEnumerator SpeedSlider_Co(Slider slider, float amount, float duration)
    {
        // 시작 값과 목표 값 계산
        float startValue = slider.value;
        float endValue = startValue + amount;

        // 경과 시간 추적
        float elapsedTime = 0;

        // 지정된 시간 동안 반복
        while (elapsedTime < duration)
        {
            // 경과 시간에 따라 슬라이더 값을 변경
            slider.value = Mathf.Lerp(startValue, endValue, elapsedTime / duration);

            // 경과 시간 업데이트
            elapsedTime += Time.deltaTime;

            // 다음 프레임까지 대기
            yield return null;
        }

        // 최종 값 설정 (목표 값에 정확히 맞추기 위함)
        slider.value = endValue;

        slider_Co = null;
    }
}
