using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Bubble : MonoBehaviour, IPointerDownHandler, IBubble
{ // bubble prefab's script
    private GameMode gameMode;
    private RectTransform bubbleRectTrans;
    private Animator bubbleAnimator;
    [SerializeField] private Vector2 bubbleSize = Vector2.zero;
    public int touchCount = 0;
    private Vector2 localPos;
    private Vector2 puzzleSize = Vector2.zero;
    private PuzzleObject puzzleObject = null;
    public int where = 0;
    [SerializeField] private bool inBound = true;

    [Header("Move Parameter")]
    // Bubble moving
    public float currentSpeed = 0f;
    private Coroutine moveCoroutine = null;
    [SerializeField] private AnimationCurve decelOverTime;
    [SerializeField] private float decel = 250f; // move 속도 감소
    [SerializeField] private float speedRate = 10f;

    private void OnEnable()
    {
        StartCoroutine(CenterSave());
        gameMode = GameManager.Instance.GameMode; // game start 시 load
        bubbleRectTrans = GetComponent<RectTransform>();
        bubbleAnimator = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        switch (gameMode)
        {
            case GameMode.PushPush:
                currentSpeed = 0f;
                PuzzlePiece piece = transform.parent.GetComponent<PuzzlePiece>();
                if (GameManager.Instance.backButtonClick) return;
                piece.OnBubbleDestroy();
                piece.index = GameManager.Instance.pushPush.pieceCount;
                Debug.Log(piece.index);
                GameManager.Instance.pushPush.pieceCount++;
                break;
            case GameMode.Speed:
                GameManager.Instance.OnDestroyBubble();
                break;
        }

        if(moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    { // 현재 오브젝트 내부에서 클릭하는 순간 1회 호출
        AudioManager.instance.SetCommonAudioClip_SFX(5);
        Vector2 bubblePosition = transform.position;
        Vector2 touchPosition = eventData.position;

        if (moveCoroutine != null)
        { // move coroutine stop
            StopCoroutine(moveCoroutine);
        }

        switch (gameMode)
        {
            case GameMode.PushPush:
                PushPushMode(bubblePosition, touchPosition);
                break;
            case GameMode.Speed:
                SpeedMode(bubblePosition, touchPosition);
                break;
        }
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

    public void PushPushMode(Vector2 _bubblePosition, Vector2 _touchPosition)
    {
        // Bubble Move
        Vector2 dir = (_bubblePosition - _touchPosition).normalized;// Touch Position의 반대 방향
        float speed = (_bubblePosition - _touchPosition).magnitude;
        moveCoroutine = StartCoroutine(BubbleMove_Co(dir, speed));

        // bubble 터트렸을 때
        touchCount--;
        if (touchCount <= 0)
        {
            GameManager.Instance.bubbleObject.Remove(gameObject);
            AudioManager.instance.SetAudioClip_SFX(4, false);
            Destroy(gameObject);
        }
    }

    public void SpeedMode(Vector2 _bubblePosition, Vector2 _touchPosition)
    {
        // Bubble move
        Vector2 dir = (_bubblePosition - _touchPosition).normalized;
        float speed = (_bubblePosition - _touchPosition).magnitude;
        moveCoroutine = StartCoroutine(BubbleMove_Co(dir, speed));

        touchCount--;
        if (touchCount <= 0)
        {
            GameManager.Instance.bubbleObject.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    public void BombMode(Vector2 _player1, Vector2 _player2, bool _onlyMove)
    { // only move 시 상단에 배치 및 touch 기능 없음
        Vector2 dir = Vector2.zero;
        // player1 turn -> if 로 바꾸기
        dir = _player1 - _player2;
        // player2 turn
        dir = _player2 - _player1;
        dir.y = 0;
        // touch는 하되 어떻게 해야할 지 생각
        if (_onlyMove)
        {
            //   StartCoroutine(BubbleMove_Co(dir, 5f, 0.4f));
        }
        else
        {
            //StartCoroutine(BubbleMove_Co(-dir, 5f, 0.4f)); // onlymove와 dir반대
        }
    }

    public void MemoryMode()
    {
        bool isShining = false; // 빛났는 지 아닌지
        bool touchable = false; // 빛난 뒤 2초 후 터치 가능
        // shine animation 추가 필요
        StartCoroutine(BubbleTouchable_Co(bubbleAnimator, (_result) =>
        {
            touchable = _result; // 2초 뒤 true
        }));
        if (!touchable) return;
        if (isShining)
        {
            // animation -> 터짐
            // score += 100
        }
        else
        {
            // life 감소
            // animation -> 까만 빛
        }
    }

    private IEnumerator BubbleTouchable_Co(Animator _bubbleAnimation, Action<bool> callback)
    { // Bombmode에서 random shining touchable 부여, Memory mode도 필요
        // shine animation Setbool
        yield return new WaitForSeconds(2f);
        callback(true);
    }

    // Bubble lerp Translate moving, pushpush, speed mode에서만 사용
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

    private IEnumerator CenterSave()
    {
        yield return null;
        localPos = GetComponent<RectTransform>().localPosition;
    }
}