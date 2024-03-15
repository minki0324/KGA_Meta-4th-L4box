using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Bg_Canvas에 들어가는 스크립트

public class HelpScriptManager : MonoBehaviour
{ // Help Panel Script
    [Header("Main Canvas")]
    [SerializeField] private Canvas mainCanvas;

    [Header("캔버스")]
    [SerializeField] private PushPush_Canvas pushpush_Canvas;
    [SerializeField] private SelectListSetting speed_Canvas;
    [SerializeField] private Memory_Canvas memory_Canvas;
    [SerializeField] private MultiManager bomb_Canvas;

    [Header("도움말 창")]
    [SerializeField] private GameObject help_Panel;
    [SerializeField] private Button help_Btn;
    [SerializeField] private Image help_Image;
    [SerializeField] private TMP_Text help_Description;
    [SerializeField] private TMP_Text page_Text;

    [Header("버튼")]
    public Button Back_Btn;
    public Button Help_Btn;
    [SerializeField] private Button previous_Btn;
    [SerializeField] private Button next_Btn;

    [Header("도움말 팝업창 이미지 리스트")]
    [SerializeField] private List<Sprite> pushpushImage_List;
    [SerializeField] private List<Sprite> speedImage_List;
    [SerializeField] private List<Sprite> memoryImage_List;
    [SerializeField] private List<Sprite> bombImage_List;

    //도움말창이 켜져있는가 판단하는 변수, 도움말 창이 켜져있으면 그 외 모든 버튼 비활성화
    public bool bisHelpPanelOn = false;

    //도움말 페이지 번호
    public int maxPage;
    public int currentPage;

    #region Unity Callback
    private void OnEnable()
    {

        Button_Enable();
        switch(GameManager.Instance.GameMode)
        {
            case GameMode.PushPush:
              //  pushMode_Panel.SetActive(true);
                break;

            case GameMode.Speed:
             //   speedMode_Panel.SetActive(true);
                break;

            case GameMode.Memory:
                //memoryMode_Panel.SetActive(true);
                break;
        }
    }

    #endregion

    #region Other Method
    //좌측 하단 뒤로가기 버튼 클릭 시 호출되는 메소드
    /*public void BackBtn_Clicked()
    {
        if(!bisHelpPanelOn)
        {
            //GameManager.Instance.gameMode = Mode.None;

            switch (GameManager.Instance.GameMode)
            {
                case GameMode.PushPush:
                    pushpush_Canvas.BackBtn_Clicked();
                    break;

                case GameMode.Speed:
                    //speed_Canvas.BackBtn_Clicked();
                    break;

                case GameMode.Memory:
                    memory_Canvas.BackBtn_Clicked();
                    break;

                case GameMode.Multi:
                    //bomb_Canvas.BackBtn_Clicked();
                    break;
            }
        }
    }*/

    //우측하단 도움말 버튼 눌리면 호출될 메소드
    /*public void HelpBtn_Clicked()
    {     
        if(!bisHelpPanelOn)
        {
            AudioManager.instance.SetCommonAudioClip_SFX(3);

            help_Panel.SetActive(true);
            // 활성화 전용
            bisHelpPanelOn = true; 
            help_Btn.enabled = false;
            Back_Btn.enabled = false;
            currentPage = 1;
            Help_Window();

            switch (GameManager.Instance.GameMode)
            {
                case GameMode.PushPush:
                    pushpush_Canvas.Disable_Objects();
                    break;

                case GameMode.Speed:
                    speed_Canvas.Disable_Objects();
                    break;

                case GameMode.Memory:
                    memory_Canvas.DisalbeObjects();
                    break;

                case GameMode.Multi:
                    //bomb_Canvas.Disable_Objects();
                    break;
            }
        }
    }*/

    //도움말 창의 x키(BackBtn) 눌리면 호출될 메소드
    /*public void Help_BackBtn_Clicked()
    {
        if(bisHelpPanelOn)
        {
            AudioManager.instance.SetCommonAudioClip_SFX(3);
            help_Btn.enabled = true;
            Back_Btn.enabled = true;
            bisHelpPanelOn = false;
            help_Panel.SetActive(false);

            switch (GameManager.Instance.GameMode)
            {
                case GameMode.PushPush:
                    pushpush_Canvas.Enable_Objects();
                    break;

                case GameMode.Speed:
                    speed_Canvas.Enable_Objects();
                    break;

                case GameMode.Memory:
                    memory_Canvas.EnableObjects();
                    break;

                case GameMode.Multi:
                    //bomb_Canvas.Enable_Objects();
                    break;
            }
        }
    }*/

