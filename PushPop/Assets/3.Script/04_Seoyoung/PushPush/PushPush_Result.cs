using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPush_Result : MonoBehaviour
{
    //���� �� ó������ ��ư
    public void Success_BackBtn_Clicked()
    {
        GameManager.Instance.puzzleClass.Clear();
    }
}
