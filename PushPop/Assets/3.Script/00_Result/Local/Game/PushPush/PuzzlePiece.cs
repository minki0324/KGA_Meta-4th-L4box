using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour
{
    private bool isGround = false;
    [SerializeField] private float speed = 5f;
    public PuzzleObject puzzle;

    #region Unity Callback
    #endregion

    #region Other Method

    public IEnumerator PuzzleMove_Co()
    {
        while(!isGround)
        {
            transform.Translate(Vector2.down * (Time.deltaTime * speed * 100f));

             if (0f + (puzzle.puzzleArea.y / 2f) > transform.position.y)
            { // boundary bottom
                isGround = true;
            }

            yield return null;

        }

        transform.position = new Vector2(transform.position.x, (puzzle.puzzleArea.y / 2f) + 10f);
        
        if(GameManager.Instance.bubbleObject.Count == 0)
        {
            PuzzleLozic puzzle = FindObjectOfType<PuzzleLozic>();
            puzzle.SettingGame();
        }
    }

    public void OnBubbleDestroy()
    {
        StartCoroutine(PuzzleMove_Co());

        
    }
    #endregion

}
