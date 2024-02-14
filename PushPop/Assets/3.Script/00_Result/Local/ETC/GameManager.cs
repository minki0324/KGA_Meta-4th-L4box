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

public class GameManager : MonoBehaviour, IGameMode
{
    public static GameManager instance;
    public GameMode gameMode;
    public int TimerTime;

    

    // Bubble
    [Header("Bubble Info")]
    [SerializeField] private Canvas bubbleCanvas = null;
    [SerializeField] private GameObject bubblePrefab = null;
    private List<GameObject> bubbleObject = new List<GameObject>();
    [SerializeField] private Vector2 bubbleSize;

    [SerializeField] private Transform[] pos = new Transform[4]; // pushpush, speed, bomb1, bomb2
    private List<Transform> bubblePos = new List<Transform>(); // Mode에 따라 달라짐
    public int touchCount = 0;

    // Camera size
    public float yScreenHalfSize;
    public float xScreenHalfSize;

    [Header("Score")]
    [SerializeField] private TMP_Text scoreText;
    private Coroutine timer = null;
    public int score = 0;
    public float timeScore = 0;

    [Header("User Infomation")]
    public int UID;
    public string Profile_name;
    public int Profile_Index;
    public int DefaultImage;
    public Sprite[] ProfileImages;
    public bool _isImageMode = true; // false = 사진찍기, true = 이미지 선택

    #region Unity Callback
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // camera size
        yScreenHalfSize = Camera.main.orthographicSize;
        xScreenHalfSize = yScreenHalfSize * Camera.main.aspect;
    }
    #endregion

    #region Other Method
    public void GameStart(int _gameMode)
    { // game Start 시 호출되는 method
        if (_gameMode.Equals(0))
        {
            for (int i = 0; i < pos[_gameMode].transform.childCount; i++)
            { // 지정된 position
                bubblePos.Add(pos[_gameMode].GetChild(i));
            }
        }

        // score 초기화
        score = 0;
        timeScore = 0;

        timer = StartCoroutine(GameReady_Co()); // Game 시작 전 대기

        Mode gameMode = (Mode)_gameMode;
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
            timeScore += Time.deltaTime;
            scoreText.text = timeScore.ToString("F3");
            yield return null;
        }
    }

    public void GameClear()
    { // Game End 시 호출하는 method
        if (PushPop.instance.pushPopButton.Count == 0)
        {
            bubblePos.Clear(); // bubble transform mode에 따라 달라짐
            PushPop.instance.PushPopClear();
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
            bubbleObject[i].transform.position = new Vector2();
        }
    }

    public void SpeedMode()
    {
        // position count 한 개, 위치 가운데, scale 조정
        GameObject bubble = Instantiate(bubblePrefab, bubbleCanvas.transform);
        bubbleObject.Add(bubble);
        bubbleObject[0].GetComponent<RectTransform>().sizeDelta = new Vector2(500f, 500f);
        bubbleObject[0].transform.localPosition = new Vector2(0, 0);
        touchCount = Random.Range(2, 10); // 2 ~ 9회
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

    private void BubblePositionSetting()
    {

    }

    private void PushPopPositionSetting()
    {
        
    }

    /// <summary>
    /// 변경할 gameobject를 매개변수로 받아서 그 안의 Image Component를 통해 프로필 이미지를 출력
    /// </summary>
    /// <param name="printobj"></param>
    public void ProfileImagePrint(GameObject printobj)
    {
        Image image = printobj.GetComponent<Image>();

        if (_isImageMode) // 이미지 선택모드
        {   // 저장된 Index의 이미지를 프로필 Sprite에 넣어줌
            image.sprite = ProfileImages[DefaultImage];
        }
        else if (!_isImageMode) // 사진찍기 모드
        {
            Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(UID, Profile_Index);
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
