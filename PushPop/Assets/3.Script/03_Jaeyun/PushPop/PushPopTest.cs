using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

[ExecuteInEditMode]
public class PushPopTest : MonoBehaviour
{
    public static PushPopTest Instance = null;

    private void Awake()
    {
        Instance = this;
    }

    [Header("PushPop Canvas")]
    public Transform pushPopCanvas;
    public GameObject pushPopButtonPrefab;
    public GameObject boardPrefabUI;
    public RectTransform boardSizeUI;
    private GameObject pushPopBoard = null; // instantiate object
    public Transform buttonCanvas;

    [Header("PushPop Board")]
    public GameObject boardPrefab = null; // board Prefab
    private Vector3 boardSize;
    private PolygonCollider2D boardCollider; // board collider
    private GameObject pushObject = null; // instantiate object

    [Header("Sprite Setting")]
    public Sprite boardSprite = null; // board sprite, custom sprite out line setting 필요 
    public SpriteAtlas spriteAtlas = null;

    private Vector2 grid = Vector2.zero;

    [Header("Grid Pos")]
    public GameObject posPrefab = null; // grid에 지정할 pos prefab
    private List<GameObject> pos = new List<GameObject>();
    
    [Header("Setting")]
    public float percentage = 0; // gameobject에 따른 gird 비율
    public Vector2 buttonSize = Vector2.zero; // x, y 동일

    public string spriteName = string.Empty;

    [Header("Result")]
    public int buttonCount = 0;

    private List<GameObject> pushPopButton = new List<GameObject>();
    public List<GameObject> activePos = new List<GameObject>();

    public void PushPop()
    {
        if (spriteName == string.Empty)
        {
            boardSprite = null;
            Debug.Log("Sprite Name을 넣어주세요.");
            return;
        }
        else
        {
            boardSprite = spriteAtlas.GetSprite(spriteName);
            if (boardSprite == null)
            {
                Debug.Log("올바른 파일명을 입력해주세요.");
            }
            else
            {
                PushPopClear();
                CreatePushPopBoard();
                CreateGrid();
                PushPopButtonSetting();
            }
        }
    }

    // Sprite 모양에 따른 Polygon collider setting
    public void CreatePushPopBoard()
    {
        // canvas setting
        pushPopBoard = Instantiate(boardPrefabUI, pushPopCanvas);
        pushPopBoard.GetComponent<Image>().sprite = boardSprite;

        // board size setting
        boardSizeUI = pushPopBoard.GetComponent<RectTransform>();
        boardSizeUI.sizeDelta = new Vector2(700f, 700f); // board Size

        // collider check할 gameObject
        pushObject = Instantiate(boardPrefab);
        SpriteRenderer pushObjectSprite = pushObject.GetComponent<SpriteRenderer>();
        pushObjectSprite.sprite = boardSprite;

        // size setting
        Rect boardRect = pushPopBoard.GetComponent<RectTransform>().rect;
        float scale = Mathf.Min(boardRect.width / boardSprite.textureRect.size.x, boardRect.width / boardSprite.textureRect.size.y) * 0.95f;
        pushObject.transform.localScale = new Vector3(scale, scale, 1f);

        pushObject.AddComponent<PolygonCollider2D>(); // Polygon Collider Setting
        boardCollider = pushObject.GetComponent<PolygonCollider2D>();
    }

    public void CreateGrid()
    {
        // board Size Setting
        boardSize = new Vector3(boardCollider.bounds.size.x, boardCollider.bounds.size.y, 1f); // collider size
        grid.x = (int)(boardSize.x / percentage);
        grid.y = (int)(boardSize.y / percentage);

        // grid pos setting
        for (int y = 0; y <= grid.y; y++)
        {
            for (int x = 0; x <= grid.x; x++)
            {
                float posX = -boardSize.x / grid.x * x;
                float posY = -boardSize.y / grid.y * y;
                GetPushPopButton(pos, posPrefab, transform, posX, posY);
            }
        }

        buttonCount = activePos.Count;
    }

    // PushPop Button Setting
    public void PushPopButtonSetting()
    {
        for (int i = 0; i < activePos.Count; i++)
        {
            GetPushPopButton(pushPopButton, pushPopButtonPrefab, buttonCanvas);
            pushPopButton[i].GetComponent<RectTransform>().sizeDelta = buttonSize;
            pushPopButton[i].transform.position = Camera.main.WorldToScreenPoint(activePos[i].transform.position);
        }
    }

    // PushPop Button Object Pooling
    private void GetPushPopButton(List<GameObject> _pos, GameObject _prefab, Transform _parent)
    {
        for (int i = 0; i < _pos.Count; i++)
        {
            if (!_pos[i].activeSelf) // 기존 button이 활성화 되어있지 않다면 true
            {
                _pos[i].SetActive(true);
                return;
            }
        }

        GameObject newPos = Instantiate(_prefab, _parent); // Button이 더 필요하다면 새로 생성
        _pos.Add(newPos);
        return;
    }

    // PushPop position Object Pooling
    private void GetPushPopButton(List<GameObject> _pos, GameObject _prefab, Transform _parent, float _posX, float _posY)
    {
        GameObject newPos = Instantiate(_prefab, _parent); // Button이 더 필요하다면 새로 생성
        _pos.Add(newPos);
        _pos[_pos.Count - 1].transform.position = boardPrefab.transform.position + new Vector3(boardSize.x / 2, boardSize.y / 2, 0f) + new Vector3(_posX, _posY, 1f); // grid 배치
        _pos[_pos.Count - 1].GetComponent<PushPopCheckEditor>().PointContains(); // collider chekck
        return;
    }

    // PushPop 초기화
    public void PushPopClear()
    {
        if (pushObject == null) return;
        for (int i = 0; i < activePos.Count; i++)
        {
            activePos[i].SetActive(false);
        }
        activePos.Clear();
        for (int i = 0; i < pushPopButton.Count; i++)
        {
            DestroyImmediate(pushPopButton[i]);
        }
        pushPopButton.Clear();
        DestroyImmediate(pushPopBoard);
        DestroyImmediate(pushObject);
    }
}
