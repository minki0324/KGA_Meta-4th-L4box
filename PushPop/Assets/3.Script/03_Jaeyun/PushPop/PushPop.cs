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
    public GameObject boardPrefabUI = null; // PushPop Board Canvas Prefab
    private RectTransform boardSizeUI;
    public List<GameObject> pushPopBoardUIObject = new List<GameObject>(); // mode에 따라 개수 달라짐, pushPopBoard UI상 GameObject List

    [Header("PushPop GameObject")]
    [SerializeField] private SpriteAtlas pushPopSpriteAtlas; // pushPop Atlas 참조
    private int spriteName; // stage에 따라 달라짐
    public GameObject boardPrefab = null; // PushPop Prefab
    public Sprite boardSprite = null;
    private Vector3 boardSize;
    private PolygonCollider2D boardCollider;
    public List<GameObject> pushPopBoardObject = new List<GameObject>(); // pushPopBoard의 GameObject List

    [Header("Grid Setting")]
    public Transform buttonCanvas = null;
    private Vector2 grid = Vector2.zero;
    public float percentage = 0; // gameobject에 따른 gird 비율
    public Vector2 buttonSize = Vector2.zero; // x, y 동일
    public GameObject PosPrefab = null; // grid에 지정할 pos prefab
    private List<GameObject> pos = new List<GameObject>(); // grid 배치된 posPrefab

    public List<GameObject> pushPopButton = new List<GameObject>();
    public List<GameObject> activePos = new List<GameObject>();

    public GameObject pushPopAni = null;
    public bool pushTurn = true;

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

    /*// PushPop Game Start
    public void CreatePushPop(GameObject _pushPopBoardObject)
    {
        CreatePushPopBoard();
        CreateGrid(_pushPopBoardObject); // pushPop board마다 필요, CreatePushPop처럼 한 번에 묶지 말고 따로 실행해야 할듯
        PushPopButtonSetting();
    }*/

    // SpriteAtlas에서 Sprite 갖고오기
    private Sprite SpriteAtlas(string _spriteName)
    {
        _spriteName = $"{_spriteName}(Clone)";
        Sprite[] sprites = new Sprite[pushPopSpriteAtlas.spriteCount];
        pushPopSpriteAtlas.GetSprites(sprites);

        foreach (Sprite sprite in sprites)
        {
            if (sprite.name.Equals(_spriteName))
            {
                return sprite;
            }
        }
        return null;
    }

    // Sprite 모양에 따른 Polygon collider setting
    public void CreatePushPopBoard(Transform parent)
    { // bomb mode일 때는 2회 호출
        // sprite atlas setting
        // spriteName = GameManager.Instance.PushPopStage;
        // boardSprite = SpriteAtlas(spriteName.ToString());

        if (boardSprite == null) return;

        // canvas setting
        GameObject pushPopBoard = Instantiate(boardPrefabUI, parent);
        pushPopBoard.GetComponent<Image>().sprite = boardSprite;
        PopParent = pushPopBoard;
        pushPopAni = pushPopBoard;

        // board size setting
        boardSizeUI = pushPopBoard.GetComponent<RectTransform>();
        boardSizeUI.sizeDelta = GameManager.Instance.BoardSize;
        pushPopBoardUIObject.Add(pushPopBoard);

        // gameObject setting
        GameObject pushObject = Instantiate(boardPrefab);
        pushObject.GetComponent<SpriteRenderer>().sprite = boardSprite;
        // size setting
        Rect boardRect = pushPopBoard.GetComponent<RectTransform>().rect;
        // Vector2 boardSize = GameManager.Instance.BoardSizeGameObject;
        // float scale = Mathf.Min(boardSize.x / boardSprite.textureRect.size.x, boardSize.x / boardSprite.textureRect.size.y) * 0.95f;
        float scale = Mathf.Min(boardRect.width / boardSprite.textureRect.size.x, boardRect.width / boardSprite.textureRect.size.y) * 0.95f;
        pushObject.transform.localScale = new Vector3(scale, scale, 1f);
        if (!pushTurn)
        { // image flip
            boardSizeUI.localScale = new Vector3(-1, 1, 1);
            pushObject.transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
        // polygon collider setting
        pushObject.AddComponent<PolygonCollider2D>();
        boardCollider = pushObject.GetComponent<PolygonCollider2D>();
        pushPopBoardObject.Add(pushObject);
    }

    // pushpop button 생성할 grid
    public void CreateGrid(GameObject _pushPopBoardObject)
    {
        // board Size Setting
        boardSize = new Vector3(boardCollider.bounds.size.x, boardCollider.bounds.size.y, 1f); // collider size
        grid.x = (int)(boardSize.x / percentage);
        grid.y = (int)(boardSize.y / percentage);

        for (int y = 0; y <= grid.y; y++)
        {
            for (int x = 0; x <= grid.x; x++)
            {
                float posX = -boardSize.x / grid.x * x;
                float posY = -boardSize.y / grid.y * y;
                GetPushPopButton(pos, PosPrefab, _pushPopBoardObject.transform, posX, posY);
            }
        }
    }

    // PushPop Button Setting
    public void PushPopButtonSetting(Transform parent)
    {
        for (int i = 0; i < activePos.Count; i++)
        {
            GetPushPopButton(pushPopButton, pushPopButtonPrefab, parent);
            pushPopButton[i].GetComponent<RectTransform>().sizeDelta = buttonSize;
            pushPopButton[i].transform.position = Camera.main.WorldToScreenPoint(activePos[i].transform.position);
        }
    }
    #region ObjectPooling
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
        _parent = this.gameObject.transform;

        for (int i = 0; i < _pos.Count; i++)
        {
            if (!_pos[i].activeSelf) // 기존 button이 활성화 되어있지 않다면 true
            {
                _pos[i].SetActive(true);
                _pos[i].transform.position = boardPrefab.transform.position + new Vector3(boardSize.x / 2, boardSize.y / 2, 0f) + new Vector3(_posX, _posY, 1f); // grid 배치
                _pos[i].GetComponent<PushPopCheck>().PointContains(); // collider check
                return;
            }
        }

        GameObject newPos = Instantiate(_prefab, _parent); // Button이 더 필요하다면 새로 생성
        _pos.Add(newPos);
        _pos[_pos.Count - 1].transform.position = boardPrefab.transform.position + new Vector3(boardSize.x / 2, boardSize.y / 2, 0f) + new Vector3(_posX, _posY, 1f); // grid 배치
        _pos[_pos.Count - 1].GetComponent<PushPopCheck>().PointContains(); // collider chekc
        return;
    }
    #endregion
    // Game Clear 시 호출되는 method
    public void PushPopClear()
    {
        // gameObject claer
        for (int i = 0; i < pushPopBoardObject.Count; i++)
        {
            Destroy(pushPopBoardObject[i]);
        }
        pushPopBoardObject.Clear();

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
        for (int i = 0; i < pushPopBoardUIObject.Count; i++)
        {
            Destroy(pushPopBoardUIObject[i]);
        }
        pushPopBoardUIObject.Clear();
    }
}
