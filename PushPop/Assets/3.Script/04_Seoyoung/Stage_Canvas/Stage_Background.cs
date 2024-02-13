using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Bg_Canvas�� ���� ��ũ��Ʈ

public class Stage_Background : MonoBehaviour
{
    [Header("Main Canvas")]
    [SerializeField] private Canvas mainCanvas;


    [Header("���� â")]
    [SerializeField] private GameObject help_Panel;

    [SerializeField] private Button help_Btn;

    [SerializeField] private TMP_Text help_Description;

    [SerializeField] private TMP_Text page_Text;

    //����â�� �����ִ°� �Ǵ��ϴ� ����
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
        switch(GameManager.instance.gameMode)
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

    private void OnDisable()
    {
       // pushMode_Panel.SetActive(false);

    }
    #endregion

    #region Other Method

    private void Init()
    {   
        help_Panel.SetActive(false);
        gameObject.SetActive(false);        
    }

    public void BackBtn_Clicked()
    {
        mainCanvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }


    public void HelpBtn_Clicked()
    {
        //Help(����) ��ư ������ ȣ��� �޼ҵ�
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
        //���� â�� xŰ(BackBtn) ������ ȣ��� �޼ҵ�
        help_Panel.SetActive(false);
        help_Btn.enabled = true;
    }

    public void NextBtn_Clicked()
    {
        //Next(����) ��ư ������ ȣ��� �޼ҵ�
        currentPage += 1;
        Help_Scripts();
    }

    public void PreviousBtn_Clicked()
    {
        //Previous(����) ��ư ������ ȣ��� �޼ҵ�
        currentPage -= 1;
        Help_Scripts();
    }


    private void Help_Scripts()
    {
        //���� ��ư ������ ������ ��ũ��Ʈ 
        switch (GameManager.instance.gameMode)
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
        }

        page_Text.text = $"{currentPage}/{maxPage}";
    }

    #endregion

}
