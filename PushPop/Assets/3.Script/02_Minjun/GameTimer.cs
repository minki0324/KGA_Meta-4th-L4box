using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    //���ӽ����ϱ��� Ÿ�̸� ���� �� ������ ����ϴ� ��ũ��Ʈ
    //�⺻ 5�� ����
    private float gameTime = 300f;

    //��ư������ 30���߰�
    public void AddTime30()
    {
        gameTime += 30f;
        Mathf.Clamp(gameTime, 30f, 600f);
    }
    //��ư������ 30������
    public void RemoveTime30()
    {
        gameTime -= 30f;
        Mathf.Clamp(gameTime, 30f, 600f);
    }
    public void StartTimer()
    {
        gameTime -= Time.deltaTime;
    }
}
