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

    int time = 330;
    int min;
    int sec;

    bool bCanStart = false;

    private void Start()
    {
        Init();
        Calculate_Time();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        time = 330;
    }

    private void Update()
    {
        if (time <= 0)
        {
            DecreaseTime_Btn.enabled = false;
        }
        else
        {
            DecreaseTime_Btn.enabled = true ;
        }
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
        sec = time % 60;    //60으로 나눈 나머지 = 초
        min = time / 60;
        TimeText_InputField.text = $"{string.Format("{0:0}", min)}분 {sec}초";
    }


    public void IncreaseTimeBtn_Clicked()
    {
        time += 30;
        Calculate_Time();
    }

    public void DecreaseTimeBtn_Clicked()
    {
        time -= 30;
        Calculate_Time();
    }


    //InputField에 시간 직접 입력
    public void TextFieldValue_Changed(string text)
    {
        int InputNum = 0;
        bool bIsNumber;

        bIsNumber = int.TryParse(text, out InputNum);

        if(bIsNumber)
        {
            time = InputNum;
            StartCoroutine(Calculate_Time_co());
        }
        else
        {
           
            if(TimeText_InputField.text == $"{string.Format("{0:0}", min)}분 {sec}초")
            {

            }
            else if(TimeText_InputField.text == string.Empty)
            {
                Debug.Log("시간 미입력 시");
            }
            else
            {
                Debug.Log("숫자가 아닙니다");
            }
        }

    }

    private IEnumerator Calculate_Time_co()
    {
        yield return new WaitForSeconds(1.5f);
        Calculate_Time();
    }


    public void ConfirmBtn_Clicked()
    {
        if(bCanStart)
        {

            GameManager.instance.TimerTime = time;
            if (GameManager.instance.gameMode.Equals(GameMode.PushPush))
            {
                //푸쉬푸쉬 모드 스테이지 선택창 열기
            }
            else if (GameManager.instance.gameMode.Equals(GameMode.Speed))
            {
                //스피드 모드 스테이지 선택창 열기
            }
            else if (GameManager.instance.gameMode.Equals(GameMode.Memory))
            {
                //메모리 모드 스테이지 선택창 열기
            }
        }
        else
        {
            Debug.Log("시간 입력 좀 해주세요..");
            bCanStart = false;
        }


    }
}
