using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PushPop : MonoBehaviour
{
    public static PushPop Instance = null;
    public GameObject PopParent = null; // 민기 추가 Bomb모드에서 필요

    [Header("PushPop Canvas")]
    public Transform pushPopCanvas = null; // Canvas_PushPop
    public GameObject pushPopButtonPrefab = null; // PushPop Button Prefab
    public GameObject BoardPrefabUI = null; // PushPop Board Canvas Prefab
    private RectTransform boardSizeUI;
    public Vector2 BoardSize = Vector2.zero;
    public Vector2 BoardPos = Vector2.zero;
    public List<GameObject> PushPopBoardUIObject = new List<GameObject>(); // mode에 따라 개수 달라짐, pushPopBoard UI상 GameObject List

    [Header("PushPush")]
    public Stack<GameObject> StackPops = new Stack<GameObject>(); // UI상 보이는 버튼담는 스택
    public Stack<GameObject> StackFakePops = new Stack<GameObject>(); // OverLap 검사를 하기위한 gameObject 스택

    [Header("PushPop GameObject")]
    [SerializeField] private SpriteAtlas pushPopSpriteAtlas; // pushPop Atlas 참조
    public GameObject boardPrefab = null; // PushPop Prefab
    public Sprite BoardSprite = null;
    private Vector3 boardObjectSize;
    private PolygonCollider2D boardCollider;
    public List<GameObject> PushPopBoardObject = new List<GameObject>(); // pushPopBoard의 GameObject List

    [Header("Grid Setting")]
    public Transform buttonCanvas = null;
    private Vector2 grid = Vector2.zero;
    public float Percentage = 0; // gameobject에 따른 gird 비율
    public Vector2 ButtonSize = Vector2.zero; // x, y 동일
    public GameObject PosPrefab = null; // grid에 지정할 pos prefab
    private List<GameObject> pos = new List<GameObject>(); // grid 배치된 posPrefab

    public List<GameObject> pushPopButton = new List<GameObject>();
    public List<GameObject> activePos = new List<GameObject>();
    public int ActivePosCount = 0;

    public Sprite[] pushPopBtnSprites;
    public GameObject pushPopAni = null;
    public bool Turning = false;
    public int PushCount = 0; // only pushpush mode, 버튼눌렀을때 + 되는 카운트, 버튼리스트.Count 비교해서 동일시 클리어 판정(GameManager)

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void Init()
    {
        // gameObject claer
        for (int i = 0; i < PushPopBoardObject.Count; i++)
        {
            Destroy(PushPopBoardObject[i]);
        }
        PushPopBoardObject.Clear();

        if (!activePos.Count.Equals(0))
        {
            for (int i = 0; i < activePos.Count; i++)
            {
                activePos[i].SetActive(false);
            }
            activePos.Clear();
        }

        for (int i = 0; i < pushPopButton.Count; i++)
        {
            pushPopButton[i].SetActive(false);
        }

        // canvas clear
        for (int i = 0; i < PushPopBoardUIObject.Count; i++)
        {
            Destroy(PushPopBoardUIObject[i]);
        }
        PushPopBoardUIObject.Clear();
    }

    // Sprite 모양에 따른 Polygon collider setting
    public void CreatePushPopBoard(Transform parent)
    { // bomb mode일 때는 2회 호출
        if (BoardSprite == null) return;

        // canvas setting
        GameObject pushPopBoard = Instantiate(BoardPrefabUI, parent);
        pushPopBoard.GetComponent<Image>().sprite = BoardSprite;
        PopParent = pushPopBoard;
        pushPopAni = pushPopBoard;

        // board size setting
        boardSizeUI = pushPopBoard.GetComponent<RectTransform>();
        boardSizeUI.sizeDelta = BoardSize;
        PushPopBoardUIObject.Add(pushPopBoard);
        PushPopBoardUIObject[0].transform.localPosition = BoardPos;

        // gameObject setting
        GameObject pushObject = Instantiate(boardPrefab);
        pushObject.GetComponent<SpriteRenderer>().sprite = BoardSprite;

        // size setting
        Rect boardRect = pushPopBoard.GetComponent<RectTransform>().rect;
        float scale = Mathf.Min(boardRect.width / BoardSprite.textureRect.size.x, boardRect.width / BoardSprite.textureRect.size.y) * 0.95f;
        pushObject.transform.localScale = new Vector3(scale, scale, 1f);
        if (Turning)
        { // image flip
            boardSizeUI.localScale = new Vector3(-1, 1, 1);
            pushObject.transform.rotation = Quaternion.Euler(0, 180f, 0);
        }

        // polygon collider setting
        pushObject.AddComponent<PolygonCollider2D>();
        boardCollider = pushObject.GetComponent<PolygonCollider2D>();
        PushPopBoardObject.Add(pushObject);
    }

    // pushpop button 생성할 grid
    public void CreateGrid(GameObject _pushPopBoardObject)
    {
        // board Size Setting
        boardObjectSize = new Vector3(boardCollider.bounds.size.x, boardCollider.bounds.size.y, 1f); // collider size
        grid.x = (int)(boardObjectSize.x / Percentage);
        grid.y = (int)(boardObjectSize.y / Percentage);

        for (int y = 0; y <= grid.y; y++)
        {
            for (int x = 0; x <= grid.x; x++)
            {
                float posX = -boardObjectSize.x / grid.x * x;
                float posY = -boardObjectSize.y / grid.y * y;
                GetPushPopPos(pos, PosPrefab, _pushPopBoardObject.transform, posX, posY, y);
            }
        }
    }

    // PushPop Button Setting
    public void PushPopButtonSetting(Transform parent)
    {
        PushPopCheck pos = activePos[0].GetComponent<PushPopCheck>();
        int index = pos.spriteIndex;

        for (int i = 0; i < activePos.Count; i++)
        {
            GameObject pushpop = GetPushPopButton(pushPopButton, pushPopButtonPrefab, parent);
            pushpop.GetComponent<RectTransform>().sizeDelta = ButtonSize;
            pushpop.GetComponent<Image>().sprite = pushPopBtnSprites[activePos[i].GetComponent<PushPopCheck>().spriteIndex-index];
            pushpop.transform.position = Camera.main.WorldToScreenPoint(activePos[i].transform.position);
        }

        ActivePosCount = activePos.Count;
    }
    #region ObjectPooling
    // PushPop Button Object Pooling
    private GameObject GetPushPopButton(List<GameObject> _pos, GameObject _prefab, Transform _parent)
    {
        for (int i = 0; i < _pos.Count; i++)
        {
            if (!_pos[i].activeSelf) // 기존 button이 활성화 되어있지 않다면 true
            {
                _pos[i].SetActive(true);
                return _pos[i];
            }
        }

        GameObject newPos = Instantiate(_prefab, _parent); // Button이 더 필요하다면 새로 생성
        _pos.Add(newPos);
        return newPos;
    }

    // PushPop position Object Pooling
    private GameObject GetPushPopPos(List<GameObject> _pos, GameObject _prefab, Transform _parent, float _posX, float _posY, int _spriteIndex)
    {
        _parent = this.gameObject.transform;

        for (int i = 0; i < _pos.Count; i++)
        {
            if (!_pos[i].activeSelf) // 기존 button이 활성화 되어있지 않다면 true
            {
                _pos[i].SetActive(true);
                _pos[i].transform.position = boardPrefab.transform.position + new Vector3(boardObjectSize.x / 2, boardObjectSize.y / 2, 0f) + new Vector3(_posX, _posY, 1f); // grid 배치
                _pos[i].GetComponent<PushPopCheck>().PointContains(); // collider check
                _pos[i].GetComponent<PushPopCheck>().spriteIndex = _spriteIndex;
                return _pos[i];
            }
        }

        GameObject newPos = Instantiate(_prefab, _parent); // Button이 더 필요하다면 새로 생성
        _pos.Add(newPos);
        _pos[_pos.Count - 1].transform.position = boardPrefab.transform.position + new Vector3(boardObjectSize.x / 2, boardObjectSize.y / 2, 0f) + new Vector3(_posX, _posY, 1f); // grid 배치
        _pos[_pos.Count - 1].GetComponent<PushPopCheck>().PointContains(); // collider chekc
        _pos[_pos.Count - 1].GetComponent<PushPopCheck>().spriteIndex = _spriteIndex;
        return newPos;
    }
    #endregion
    // Game Clear 시 호출되는 method
    public void PushPopClear()
    {
        // gameObject claer
        for (int i = 0; i < PushPopBoardObject.Count; i++)
        {
            Destroy(PushPopBoardObject[i]);
        }
        PushPopBoardObject.Clear();

        if (!activePos.Count.Equals(0))
        {
            for (int i = 0; i < activePos.Count; i++)
            {
                activePos[i].SetActive(false);
            }
            activePos.Clear();
        }

        for (int i = 0; i < pushPopButton.Count; i++)
        {
            pushPopButton[i].SetActive(false);
        }

        // canvas clear
        for (int i = 0; i < PushPopBoardUIObject.Count; i++)
        {
            Destroy(PushPopBoardUIObject[i]);
        }
        PushPopBoardUIObject.Clear();
    }

    
}
