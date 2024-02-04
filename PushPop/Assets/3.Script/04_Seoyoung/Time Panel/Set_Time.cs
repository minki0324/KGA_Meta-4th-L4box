using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;


public class Set_Time : MonoBehaviour
{
    [Header("�ð� ����/���� ��ư")]
    [SerializeField] Button IncreaseTime_Btn;
    [SerializeField] Button DecreaseTime_Btn;

    [Header("�ð� �ؽ�Ʈ")]
    [SerializeField] TMP_Text time_textMesh;

    [Header("����/�ڷΰ��� ��ư")]
    [SerializeField] Button Confirm_Btn;
    [SerializeField] Button Back_Btn;

    int Time = 330;
    int min;
    int sec;

    private void Start()
    {
        Init();
        Calculate_Time();
       
    }

    private void OnEnable()
    {
        Time = 330;
    }

    private void Init()
    {
        
        IncreaseTime_Btn.onClick.AddListener(IncreaseTimeBtn_Clicked);
        DecreaseTime_Btn.onClick.AddListener(DecreaseTimeBtn_Clicked);
        Confirm_Btn.onClick.AddListener(ConfirmBtn_Clicked);
        Back_Btn.onClick.AddListener(() => { 
            gameObject.SetActive(false);
            GameManger_2.instance.gameMode = GameMode.None;
        });
    }

    private void Calculate_Time()
    {

        sec = Time % 60;    //60���� ���� ������ = ��
        min = Time / 60;
        time_textMesh.text = $"{string.Format("{0:0}", min)}�� {sec}��";
    }


    public void IncreaseTimeBtn_Clicked()
    {
        Time += 30;
        Calculate_Time();
    }

    public void DecreaseTimeBtn_Clicked()
    {
        Time -= 30;
        Calculate_Time();
    }


    public void ConfirmBtn_Clicked()
    {
        GameManger_2.instance.TimerTime = Time;
        if (GameManger_2.instance.gameMode.Equals(GameMode.PushPush))
        {
            //Ǫ��Ǫ�� ���
        }
        else if (GameManger_2.instance.gameMode.Equals(GameMode.Speed))
        {
            //���ǵ� ���
        }
        else if(GameManger_2.instance.gameMode.Equals(GameMode.Memory))
        {
            //�޸� ���
        }

    }
}
