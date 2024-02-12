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
    protected Mode gameMode = Mode.PushPush; // game mode
    private bool isMoving = false;
    private Animator bubbleAnimator;

    private void OnEnable()
    {
        gameMode = Mode.PushPush; // GameManager Mode Load
    }

    public void OnPointerDown(PointerEventData eventData)
    { // ���� ������Ʈ ���ο��� Ŭ���ϴ� ���� 1ȸ ȣ��

        Vector2 bubblePosition = transform.position;
        Vector2 touchPosition = eventData.position;

        switch (gameMode)
        {
            case Mode.PushPush:
                PushPushMode(bubblePosition, touchPosition);
                break;
            case Mode.Speed:
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
        Vector2 dir = _bubblePosition - _touchPosition; // Touch Position�� �ݴ� ����

        StopAllCoroutines();
        StartCoroutine(BubbleMove_Co(dir));
    }

    public void SpeedMode(Vector2 _bubblePosition, Vector2 _touchPosition)
    {
        // Bubble Move
        Vector2 dir = _bubblePosition - _touchPosition;
        dir.y = 0f; // �¿�θ� �̵�

        StopAllCoroutines();
        StartCoroutine(BubbleMove_Co(dir));
        GameManager.instance.touchCount--;
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
            StartCoroutine(BubbleMove_Co(dir));
        }
        else
        {
            StartCoroutine(BubbleMove_Co(dir)); // onlymove�� dir�ݴ�
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

    private IEnumerator BubbleMove_Co(Vector2 _dir)
    {
        float timer = 2f;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            transform.Translate(_dir * (Time.deltaTime * 0.5f));
            yield return null;
        }
    }
}