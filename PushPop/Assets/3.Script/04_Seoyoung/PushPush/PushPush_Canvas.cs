using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class PushPush_Canvas : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private Canvas main_Canvas;
    [SerializeField] private Help_Canvas help_Canvas;

    [Header("Panel")]
    [SerializeField] private GameObject selectCategory_Panel;
    [SerializeField] private GameObject selectMold_Panel;
    [SerializeField] private GameObject puzzle_Panel;
    [SerializeField] private GameObject pushpushGame_Panel;
    [SerializeField] private GameObject Warning_Panel;
    [SerializeField] private GameObject bubblePanel;


    [Header("ScrollView")]
    [SerializeField] private ScrollRect selectCategory_ScrollView;
    [SerializeField] private GameObject content_ScrollView;
    [SerializeField] private GameObject categoryIcon_Prefab;

    [Header("Button")]
    [SerializeField] private Button next_Btn;
    [SerializeField] private Button prievious_Btn;
    [SerializeField] private Button gameStart_Btn;
    [SerializeField] private Button Back_Btn;
    [SerializeField] private Button GameBack_Btn;   //���� ���� �� �ߴ� �����ϴ� �ڷΰ���
    [SerializeField] private Button GoOut_Btn;
    [SerializeField] private Button Cancle_Btn;

    [Header("Selected Mold Icon Image & Text")]
    [SerializeField] private TMP_Text selectedCategory_Text;    //���õ� ī�װ� �ؽ�Ʈ

    [SerializeField] private Image selectedMoldIcon_Image;      //���õ� ���� ������ �̹���
    [SerializeField] private TMP_Text selectedMoldIcon_Text;    //���õ� ���� �ؽ�Ʈ
    [SerializeField] private TMP_Text Page_Text;                //������ �ؽ�Ʈ


    [Header("Category Icon(Btn) List")]
    [SerializeField] private List<Button> categoryBtn_List;

    [Header("Category Icon List(Iamge & Text)")]
    [SerializeField] private List<Sprite> categoryIcon_List;
    [SerializeField] private List<TMP_Text> categoryText_List;

    //�̹����� �ҷ��� ����Ʈ �ϳ��� �������� ����Ʈ�� ����
    [Header("Mold Icon(Image) List")]
    [SerializeField] private List<Sprite> icon_List10;
    [SerializeField] private List<Sprite> icon_List11;
    [SerializeField] private List<Sprite> icon_List12;
    [SerializeField] private List<Sprite> icon_List13;
    [SerializeField] private List<Sprite> icon_List14;
    [SerializeField] private List<Sprite> icon_List15;
    [SerializeField] private List<Sprite> icon_List16;
    [SerializeField] private List<Sprite> icon_List17;
    [SerializeField] private List<Sprite> icon_List18;
    [SerializeField] private List<Sprite> icon_List19;

    [SerializeField] private List<Sprite> moldIcon_List;    //���õ� ī�װ� ���� ������ ����Ʈ
    [SerializeField] private PuzzleLozic puzzleLozic;

    //Ǫ��Ǫ�� ���ӿ� �Ѱ��� �̹���
    public Sprite SelectedMold { get; private set; }
    //[SerializeField] private GameObject blurPanel;
    int currentPage;
    int maxPage;

  
    //����/�ڷΰ��� �г� ��ư�� ��Ȱ��ȭ �߰��ϱ�

    #region Unity Callback

    private void Start()
    {
        Init();
        maxPage = moldIcon_List.Count;
    }

    private void OnEnable()
    {
        help_Canvas.gameObject.SetActive(true);
        selectCategory_Panel.SetActive(true);
        //background_Canvas.gameObject.SetActive(true);
        selectMold_Panel.SetActive(false);
        //pushpushGame_Panel.SetActive(false);
        GameBack_Btn.gameObject.SetActive(false);

        if(Warning_Panel.activeSelf)
        {
            Warning_Panel.SetActive(false);
        }


        help_Canvas.transform.SetParent(gameObject.transform);
        help_Canvas.transform.SetSiblingIndex(3);

        currentPage = 1;
        maxPage = moldIcon_List.Count;
        Page_Text.text = $"{currentPage}/{maxPage}";

        AudioManager.instance.SetAudioClip_BGM(1);
    }


    private void OnDisable()
    {
        help_Canvas.gameObject.SetActive(false);
        //background_Canvas.gameObject.SetActive(false);
    }

    #endregion

    #region Other Method



    private void Init()
    {
        categoryBtn_List = new List<Button>();
        moldIcon_List = new List<Sprite>();

        //Catetory Icon����Ʈ�� �����̸����� �������� ������
        // categoryIcon_List = categoryIcon_List.OrderBy(x => x.name).ToList();
        
        // gameObject.SetActive(false);

      
        for (int i = 0; i < categoryIcon_List.Count; i++)
        {
            GameObject a = Instantiate(categoryIcon_Prefab, content_ScrollView.transform);

            //��ư ����Ʈ �ʱ�ȭ
            categoryBtn_List.Add(content_ScrollView.transform.GetChild(i).transform.GetChild(0).GetComponent<Button>());
            categoryBtn_List[i].GetComponent<Image>().sprite = categoryIcon_List[i];

            //�ؽ�Ʈ ����Ʈ �ʱ�ȭ
            categoryText_List.Add(categoryBtn_List[i].transform.GetChild(0).GetComponent<TMP_Text>());
            categoryText_List[i].text = Mold_Dictionary.instance.category_Dictionary[int.Parse(categoryIcon_List[i].name)];

            //��ư �̺�Ʈ �ʱ�ȭ     
            int temp = i;
            categoryBtn_List[i].onClick.AddListener(delegate { CategoryIcon_Clicked(int.Parse(categoryIcon_List[temp].name)); });

        }

    }

    //ī�װ� ������(��ư) Ŭ�� �� ȣ��Ǵ� �޼ҵ�
    public void CategoryIcon_Clicked(int key)
    {
        if (!help_Canvas.bisHelpPanelOn)
        {     
            //key�� ī�װ� ��ųʸ� key��
            moldIcon_List.Clear();

            AudioManager.instance.SetCommonAudioClip_SFX(3);

            switch (key)
            {
               
                case 10:
                    for (int i = 0; i < icon_List10.Count; i++)
                    {
                        moldIcon_List.Add(icon_List10[i]);
                    }
                    break;

                case 11:
                    for (int i = 0; i < icon_List11.Count; i++)
                    {
                        moldIcon_List.Add(icon_List11[i]);
                    }
                    break;

                case 12:
                    for (int i = 0; i < icon_List12.Count; i++)
                    {
                        moldIcon_List.Add(icon_List12[i]);
                    }
                    break;

                case 13:
                    for (int i = 0; i < icon_List13.Count; i++)
                    {
                        moldIcon_List.Add(icon_List13[i]);
                    }
                    break;

                case 14:
                    for (int i = 0; i < icon_List14.Count; i++)
                    {
                        moldIcon_List.Add(icon_List14[i]);
                    }
                    break;

                case 15:
                    for (int i = 0; i < icon_List15.Count; i++)
                    {
                        moldIcon_List.Add(icon_List15[i]);
                    }
                    break;

                case 16:
                    for (int i = 0; i < icon_List16.Count; i++)
                    {
                        moldIcon_List.Add(icon_List16[i]);
                    }
                    break;


                case 17:
                    for (int i = 0; i < icon_List17.Count; i++)
                    {
                        moldIcon_List.Add(icon_List17[i]);
                    }
                    break;

                case 18:
                    for (int i = 0; i < icon_List18.Count; i++)
                    {
                        moldIcon_List.Add(icon_List18[i]);
                    }
                    break;

                case 19:
                    for (int i = 0; i < icon_List19.Count; i++)
                    {
                        moldIcon_List.Add(icon_List19[i]);
                    }
                    break;
            }


            selectMold_Panel.SetActive(true);

            currentPage = 1;

            maxPage = moldIcon_List.Count;
            selectedCategory_Text.text = Mold_Dictionary.instance.category_Dictionary[key];
            //blurPanel.SetActive(true);
            selectedMoldIcon_Image.sprite = moldIcon_List[currentPage - 1];
            selectedMoldIcon_Text.text = Mold_Dictionary.instance.icon_Dictionry[int.Parse(moldIcon_List[currentPage - 1].name)];
            Page_Text.text = $"{currentPage}/{maxPage}";


            //��ư enable = false �Լ�
            Disable_Objects();

            help_Canvas.Back_Btn.interactable = false;
            help_Canvas.Help_Btn.interactable = false;

        }

    }

    //���� ���� �гο��� ���� ��ư�� Ŭ�� �� ȣ��Ǵ� �޼ҵ�
    public void NextBtn_Clicked()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        if (currentPage < maxPage)
        {
            prievious_Btn.enabled = true;
            currentPage += 1;

            selectedMoldIcon_Image.sprite = moldIcon_List[currentPage - 1];
            selectedMoldIcon_Text.text = Mold_Dictionary.instance.icon_Dictionry[int.Parse(moldIcon_List[currentPage - 1].name)];
            Page_Text.text = $"{currentPage}/{maxPage}";
        }

        if (currentPage == maxPage)
        {
            next_Btn.enabled = false;
        }
        else
        {
            next_Btn.enabled = true;
        }

    }

    //���� ���� �гο��� ���� ��ư�� Ŭ�� �� ȣ��Ǵ� �޼ҵ�
    public void PreviousBtn_Clicked()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        if (currentPage > 0)
        {
            next_Btn.enabled = true;
            currentPage -= 1;

            selectedMoldIcon_Image.sprite = moldIcon_List[currentPage - 1];
            selectedMoldIcon_Text.text = Mold_Dictionary.instance.icon_Dictionry[int.Parse(moldIcon_List[currentPage - 1].name)];
            Page_Text.text = $"{currentPage}/{maxPage}";
        }


        if (currentPage == 1)
        {
            prievious_Btn.enabled = false;
        }
        else
        {
            prievious_Btn.enabled = true;
        }

    }

    public void BubbleStart_Clicked()
    {
        //blurPanel.SetActive(false);
        selectCategory_Panel.SetActive(false);
        selectMold_Panel.SetActive(false);
        help_Canvas.gameObject.SetActive(false);
    }

    //���� ���� �гο��� ���ӽ��� ��ư�� ������ ȣ��Ǵ� �޼ҵ�
    public void GameStartBtn_Clicked()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(0);
        AudioManager.instance.SetAudioClip_BGM(2);

        SelectedMold = selectedMoldIcon_Image.sprite;

        //pushpushGame_Panel.SetActive(true);
        GameBack_Btn.gameObject.SetActive(true);
        selectCategory_Panel.SetActive(false);
        selectMold_Panel.SetActive(false);
        help_Canvas.gameObject.SetActive(false);
        //PushPush ���� ����
        int puzzleIDIndex = int.Parse(moldIcon_List[currentPage - 1].name);
        puzzleLozic.SelectPuzzleButton(puzzleIDIndex);
        GameManager.Instance.PushPushMode();
    }

    //���� ���� �гο��� x��ư�� ������ ȣ��Ǵ� �޼ҵ�
    public void SelectMold_BackBtn_Clicked()
    {
        if (!help_Canvas.bisHelpPanelOn)
        {
            //��ư enable = true �Լ�
            Enable_Objects();

            AudioManager.instance.SetCommonAudioClip_SFX(3);

            help_Canvas.Back_Btn.interactable = true;
            help_Canvas.Help_Btn.interactable = true;

            selectMold_Panel.SetActive(false);
            moldIcon_List.Clear();
            next_Btn.enabled = true;
            Back_Btn.enabled = true;
        }

    }

    //���� �ϴ� �ڷΰ��� ��ư Ŭ�� �� ȣ��Ǵ� �޼ҵ�
    public void BackBtn_Clicked()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        AudioManager.instance.SetAudioClip_BGM(0);

        help_Canvas.transform.SetParent(null);
        help_Canvas.transform.SetAsLastSibling();  
        main_Canvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }


    //���� ���� �� ���� �ϴ� �ڷΰ��� ��ư Ŭ�� �� ȣ��Ǵ� �޼ҵ�
    public void Game_BackBtn_Clicked()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        Time.timeScale = 0;
        Warning_Panel.SetActive(true);
        GameBack_Btn.interactable = false;
    }


    public void GoOutBtn_Clicked()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        AudioManager.instance.SetAudioClip_BGM(1);
        Time.timeScale = 1;
        GameBack_Btn.interactable = true;
        GameBack_Btn.gameObject.SetActive(false);
        puzzle_Panel.SetActive(false);
        pushpushGame_Panel.SetActive(false);
        Warning_Panel.SetActive(false);
        selectCategory_Panel.SetActive(true);
        help_Canvas.gameObject.SetActive(true);
        Enable_Objects();
    }

    public void CancelBtn_Clicked()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        Time.timeScale = 1;
        GameBack_Btn.interactable = true;
        Warning_Panel.SetActive(false);
    }

    //���� �ϴ� ���� ��ư Ŭ�� �� ȣ��Ǵ� �Լ� :  ���� ��ư ������ �� �� ��ư ��Ȱ��ȭ
    public void Disable_Objects()
    {
        for (int i = 0; i < categoryBtn_List.Count; i++)
        {
            categoryBtn_List[i].interactable = false;
        }

        selectCategory_ScrollView.enabled = false;
        selectCategory_ScrollView.transform.GetChild(1).GetComponent<Scrollbar>().interactable = false;
    }


    //���� â�� �ڷΰ��� ��ư Ŭ�� �� ȣ��Ǵ� �Լ� :  �� �� ��ư Ȱ��ȭ
    public void Enable_Objects()
    {
        for (int i = 0; i < categoryBtn_List.Count; i++)
        {
            categoryBtn_List[i].interactable = true;
        }

        selectCategory_ScrollView.enabled = true;
        selectCategory_ScrollView.transform.GetChild(1).GetComponent<Scrollbar>().interactable = true;
    }


   

    #endregion

}