    //도움말 창의 Next(다음) 버튼 눌리면 호출될 메소드
    public void NextBtn_Clicked()
    {     
        if(currentPage < maxPage)
        {
            AudioManager.Instance.SetCommonAudioClip_SFX(3);
            currentPage += 1;
            Help_Window();

            previous_Btn.interactable = true;
            if(currentPage >= maxPage)
            {
                next_Btn.interactable = false;
            }
            else
            {
                next_Btn.interactable = true;
            }
        }
    }

    //도움말 창의 Previous(이전) 버튼 눌리면 호출될 메소드
    public void PreviousBtn_Clicked()
    {
        if (currentPage > 1)
        {
            AudioManager.Instance.SetCommonAudioClip_SFX(3);
            currentPage -= 1;
            Help_Window();

            next_Btn.interactable = true;
            if(currentPage <= 1)
            {
                previous_Btn.interactable = false;
            }
            else
            {
                previous_Btn.interactable = true;
            }
        }
    }

    //헬프 버튼 누르면 나오는 스크립트 
    private void Help_Window()
    {
        //도움말 텍스트는 json 파일에
        //도움말 이미지의 경우 리스트에 순차적으로 넣어주세요 :)
        previous_Btn.interactable = false;
        next_Btn.interactable = true;

        switch (GameManager.Instance.GameMode)
        {

            //푸시푸시모드 도움말
            case GameMode.PushPush:
                maxPage = DataManager.Instance.helpScripts_List[0].script.Count;

                for(int i = 0; i < DataManager.Instance.helpScripts_List[0].script.Count; i++)
                {
                    if(currentPage == DataManager.Instance.helpScripts_List[0].script[i].pageNum)
                    {
                        help_Description.text = $"{currentPage}. {DataManager.Instance.helpScripts_List[0].script[i].content}";
                        help_Image.sprite = pushpushImage_List[i];
                    }
                }
                break;

            //스피드 모드 도움말
            case GameMode.Speed:
                maxPage = DataManager.Instance.helpScripts_List[1].script.Count;

                for (int i = 0; i < DataManager.Instance.helpScripts_List[1].script.Count; i++)
                {
                    if (currentPage == DataManager.Instance.helpScripts_List[1].script[i].pageNum)
                    {
                        help_Description.text = $"{currentPage}. {DataManager.Instance.helpScripts_List[1].script[i].content}";
                        help_Image.sprite = speedImage_List[i];
                    }
                }
                break;

            //메모리 모드 도움말
            case GameMode.Memory:
                maxPage = DataManager.Instance.helpScripts_List[2].script.Count;

                for (int i = 0; i < DataManager.Instance.helpScripts_List[2].script.Count; i++)
                {
                    if (currentPage == DataManager.Instance.helpScripts_List[2].script[i].pageNum)
                    {
                        help_Description.text = $"{currentPage}. {DataManager.Instance.helpScripts_List[2].script[i].content}";
                        help_Image.sprite = memoryImage_List[i];
                    }
                }
                break;

            //2인모드 도움맒
            case GameMode.Multi:
                maxPage = DataManager.Instance.helpScripts_List[3].script.Count;

                for (int i = 0; i < DataManager.Instance.helpScripts_List[3].script.Count; i++)
                {
                    if (currentPage == DataManager.Instance.helpScripts_List[3].script[i].pageNum)
                    {
                        help_Description.text = $"{currentPage}. {DataManager.Instance.helpScripts_List[3].script[i].content}";
                        help_Image.sprite = bombImage_List[i];
                    }
                }
                break;
        }

        page_Text.text = $"{currentPage}/{maxPage}";
    }




    public void Button_Enable()
    {
        Back_Btn.interactable = true;
        help_Btn.interactable = true;

        Back_Btn.GetComponent<Image>().color = new Color(255f, 255f, 255f);
        help_Btn.GetComponent<Image>().color = new Color(255f, 255f, 255f);

    }

    public void Button_Disable()
    {
        Back_Btn.interactable = false;
        help_Btn.interactable = false;

        Back_Btn.GetComponent<Image>().color = new Color(188f, 188f, 188f);
        help_Btn.GetComponent<Image>().color = new Color(188f, 188f, 188f);
    }



    #endregion

}
