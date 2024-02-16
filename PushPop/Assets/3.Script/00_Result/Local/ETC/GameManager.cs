using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    //���ӸŴ����� ���� ��
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
    public GameMode _gameMode; // delete �ʿ�
    public Mode gameMode;
    public int ShutdownTime; // delete �ʿ�

    [Header("GameScript")]
    [SerializeField] private CostomPushpopManager pushpushScript;

    // Bubble
    [Header("Bubble Info")]
    [SerializeField] private GameObject bubbleCanvas = null;
    [SerializeField] private GameObject bubblePrefab = null;
    public List<GameObject> bubbleObject = new List<GameObject>();
    [SerializeField] private float bubbleSize;

    [SerializeField] private Transform[] pos = new Transform[4]; // pushpush, speed, bomb1, bomb2
    private List<Transform> bubblePos = new List<Transform>(); // Mode�� ���� �޶���

    [Header("PushPop Info")]
    public int PushPopStage = 0;
    public Vector2 BoardSize;

    [Header("Puzzle Info")]
    public Vector2 puzzleSize;
    public Vector2 finalCenter;
    public List<PuzzleObject> puzzleClass = new List<PuzzleObject>();
    [SerializeField] private PuzzleLozic puzzleLogic;
    public List<PushPushObject> push = new List<PushPushObject>();

    [Header("Score")]
    [SerializeField] private TMP_Text scoreText;
    private Coroutine timer = null;
    public int Score = 0;
    public float TimeScore = 0;

    [Header("User Infomation")]
    public int UID;
    public string ProfileName;
    public int ProfileIndex;
    public int DefaultImage;
    public Sprite[] ProfileImages;
    public bool IsImageMode = true; // false = �������, true = �̹��� ����


    #region Unity Callback
    private void Awake()
    {
        if(Instance == null)
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
    { // game Start �� ȣ��Ǵ� method
        /*if (gameMode.Equals(0))
        {
            for (int i = 0; i < pos[(int)gameMode].transform.childCount; i++)
            { // ������ position
                bubblePos.Add(pos[(int)gameMode].GetChild(i));
            }
        }*/

        // score �ʱ�ȭ
        Score = 0;
        TimeScore = 0;

        timer = StartCoroutine(GameReady_Co()); // Game ���� �� ���
        
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
                break;
        }
    }

    private IEnumerator GameReady_Co()
    {
        // game start ���� ����ֱ�, panel �� ���ƹ����� �ɵ�?
        // �غ�~~
        yield return new WaitForSeconds(2f);
        // ����!!
        yield return new WaitForSeconds(1f);

        while (true)
        {
            TimeScore += Time.deltaTime;
            scoreText.text = TimeScore.ToString("F3");
            yield return null;
        }
    }

    public void GameClear()
    { // Game End �� ȣ���ϴ� method
        if (PushPop.Instance.pushPopButton.Count == 0)
        {
            Debug.Log("����Ŭ����");
           

            switch(gameMode)
            {
                case Mode.PushPush:
                    //���
                    int[] spriteIndexs = new int[pushpushScript.puzzleBoard.transform.childCount];
                    Vector2[] childPos = new Vector2[pushpushScript.puzzleBoard.transform.childCount];
                    for(int i = 0; i< pushpushScript.puzzleBoard.transform.childCount; i++)
                    {
                        PushPopButton pop = pushpushScript.puzzleBoard.transform.GetChild(i).GetComponent<PushPopButton>();
                        Debug.Log("�迭 : " + spriteIndexs[i]);
                        Debug.Log("pop : " + pop);
                        Debug.Log("pop.spriteIndex : " + pop.spriteIndex);
                        spriteIndexs[i] = pop.spriteIndex;
                        childPos[i] = pop.gameObject.transform.position;
                    }

                    PushPushObject newPush = new PushPushObject(puzzleLogic.currentPuzzle.PuzzleID, pushpushScript.StackPops.Count, spriteIndexs, childPos);
                    pushpushScript.result.SetActive(true);
                    //���
                    pushpushScript.resultText.text = Mold_Dictionary.instance.icon_Dictionry[puzzleLogic.currentPuzzle.PuzzleID];
                    /*pushpushScript.resultImage.sprite = puzzleLogic.currentPuzzle.board;*/

                    pushpushScript.puzzleBoard.GetComponent<Animator>().SetTrigger("Clear");

                    for (int i = 0; i < pushpushScript.puzzleBoard.transform.childCount; i++)
                    {
                        pushpushScript.puzzleBoard.transform.GetChild(i).GetComponent<Button>().interactable = true;
                    }
                    break;
            }

            bubblePos.Clear(); // bubble transform mode�� ���� �޶���

            PushPop.Instance.PushPopClear();

            if (timer != null)
            {
                StopCoroutine(timer); // timer coroutine stop;
            }

            // Ranking SQL Update
            // Ranking.instance.UpdateTimerScore(timeScore);
        }
    }

    public void PushPushMode()
    {
        // puzzle ����
        if (puzzleLogic == null)
        {
            puzzleLogic = FindObjectOfType<PuzzleLozic>();
        }
        if(!puzzleLogic.gameObject.activeSelf)
        {
            puzzleLogic.gameObject.SetActive(true);
        }
        puzzleLogic.SettingPuzzle();
        // puzzle position
        for (int i = 0; i < puzzleClass.Count; i++)
        {
            CreateBubble(puzzleClass[i].puzzleArea, puzzleClass[i].puzzleCenter, puzzleClass[i].puzzleObject);
        }
        // puzzle ���� ������ �� if ()

        // pushpop ����
           /* for (int i = 0; i < PushPop.Instance.pushPopBoardObject.Count; i++)
            {
                PushPop.Instance.CreatePushPop(PushPop.Instance.pushPopBoardObject[i]);
            }*/
    }

    public void SpeedMode()
    {
        // position count �� ��, ��ġ ���, scale ����
        bubbleSize = 500f; // speed mode bubble size setting
        BoardSize = new Vector2(bubbleSize, bubbleSize);

        // bubble position
        //CreateBubble();

        if (this.bubbleObject.Count == 0)
        { // touch count 0���� ���� ��
            // pushpop ����
            for (int i = 0; i < PushPop.Instance.pushPopBoardObject.Count; i++)
            {
                PushPop.Instance.CreatePushPop(PushPop.Instance.pushPopBoardObject[i]);
                Destroy(gameObject);
            }
        }
    }

    public void MemoryMode()
    {
        // pushpop buttonó�� ����
    }

    public void BombMode()
    {
        // ��� ��ġ

        // ���� ���忡 ��ġ

    }

    private void CreateBubble(Vector2 _size, Vector2 _pos, GameObject _puzzle)
    { // bubble size, pos, parent ��� setting method
        GameObject bubbleObject = Instantiate(bubblePrefab, _puzzle.transform);
        Bubble bubble = bubbleObject.GetComponent<Bubble>();

        this.bubbleObject.Add(bubbleObject);
        bubble.BubbleSetting(_size, _pos, _puzzle.transform);
        _puzzle.GetComponent<Image>().raycastTarget = false;
        //_puzzle.SetParent(bubble.transform);
        /*bubble.touchCount = 1; */
        bubble.touchCount = Random.Range(2, 10); // 2 ~ 9ȸ, Mode���� �ٸ��� ���� ... todo touch count �ٲ��� ��
    }

    /// <summary>
    /// ������ gameobject�� �Ű������� �޾Ƽ� �� ���� Image Component�� ���� ������ �̹����� ���
    /// </summary>
    /// <param name="printobj"></param>
    public void ProfileImagePrint(GameObject printobj)
    {
        Image image = printobj.GetComponent<Image>();

        if (IsImageMode) // �̹��� ���ø��
        {   // ����� Index�� �̹����� ������ Sprite�� �־���
            image.sprite = ProfileImages[DefaultImage];
        }
        else if (!IsImageMode) // ������� ���
        {
            Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(UID, ProfileIndex);
            Sprite profileSprite = TextureToSprite(profileTexture);
            image.sprite = profileSprite;
        }
    }

    /// <summary>
    /// SQL_Manager���� Texture2D�� ��ȯ�� �̹��������� Sprite�� �ѹ� �� ��ȯ�ϴ� Method
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
