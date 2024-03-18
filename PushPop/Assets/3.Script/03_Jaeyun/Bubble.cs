using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bubble : MonoBehaviour, IPointerDownHandler
{ // bubble prefab's script
    private GameMode gameMode;
    [SerializeField] private RectTransform bubbleRectTrans;
    private Vector2 bubbleSize = Vector2.zero;
    public int TouchCount = 0;
    private Vector2 localPos = Vector2.zero;
    private Vector2 puzzleSize = Vector2.zero;

    [Header("Move Parameter")]
    // Bubble moving
    public float currentSpeed = 0f;
    private Coroutine moveCoroutine = null;
    [SerializeField] private AnimationCurve decelOverTime;
    [SerializeField] private float decel = 250f; // move 속도 감소
    [SerializeField] private float speedRate = 10f;

    private void OnEnable()
    {
        gameMode = GameManager.Instance.GameMode; // game start 시 load
    }

    private void OnDestroy()
    {
        switch (gameMode)
        {
            case GameMode.PushPush:
                if (GameManager.Instance.LiveBubbleCount.Equals(0)) return;
                PuzzlePiece piece = transform.parent.GetComponent<PuzzlePiece>();
                piece.BubbleOnDestroy();
                break;
            case GameMode.Speed:
                if (GameManager.Instance.LiveBubbleCount.Equals(0)) return;
                GameManager.Instance.OnDestroyBubble();
                break;
        }

        StopAllCoroutines();
    }

    public void OnPointerDown(PointerEventData eventData)
    { // 현재 오브젝트 내부에서 클릭하는 순간 1회 호출
        AudioManager.Instance.SetCommonAudioClip_SFX(5);
        StopAllCoroutines();
        BubbleTouch(transform.position, eventData.position);
    }

    public void BubbleSetting(Vector2 _puzzleSize, Vector2 _puzzlePos)
    { // GameManager에서 Mode 선택 시 Position 개수 만큼 호출되는 method
      // position setting
        bubbleRectTrans.anchoredPosition = _puzzlePos;
        // size setting
        float bigger = _puzzleSize.x > _puzzleSize.y ? _puzzleSize.x : _puzzleSize.y;
        puzzleSize = _puzzleSize;

        if (gameMode.Equals(GameMode.PushPush))
        {
            bigger *= 1.3f;
        }
        bubbleRectTrans.sizeDelta = new Vector2(bigger, bigger);
        bubbleSize = bubbleRectTrans.sizeDelta;
    }

    private void BubbleTouch(Vector2 _bubblePosition, Vector2 _touchPosition)
    {
        // Bubble Move
        Vector2 dir = (_bubblePosition - _touchPosition).normalized;// Touch Position의 반대 방향
        float speed = (_bubblePosition - _touchPosition).magnitude;
        moveCoroutine = StartCoroutine(BubbleMove_Co(dir, speed));

        // bubble 터트렸을 때
        TouchCount--;
        if (TouchCount <= 0)
        {
            GameManager.Instance.bubbleObject.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    private IEnumerator BubbleMove_Co(Vector2 _dir, float _maxSpeed)
    {
        currentSpeed = _maxSpeed; // maxSpeed 초기화
        float bubbleScale = bubbleRectTrans.lossyScale.x; // x, y 같음

        // 속도가 0이 되었을 때까지 이동
        while (currentSpeed >= 0)
        {
            if (0f + (bubbleSize.x * bubbleScale / 2f) > transform.position.x)
            { // boundary left
                _dir = Vector2.Reflect(_dir, Vector2.right).normalized;
            }
            else if (transform.position.x > Screen.width - (bubbleSize.x * bubbleScale / 2f))
            { // boundary right
                _dir = Vector2.Reflect(_dir, Vector2.left).normalized;
            }
            else if (0f + (bubbleSize.y * bubbleScale / 2f) > transform.position.y)
            { // boundary bottom
                _dir = Vector2.Reflect(_dir, Vector2.up).normalized;
            }
            else if (transform.position.y > Screen.height - (bubbleSize.y * bubbleScale / 2f))
            { // boundary up
                _dir = Vector2.Reflect(_dir, Vector2.down).normalized;
            }

            transform.parent.Translate(_dir * (Time.deltaTime * currentSpeed * speedRate)); // bubble move

            // moving lerp
            float lerpDecel = decelOverTime.Evaluate(1 - currentSpeed / _maxSpeed) * decel;
            currentSpeed = Mathf.Max(0f, currentSpeed - lerpDecel * Time.deltaTime);

            yield return null;
        }
    }

    private IEnumerator CenterSave_Co()
    {
        yield return null;
        localPos = GetComponent<RectTransform>().localPosition;
    }
}