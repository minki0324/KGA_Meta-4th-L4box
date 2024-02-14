using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Mode
{
    PushPush = 0,
    Speed,
    Memory,
    Bomb,
}

public class Bubble : MonoBehaviour, IPointerDownHandler, IBubble
{ // bubble prefab���� �޾��� script

    public AnimationCurve decelOverTime;
    [Range(10f, 100f)] public float maxDecel = 30f;
    [Range(10f, 1000f)] public float maxSpeed = 100f;

    protected Mode gameMode = Mode.Speed; // game mode
    private Vector2 bubbleSize = Vector2.zero;
    private bool isMoving = false;
    private Animator bubbleAnimator;

    private Coroutine moveCoroutine = null;
    bool coll = false;

    // screen Size
    private Vector2 screenSize = Vector2.zero;

    private void OnEnable()
    {
        Rect rectTransform = gameObject.GetComponent<RectTransform>().rect;
        bubbleSize = new Vector2(rectTransform.width, rectTransform.height);
        gameMode = Mode.Speed; // GameManager Mode Load
        screenSize = new Vector2(2 * Camera.main.orthographicSize * Camera.main.aspect, 2 * Camera.main.orthographicSize);
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

    public void PushPushMode(Vector2 _bubblePosition, Vector2 _touchPosition)
    {
        // Bubble Move
        Vector2 dir = (_bubblePosition - _touchPosition).normalized;// Touch Position�� �ݴ� ����

        //moveCoroutine = StartCoroutine(BubbleMove_Co(dir, 2f, 0.4f));
    }

    public void SpeedMode(Vector2 _bubblePosition, Vector2 _touchPosition)
    {
        // Bubble Move

        Vector2 dir = (_bubblePosition - _touchPosition).normalized;
        //dir.y = 0f; // �¿�θ� �̵�

        moveCoroutine = StartCoroutine(BubbleMove_Co(_bubblePosition, dir, maxSpeed, maxDecel));
        GameManager.instance.touchCount--;

        if (GameManager.instance.touchCount <= 0)
        { // touch count 0���� ���� ��
            // pushpop ����
            PushPop.instance.CreatePushPop();
            Destroy(this.gameObject);
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
    { // Bombmode���� random shining touchable �ο�
        // shine animation Setbool
        yield return new WaitForSeconds(2f);
        callback(true);
    }

    private IEnumerator BubbleMove_Co(Vector2 _bubblePosition, Vector2 _dir, float _maxSpeed, float _decel)
    {
        float currentSpeed = _maxSpeed; // maxSpeed �ʱ�ȭ

        while (currentSpeed >= 0)
        {
            if (0f + bubbleSize.x / 2f > transform.position.x)
            { // boundary left
                _dir = ReflectionVector(_dir, Vector2.right);
                transform.position = new Vector2((bubbleSize.x / 2f + 10f), transform.position.y);
            }
            else if (transform.position.x > Screen.width - bubbleSize.x / 2f)
            { // boundary right
                _dir = ReflectionVector(_dir, Vector2.left);
                transform.position = new Vector2(Screen.width - (bubbleSize.x / 2f + 10f), transform.position.y);
            }
            else if (0f + bubbleSize.y / 2f + 10f > transform.position.y)
            { // boundary bottom
                _dir = ReflectionVector(_dir, Vector2.up);
                transform.position = new Vector2(transform.position.x, bubbleSize.y / 2f + 10f);
            }
            else if (transform.position.y > Screen.height - bubbleSize.y / 2f)
            { // boundary up
                _dir = ReflectionVector(_dir, Vector2.down);
                transform.position = new Vector2(transform.position.x, Screen.height - (bubbleSize.y / 2f + 10f));
            }

            _bubblePosition = transform.position;

            transform.Translate(_dir * (Time.deltaTime * currentSpeed * 200f));

            // Lerp
            var lerpDecel = decelOverTime.Evaluate(1 - currentSpeed / _maxSpeed) * _decel;
            currentSpeed = Mathf.Max(0f, currentSpeed - lerpDecel * Time.deltaTime);

            yield return null;
        }
    }

    private Vector2 ReflectionVector(Vector2 _inDrection, Vector2 _isNomal)
    {
        _inDrection = Vector2.Reflect(_inDrection.normalized, _isNomal).normalized; // �ݻ簢
        return _inDrection;
    }
}