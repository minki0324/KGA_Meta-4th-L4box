using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Stage_Canvas에 들어가는 스크립트

public class Stage_Background : MonoBehaviour
{
    [Header("Main Canvas")]
    [SerializeField] Canvas mainCanvas;

    [Header("도움말 창")]
    [SerializeField]
    private GameObject help_Panel;

    [SerializeField]
    private Button help_Btn;

    [SerializeField]
    private TMP_Text help_Description;

    [SerializeField]
    private TMP_Text page_Text;

    //도움말창이 켜져있는가 판단하는 변수
    public bool bisHelpPanelOn = false;

    //도움말 페이지 번호
    private int maxPage;
    private int currentPage;

    #region Unity Callback

    private void Start()
    {
        help_Panel.SetActive(false);
        //gameObject.SetActive(false);        
    }

    #endregion

    #region Other Method

    public void BackBtn_Clicked()
    {
        mainCanvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }


    public void HelpBtn_Clicked()
    {
        //Help(도움말) 버튼 눌리면 호출될 메소드
        if(bisHelpPanelOn == false)
        {
            help_Panel.SetActive(true);
            bisHelpPanelOn = true;
            help_Btn.enabled = false;
            currentPage = 1;
            Help_Scripts();
        }
    }

    public void Help_BackBtn_Clicked()
    {
        //도움말 창의 x키(BackBtn) 눌리면 호출될 메소드
        help_Panel.SetActive(false);
        help_Btn.enabled = true;
    }

    public void NextBtn_Clicked()
    {
        //Next(다음) 버튼 눌리면 호출될 메소드
        currentPage += 1;
        Help_Scripts();
    }

    public void PreviousBtn_Clicked()
    {
        //Previous(이전) 버튼 눌리면 호출될 메소드
        currentPage -= 1;
        Help_Scripts();
    }


    private void Help_Scripts()
    {
        //헬프 버튼 누르면 나오는 스크립트 
        switch (GameManager.instance.gameMode)
        {

            //푸시푸시모드 도움말
            case GameMode.PushPush:
                maxPage = 2;
                switch (currentPage)
                {
                    case 1:
                        //1페이지 내용
                        help_Description.text = "푸시푸시 1페이지";
                        break;

                    case 2:
                        //2페이지 내용
                        help_Description.text = "푸시푸시 2페이지";
                        break;

                }
                break;

            //스피드 모드 도움말
            case GameMode.Speed:
                maxPage = 2;
                switch (currentPage)
                {
                    case 1:
                        help_Description.text = "스피드 1페이지";
                        break;

                    case 2:
                        //2페이지 내용
                        help_Description.text = "스피드 2페이지";
                        break;
                }
                break;

            //메모리 모드 도움말
            case GameMode.Memory:
                maxPage = 1;
                switch (currentPage)
                {
                    case 1:

                        break;
                }
                break;

            //2인모드 도움맒
            case GameMode.Multi:
                maxPage = 1;
                switch (currentPage)
                {
                    case 1:

                        break;
                }
                break;
        }

        page_Text.text = $"{currentPage}/{maxPage}";
    }

    #endregion

}
