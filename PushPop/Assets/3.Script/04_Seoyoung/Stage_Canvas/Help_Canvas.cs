using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Bg_Canvas에 들어가는 스크립트

public class Help_Canvas : MonoBehaviour
{
    [Header("Main Canvas")]
    [SerializeField] private Canvas mainCanvas;

    [Header("캔버스")]
    [SerializeField] private PushPush_Canvas pushpush_Canvas;
    [SerializeField] private Speed_Canvas speed_Canvas;
    [SerializeField] private Memory_Canvas memory_Canvas;
    [SerializeField] private Bomb bomb_Canvas;


    [Header("도움말 창")]
    [SerializeField] private GameObject help_Panel;

    [SerializeField] private Button help_Btn;

    [SerializeField] private TMP_Text help_Description;

    [SerializeField] private TMP_Text page_Text;

    [Header("뒤로가기&도움말 버튼")]
    public Button Back_Btn;
    public Button Help_Btn;

    //도움말창이 켜져있는가 판단하는 변수, 도움말 창이 켜져있으면 그 외 모든 버튼 비활성화
    public bool bisHelpPanelOn = false;

    //도움말 페이지 번호
    private int maxPage;
    private int currentPage;

    #region Unity Callback

    private void Start()
    {
        Init();
    }
    private void OnEnable()
    {
        Back_Btn.enabled = true;
        help_Btn.enabled = true;
        switch(GameManager.Instance.gameMode)
        {
            case Mode.PushPush:
              //  pushMode_Panel.SetActive(true);
                break;

            case Mode.Speed:
             //   speedMode_Panel.SetActive(true);
                break;

            case Mode.Memory:
                //memoryMode_Panel.SetActive(true);
                break;
        }
    }

    #endregion

    #region Other Method
    private void Init()
    {   
        help_Panel.SetActive(false); 
    }

    //우측 하단 뒤로가기 버튼 클릭 시 호출되는 메소드
    public void BackBtn_Clicked()
    {
        if(!bisHelpPanelOn)
        {
            //GameManager.Instance.gameMode = Mode.None;

            switch (GameManager.Instance.gameMode)
            {
                case Mode.PushPush:
                    pushpush_Canvas.BackBtn_Clicked();
                    break;

                case Mode.Speed:
                    speed_Canvas.BackBtn_Clicked();
                    break;

                case Mode.Memory:
                    memory_Canvas.BackBtn_Clicked();
                    break;

                case Mode.Bomb:
                    bomb_Canvas.BackBtn_Clicked();
                    break;
            }

           
        }
 
    }


    //좌측하단 도움말 버튼 눌리면 호출될 메소드
    public void HelpBtn_Clicked()
    {     
        if(!bisHelpPanelOn)
        {
            help_Panel.SetActive(true);
            bisHelpPanelOn = true;
            help_Btn.enabled = false;
            Back_Btn.enabled = false;
            currentPage = 1;
            Help_Scripts();

            switch (GameManager.Instance.gameMode)
            {
                case Mode.PushPush:
                    pushpush_Canvas.Disable_Objects();
                    break;

                case Mode.Speed:
                    speed_Canvas.Disable_Objects();
                    break;

                case Mode.Memory:
                    memory_Canvas.DisalbeObjects();
                    break;

                case Mode.Bomb:
                    bomb_Canvas.Disable_Objects();
                    break;
            }
        }
    }


    //도움말 창의 x키(BackBtn) 눌리면 호출될 메소드
    public void Help_BackBtn_Clicked()
    {
        if(bisHelpPanelOn)
        {
            help_Btn.enabled = true;
            Back_Btn.enabled = true;
            bisHelpPanelOn = false;
            help_Panel.SetActive(false);


            switch (GameManager.Instance.gameMode)
            {
                case Mode.PushPush:
                    pushpush_Canvas.Enable_Objects();
                    break;

                case Mode.Speed:
                    speed_Canvas.Enable_Objects();
                    break;

                case Mode.Memory:
                    memory_Canvas.EnableObjects();
                    break;

                case Mode.Bomb:
                    bomb_Canvas.Enable_Objects();
                    break;
            }

        }
       
    }


    //도움말 창의 Next(다음) 버튼 눌리면 호출될 메소드
    public void NextBtn_Clicked()
    {     
        if(currentPage < maxPage)
        {
            currentPage += 1;
            Help_Scripts();
        }
     
    }


    //도움말 창의 Previous(이전) 버튼 눌리면 호출될 메소드
    public void PreviousBtn_Clicked()
    {
        if (currentPage > 1)
        {
            currentPage -= 1;
            Help_Scripts();
        }
    }


    //헬프 버튼 누르면 나오는 스크립트 
    private void Help_Scripts()
    {
        switch (GameManager.Instance.gameMode)
        {

            //푸시푸시모드 도움말
            case Mode.PushPush:
                maxPage = DataManager2.instance.helpScripts_List[0].script.Count;

                for(int i = 0; i < DataManager2.instance.helpScripts_List[0].script.Count; i++)
                {
                    if(currentPage == DataManager2.instance.helpScripts_List[0].script[i].pageNum)
                    {
                        help_Description.text = DataManager2.instance.helpScripts_List[0].script[i].content;
                    }
                }
                break;

            //스피드 모드 도움말
            case Mode.Speed:
                maxPage = DataManager2.instance.helpScripts_List[1].script.Count;

                for (int i = 0; i < DataManager2.instance.helpScripts_List[1].script.Count; i++)
                {
                    if (currentPage == DataManager2.instance.helpScripts_List[1].script[i].pageNum)
                    {
                        help_Description.text = DataManager2.instance.helpScripts_List[1].script[i].content;
                    }
                }
                break;

            //메모리 모드 도움말
            case Mode.Memory:
                maxPage = DataManager2.instance.helpScripts_List[2].script.Count;

                for (int i = 0; i < DataManager2.instance.helpScripts_List[2].script.Count; i++)
                {
                    if (currentPage == DataManager2.instance.helpScripts_List[2].script[i].pageNum)
                    {
                        help_Description.text = DataManager2.instance.helpScripts_List[2].script[i].content;
                    }
                }
                break;

            //2인모드 도움맒
            case Mode.Bomb:
                maxPage = DataManager2.instance.helpScripts_List[3].script.Count;

                for (int i = 0; i < DataManager2.instance.helpScripts_List[3].script.Count; i++)
                {
                    if (currentPage == DataManager2.instance.helpScripts_List[3].script[i].pageNum)
                    {
                        help_Description.text = DataManager2.instance.helpScripts_List[3].script[i].content;
                    }
                }
                break;
        }

        page_Text.text = $"{currentPage}/{maxPage}";
    }




    #endregion

}
