using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode // GameMode
{
    PushPush = 0,
    Speed,
    Memory,
    Multi,
    Lobby,
    Title
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

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public GameMode GameMode = GameMode.Title;

    [Header("ShutDown")]
    public float ShutdownTimer = 0f;
    public bool IsGameClear = false; // Game Clear 시 true, 아니면 false
    public bool InGame = false; // Shutdown setting 시 ture
    public bool IsShutdown = false; // Shutdown End 시 ture
    public bool IsLoading = false; // loading 끝난 뒤 false
    public bool OnShutdownAlarm = false;

    [Header("Current Board")]
    public int CurrentIcon = 0; // 선택한 아이콘 리스트 순서
    public int CurrentIconName = 0; // 선택한 아이콘 이름
    public Difficulty Difficulty = Difficulty.Easy;

    [Header("Bubble Info")]
    [SerializeField] private GameObject bubblePrefab = null;
    public List<GameObject> bubbleObject = new List<GameObject>();

    [Header("PushPop Info")]
    public int PushPopStage = 0;

    [Header("Puzzle Info")]
    public Vector2 puzzleSize;
    public Vector2 finalCenter;
    public bool IsCustomMode = false;

    [Header("PushPush Game CallBack")]
    public int LiveBubbleCount = 0; // Bubble을 터트리지 않은 상태로 나갈 때 0으로 초기화하여 OnDestroy 때 return 시킴
    public Action NextMode;
    public Action OnDestroyBubble; // Bubble이 OnDestroy 했을 때
    public Action GameEnd; // PushPop button
    public Action Shutdown; // shutdown 시 canvas 별 setActive
    public Action ShutdownAlarm; // in game alarm
    public CompletedStage myMeomoryStageInfo;

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

        if (InGame && IsShutdown && IsGameClear)
        {
            GameShutDown();
        }
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

    private void GameShutDown()
    {
        StopAllCoroutines(); // ShutdownAlarm_Co() stop
        AudioManager.Instance.SetAudioClip_BGM(0);
        GameMode = GameMode.Lobby;
        Shutdown?.Invoke();

        IsShutdown = false;
        IsGameClear = false;
        InGame = false;
    }

    public void ShutdownAlarmStart()
    {
        StopAllCoroutines();
        StartCoroutine(ShutdownAlarm_Co());
    }

    public void ShutdownCoroutineStop()
    { // 뒤로가기 시 stop
        StopAllCoroutines();
    }

    private IEnumerator ShutdownAlarm_Co()
    {
        while (true)
        {
            if (IsShutdown)
            {
                yield break;
            }
            yield return new WaitForSeconds(60f);
            OnShutdownAlarm = true;
            if (!IsLoading) ShutdownAlarm?.Invoke();
        }
    }

    public void CreateBubble(Vector2 _size, Vector2 _pos, GameObject _puzzle)
    { // bubble size, pos, parent 상속 setting method
        GameObject bubbleObject = Instantiate(bubblePrefab, _puzzle.transform);
        Bubble bubble = bubbleObject.GetComponent<Bubble>();

        this.bubbleObject.Add(bubbleObject);
        bubble.BubbleSetting(_size, _pos);
        _puzzle.GetComponent<Image>().raycastTarget = false;
        bubble.TouchCount = UnityEngine.Random.Range(10, 21); // 10 ~ 20회
    }
}
