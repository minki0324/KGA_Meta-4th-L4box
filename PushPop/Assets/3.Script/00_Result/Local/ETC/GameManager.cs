using System.Collections;
using System.Collections.Generic;
using TMPro;
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

public class GameManager : MonoBehaviour, IGameMode
{
    public static GameManager Instance = null;
    public GameMode _gameMode; // delete 필요
    public Mode gameMode;
    public int TimerTime; // delete 필요

    // Bubble
    [Header("Bubble Info")]
    [SerializeField] private Canvas bubbleCanvas = null;
    [SerializeField] private GameObject bubblePrefab = null;
    public List<GameObject> bubbleObject = new List<GameObject>();
    [SerializeField] private float bubbleSize;

    [SerializeField] private Transform[] pos = new Transform[4]; // pushpush, speed, bomb1, bomb2
    private List<Transform> bubblePos = new List<Transform>(); // Mode에 따라 달라짐

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
    public bool IsImageMode = true; // false = 사진찍기, true = 이미지 선택

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
    { // game Start 시 호출되는 method
        if (gameMode.Equals(0))
        {
            for (int i = 0; i < pos[(int)gameMode].transform.childCount; i++)
            { // 지정된 position
                bubblePos.Add(pos[(int)gameMode].GetChild(i));
            }
        }

        // score 초기화
        Score = 0;
        TimeScore = 0;

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
                break;
        }
    }

    private IEnumerator GameReady_Co()
    {
        // game start 문구 띄워주기, panel 다 막아버리면 될듯?
        // 준비~~
        yield return new WaitForSeconds(2f);
        // 시작!!
        yield return new WaitForSeconds(1f);

        while (true)
        {
            TimeScore += Time.deltaTime;
            scoreText.text = TimeScore.ToString("F3");
            yield return null;
        }
    }

    public void GameClear()
    { // Game End 시 호출하는 method
        if (PushPop.Instance.pushPopButton.Count == 0)
        {
            bubblePos.Clear(); // bubble transform mode에 따라 달라짐
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
            // bubbleObject[i].transform.position = Vector2.zero; // puzzle size 가로, 세로 길이 중에서 더 큰 기준으로 갖고오기
            // puzzle bubble에 상속
        }
        // puzzle 전부 맞췄을 시 if ()
        // pushpop 생성
            for (int i = 0; i < PushPop.Instance.pushPopBoardObject.Count; i++)
            {
                PushPop.Instance.CreatePushPop(PushPop.Instance.pushPopBoardObject[i]);
            }
    }

    public void SpeedMode()
    {
        // position count 한 개, 위치 가운데, scale 조정
        bubbleSize = 500f; // speed mode bubble size setting
        BoardSize = new Vector2(bubbleSize, bubbleSize);

        // bubble position
        CreateBubble();

        if (this.bubbleObject.Count == 0)
        { // touch count 0보다 작을 시
            // pushpop 생성
            for (int i = 0; i < PushPop.Instance.pushPopBoardObject.Count; i++)
            {
                PushPop.Instance.CreatePushPop(PushPop.Instance.pushPopBoardObject[i]);
                Destroy(gameObject);
            }
        }
    }

    public void MemoryMode()
    {
        // pushpop button처럼 생성
    }

    public void BombMode()
    {
        // 상단 배치

        // 게임 보드에 배치

    }

    private void CreateBubble()
    {
        GameObject bubbleObject = Instantiate(bubblePrefab, bubbleCanvas.transform);
        Bubble bubble = bubbleObject.GetComponent<Bubble>();
        this.bubbleObject.Add(bubbleObject);
        bubble.BubbleSetting(bubbleSize); // position 추가 필요
        bubbleObject.transform.localPosition = new Vector2(0, 0); // -> BubbleSetting에서 setting 될듯!!
        bubble.touchCount = Random.Range(2, 10); // 2 ~ 9회, Mode별로 다르게 설정
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
