using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;


public class Set_Time : MonoBehaviour
{
    [Header("시간 증가/감소 버튼")]
    [SerializeField] private Button IncreaseTime_Btn;
    [SerializeField] private Button DecreaseTime_Btn;

    [Header("시간 텍스트")]
    [SerializeField] TMP_InputField TimeText_InputField;

    [Header("시작/뒤로가기 버튼")]
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
        TimeText_InputField.onValueChanged.AddListener(delegate { TextFieldValue_Changed(TimeText_InputField.text); });
        IncreaseTime_Btn.onClick.AddListener(IncreaseTimeBtn_Clicked);
        DecreaseTime_Btn.onClick.AddListener(DecreaseTimeBtn_Clicked);
        Confirm_Btn.onClick.AddListener(ConfirmBtn_Clicked);
        Back_Btn.onClick.AddListener(() => { 
            gameObject.SetActive(false);
            GameManager.instance.gameMode = GameMode.None;
        });
    }

    private void Calculate_Time()
    {
        sec = Time % 60;    //60으로 나눈 나머지 = 초
        min = Time / 60;
        TimeText_InputField.text = $"{string.Format("{0:0}", min)}분 {sec}초";
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

    public void TextFieldValue_Changed(string text)
    {
        //:)
    }


    public void ConfirmBtn_Clicked()
    {
        GameManager.instance.TimerTime = Time;
        if (GameManager.instance.gameMode.Equals(GameMode.PushPush))
        {
            //푸쉬푸쉬 모드 스테이지 선택창 열기
        }
        else if (GameManager.instance.gameMode.Equals(GameMode.Speed))
        {
            //스피드 모드 스테이지 선택창 열기
        }
        else if(GameManager.instance.gameMode.Equals(GameMode.Memory))
        {
            //메모리 모드 스테이지 선택창 열기
        }

    }
}
