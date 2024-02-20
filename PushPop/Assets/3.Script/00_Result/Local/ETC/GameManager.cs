using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    //게임매니저에 넣을 것
    None = 0,
    PushPush,
    Speed,
    Memory,
    Bomb
}

public enum Mode // GameMode
{
    PushPush = 0,
    Speed,
    Memory,
    Bomb,
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
    public GameMode _gameMode; // delete 필요
    public Mode gameMode;
    public int ShutdownTime; // delete 필요

    [Header("GameScript")]
    [SerializeField] private CustomPushpopManager pushpushScript;
    [SerializeField] private CostomPushpopManager pushpushScript;
    public Bomb bombScript;

    // Bubble
    [Header("Bubble Info")]
    [SerializeField] private GameObject bubbleCanvas = null;
    [SerializeField] private GameObject bubblePrefab = null;
    public List<GameObject> bubbleObject = new List<GameObject>();
    [SerializeField] private float bubbleSize;

    [SerializeField] private Transform[] pos = new Transform[4]; // pushpush, speed, bomb1, bomb2
    private List<Transform> bubblePos = new List<Transform>(); // Mode에 따라 달라짐

    [Header("PushPop Info")]
    public Sprite moldIcon = null;
    public int PushPopStage = 0;
    public Vector2 BoardSize;
    [SerializeField] Transform boardPos = null;
    public int buttonActive = 0;

    [Header("Puzzle Info")]
    public Vector2 puzzleSize;
    public Vector2 finalCenter;
    public List<PuzzleObject> puzzleClass = new List<PuzzleObject>();
    [SerializeField] private PuzzleLozic puzzleLogic;
    public List<PushPushObject> push = new List<PushPushObject>();

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
    
