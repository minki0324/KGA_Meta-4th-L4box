using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bubble : MonoBehaviour, IPointerDownHandler, IBubble
{ // bubble prefab's script
    private Mode gameMode;
    private RectTransform bubbleRectTrans;
    private Animator bubbleAnimator;
    private Vector2 bubbleSize = Vector2.zero;
    public int touchCount = 0;


    [Header("Move Parameter")]
    // Bubble moving
    private float currentSpeed = 0f;
    private Coroutine moveCoroutine = null;
    [SerializeField] private AnimationCurve decelOverTime;
    [SerializeField] private float decel = 250f; // move �ӵ� ����
    [SerializeField] private float speedRate = 10f;

    private void OnEnable()
    {
        gameMode = GameManager.Instance.gameMode; // game start �� load
        bubbleRectTrans = GetComponent<RectTransform>();
        bubbleAnimator = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        switch (gameMode)
        {
            case Mode.PushPush:
                currentSpeed = 0f;
                PuzzlePiece piece = transform.parent.GetComponent<PuzzlePiece>();
                piece.OnBubbleDestroy();
                break;
            case Mode.Speed:
                Speed_Timer speedTimer = FindObjectOfType<Speed_Timer>();
                if (speedTimer == null) return;
                speedTimer.time_Slider.gameObject.SetActive(true);
                GameManager.Instance.SpeedModePushPopCreate();
                break;
            case Mode.Memory:
                break;
            case Mode.Bomb:
                break;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    { // ���� ������Ʈ ���ο��� Ŭ���ϴ� ���� 1ȸ ȣ��
        Vector2 bubblePosition = transform.position;
        Vector2 touchPosition = eventData.position;

        if (moveCoroutine != null)
        { // move coroutine stop
            StopCoroutine(moveCoroutine);
        }

        switch (gameMode)
        {
            case Mode.PushPush:
                PushPushMode(bubblePosition, touchPosition);
                break;
            case Mode.Speed:
                SpeedMode(bubblePosition, touchPosition);
                break;
            case Mode.Memory:
                break;
            case Mode.Bomb:
                break;
        }
    }

    public void BubbleSetting(Vector2 _puzzleSize, Vector2 _puzzlePos, Transform _puzzle)
    { // GameManager���� Mode ���� �� Position ���� ��ŭ ȣ��Ǵ� method
      // position setting
        bubbleRectTrans.anchoredPosition = _puzzlePos;
        // size setting
        float bigger = _puzzleSize.x > _puzzleSize.y ? _puzzleSize.x : _puzzleSize.y;

        bubbleRectTrans.sizeDelta = new Vector2(bigger, bigger);
        bubbleSize = bubbleRectTrans.sizeDelta;
    }

    public void PushPushMode(Vector2 _bubblePosition, Vector2 _touchPosition)
    {
        // Bubble Move
        Vector2 dir = (_bubblePosition - _touchPosition).normalized;// Touch Position�� �ݴ� ����
        float speed = (_bubblePosition - _touchPosition).magnitude;
        moveCoroutine = StartCoroutine(BubbleMove_Co(dir, speed, Mode.PushPush)); // jaeyun todo ���߿� �⺻�� �˾Ƽ� �� �������� ��

        // bubble ��Ʈ���� ��
        touchCount--;
        if (touchCount <= 0)
        {
            GameManager.Instance.bubbleObject.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    public void SpeedMode(Vector2 _bubblePosition, Vector2 _touchPosition)
    {
        // Bubble move
        Vector2 dir = (_bubblePosition - _touchPosition).normalized;
        float speed = (_bubblePosition - _touchPosition).magnitude;
        // dir.y = 0f; //�¿�θ� �̵�

        moveCoroutine = StartCoroutine(BubbleMove_Co(dir, speed));
        touchCount--;

        if (touchCount <= 0)
        {
            GameManager.Instance.bubbleObject.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    public void BombMode(Vector2 _player1, Vector2 _player2, bool _onlyMove)
    { // only move �� ��ܿ� ��ġ �� touch ��� ����
        float timer = 0;
        Vector2 dir = Vector2.zero;
        // player1 turn -> if �� �ٲٱ�
        dir = _player1 - _player2;
        // player2 turn
        dir = _player2 - _player1;
        dir.y = 0;
        // touch�� �ϵ� ��� �ؾ��� �� ����
        if (_onlyMove)
        {
            //   StartCoroutine(BubbleMove_Co(dir, 5f, 0.4f));
        }
        else
        {
            //StartCoroutine(BubbleMove_Co(-dir, 5f, 0.4f)); // onlymove�� dir�ݴ�
        }
    }

    public void MemoryMode()
    {
        bool isShining = false; // ������ �� �ƴ���
        bool touchable = false; // ���� �� 2�� �� ��ġ ����
        // shine animation �߰� �ʿ�
        StartCoroutine(BubbleTouchable_Co(bubbleAnimator, (_result) =>
        {
            touchable = _result; // 2�� �� true
        }));
        if (!touchable) return;
        if (isShining)
        {
            // animation -> ����
            // score += 100
        }
        else
        {
            // life ����
            // animation -> � ��
        }
    }

    private IEnumerator BubbleTouchable_Co(Animator _bubbleAnimation, Action<bool> callback)
    { // Bombmode���� random shining touchable �ο�, Memory mode�� �ʿ�
        // shine animation Setbool
        yield return new WaitForSeconds(2f);
        callback(true);
    }

    // Bubble lerp Translate moving, pushpush, speed mode������ ���
    private IEnumerator BubbleMove_Co(Vector2 _dir, float _maxSpeed)
    {
        currentSpeed = _maxSpeed; // maxSpeed �ʱ�ȭ
        float bubbleScale = bubbleRectTrans.lossyScale.x; // x, y ����

        // �ӵ��� 0�� �Ǿ��� ������ �̵�
        while (currentSpeed >= 0)
        {
            if (0f + (bubbleSize.x * bubbleScale / 2f) > transform.position.x)
            { // boundary left
                _dir = Vector2.Reflect(_dir, Vector2.right).normalized;
                transform.parent.position = new Vector2((bubbleSize.x * bubbleScale / 2f) + 10f, transform.position.y);
            }
            else if (transform.position.x > Screen.width - (bubbleSize.x * bubbleScale / 2f))
            { // boundary right
                _dir = Vector2.Reflect(_dir, Vector2.left).normalized;
                transform.parent.position = new Vector2(Screen.width - ((bubbleSize.x * bubbleScale / 2f) + 10f), transform.position.y);
            }
            else if (0f + (bubbleSize.y * bubbleScale / 2f) > transform.position.y)
            { // boundary bottom
                _dir = Vector2.Reflect(_dir, Vector2.up).normalized;
                transform.parent.position = new Vector2(transform.position.x, (bubbleSize.y * bubbleScale / 2f) + 10f);
            }
            else if (transform.position.y > Screen.height - (bubbleSize.y * bubbleScale / 2f))
            { // boundary up
                _dir = Vector2.Reflect(_dir, Vector2.down).normalized;
                transform.parent.position = new Vector2(transform.position.x, Screen.height - ((bubbleSize.y * bubbleScale / 2f) + 10f));
            }

            transform.parent.Translate(_dir * (Time.deltaTime * currentSpeed * speedRate)); // bubble move

            // moving lerp
            float lerpDecel = decelOverTime.Evaluate(1 - currentSpeed / _maxSpeed) * decel;
            currentSpeed = Mathf.Max(0f, currentSpeed - lerpDecel * Time.deltaTime);

            yield return null;
        }
    }


    private IEnumerator BubbleMove_Co(Vector2 _dir, float _maxSpeed, Mode _gameMode)
    {
        currentSpeed = _maxSpeed; // maxSpeed �ʱ�ȭ
        float bubbleScale = bubbleRectTrans.lossyScale.x; // x, y ����

        // �ӵ��� 0�� �Ǿ��� ������ �̵�
        while (currentSpeed >= 0)
        {
            if (0f + (bubbleSize.x * bubbleScale / 2f) > transform.position.x)
            { // boundary left
                _dir = Vector2.Reflect(_dir, Vector2.right).normalized;
                transform.position = new Vector2((bubbleSize.x * bubbleScale / 2f) + 10f, transform.position.y);
            }
            else if (transform.position.x > Screen.width - (bubbleSize.x * bubbleScale / 2f))
            { // boundary right
                _dir = Vector2.Reflect(_dir, Vector2.left).normalized;
                transform.position = new Vector2(Screen.width - ((bubbleSize.x * bubbleScale / 2f) + 10f), transform.position.y);
            }
            else if (0f + (bubbleSize.y * bubbleScale / 2f) > transform.position.y)
            { // boundary bottom
                _dir = Vector2.Reflect(_dir, Vector2.up).normalized;
                transform.position = new Vector2(transform.position.x, (bubbleSize.y * bubbleScale / 2f) + 10f);
            }
            else if (transform.position.y > Screen.height - (bubbleSize.y * bubbleScale / 2f))
            { // boundary up
                _dir = Vector2.Reflect(_dir, Vector2.down).normalized;
                transform.position = new Vector2(transform.position.x, Screen.height - ((bubbleSize.y * bubbleScale / 2f) + 10f));
            }

            transform.parent.Translate(_dir * (Time.deltaTime * currentSpeed * speedRate)); // bubble move

            // moving lerp
            float lerpDecel = decelOverTime.Evaluate(1 - currentSpeed / _maxSpeed) * decel;
            currentSpeed = Mathf.Max(0f, currentSpeed - lerpDecel * Time.deltaTime);

            yield return null;
        }
    }
}