using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Emotion : NetworkBehaviour
{
    //���� EMotion �� �����ϰ� �ֱ� ���� ��ũ��Ʈ
    public static Emotion instance;
    //�̸�� ���� �ν����Ϳ��� �߰��ϱ�
    public Sprite[] emotions;
    private void Awake()
    {
        instance = this;
    }
}
