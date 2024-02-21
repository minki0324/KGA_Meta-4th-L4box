using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Bg_Canvas�� ���� ��ũ��Ʈ

public class Help_Canvas : MonoBehaviour
{
    [Header("Main Canvas")]
    [SerializeField] private Canvas mainCanvas;

    [Header("ĵ����")]
    [SerializeField] private PushPush_Canvas pushpush_Canvas;
    [SerializeField] private Speed_Canvas speed_Canvas;
    [SerializeField] private Memory_Canvas memory_Canvas;
    [SerializeField] private Bomb bomb_Canvas;


    [Header("���� â")]
    [SerializeField] private GameObject help_Panel;

    [SerializeField] private Button help_Btn;

    [SerializeField] private TMP_Text help_Description;

    [SerializeField] private TMP_Text page_Text;

    [Header("�ڷΰ���&���� ��ư")]
    public Button Back_Btn;
    public Button Help_Btn;

    //����â�� �����ִ°� �Ǵ��ϴ� ����, ���� â�� ���������� �� �� ��� ��ư ��Ȱ��ȭ
    public bool bisHelpPanelOn = false;

    //���� ������ ��ȣ
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

    //���� �ϴ� �ڷΰ��� ��ư Ŭ�� �� ȣ��Ǵ� �޼ҵ�
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


    //�����ϴ� ���� ��ư ������ ȣ��� �޼ҵ�
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


    //���� â�� xŰ(BackBtn) ������ ȣ��� �޼ҵ�
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


    //���� â�� Next(����) ��ư ������ ȣ��� �޼ҵ�
    public void NextBtn_Clicked()
    {     
        if(currentPage < maxPage)
        {
            currentPage += 1;
            Help_Scripts();
        }
     
    }


    //���� â�� Previous(����) ��ư ������ ȣ��� �޼ҵ�
    public void PreviousBtn_Clicked()
    {
        if (currentPage > 1)
        {
            currentPage -= 1;
            Help_Scripts();
        }
    }


    //���� ��ư ������ ������ ��ũ��Ʈ 
    private void Help_Scripts()
    {
        switch (GameManager.Instance.gameMode)
        {

            //Ǫ��Ǫ�ø�� ����
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

            //���ǵ� ��� ����
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

            //�޸� ��� ����
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

            //2�θ�� ����
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
