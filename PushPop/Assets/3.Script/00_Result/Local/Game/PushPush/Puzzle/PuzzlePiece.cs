using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour
{ // puzzle object prefabs
    public PuzzleObject Puzzle;
    private bool isGround = false;
    private float speed = 5f;
    private float centerPos = 0f;

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void BubbleOnDestroy()
    {
        StartCoroutine(PuzzleMove_Co());
    }

    private IEnumerator PuzzleMove_Co()
    { // bubble ������ �� ����Ǵ� Method
        while (!isGround)
        {
            centerPos = transform.position.y + Puzzle.puzzleCenter.y; // puzzle transform position update
            transform.Translate(Vector2.down * (Time.deltaTime * speed * 200f));

            // screen bottom line
            if (0f + Puzzle.puzzleArea.y / 2 + 10f > centerPos)
            {
                isGround = true;
            }

            yield return null;
        }

        if (Puzzle.puzzleCenter.y < 0)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - Puzzle.puzzleCenter.y + 20f);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 10f);
        }

        GameManager.Instance.LiveBubbleCount--;
        if (GameManager.Instance.LiveBubbleCount.Equals(0))
        { // ��� ��￡�� ���� ������� ��� ���� ����������
            GameManager.Instance.NextMode?.Invoke(); // puzzle mode�� �Ѿ
        }
    }
}
