using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using Mirror;
public enum GameMode // GameMode
{
    PushPush = 0,
    Speed,
    Memory,
    Multi,
    None,
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

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public GameMode GameMode = GameMode.None;

    [Header("ShutDown")]
    public float ShutdownTimer = 0f;
    public bool InGame = false; // Shutdown setting 시 ture
    public bool IsShutdown = false; // Shutdown End 시 ture

    [Header("Game Script")]
    public MultiManager multiGame = null;
    public PushPushManager pushPush;
    public MemoryManager MemoryManager = null;
    

    [Header("Speed Game")]
    public int CurrentIcon = 0; // 선택한 아이콘 리스트 순서
    public int CurrentIconName = 0; // 선택한 아이콘 이름
    public Difficulty Difficulty = Difficulty.Easy;

    public int boardName = 0; // mold name
    public int currentTime = 0;
    public Coroutine speedCreate = null;

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
    public int LiveBubbleCount = 0; // Bubble을 터트리지 않은 상태로 나갈 때 true, 터트리면 false
    public Action OnDestroyBubble; // Bubble이 OnDestroy 했을 때
    public Action GameEnd; // PushPop button

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
    public CompletedStage myMeomoryStageInfo;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        for (int i = 0; i < PushPop.Instance.PushPopBoardObject.Count; i++)
        {
            Destroy(PushPop.Instance.PushPopBoardObject[i]);
        }
        PushPop.Instance.PushPopBoardObject.Clear();
        AudioManager.instance.SetCommonAudioClip_SFX(1);
        _panel.SetActive(true);
        DialogManager.instance.Print_Dialog(text, "준비 ~");
        yield return new WaitForSeconds(2f);

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
                        // PushPop.Instance.PushPopClear();
                    }
                    break;
        }
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
            CreateBubble(puzzleClass[i].puzzleArea, puzzleClass[i].puzzleCenter, puzzleClass[i].puzzleObject);
        }

    }

    public void CreateBubble(Vector2 _size, Vector2 _pos, GameObject _puzzle)
    { // bubble size, pos, parent 상속 setting method
        GameObject bubbleObject = Instantiate(bubblePrefab, _puzzle.transform);
        Bubble bubble = bubbleObject.GetComponent<Bubble>();

        this.bubbleObject.Add(bubbleObject);
        bubble.BubbleSetting(_size, _pos);
        _puzzle.GetComponent<Image>().raycastTarget = false;
        // _puzzle.GetComponent<RectTransform>().sizeDelta = BoardSize;
        //_puzzle.SetParent(bubble.transform);
        //bubble.touchCount = 1;
        bubble.touchCount = UnityEngine.Random.Range(10, 21); // 2 ~ 9회, Mode별로 다르게 설정 ... todo touch count 바꿔줄 것
    }

    public void PrintSpeed(int _spriteName)
    {
        //Ranking.Instance.LoadTimer(printTimer, printImage, printName, _spriteName);
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
}
