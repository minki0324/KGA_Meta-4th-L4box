using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class PushPop : MonoBehaviour
{
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
    public float spacing = 0; // Button 간 거리

    [Header("Grid Pos")]
    public GameObject posPrefab = null; // pos object prefab

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
        boardSize = new Vector3(boardSprite.bounds.size.x, boardSprite.bounds.size.y, 1f); // sprite size
        grid.x = (int)(boardSize.x / percentage);
        grid.y = (int)(boardSize.y / percentage);
    }

    public void DrawGrid()
    {
        // Create Grid
        Debug.Log("Draw Grid");
        List<GameObject> pos = new List<GameObject>();

        for (int row = 0; row <= grid.y; row++)
        {
            for (int col = 0; col <= grid.x; col++)
            {
                // grid pos setting
                float posX = -boardSize.x / 2 + row + spacing;
                float posY = -boardSize.y / 2 + col + spacing;

                pos.Add(Instantiate(posPrefab, pushObject.transform));
                pos[pos.Count - 1].transform.position = gameObject.transform.position + new Vector3(posX, posY, 1f);
                if (col.Equals(boardSize.x)) break;
            }
            if (row.Equals(boardSize.y)) break;
        }
    }
}
