using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;


public class Set_Time : MonoBehaviour
{
    [Header("�ð� ����/���� ��ư")]
    [SerializeField] private Button IncreaseTime_Btn;
    [SerializeField] private Button DecreaseTime_Btn;

    [Header("�ð� �ؽ�Ʈ")]
    [SerializeField] TMP_InputField TimeText_InputField;

    [Header("����/�ڷΰ��� ��ư")]
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
        sec = time % 60;    //60���� ���� ������ = ��
        min = time / 60;
        TimeText_InputField.text = $"{string.Format("{0:0}", min)}�� {sec}��";
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

            }
            else if(TimeText_InputField.text == string.Empty)
            {
                Debug.Log("�ð� ���Է� ��");
            }
            else
            {
                Debug.Log("���ڰ� �ƴմϴ�");
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
                //Ǫ��Ǫ�� ��� �������� ����â ����
            }
            else if (GameManager.instance.gameMode.Equals(GameMode.Speed))
            {
                //���ǵ� ��� �������� ����â ����
            }
            else if (GameManager.instance.gameMode.Equals(GameMode.Memory))
            {
                //�޸� ��� �������� ����â ����
            }
        }
        else
        {
            Debug.Log("�ð� �Է� �� ���ּ���..");
            bCanStart = false;
        }


    }
}
