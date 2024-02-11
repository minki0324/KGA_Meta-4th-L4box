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
        int touchCount = Random.Range(1, 11); // 1 ~ 10ȸ ��ġ ����

        // Bubble Move
        Vector2 dir = _bubblePosition - _touchPosition;
        dir.y = 0f; // �¿�θ� �̵�

        StopAllCoroutines();
        StartCoroutine(BubbleMove_Co(dir));
        touchCount--;
    }

    public void BombMode()
    {
    }

    public void MemoryMode()
    {
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