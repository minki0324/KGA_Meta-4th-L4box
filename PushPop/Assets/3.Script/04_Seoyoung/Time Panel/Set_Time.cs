using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;


//Main_Canvas의 TimeSet_Panel에 들어갈 스크립트
public class Set_Time : MonoBehaviour
{

    [Header("캔버스")]
    [SerializeField] private GameObject pushpushMode_Canvas;
    [SerializeField] private GameObject speedMode_Canvas;
    [SerializeField] private GameObject memoryMode_Canvas;
    [SerializeField] private GameObject bombMode_Canvas;
    [SerializeField] private GameObject main_Canvas;

    [SerializeField] private Canvas Background_Canvas;  //도움말 & 뒤로가기 버튼 캔버스
    [SerializeField] private Main_Button main_Button;   //메인 버튼

    [Header("시간 증가/감소 버튼")]
    [SerializeField] private Button IncreaseTime_Btn;
    [SerializeField] private Button DecreaseTime_Btn;

    [Header("시간 텍스트 (최소 5분/최대 15분)")]
    [SerializeField] private TMP_Text TimeText;

    [Header("시작/뒤로가기 버튼")]
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
    private void Init()
    {
        
        Confirm_Btn.onClick.AddListener(ConfirmBtn_Clicked);
        Back_Btn.onClick.AddListener(() => {
            gameObject.SetActive(false);
            

        });
    }

    private void Calculate_Time()
    {
        sec = time % 60;    //60으로 나눈 나머지 = 초
        min = time / 60;
        if(time % 60 == 0)
        {
            TimeText.text = $"{string.Format("{0:0}", min)}분";
        }
        else
        {
            TimeText.text = $"{string.Format("{0:0}", min)}분 {sec}초";
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


    //InputField에 시간 직접 입력
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
            if (TimeText.text == $"{string.Format("{0:0}", min)}분 {sec}초")
            {

            }
            else
            {
                Debug.Log("숫자가 아닙니다");
                time = 300;
                Calculate_Time();
            }


            if (TimeText.text == string.Empty || time < 60)
            {
                Debug.Log("시간 미입력 시");
                time = 60;
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
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        GameManager.Instance.shutdownTimer = time;
        if (GameManager.Instance.gameMode.Equals(Mode.PushPush))
        { // 푸시푸시 시작
            pushpushMode_Canvas.SetActive(true);
            help_Canvas.transform.SetParent(pushpushMode_Canvas.transform);
            help_Canvas.transform.SetSiblingIndex(3);
        }
        else if (GameManager.Instance.gameMode.Equals(Mode.Speed))
        { // 스피드 시작
            speedMode_Canvas.SetActive(true);
        }
        else if (GameManager.Instance.gameMode.Equals(Mode.Memory))
        { // 메모리 시작
            memoryMode_Canvas.SetActive(true);
            memoryMode_Canvas.GetComponent<Memory_Canvas>().RankingLoad();
        }
        else if (GameManager.Instance.gameMode.Equals(Mode.Bomb))
        { // 2인모드 시작
            bombMode_Canvas.SetActive(true);
            bombMode_Canvas.transform.GetComponent<Bomb>().PrintVersus();
        }     
        help_Canvas.SetActive(true);
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