    [Header("2P Player")]
    public string ProfileName2P = string.Empty;
    public int ProfileIndex2P = 0;
    public int DefaultImage2P = 0;
    public bool IsimageMode2P = true;

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
        // 준비~~
        yield return new WaitForSeconds(2f);
        // 시작!!
        yield return new WaitForSeconds(1f);
    }

    public void GameClear()
    { // Game End 시 호출하는 method
        // button active check
        if(gameMode.Equals(Mode.Bomb))
        {
            if(bombScript.popList1P.Count.Equals(0) || bombScript.popList2P.Count.Equals(0))
            {
                bombScript.RepeatGameLogic();
                return;
            }
        }
        else if (buttonActive == 0)
        {
            switch (gameMode)
            {
                case Mode.PushPush:
                    //담고
                    int[] spriteIndexs = new int[pushpushScript.puzzleBoard.transform.childCount];
                    Vector2[] childPos = new Vector2[pushpushScript.puzzleBoard.transform.childCount];
                    for (int i = 0; i < pushpushScript.puzzleBoard.transform.childCount; i++)
                    {
                        PushPopButton pop = pushpushScript.puzzleBoard.transform.GetChild(i).GetComponent<PushPopButton>();
                        Debug.Log("배열 : " + spriteIndexs[i]);
                        Debug.Log("pop : " + pop);
                        Debug.Log("pop.spriteIndex : " + pop.spriteIndex);
                        spriteIndexs[i] = pop.spriteIndex;
                        childPos[i] = pop.gameObject.transform.localPosition;
                    }

                    PushPushObject newPush = new PushPushObject(puzzleLogic.currentPuzzle.PuzzleID, pushpushScript.StackPops.Count, spriteIndexs, childPos);
                    string json = JsonUtility.ToJson(newPush);
                    SQL_Manager.instance.SQL_AddPushpush(json, ProfileIndex);

                    

                    pushpushScript.result.SetActive(true);
                    //출력
                    pushpushScript.resultText.text = Mold_Dictionary.instance.icon_Dictionry[puzzleLogic.currentPuzzle.PuzzleID];
                    /*pushpushScript.resultImage.sprite = puzzleLogic.currentPuzzle.board;*/


                    for (int i = 0; i < pushpushScript.puzzleBoard.transform.childCount; i++)
                    {
                        pushpushScript.puzzleBoard.transform.GetChild(i).GetComponent<Button>().interactable = true;
                    }

                    bubblePos.Clear(); // bubble transform mode에 따라 달라짐
                    PushPop.Instance.PushPopClear();
                    break;
            }
                case Mode.Speed:
                    // button active false
                    for (int i = 0; i < PushPop.Instance.buttonCanvas.childCount; i++)
                    {
                        PushPop.Instance.buttonCanvas.GetChild(i).gameObject.SetActive(false);
                    }

                    Speed_Timer speed_Timer = FindObjectOfType<Speed_Timer>();
                    speed_Timer.time_Slider.value += count;
                    if (speed_Timer.time_Slider.value.Equals(1f))
                    {
                        Debug.Log("Game Clear");
                        // Game Clear
                        bubblePos.Clear(); // bubble transform mode에 따라 달라짐
                        PushPop.Instance.PushPopClear();
                        speed_Timer.StopCoroutine(speed_Timer.timer);
                        // Ranking SQL Update
                        // Ranking.instance.UpdateTimerScore(PushPop.Instance.currentTime);
                    }
                    else
                    {
                        StartCoroutine(PushPushCreate_Co());
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

    private IEnumerator PushPushCreate_Co()
    {
        // animation
        Animator pushAni = PushPop.Instance.pushPopAni.GetComponent<Animator>();
        pushAni.SetTrigger("Turning");

        yield return new WaitForSeconds(2f);

        PushPop.Instance.pushTurn = !PushPop.Instance.pushTurn;
        bubblePos.Clear();
        PushPop.Instance.PushPopClear();

        // pushpop 생성, PushPop.Instance.pushTurn == false일 때 Rotate 180 돌려준 뒤에 add
        PushPop.Instance.CreatePushPopBoard();
        PushPop.Instance.CreateGrid(PushPop.Instance.pushPopBoardObject[0]);
        PushPop.Instance.PushPopButtonSetting();
        buttonActive = PushPop.Instance.activePos.Count - 1;
    }

    public void PushPushMode()
    {
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
        // puzzle position
        for (int i = 0; i < puzzleClass.Count; i++)
        {
            CreateBubble(puzzleClass[i].puzzleArea, puzzleClass[i].puzzleCenter, puzzleClass[i].puzzleObject);
        }
        // puzzle 전부 맞췄을 시 if ()

        // pushpop 생성
        /* for (int i = 0; i < PushPop.Instance.pushPopBoardObject.Count; i++)
         {
             PushPop.Instance.CreatePushPop(PushPop.Instance.pushPopBoardObject[i]);
         }*/
    }

    public void SpeedMode()
    {
        StartCoroutine(GameReady_Co());
        // position count 한 개, 위치 가운데, scale 조정
        bubbleSize = 500f; // speed mode bubble size setting
        BoardSize = new Vector2(300f, 300f); // scale

        // bubble position
        GameObject board = Instantiate(PushPop.Instance.boardPrefabUI, PushPop.Instance.pushPopCanvas); // image
        board.GetComponent<Image>().sprite = PushPop.Instance.boardSprite;
        PushPop.Instance.pushPopBoardObject.Add(board);
        CreateBubble(BoardSize, board.transform.localPosition, board);
    }

    public void SpeedModePushPopCreate()
    {
        BoardSize = new Vector2(700f, 700f); // scale
        for (int i = 0; i < PushPop.Instance.pushPopBoardObject.Count; i++)
        {
            Destroy(PushPop.Instance.pushPopBoardObject[i]);
        }
        PushPop.Instance.pushPopBoardObject.Clear();

        // pushpop 생성
        PushPop.Instance.CreatePushPopBoard();
        PushPop.Instance.CreateGrid(PushPop.Instance.pushPopBoardObject[0]);
        PushPop.Instance.PushPopButtonSetting();
        buttonActive = PushPop.Instance.activePos.Count - 1;
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
        _puzzle.GetComponent<RectTransform>().sizeDelta = BoardSize;
        //_puzzle.SetParent(bubble.transform);
        bubble.touchCount = 1;
        /*bubble.touchCount = Random.Range(2, 10); */// 2 ~ 9회, Mode별로 다르게 설정 ... todo touch count 바꿔줄 것
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

}
