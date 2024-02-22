using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Speed_Canvas : MonoBehaviour
{

    public enum Difficulty
    {
        Easy = 0,
        Normal,
        Hard
    }

    public Difficulty difficulty;

    [Header("�г�")]
    [SerializeField] private GameObject selectDifficulty_Panel;
    [SerializeField] private GameObject selectCategory_Panel;
    [SerializeField] private GameObject ready_Panel;
    [SerializeField] private GameObject speedGame_Panel;

    [Header("ĵ����")]
    [SerializeField] private Canvas main_Canvas;
    [SerializeField] private Help_Canvas help_Canvas;

    [Header("���� ������Ʈ/������")]
    [SerializeField] private GameObject Content;
    [SerializeField] private GameObject Mold_Prefab;

    [Header("�ؽ�Ʈ")]
    [SerializeField] private TMP_Text Difficulty_Text;
    [SerializeField] private TMP_Text SelectMold_Text;

    [Header("��ũ�Ѻ�")]
    [SerializeField] private ScrollRect SelectCategory_ScrollView;

    [Header("��ư")]
    [SerializeField] private List<Button> Difficulty_Btn;

    [Header("Ready�г� ����")]
    [SerializeField] private Image selected_Image;
    [SerializeField] private TMP_Text selected_Text;

    [Header("���̵� �� ������ ����Ʈ")]
    [SerializeField] private List<Sprite> easyIcon_List;
    [SerializeField] private List<Sprite> normalIcon_List;
    [SerializeField] private List<Sprite> hardIcon_List;

    [SerializeField] private List<Button> iconButton_List;


   
    //���ǵ���ӿ� �Ѱ��� ����
    public Sprite moldIcon { get; private set; }



    //ī�װ� ����â�� �������� ī�װ��� ����, ���������� ����ĵ������ ���ư�
    public bool bSelectCategoryPanel_On = false;


    #region Unity Callback

    private void OnEnable()
    {
        selectDifficulty_Panel.SetActive(true);
        help_Canvas.gameObject.SetActive(true);
        help_Canvas.transform.SetParent(gameObject.transform);
        help_Canvas.transform.SetSiblingIndex(3);
    }

    private void Start()
    {
        iconButton_List = new List<Button>();
        selectCategory_Panel.SetActive(false);
        ready_Panel.SetActive(false);
        speedGame_Panel.SetActive(false);

        selectDifficulty_Panel.SetActive(true);
        help_Canvas.gameObject.SetActive(true);
        //gameObject.SetActive(false);
    }



    private void OnDisable()
    {
        help_Canvas.gameObject.SetActive(false);
        selectCategory_Panel.SetActive(false);

    }

    #endregion

    #region Other Method

    //���̵� �񴰹�� (����/����/�����)��ư ������ �� ȣ��Ǵ� �Լ� : ����(0), ����(1), �����(2)�� �Ű������� ��
    public void DifficultyBtn_Clicked(int index)
    {
        if (!help_Canvas.bisHelpPanelOn)
        {
            selectCategory_Panel.SetActive(true);
            bSelectCategoryPanel_On = true;

            //-----------�ڷΰ��⿡ ���� ��----------------------------------
            for (int i = 0; i < Content.transform.childCount; i++)
            {
                Destroy(Content.transform.GetChild(i).gameObject);
            }
            iconButton_List.Clear();

            //-------------------------------------------------------------

            switch (index)
            {
                case 0:
                    difficulty = Difficulty.Easy;

                    Difficulty_Text.text = "����";
                    SelectMold_Text.text = "����";
                    for (int i = 0; i < easyIcon_List.Count; i++)
                    {
                        GameObject a = Instantiate(Mold_Prefab, Content.transform);

                        //��ư ����Ʈ �ʱ�ȭ
                        iconButton_List.Add(a.transform.GetChild(0).GetComponent<Button>());


                        //��������Ʈ ����
                        a.transform.GetChild(0).GetComponent<Image>().sprite = easyIcon_List[i];

                        //�ؽ�Ʈ ���� : ��������Ʈ �̸��� Ű������ value��������
                        a.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = Mold_Dictionary.instance.icon_Dictionry[int.Parse(easyIcon_List[i].name)];
                    }

                    break;

                case 1:
                    difficulty = Difficulty.Normal;

                    Difficulty_Text.text = "����";
                    SelectMold_Text.text = "����";
                    for (int i = 0; i < normalIcon_List.Count; i++)
                    {
                        GameObject a = Instantiate(Mold_Prefab, Content.transform);

                        //��ư ����Ʈ �ʱ�ȭ
                        iconButton_List.Add(a.transform.GetChild(0).GetComponent<Button>());

                        //��������Ʈ ����
                        a.transform.GetChild(0).GetComponent<Image>().sprite = normalIcon_List[i];

                        //�ؽ�Ʈ ����
                        a.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = Mold_Dictionary.instance.icon_Dictionry[int.Parse(normalIcon_List[i].name)];
                    }
                    break;

                case 2:
                    difficulty = Difficulty.Hard;

                    Difficulty_Text.text = "�����";
                    SelectMold_Text.text = "�����";
                    for (int i = 0; i < hardIcon_List.Count; i++)
                    {
                        GameObject a = Instantiate(Mold_Prefab, Content.transform);

                        //��ư ����Ʈ �ʱ�ȭ
                        iconButton_List.Add(a.transform.GetChild(0).GetComponent<Button>());

                        //��������Ʈ ����
                        a.transform.GetChild(0).GetComponent<Image>().sprite = hardIcon_List[i];

                        //�ؽ�Ʈ ����
                        a.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = Mold_Dictionary.instance.icon_Dictionry[int.Parse(hardIcon_List[i].name)];
                    }
                    break;
            }

            for (int i = 0; i < iconButton_List.Count; i++)
            {
                int temp = i;
                iconButton_List[temp].onClick.AddListener(delegate { IconBtn_Clicked(iconButton_List[temp].gameObject); });
            }
        }


    }


    //���� ������ ������ �� ȣ��Ǵ� �Լ�
    public void IconBtn_Clicked(GameObject button)
    {
        if (!help_Canvas.bisHelpPanelOn)
        {
            ready_Panel.SetActive(true);


            help_Canvas.Back_Btn.interactable = false;
            help_Canvas.Help_Btn.interactable = false;
           

            selected_Image.sprite = button.GetComponent<Image>().sprite;
            selected_Text.text = button.transform.GetChild(0).GetComponent<TMP_Text>().text;

            moldIcon = selected_Image.sprite;
        }
    }


    //�غ�â�� x�� ������ �� ȣ��Ǵ� �Լ�
    public void ReadyPanel_BackBtn_Clicked()
    {
        if (!help_Canvas.bisHelpPanelOn)
        {
            ready_Panel.SetActive(false);
            help_Canvas.Back_Btn.interactable = true;
            help_Canvas.Help_Btn.interactable = true;
        }
    }


    //�غ�â�� ���ӽ��� ��ư ������ �� ȣ��Ǵ� �Լ�
    public void GameStartBtn_Clicked()
    {
        Debug.Log("�յ�");
        speedGame_Panel.SetActive(true);
        selectCategory_Panel.SetActive(false);
        selectDifficulty_Panel.SetActive(false);
        ready_Panel.SetActive(false);
        bSelectCategoryPanel_On = false;
        help_Canvas.gameObject.SetActive(false);

        PushPop.Instance.boardSprite = moldIcon; // pushpop
        GameManager.Instance.SpeedMode(); // Speed Mode start
    }


    //�����ϴ� �ڷΰ��� ��ư�� ������ �� ȣ��Ǵ� �Լ�
    public void BackBtn_Clicked()
    {
        //���̵� ����â�� �����ְ� ī�װ� ����â�� ���������� ����ȭ�����ΰ���
        //ī�װ� ����â�� ���������� ī�װ� ����â ����
        if (!help_Canvas.bisHelpPanelOn)
        {
            if (bSelectCategoryPanel_On)
            {
                selectCategory_Panel.SetActive(false);
                bSelectCategoryPanel_On = false;
            }
            else
            {
                help_Canvas.transform.SetParent(null);
                help_Canvas.transform.SetAsLastSibling();

                gameObject.SetActive(false);
                main_Canvas.gameObject.SetActive(true);
            }
        }

    }


    //���� �ϴ� �ڷΰ��� ��ư Ŭ�� �� ȣ��Ǵ� �Լ� :  ���� ��ư ������ �� �� ��ư ��Ȱ��ȭ
    public void Disable_Objects()
    {
        for (int i = 0; i < iconButton_List.Count; i++)
        {
            iconButton_List[i].interactable = false;
        }


        for(int i = 0; i<Difficulty_Btn.Count; i++)
        {
            Difficulty_Btn[i].interactable = false;
        }

        SelectCategory_ScrollView.enabled = false;
        SelectCategory_ScrollView.transform.GetChild(1).GetComponent<Scrollbar>().interactable = false;
    }


    //���� â�� �ڷΰ��� ��ư Ŭ�� �� ȣ��Ǵ� �Լ� :  �� �� ��ư Ȱ��ȭ
    public void Enable_Objects()
    {
        for (int i = 0; i < iconButton_List.Count; i++)
        {
            iconButton_List[i].interactable = true;
        }


        for (int i = 0; i < Difficulty_Btn.Count; i++)
        {
            Difficulty_Btn[i].interactable = true;
        }

        SelectCategory_ScrollView.enabled = true;
        SelectCategory_ScrollView.transform.GetChild(1).GetComponent<Scrollbar>().interactable = true;

    }


    #endregion

}
