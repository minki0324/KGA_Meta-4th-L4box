using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;


//Main_Canvas�� TimeSet_Panel�� �� ��ũ��Ʈ
public class Set_Time : MonoBehaviour
{

    [Header("ĵ����")]
    [SerializeField] private Main_Button main_Canvas;
    [SerializeField] private Canvas pushMode_Canvas;
    [SerializeField] private Canvas speedMode_Canvas;
    [SerializeField] private Canvas memoryMode_Canvas;

    [SerializeField] private Canvas Background_Canvas;  //���� & �ڷΰ��� ��ư ĵ����

    [Header("�ð� ����/���� ��ư")]
    [SerializeField] private Button IncreaseTime_Btn;
    [SerializeField] private Button DecreaseTime_Btn;

    [Header("�ð� �ؽ�Ʈ (�ּ� 5��/�ִ� 15��)")]
    [SerializeField] TMP_InputField TimeText_InputField;

    [Header("����/�ڷΰ��� ��ư")]
    [SerializeField] Button Confirm_Btn;
    [SerializeField] Button Back_Btn;

    int time = 300;
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
        time = 300;
        bCanStart = true;
        //main_Canvas.Disable_Button();
    }

    private void OnDisable()
    {
      //  main_Canvas.Enable_Button();
    }




    private void Update()
    {
        if (time <= 300)
        {
            DecreaseTime_Btn.enabled = false;
        }
        else
        {
            DecreaseTime_Btn.enabled = true ;
        }

        if (time >= 900)
        {
            IncreaseTime_Btn.enabled = false;
        }
        else
        {
            IncreaseTime_Btn.enabled = true;
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
        sec = time % 60;    //60���� ���� ������ = ��
        min = time / 60;
        TimeText_InputField.text = $"{string.Format("{0:0}", min)}�� {sec}��";
    }


    public void IncreaseTimeBtn_Clicked()
    {
        time += 60;
        Calculate_Time();
    }

    public void DecreaseTimeBtn_Clicked()
    {
        time -= 60;
        Calculate_Time();
    }


    //InputField�� �ð� ���� �Է�
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
            if(TimeText_InputField.text == $"{string.Format("{0:0}", min)}�� {sec}��")
            {
                bCanStart = true;
            }
            else
            {
                Debug.Log("���ڰ� �ƴմϴ�");
                time = 300;
                bCanStart = true;
                Calculate_Time();
                StartCoroutine(Calculate_Time_co());
            }


            if(TimeText_InputField.text == string.Empty || time < 300)
            {
                Debug.Log("�ð� ���Է� ��");
                time = 300;
                bCanStart = true;
                Calculate_Time();
                StartCoroutine(Calculate_Time_co());
            }
            
            if(time > 900)
            {
                time = 900;
                bCanStart = true;
                Calculate_Time();
                StartCoroutine(Calculate_Time_co());
            }
           
        }

       
    }

    private IEnumerator Calculate_Time_co()
    {
        yield return new WaitForSeconds(1.5f);
        TimeText_InputField.enabled = false;
        Calculate_Time();
        TimeText_InputField.enabled = true;
    }


    public void ConfirmBtn_Clicked()
    {
        if(bCanStart)
        {
            GameManager.instance.TimerTime = time;
            if(GameManager.instance.gameMode.Equals(GameMode.Speed))
            {
                speedMode_Canvas.gameObject.SetActive(true);
            }
            else if(GameManager.instance.gameMode.Equals(GameMode.Memory))
            {
                memoryMode_Canvas.gameObject.SetActive(true);
            }

            gameObject.SetActive(false);
            main_Canvas.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("�ð� �Է� �� ���ּ���..");
            bCanStart = false;
        }


    }
}
