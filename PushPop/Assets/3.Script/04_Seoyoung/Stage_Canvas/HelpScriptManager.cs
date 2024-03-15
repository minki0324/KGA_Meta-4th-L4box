using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Bg_Canvas�� ���� ��ũ��Ʈ

public class HelpScriptManager : MonoBehaviour
{ // Help Panel Script
    [Header("Main Canvas")]
    [SerializeField] private Canvas mainCanvas;

    [Header("ĵ����")]
    [SerializeField] private PushPush_Canvas pushpush_Canvas;
    [SerializeField] private SelectListSetting speed_Canvas;
    [SerializeField] private Memory_Canvas memory_Canvas;
    [SerializeField] private MultiManager bomb_Canvas;

    [Header("���� â")]
    [SerializeField] private GameObject help_Panel;
    [SerializeField] private Button help_Btn;
    [SerializeField] private Image help_Image;
    [SerializeField] private TMP_Text help_Description;
    [SerializeField] private TMP_Text page_Text;

    [Header("��ư")]
    public Button Back_Btn;
    public Button Help_Btn;
    [SerializeField] private Button previous_Btn;
    [SerializeField] private Button next_Btn;

    [Header("���� �˾�â �̹��� ����Ʈ")]
    [SerializeField] private List<Sprite> pushpushImage_List;
    [SerializeField] private List<Sprite> speedImage_List;
    [SerializeField] private List<Sprite> memoryImage_List;
    [SerializeField] private List<Sprite> bombImage_List;

    //����â�� �����ִ°� �Ǵ��ϴ� ����, ���� â�� ���������� �� �� ��� ��ư ��Ȱ��ȭ
    public bool bisHelpPanelOn = false;

    //���� ������ ��ȣ
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
    //���� �ϴ� �ڷΰ��� ��ư Ŭ�� �� ȣ��Ǵ� �޼ҵ�
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

    //�����ϴ� ���� ��ư ������ ȣ��� �޼ҵ�
    /*public void HelpBtn_Clicked()
    {     
        if(!bisHelpPanelOn)
        {
            AudioManager.instance.SetCommonAudioClip_SFX(3);

            help_Panel.SetActive(true);
            // Ȱ��ȭ ����
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

    //���� â�� xŰ(BackBtn) ������ ȣ��� �޼ҵ�
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

    //���� â�� Next(����) ��ư ������ ȣ��� �޼ҵ�
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

    //���� â�� Previous(����) ��ư ������ ȣ��� �޼ҵ�
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

    //���� ��ư ������ ������ ��ũ��Ʈ 
    private void Help_Window()
    {
        //���� �ؽ�Ʈ�� json ���Ͽ�
        //���� �̹����� ��� ����Ʈ�� ���������� �־��ּ��� :)
        previous_Btn.interactable = false;
        next_Btn.interactable = true;

        switch (GameManager.Instance.GameMode)
        {

            //Ǫ��Ǫ�ø�� ����
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

            //���ǵ� ��� ����
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

            //�޸� ��� ����
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

            //2�θ�� ����
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
