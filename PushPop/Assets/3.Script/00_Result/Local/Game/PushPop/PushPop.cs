using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PushPop : MonoBehaviour
{
    public static PushPop Instance = null;
    public GameObject PopParent = null; // �α� �߰� Bomb��忡�� �ʿ�

    [Header("PushPop Canvas")]
    public GameObject PushPopButtonPrefab = null; // PushPop Button Prefab
    public GameObject BoardPrefabUI = null; // PushPop Board Canvas Prefab
    [HideInInspector] public Vector2 BoardSize = Vector2.zero;
    [HideInInspector] public Vector2 BoardPos = Vector2.zero;
    [HideInInspector] public List<GameObject> PushPopBoardUIObject = new List<GameObject>(); // mode�� ���� ���� �޶���, pushPopBoard UI�� GameObject List
    private RectTransform boardSizeUI;

    [Header("PushPush")]
    public PushPushManager PushpushManager = null;
    public Stack<GameObject> StackPops = new Stack<GameObject>(); // UI�� ���̴� ��ư��� ����
    public Stack<GameObject> StackFakePops = new Stack<GameObject>(); // OverLap �˻縦 �ϱ����� gameObject ����

    [Header("Multi")]
    [HideInInspector] public List<GameObject> popButtonList1P = new List<GameObject>(); // 1P �� Pushpop button list
    [HideInInspector] public List<GameObject> popButtonList2P = new List<GameObject>(); // 2P �� Pushpop List

    [Header("PushPop GameObject")]
    [SerializeField] private GameObject boardPrefab = null; // PushPop Prefab
    [HideInInspector] public Sprite BoardSprite = null;
    private Vector3 boardObjectSize;
    private PolygonCollider2D boardCollider;
    [HideInInspector] public List<GameObject> PushPopBoardObject = new List<GameObject>(); // pushPopBoard�� GameObject List

    [Header("Grid Setting")]
    private Vector2 grid = Vector2.zero;
    [HideInInspector] public float Percentage = 0; // gameobject�� ���� gird ����
    [HideInInspector] public Vector2 ButtonSize = Vector2.zero; // x, y ����
    [SerializeField] private GameObject posPrefab = null; // grid�� ������ pos prefab
    private List<GameObject> pos = new List<GameObject>(); // grid ��ġ�� posPrefab

    [HideInInspector] public List<GameObject> pushPopButton = new List<GameObject>(); // object pooling ������ �ʿ�
    [HideInInspector] public List<GameObject> activePos = new List<GameObject>();
    [HideInInspector] public int ActivePosCount = 0;

    [SerializeField] private Sprite[] pushPopBtnSprites;
    private GameObject pushPopAni = null;
    [HideInInspector] public bool Turning = false;
    [HideInInspector] public int PushCount = 0; // only pushpush mode, ��ư�������� + �Ǵ� ī��Ʈ, ��ư����Ʈ.Count ���ؼ� ���Ͻ� Ŭ���� ����(GameManager)

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

    // Sprite ��翡 ���� Polygon collider setting
    public void CreatePushPopBoard(Transform parent)
    { // bomb mode�� ���� 2ȸ ȣ��
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

    // pushpop button ������ grid
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
        GameObject newPos = Instantiate(_prefab, _parent); // Button�� �� �ʿ��ϴٸ� ���� ����
        _pos.Add(newPos);
        return newPos;
    }

    private GameObject GetPushPopPos(List<GameObject> _pos, GameObject _prefab, Transform _parent, float _posX, float _posY, int _spriteIndex)
    {
        _parent = this.gameObject.transform;

        GameObject newPos = Instantiate(_prefab, _parent); // Button�� �� �ʿ��ϴٸ� ���� ����
        _pos.Add(newPos);
        _pos[_pos.Count - 1].transform.position = boardPrefab.transform.position + new Vector3(boardObjectSize.x / 2, boardObjectSize.y / 2, 0f) + new Vector3(_posX, _posY, 1f); // grid ��ġ
        _pos[_pos.Count - 1].GetComponent<PushPopCheck>().PointContains(); // collider chekc
        _pos[_pos.Count - 1].GetComponent<PushPopCheck>().SpriteIndex = _spriteIndex;
        return newPos;
    }
    #endregion
}
