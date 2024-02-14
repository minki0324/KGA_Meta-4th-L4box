using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.U2D;

public class PushPop : MonoBehaviour
{
    public static PushPop instance = null;

    [Header("PushPop Canvas")]
    [SerializeField] private Transform pushPopCanvas = null;
    [SerializeField] private GameObject pushPopButtonPrefab = null;
    [SerializeField] private GameObject pushPopBoardPrefab;
    private List<GameObject> pushPopBoardObject = new List<GameObject>();

    [Header("PushPop Board")]
    [SerializeField] private SpriteAtlas spriteAtlas;
    [SerializeField] private string spriteName = string.Empty;
    [SerializeField] private GameObject boardPrefab = null; // board Prefab
    [SerializeField] private Sprite boardSprite = null; // board sprite, custom sprite out line setting 필요 
    private Vector3 boardSize = Vector3.zero;
    private PolygonCollider2D boardCollider; // board collider
    public GameObject pushObject = null; // instantiate object

    [Header("Grid Size")]
    private Vector2 grid = Vector2.zero;
    [SerializeField] private float percentage = 0; // gameobject에 따른 gird 비율
    [SerializeField] private Vector2 buttonSize = Vector2.zero; // x, y 동일

    [Header("Grid Pos")]
    public GameObject posPrefab = null; // grid에 지정할 pos prefab
    private List<GameObject> pos = new List<GameObject>(); // grid 배치된 posPrefab

    public List<GameObject> pushPopButton = new List<GameObject>();
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

    // PushPop Game Start
    public void CreatePushPop()
    {
        CreatePushPopBoard();
        CreateGrid();
        PushPopButtonSetting();
    }

    // SpriteAtlas에서 Sprite 갖고오기
    private Sprite SpriteAtlas(string _spriteName)
    {
        _spriteName += "(Clone)";
        Sprite[] sprites = new Sprite[spriteAtlas.spriteCount];
        spriteAtlas.GetSprites(sprites);

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
    private void CreatePushPopBoard()
    { // bomb mode일 때는 2회 호출
        // sprite atlas
        boardSprite = SpriteAtlas(spriteName);

        // canvas
        GameObject pushPopBoard = Instantiate(pushPopBoardPrefab, pushPopCanvas);
        pushPopBoard.GetComponent<Image>().sprite = boardSprite;
        Rect boardRect = pushPopBoardPrefab.GetComponent<RectTransform>().rect;
        float scale = Mathf.Min(boardRect.width / boardSprite.textureRect.size.x, boardRect.width / boardSprite.textureRect.size.y) * 0.95f;
        pushPopBoardObject.Add(pushPopBoard);

        // gameObject
        pushObject = Instantiate(boardPrefab);
        pushObject.GetComponent<SpriteRenderer>().sprite = boardSprite;
        pushObject.transform.localScale = new Vector3(scale, scale, 1f);
        pushObject.AddComponent<PolygonCollider2D>(); // Polygon Collider Setting
        boardCollider = pushObject.GetComponent<PolygonCollider2D>();
    }

    private void CreateGrid()
    {
        // board Size Setting
        boardSize = new Vector3(boardCollider.bounds.size.x, boardCollider.bounds.size.y, 1f); // collider size
        grid.x = (int)(boardSize.x / percentage);
        grid.y = (int)(boardSize.y / percentage);

        // grid pos setting
        for (int col = 0; col <= grid.x; col++)
        {
            for (int row = 0; row <= grid.y; row++)
            {
                float posX = -boardSize.x / grid.x * col;
                float posY = -boardSize.y / grid.y * row;
                GetPushPopButton(pos, posPrefab, pushObject.transform, posX, posY);
            }
        }
    }

    // PushPop Button Setting
    private void PushPopButtonSetting()
    {
        for (int i = 0; i < activePos.Count; i++)
        {
            GetPushPopButton(pushPopButton, pushPopButtonPrefab, pushPopCanvas);
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
        for (int i = 0; i < activePos.Count; i++)
        {
            activePos[i].SetActive(false);
        }
        activePos.Clear();
        if (pushObject != null)
        {
            pushObject.SetActive(false);
        }

        // canvas clear
        for (int i = 0; i < pushPopBoardObject.Count; i++)
        {
            Destroy(pushPopBoardObject[i]);
        }
        pushPopBoardObject.Clear();
    }

    public void DestroyBoard()
    {
        for (int i = 0; i < pushPopButton.Count; i++)
        {
            pushPopButton[i].SetActive(false);
        }
        Destroy(pushObject);
    }
}
