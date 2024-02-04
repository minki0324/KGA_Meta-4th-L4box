using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PushPop : MonoBehaviour
{
    [Header("PushPop Canvas")]
    public Canvas pushPopCanvas; // [SerializeField] private
    public GameObject pushPopButton;

    [Header("Board")]
    public GameObject boardObject = null; // Board Prefab
    public Sprite boardSprite = null;
    public Vector3 boardSize = Vector3.zero;

    private GameObject pushObject = null; // instantiate object
    private PolygonCollider2D collider;

    // grid size
    [Header("Grid Size")]
    public Vector2 grid = Vector2.zero;
    public float percentage = 0; // gameobject에 따른 gird 비율

    public float cellSize = 0; // x, y 동일

    [Header("Grid Pos")]
    public GameObject posPrefab = null; // pos object prefab
    List<GameObject> pos;
    List<GameObject> posButton;

    public void CreateGameObject()
    { // Sprite 모양에 따른 Polygon collider setting
        pushObject = Instantiate(boardObject);
        pushObject.GetComponent<SpriteRenderer>().sprite = boardSprite;
        pushObject.AddComponent<PolygonCollider2D>(); // Polygon Collider Setting
        collider = pushObject.GetComponent<PolygonCollider2D>();
    }

    public void SetBoardSize()
    {
        // Size Setting
        boardSize = new Vector3(collider.bounds.size.x, collider.bounds.size.y, 1f); // collider size
        grid.x = (int)(boardSize.x / percentage);
        grid.y = (int)(boardSize.y / percentage);
    }

    public void DrawGrid()
    {
        // Create Grid
        pos = new List<GameObject>();

        for (int row = 0; row <= grid.y; row++)
        {
            for (int col = 0; col <= grid.x; col++)
            {
                // grid pos setting
                float posX = -boardSize.x / grid.x * row;
                float posY = -boardSize.y / grid.y * col;

                pos.Add(Instantiate(posPrefab, pushObject.transform));
                pos[pos.Count - 1].transform.position = boardObject.transform.position + new Vector3(boardSize.x / 2, boardSize.y / 2, 0f) + new Vector3(posX, posY, 1f);
            }
        }
    }

    public void SettingPushPopButton()
    {
        posButton = new List<GameObject>();
        for (int i = 0; i < pos.Count; i++)
        {
            posButton.Add(Instantiate(pushPopButton, pushPopCanvas.gameObject.transform));
            posButton[i].transform.position = Camera.main.WorldToScreenPoint(pos[i].transform.position);
        }
    }

    public void DestroyObject()
    {
        DestroyImmediate(pushObject);
    }
}
