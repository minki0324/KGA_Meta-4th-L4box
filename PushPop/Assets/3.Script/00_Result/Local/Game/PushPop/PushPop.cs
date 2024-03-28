using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PushPop : MonoBehaviour
{
    public static PushPop Instance = null;
    public GameObject PopParent = null; // 민기 추가 Bomb모드에서 필요

    [Header("PushPop Canvas")]
    public GameObject PushPopButtonPrefab = null; // PushPop Button Prefab
    public GameObject BoardPrefabUI = null; // PushPop Board Canvas Prefab
    [HideInInspector] public Vector2 BoardSize = Vector2.zero;
    [HideInInspector] public Vector2 BoardPos = Vector2.zero;
    [HideInInspector] public List<GameObject> PushPopBoardUIObject = new List<GameObject>(); // mode에 따라 개수 달라짐, pushPopBoard UI상 GameObject List
    private RectTransform boardSizeUI;

    [Header("PushPush")]
    public PushPushManager PushpushManager = null;
    public Stack<GameObject> StackPops = new Stack<GameObject>(); // UI상 보이는 버튼담는 스택
    public Stack<GameObject> StackFakePops = new Stack<GameObject>(); // OverLap 검사를 하기위한 gameObject 스택

    [Header("Multi")]
    [HideInInspector] public List<GameObject> popButtonList1P = new List<GameObject>(); // 1P 의 Pushpop button list
    [HideInInspector] public List<GameObject> popButtonList2P = new List<GameObject>(); // 2P 의 Pushpop List

    [Header("PushPop GameObject")]
    [SerializeField] private GameObject boardPrefab = null; // PushPop Prefab
    [HideInInspector] public Sprite BoardSprite = null;
    private Vector3 boardObjectSize;
    private PolygonCollider2D boardCollider;
    [HideInInspector] public List<GameObject> PushPopBoardObject = new List<GameObject>(); // pushPopBoard의 GameObject List

    [Header("Grid Setting")]
    private Vector2 grid = Vector2.zero;
    [HideInInspector] public float Percentage = 0; // gameobject에 따른 gird 비율
    [HideInInspector] public Vector2 ButtonSize = Vector2.zero; // x, y 동일
    [SerializeField] private GameObject posPrefab = null; // grid에 지정할 pos prefab
    private List<GameObject> pos = new List<GameObject>(); // grid 배치된 posPrefab

    [HideInInspector] public List<GameObject> pushPopButton = new List<GameObject>(); // object pooling 때문에 필요
    [HideInInspector] public List<GameObject> activePos = new List<GameObject>();
    [HideInInspector] public int ActivePosCount = 0;

    [SerializeField] private Sprite[] pushPopBtnSprites;
    private GameObject pushPopAni = null;
    [HideInInspector] public bool Turning = false;
    [HideInInspector] public int PushCount = 0; // only pushpush mode, 버튼눌렀을때 + 되는 카운트, 버튼리스트.Count 비교해서 동일시 클리어 판정(GameManager)

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
        ActivePosCount = 0;
        if (PushPopBoardObject.Count > 0)
        {
            for (int i = 0; i < PushPopBoardObject.Count; i++)
            {
                Destroy(PushPopBoardObject[i]);
            }
            PushPopBoardObject.Clear();
        }

        if (activePos.Count > 0)
        {
            for (int i = 0; i < activePos.Count; i++)
            {
                Destroy(activePos[i]);
            }
            activePos.Clear();
        }

        if (pushPopButton.Count > 0)
        {
            for (int i = 0; i < pushPopButton.Count; i++)
            {
                Destroy(pushPopButton[i]);
            }
            pushPopButton.Clear();
        }
        
        if (PushPopBoardUIObject.Count > 0)
        {
            for (int i = 0; i < PushPopBoardUIObject.Count; i++)
            {
                Destroy(PushPopBoardUIObject[i]);
            }
            PushPopBoardUIObject.Clear();
        }

        if (popButtonList1P.Count > 0)
        {
            for (int i = 0; i < popButtonList1P.Count; i++)
            {
                Destroy(popButtonList1P[i]);
            }
            popButtonList1P.Clear();
        }
       
        if (popButtonList2P.Count > 0)
        {
            for (int i = 0; i < popButtonList2P.Count; i++)
            {
                Destroy(popButtonList2P[i]);
            }
            popButtonList2P.Clear();
        }
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
                GetPushPopPos(pos, posPrefab, _pushPopBoardObject.transform, posX, posY, y);
            }
        }
    }

    // PushPop Button Setting
    public void PushPopButtonSetting(Transform parent)
    {
        PushPopCheck pos = activePos[0].GetComponent<PushPopCheck>();
        int index = pos.SpriteIndex;

        for (int i = 0; i < activePos.Count; i++)
        {
            GameObject pushpop = GetPushPopButton(pushPopButton, PushPopButtonPrefab, parent);
            pushpop.GetComponent<RectTransform>().sizeDelta = ButtonSize;
            pushpop.GetComponent<Image>().sprite = pushPopBtnSprites[activePos[i].GetComponent<PushPopCheck>().SpriteIndex - index];
            pushpop.transform.position = Camera.main.WorldToScreenPoint(activePos[i].transform.position);
        }

        ActivePosCount = activePos.Count;
    }
    #region ObjectPooling
    // PushPop Button Object Pooling
    private GameObject GetPushPopButton(List<GameObject> _pos, GameObject _prefab, Transform _parent)
    {
        GameObject newPos = Instantiate(_prefab, _parent); // Button이 더 필요하다면 새로 생성
        _pos.Add(newPos);
        return newPos;
    }

    private GameObject GetPushPopPos(List<GameObject> _pos, GameObject _prefab, Transform _parent, float _posX, float _posY, int _spriteIndex)
    {
        _parent = this.gameObject.transform;

        GameObject newPos = Instantiate(_prefab, _parent); // Button이 더 필요하다면 새로 생성
        _pos.Add(newPos);
        _pos[_pos.Count - 1].transform.position = boardPrefab.transform.position + new Vector3(boardObjectSize.x / 2, boardObjectSize.y / 2, 0f) + new Vector3(_posX, _posY, 1f); // grid 배치
        _pos[_pos.Count - 1].GetComponent<PushPopCheck>().PointContains(); // collider chekc
        _pos[_pos.Count - 1].GetComponent<PushPopCheck>().SpriteIndex = _spriteIndex;
        return newPos;
    }
    #endregion
}
