using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;


public class Set_Time : MonoBehaviour
{
    [SerializeField] Button IncreaseTime_Btn;
    [SerializeField] Button DecreaseTime_Btn;

    [SerializeField] TMP_Text time_textMesh;

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


    private void Init()
    {
        IncreaseTime_Btn.onClick.AddListener(IncreaseTimeBtn_Clicked);
        DecreaseTime_Btn.onClick.AddListener(DecreaseTimeBtn_Clicked);
        Confirm_Btn.onClick.AddListener(ConfirmBtn_Clicked);
        Back_Btn.onClick.AddListener(() => { gameObject.SetActive(false); });
    }

    private void Calculate_Time()
    {

        sec = Time % 60;    //60으로 나눈 나머지 = 초
        min = Time / 60;
        time_textMesh.text = $"{string.Format("{0:0}", min)}분 {sec}초";
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
        //게임패널 on or 씬넘어가기
    }
}
