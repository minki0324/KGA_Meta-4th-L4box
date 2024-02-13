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
    Multi
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
    //게임 시작되면 호출
    public IEnumerator Timer_co()
    {
        int t = TimerTime;
        while (true)
        {
            if (t <= 0)
            {
                //시간 초기화하기
                //Main창 켜기
                //게임 종료 알림 띄우기

                yield break;
            }
            t -= 1;

            yield return new WaitForSeconds(1f);
        }
    }

    public void GameStart(int _gameMode)
    { // game Start 시 호출되는 method
        for (int i = 0; i < pos[_gameMode].transform.childCount; i++)
        { // 지정된 position
            bubblePos.Add(pos[_gameMode].GetChild(i));
        }
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

    private void GameClear()
    { // Game End 시 호출하는 method
        for (int i = 0; i < bubbleObject.Count; i++)
        {
            bubbleObject[i].SetActive(false);
        }
        bubblePos.Clear(); // bubble transform mode에 따라 달라짐
        PushPop.instance.PushPopClear();
    }

    public void PushPushMode()
    {
        float randomX = Random.Range(0f, 10f);
        // puzzle position
        GetBubbleObject(bubbleObject.Count, bubblePrefab, bubbleCanvas.transform); // bubbleObject.Count -> puzzleObject.Count로 바꿀 것
        for (int i = 0; i < bubbleObject.Count; i++)
        {
            bubbleObject[i].transform.position = new Vector2();
        }
    }

    public void SpeedMode()
    {
        // position count 한 개, 위치 가운데, scale 조정
        GetBubbleObject(1, bubblePrefab, bubbleCanvas.gameObject.transform);
        bubbleObject[0].transform.position = new Vector2(xScreenHalfSize, yScreenHalfSize);
        touchCount = Random.Range(1, 11); // 1 ~ 10회
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

    // bubble Object Pooling
    private void GetBubbleObject(int posCount, GameObject _prefab, Transform _parent)
    {
        for (int i = 0; i < posCount; i++)
        {
            if (!bubbleObject[i].activeSelf) // 기존 bubble이 활성화 되어있지 않다면 true
            {
                bubbleObject[i].SetActive(true);
                bubbleObject[i].GetComponent<RectTransform>().sizeDelta = bubbleSize;
                return;
            }
        }

        GameObject newPos = Instantiate(_prefab, _parent); // bubble이 더 필요하다면 새로 생성
        bubbleObject.Add(newPos);
        return;
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
