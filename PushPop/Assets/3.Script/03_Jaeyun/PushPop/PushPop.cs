using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PushPop : MonoBehaviour
{
    public static PushPop instance = null;

    [Header("PushPop Canvas")]
    [SerializeField] private Canvas pushPopCanvas;
    [SerializeField] private GameObject pushPopButton;

    [Header("PushPop Board")]
    [SerializeField] private GameObject boardPrefab = null; // board Prefab
    [SerializeField] private Sprite boardSprite = null; // board sprite, custom sprite out line setting 필요 
    private Vector3 boardSize = Vector3.zero;
    private PolygonCollider2D boardCollider; // board collider
    private GameObject pushObject = null; // instantiate object

    [Header("Grid Size")]
    [SerializeField] private Vector2 grid = Vector2.zero;
    [SerializeField] private float percentage = 0; // gameobject에 따른 gird 비율
    [SerializeField] private Vector2 buttonSize = Vector2.zero; // x, y 동일

    [Header("Grid Pos")]
    public GameObject posPrefab = null; // grid에 지정할 pos prefab
    private List<GameObject> pos = new List<GameObject>();

    private List<GameObject> posButton = new List<GameObject>();
    public List<GameObject> activePos = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
    }

    public void Start()
    {
        CreatePushPopBoard();
        CreateButton();
    }

    // Sprite 모양에 따른 Polygon collider setting
    public void CreatePushPopBoard()
    {
        pushObject = Instantiate(boardPrefab);
        pushObject.GetComponent<SpriteRenderer>().sprite = boardSprite;
        pushObject.AddComponent<PolygonCollider2D>(); // Polygon Collider Setting
        boardCollider = pushObject.GetComponent<PolygonCollider2D>();

        // board Size Setting
        boardSize = new Vector3(boardCollider.bounds.size.x, boardCollider.bounds.size.y, 1f); // collider size
        grid.x = (int)(boardSize.x / percentage);
        grid.y = (int)(boardSize.y / percentage);

        // grid pos setting
        for (int row = 0; row <= grid.x; row++)
        {
            for (int col = 0; col <= grid.y; col++)
            {
                float posX = -boardSize.x / grid.x * row;
                float posY = -boardSize.y / grid.y * col;

                pos.Add(Instantiate(posPrefab, pushObject.transform));
                pos[pos.Count - 1].transform.position = boardPrefab.transform.position + new Vector3(boardSize.x / 2, boardSize.y / 2, 0f) + new Vector3(posX, posY, 1f);
                pos[pos.Count - 1].GetComponent<PushPopCheck>().PointContains();
            }
        }
    }

    // PushPop Button Setting
    public void CreateButton()
    {
        Debug.Log(activePos.Count);
        for (int i = 0; i < activePos.Count; i++)
        {
            posButton.Add(Instantiate(pushPopButton, pushPopCanvas.gameObject.transform));
            posButton[i].GetComponent<RectTransform>().sizeDelta = buttonSize;
            posButton[i].transform.position = Camera.main.WorldToScreenPoint(activePos[i].transform.position);
        }
    }

    // Push Pop Object Pooling
    private void SettingPushPop()
    {

    }

    // Push Pop Button click method
    public void PushPopClick()
    {
        GameObject clickButton = EventSystem.current.currentSelectedGameObject;
        clickButton.SetActive(false);
        activePos.Remove(clickButton);
    }

    // Game Clear 시 호출되는 method
    public void PushPopClear()
    {
        for (int i = 0; i < activePos.Count; i++)
        {
            activePos[i].SetActive(false);
        }
        activePos.Clear();
    }
}
