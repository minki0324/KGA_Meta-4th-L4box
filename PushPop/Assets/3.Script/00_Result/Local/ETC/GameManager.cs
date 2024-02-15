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

public class GameManager : MonoBehaviour, IGameMode
{
    public static GameManager Instance = null;
    public GameMode _gameMode; // delete �ʿ�
    public Mode gameMode;
    public int TimerTime; // delete �ʿ�

    // Bubble
    [Header("Bubble Info")]
    [SerializeField] private Canvas bubbleCanvas = null;
    [SerializeField] private GameObject bubblePrefab = null;
    public List<GameObject> bubbleObject = new List<GameObject>();
    [SerializeField] private float bubbleSize;

    [SerializeField] private Transform[] pos = new Transform[4]; // pushpush, speed, bomb1, bomb2
    private List<Transform> bubblePos = new List<Transform>(); // Mode�� ���� �޶���

    [Header("PushPop Info")]
    public int PushPopStage = 0;
    public Vector2 BoardSize;

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
    public void GameModeSetting(Mode _gameMode)
    { // game mode setting button click method
        gameMode = _gameMode;
    }

    public void GameStageSetting(int _stage)
    { // game stage button click method
        PushPopStage = _stage;
    }

    public void GameStart()
    { // game Start �� ȣ��Ǵ� method
        if (gameMode.Equals(0))
        {
            for (int i = 0; i < pos[(int)gameMode].transform.childCount; i++)
            { // ������ position
                bubblePos.Add(pos[(int)gameMode].GetChild(i));
            }
        }

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
            bubblePos.Clear(); // bubble transform mode�� ���� �޶���
            PushPop.Instance.PushPopClear();
            StopCoroutine(timer); // timer coroutine stop;

            // Ranking SQL Update
            // Ranking.instance.UpdateTimerScore(timeScore);
        }
    }

    public void PushPushMode()
    {
        float randomX = Random.Range(0f, 10f);
        // puzzle position
        for (int i = 0; i < bubbleObject.Count; i++)
        {
            CreateBubble();
            // bubbleObject[i].transform.position = Vector2.zero; // puzzle size ����, ���� ���� �߿��� �� ū �������� �������
            // puzzle bubble�� ���
        }
        // puzzle ���� ������ �� if ()
        // pushpop ����
            for (int i = 0; i < PushPop.Instance.pushPopBoardObject.Count; i++)
            {
                PushPop.Instance.CreatePushPop(PushPop.Instance.pushPopBoardObject[i]);
            }
    }

    public void SpeedMode()
    {
        // position count �� ��, ��ġ ���, scale ����
        bubbleSize = 500f; // speed mode bubble size setting
        BoardSize = new Vector2(bubbleSize, bubbleSize);

        // bubble position
        CreateBubble();

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

    private void CreateBubble()
    {
        GameObject bubbleObject = Instantiate(bubblePrefab, bubbleCanvas.transform);
        Bubble bubble = bubbleObject.GetComponent<Bubble>();
        this.bubbleObject.Add(bubbleObject);
        bubble.BubbleSetting(bubbleSize); // position �߰� �ʿ�
        bubbleObject.transform.localPosition = new Vector2(0, 0); // -> BubbleSetting���� setting �ɵ�!!
        bubble.touchCount = Random.Range(2, 10); // 2 ~ 9ȸ, Mode���� �ٸ��� ����
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
