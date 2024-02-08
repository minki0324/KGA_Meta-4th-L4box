using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class PushPopTest : MonoBehaviour
{
    [Header("PushPop Canvas")]
    [SerializeField] private Transform pushPopCanvas;
    [SerializeField] private GameObject pushPopButtonPrefab;

    [Header("PushPop Board")]
    [SerializeField] private GameObject boardPrefab = null; // board Prefab
    [SerializeField] private Sprite boardSprite = null; // board sprite, custom sprite out line setting 필요 
    private Vector3 boardSize = Vector3.zero;
    private PolygonCollider2D boardCollider; // board collider
    private GameObject pushObject = null; // instantiate object

    [Header("Grid Size")]
    private Vector2 grid = Vector2.zero;
    [SerializeField] private float percentage = 0; // gameobject에 따른 gird 비율
    [SerializeField] private Vector2 buttonSize = Vector2.zero; // x, y 동일

    [Header("Grid Pos")]
    public GameObject posPrefab = null; // grid에 지정할 pos prefab
    private List<GameObject> pos = new List<GameObject>();

    private List<GameObject> pushPopButton = new List<GameObject>();
    public List<GameObject> activePos = new List<GameObject>();

    public void Start()
    {
        CreatePushPopBoard();
        CreateGrid();
        PushPopButtonSetting();
    }

    // Sprite 모양에 따른 Polygon collider setting
    public void CreatePushPopBoard()
    {
        pushObject = Instantiate(boardPrefab);
        pushObject.GetComponent<SpriteRenderer>().sprite = boardSprite;
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
    public void PushPopButtonSetting()
    {
        Debug.Log(activePos.Count);
        for (int i = 0; i < activePos.Count; i++)
        {
            GetPushPopButton(pushPopButton, pushPopButtonPrefab, pushPopCanvas);
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

    // Grid Position이 PushPop Board에 전부 포함되는지 확인하는 Method
    public void PointContains()
    {
        int contains = 0;
        Transform[] point = new Transform[4];

        for (int i = 0; i < pos.Count; i++)
        {
            for (int j = 0; j < point.Length; j++)
            {
                point[j] = pos[i].transform.GetChild(j).transform;

                Collider2D collider = Physics2D.OverlapPoint(point[j].position); // position check
                if (collider == null) continue;
                if (collider.CompareTag("PushPop"))
                {
                    contains++;
                }
            }
        }

        if (!contains.Equals(4))
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            activePos.Add(this.gameObject); // active pos add
        }
    }

    public void DestroyObject()
    { // test
        DestroyImmediate(pushObject);
    }
}
