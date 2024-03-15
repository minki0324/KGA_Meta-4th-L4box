using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public enum GameMode // GameMode
{
    PushPush = 0,
    Speed,
    Memory,
    Multi,
    None,
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

    public GameMode GameMode = GameMode.None;

    [Header("ShutDown")]
    public float ShutdownTimer = 0f;
    public bool InGame = false; // Shutdown setting 시 ture
    public bool IsShutdown = false; // Shutdown End 시 ture

    [Header("Game Script")]
    public MultiManager multiGame = null;
    public PushPushManager pushPush;

    public int CurrentIcon = 0; // 선택한 아이콘 리스트 순서
    public int CurrentIconName = 0; // 선택한 아이콘 이름
    public Difficulty Difficulty = Difficulty.Easy;

    [Header("Bubble Info")]
    [SerializeField] private GameObject bubblePrefab = null;
    public List<GameObject> bubbleObject = new List<GameObject>();

    [Header("PushPop Info")]
    public int PushPopStage = 0;
    public int buttonActive = 0;

    [Header("Puzzle Info")]
    public Vector2 puzzleSize;
    public Vector2 finalCenter;
    public List<PuzzleObject> puzzleClass = new List<PuzzleObject>();
    public bool IsCustomMode = false;

    [Header("PushPush Game CallBack")]
    public int puzzleListCount = 0;
    public int LiveBubbleCount = 0; // Bubble을 터트리지 않은 상태로 나갈 때 0으로 초기화하여 OnDestroy 때 return 시킴
    public Action NextMode;
    public Action OnDestroyBubble; // Bubble이 OnDestroy 했을 때
    public Action GameEnd; // PushPop button

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

    /*public void GameClear()
    { // Game End 시 호출하는 method
        switch (GameMode)
        {
            case GameMode.PushPush:
                if (PushPop.Instance.pushPopButton.Count.Equals(PushPop.Instance.PushCount))
                {
                    AudioManager.Instance.SetAudioClip_SFX(4, false);
                    PushPop.Instance.PushCount = 0;
                    PushPop.Instance.pushPopButton.Clear();
                    CustomPushpopManager customManager = pushPush.customManager;
                    //담고
                    int[] spriteIndexs = new int[customManager.puzzleBoard.transform.childCount];
                    Vector2[] childPos = new Vector2[customManager.puzzleBoard.transform.childCount];
                    for (int i = 0; i < customManager.puzzleBoard.transform.childCount; i++)
                    {
                        PushPopButton pop = customManager.puzzleBoard.transform.GetChild(i).GetComponent<PushPopButton>();
                        spriteIndexs[i] = pop.spriteIndex;
                        childPos[i] = pop.gameObject.transform.localPosition;
                    }

                    PushPushObject newPush = new PushPushObject(pushPush.puzzleManager.currentPuzzle.PuzzleID, customManager.StackPops.Count, spriteIndexs, childPos);
                    string json = JsonUtility.ToJson(newPush);
                    SQL_Manager.instance.SQL_AddPushpush(json, ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex);

                    customManager.result.SetActive(true);

                    // PushPushList 세팅
                    List<PushPushObject> pushlist = SQL_Manager.instance.SQL_SetPushPush(ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex);
                    if (pushlist == null)
                    {
                        Debug.Log("널");
                    }

                    //출력
                    customManager.resultText.text = DataManager.Instance.iconDict[pushPush.puzzleManager.currentPuzzle.PuzzleID];

                    // List를 자동으로 먼저한 순서대로 담기게 해놨음
                    customManager.resultImage.sprite = atlas.GetSprite(pushlist[0].spriteName.ToString());

                    // 기존 PushPush에서 사용했던 크기로 먼저 세팅
                    customManager.resultImage.SetNativeSize();
                    customManager.resultImage.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    for (int i = 0; i < pushlist[0].childIndex; i++)
                    { // PushPushObject Class에 저장되어있는 Btn의 index
                      // 저장된 만큼버튼 생성 및 부모설정
                        GameObject pop = Instantiate(PushPop.Instance.pushPopButtonPrefab, customManager.resultImage.transform);

                        // 버튼의 색깔 Index에 맞게 Sprite 변경
                        pop.GetComponent<Image>().sprite = btnSprites[pushlist[0].childSpriteIndex[i]];

                        // Scale과 Position 세팅
                        pop.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        pop.transform.localPosition = pushlist[0].childPosition[i];
                    }

                    // 세팅이 끝났으면 컬렉션 Bubble의 크기만큼 스케일 조정
                    customManager.resultImage.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

                    for (int i = 0; i < customManager.puzzleBoard.transform.childCount; i++)
                    {
                        customManager.puzzleBoard.transform.GetChild(i).GetComponent<Button>().interactable = true;
                    }

                    // PushPop.Instance.PushPopClear();
                }
                break;
        }
    }*/

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
