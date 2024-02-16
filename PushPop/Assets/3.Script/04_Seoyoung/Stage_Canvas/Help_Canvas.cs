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
    [SerializeField] private Canvas memory;


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
        switch(GameManager.Instance._gameMode)
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
    private void Init()
    {   
        help_Panel.SetActive(false); 
    }

    //���� �ϴ� �ڷΰ��� ��ư Ŭ�� �� ȣ��Ǵ� �޼ҵ�
    public void BackBtn_Clicked()
    {
        if(!bisHelpPanelOn)
        {
            switch (GameManager.Instance._gameMode)
            {
                case GameMode.PushPush:
                    pushpush_Canvas.BackBtn_Clicked();
                    break;

                case GameMode.Speed:
                    speed_Canvas.BackBtn_Clicked();
                    break;

                case GameMode.Memory:

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

            /*switch(GameManager.Instance.gameMode)
            {
                case GameMode.PushPush:
                    pushpush_Canvas.Disable_Objects();
                    break;

                case GameMode.Speed:
                    speed_Canvas.Disable_Objects();
                    break;
            }    */
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


           /* switch (GameManager.Instance.gameMode)
            {
                case GameMode.PushPush:
                    pushpush_Canvas.Enable_Objects();
                    break;

                case GameMode.Speed:
                    speed_Canvas.Enable_Objects();
                    break;
            }*/

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
        /*switch (GameManager.Instance.gameMode)
        {

            //Ǫ��Ǫ�ø�� ����
            case GameMode.PushPush:
                maxPage = 2;
                switch (currentPage)
                {
                    case 1:
                        //1������ ����
                        help_Description.text = "Ǫ��Ǫ�� 1������";
                        break;

                    case 2:
                        //2������ ����
                        help_Description.text = "Ǫ��Ǫ�� 2������";
                        break;

                }
                break;

            //���ǵ� ��� ����
            case GameMode.Speed:
                maxPage = 2;
                switch (currentPage)
                {
                    case 1:
                        help_Description.text = "���ǵ� 1������";
                        break;

                    case 2:
                        //2������ ����
                        help_Description.text = "���ǵ� 2������";
                        break;
                }
                break;

            //�޸� ��� ����
            case GameMode.Memory:
                maxPage = 1;
                switch (currentPage)
                {
                    case 1:

                        break;
                }
                break;

            //2�θ�� ����
            case GameMode.Bomb:
                maxPage = 1;
                switch (currentPage)
                {
                    case 1:

                        break;
                }
                break;
        }*/

        page_Text.text = $"{currentPage}/{maxPage}";
    }

    #endregion

}
