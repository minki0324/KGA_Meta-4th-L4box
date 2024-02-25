using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour
{
    public bool isGround = false;
    [SerializeField] private float speed = 5f;
    public PuzzleObject puzzle;
    private Sprite puzzleSprite;
    private float centerPos;
    public Coroutine puzzleMove;

    private void OnDisable()
    {
        if (puzzleMove != null)
        {
            StopCoroutine(puzzleMove);
        }
    }

    public IEnumerator PuzzleMove_Co()
    { // bubble 터졌을 때 실행되는 Method
        while (!isGround)
        {
            centerPos = transform.position.y + puzzle.puzzleCenter.y; // puzzle transform position update
            transform.Translate(Vector2.down * (Time.deltaTime * speed * 200f));

            // screen bottom line
            if (0f + puzzle.puzzleArea.y / 2 + 10f > centerPos)
            {
                isGround = true;
            }

            yield return null;
        }

        if (puzzle.puzzleCenter.y < 0)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - puzzle.puzzleCenter.y + 20f);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 10f);
        }

        if (GameManager.Instance.bubbleObject.Count == 0 && isGround)
        {
            PuzzleLozic puzzle = FindObjectOfType<PuzzleLozic>();
            puzzle.SettingGame();
        }
    }

    public void OnBubbleDestroy()
    {
        puzzleMove = StartCoroutine(PuzzleMove_Co());
    }

    public void AlphaCalculate(Sprite _sprite)
    {
        Texture2D texture = _sprite.texture;
        Color32[] pixels = texture.GetPixels32();

        int width = texture.width;
        int height = texture.height;

        // 알파값이 0.1 이상인 픽셀의 영역 계산
        int minX = width;
        int maxX = 0;
        int minY = height;
        int maxY = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color32 pixel = pixels[y * width + x];
                if (pixel.a >= 25) // 알파값이 0.1 이상인 경우
                {
                    minX = Mathf.Min(minX, x);
                    maxX = Mathf.Max(maxX, x);
                    minY = Mathf.Min(minY, y);
                    maxY = Mathf.Max(maxY, y);
                }
            }
        }

        Vector2 center = new Vector2((minX + maxX) / 2f, (minY + maxY) / 2f);

        // 스프라이트의 중심점 계산
        Vector2 spriteCenter = new Vector2(_sprite.rect.width / 2f, _sprite.rect.height / 2f);

        // 중심점 위치 계산
        Vector2 finalCenter = new Vector2(center.x - spriteCenter.x, center.y - spriteCenter.y);

        this.puzzle.puzzleCenter = finalCenter; // center Update
    }
}
