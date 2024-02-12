using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    //���ӸŴ����� ���� ��
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
    private List<Transform> bubblePos = new List<Transform>(); // Mode�� ���� �޶���
    public int touchCount = 0;

    // Camera size
    public float yScreenHalfSize;
    public float xScreenHalfSize;

    [Header("User Infomation")]
    public int UID;
    public string Profile_name;
    public int Profile_Index;

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

    //���� ���۵Ǹ� ȣ��
    public IEnumerator Timer_co()
    {
        int t = TimerTime;
        while (true)
        {
            if (t <= 0)
            {
                //�ð� �ʱ�ȭ�ϱ�
                //Mainâ �ѱ�
                //���� ���� �˸� ����

                yield break;
            }
            t -= 1;

            yield return new WaitForSeconds(1f);
        }
    }

    public void GameStart(int _gameMode)
    { // game Start �� ȣ��Ǵ� method
        for (int i = 0; i < pos[_gameMode].transform.childCount; i++)
        { // ������ position
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
    { // Game End �� ȣ���ϴ� method
        for (int i = 0; i < bubbleObject.Count; i++)
        {
            bubbleObject[i].SetActive(false);
        }
        bubblePos.Clear(); // bubble transform mode�� ���� �޶���
        PushPop.instance.PushPopClear();
    }

    public void PushPushMode()
    {
        float randomX = Random.Range(0f, 10f);
        // puzzle position
        GetBubbleObject(bubbleObject.Count, bubblePrefab, bubbleCanvas.transform); // bubbleObject.Count -> puzzleObject.Count�� �ٲ� ��
        for (int i = 0; i < bubbleObject.Count; i++)
        {
            bubbleObject[i].transform.position = new Vector2();
        }
    }

    public void SpeedMode()
    {
        // position count �� ��, ��ġ ���, scale ����
        GetBubbleObject(1, bubblePrefab, bubbleCanvas.gameObject.transform);
        bubbleObject[0].transform.position = new Vector2(xScreenHalfSize, yScreenHalfSize);
        touchCount = Random.Range(1, 11); // 1 ~ 10ȸ
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

    private void BubblePositionSetting()
    {

    }

    // bubble Object Pooling
    private void GetBubbleObject(int posCount, GameObject _prefab, Transform _parent)
    {
        for (int i = 0; i < posCount; i++)
        {
            if (!bubbleObject[i].activeSelf) // ���� bubble�� Ȱ��ȭ �Ǿ����� �ʴٸ� true
            {
                bubbleObject[i].SetActive(true);
                bubbleObject[i].GetComponent<RectTransform>().sizeDelta = bubbleSize;
                return;
            }
        }

        GameObject newPos = Instantiate(_prefab, _parent); // bubble�� �� �ʿ��ϴٸ� ���� ����
        bubbleObject.Add(newPos);
        return;
    }
    #endregion

}
