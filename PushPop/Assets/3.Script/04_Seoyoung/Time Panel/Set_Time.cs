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
    [SerializeField] private GameObject pushpushMode_Canvas;
    [SerializeField] private GameObject speedMode_Canvas;
    [SerializeField] private GameObject memoryMode_Canvas;
    [SerializeField] private GameObject bombMode_Canvas;
    [SerializeField] private GameObject main_Canvas;

    [SerializeField] private Canvas Background_Canvas;  //���� & �ڷΰ��� ��ư ĵ����

    [Header("�ð� ����/���� ��ư")]
    [SerializeField] private Button IncreaseTime_Btn;
    [SerializeField] private Button DecreaseTime_Btn;

    [Header("�ð� �ؽ�Ʈ (�ּ� 5��/�ִ� 15��)")]
    [SerializeField] private TMP_Text TimeText;

    [Header("����/�ڷΰ��� ��ư")]
    [SerializeField] Button Confirm_Btn;
    [SerializeField] Button Back_Btn;

    [SerializeField] private int time = 300;
    int min;
    int sec;

    #region Unity Callback
    private void OnEnable()
    {
        time = 300;
        /*main_Canvas.Disable_Button();*/
    }

    private void Start()
    {
        /*Init();*/
        Calculate_Time();
    }

    private void Update()
    {
        if (time <= 300)
        {
            DecreaseTime_Btn.enabled = false;
        }
        else
        {
            DecreaseTime_Btn.enabled = true;
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
    #endregion

    #region Other Method
    private void Init()
    {
        
        Confirm_Btn.onClick.AddListener(ConfirmBtn_Clicked);
        Back_Btn.onClick.AddListener(() => {
            gameObject.SetActive(false);
            GameManager.Instance._gameMode = GameMode.None;

        });
    }

    private void Calculate_Time()
    {
        sec = time % 60;    //60���� ���� ������ = ��
        min = time / 60;
        TimeText.text = $"{string.Format("{0:0}", min)}�� {sec}��";
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

        if (bIsNumber)
        {
            time = InputNum;
        }
        else
        {
            if (TimeText.text == $"{string.Format("{0:0}", min)}�� {sec}��")
            {

            }
            else
            {
                Debug.Log("���ڰ� �ƴմϴ�");
                time = 300;
                Calculate_Time();
            }


            if (TimeText.text == string.Empty || time < 300)
            {
                Debug.Log("�ð� ���Է� ��");
                time = 300;
                Calculate_Time();
            }

            if (time > 900)
            {
                time = 900;
                Calculate_Time();
            }
        }
    }


    public void ConfirmBtn_Clicked()
    {
        GameManager.Instance.ShutdownTime = time;
        if (GameManager.Instance.gameMode.Equals(Mode.PushPush))
        { // Ǫ��Ǫ�� ����
            pushpushMode_Canvas.SetActive(true);
        }
        else if (GameManager.Instance.gameMode.Equals(Mode.Speed))
        { // ���ǵ� ����
            speedMode_Canvas.SetActive(true);
        }
        else if (GameManager.Instance.gameMode.Equals(Mode.Memory))
        { // �޸� ����
            memoryMode_Canvas.SetActive(true);
        }
        else if (GameManager.Instance.gameMode.Equals(Mode.Bomb))
        { // 2�θ�� ����
            bombMode_Canvas.SetActive(true);
        }
        gameObject.SetActive(false);
        main_Canvas.SetActive(false);
    }
    #endregion





}
