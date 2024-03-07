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
    [SerializeField] private Main_Button main_Button;   //���� ��ư

    [Header("�ð� ����/���� ��ư")]
    [SerializeField] private Button IncreaseTime_Btn;
    [SerializeField] private Button DecreaseTime_Btn;

    [Header("�ð� �ؽ�Ʈ (�ּ� 5��/�ִ� 15��)")]
    [SerializeField] private TMP_Text TimeText;

    [Header("����/�ڷΰ��� ��ư")]
    [SerializeField] Button Confirm_Btn;
    [SerializeField] Button Back_Btn;

    public GameObject help_Canvas;

    [SerializeField] private int time = 180;
    int min;
    int sec;

    #region Unity Callback
    private void OnEnable()
    {
        time = 180;
        Calculate_Time();
        main_Button.Disable_Button();
    }

    private void Start()
    {
        /*Init();*/
        Calculate_Time();
    }

    private void OnDisable()
    {
        main_Button.Enable_Button();
    }

    private void Update()
    {
        if (time <= 60)
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
    private void Calculate_Time()
    {
        sec = time % 60;    //60���� ���� ������ = ��
        min = time / 60;
        if(time % 60 == 0)
        {
            TimeText.text = $"{string.Format("{0:0}", min)}��";
        }
        else
        {
            TimeText.text = $"{string.Format("{0:0}", min)}�� {sec}��";
        }
    }

    public void IncreaseTimeBtn_Clicked()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        time += 60;
        Calculate_Time();
    }

    public void DecreaseTimeBtn_Clicked()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        time -= 60;
        Calculate_Time();
    }

    public void ConfirmBtn_Clicked()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        GameManager.Instance.ShutdownTimer = time;
        if (GameManager.Instance.GameMode.Equals(GameMode.PushPush))
        { // Ǫ��Ǫ�� ����
            pushpushMode_Canvas.SetActive(true);
            help_Canvas.SetActive(true);
            help_Canvas.transform.SetParent(pushpushMode_Canvas.transform);
            help_Canvas.transform.SetSiblingIndex(3);
        }
        else if (GameManager.Instance.GameMode.Equals(GameMode.Speed))
        { // ���ǵ� ����
            speedMode_Canvas.SetActive(true);
        }
        else if (GameManager.Instance.GameMode.Equals(GameMode.Memory))
        { // �޸� ����
            memoryMode_Canvas.SetActive(true);
            memoryMode_Canvas.GetComponent<Memory_Canvas>().RankingLoad();
        }
        else if (GameManager.Instance.GameMode.Equals(GameMode.Multi))
        { // 2�θ�� ����
            bombMode_Canvas.SetActive(true);
            //bombMode_Canvas.transform.GetComponent<MultiManager>().PrintVersus();
        }     
        main_Canvas.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void BackBtnClicked()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        gameObject.SetActive(false);
    }

    public void SetShutdownTime(int time)
    {
        this.time = time;
        Calculate_Time();
    }
    #endregion





}
