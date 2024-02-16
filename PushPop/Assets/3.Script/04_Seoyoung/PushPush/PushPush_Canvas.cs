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
    [SerializeField] private GameObject pushpushGame_Panel;
    [SerializeField] private GameObject bubblePanel;

    [Header("ScrollView")]
    [SerializeField] private ScrollRect selectCategory_ScrollView;
    [SerializeField] private GameObject content_ScrollView;

    [Header("Button")]
    [SerializeField] private Button next_Btn;
    [SerializeField] private Button prievious_Btn;
    [SerializeField] private Button gameStart_Btn;
    [SerializeField] private Button Back_Btn;

    [Header("Selected Mold Icon Image & Text")]
    [SerializeField] private TMP_Text selectedCategory_Text;    //선택된 카테고리 텍스트

    [SerializeField] private Image selectedMoldIcon_Image;      //선택된 몰드 아이콘 이미지
    [SerializeField] private TMP_Text selectedMoldIcon_Text;    //선택된 몰드 텍스트
    [SerializeField] private TMP_Text Page_Text;                //페이지 텍스트


    [Header("Category Icon(Btn) List")]
    [SerializeField] private List<Button> categoryBtn_List;

    [Header("Category Icon List(Iamge & Text)")]
    [SerializeField] private List<Sprite> categoryIcon_List;
    [SerializeField] private List<TMP_Text> categoryText_List;

    //이미지를 불러와 리스트 하나로 동적으로 리스트를 변경
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

    [SerializeField] private List<Sprite> moldIcon_List;    //선택된 카테고리 몰드 아이콘 리스트
    [SerializeField] private PuzzleLozic puzzleLozic;

    //푸쉬푸쉬 게임에 넘겨줄 이미지
    public Sprite SelectedMold { get; private set; }
    [SerializeField] private GameObject blurPanel;
    int currentPage;
    int maxPage;

  
    //도움말/뒤로가기 패널 버튼들 비활성화 추가하기

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
        pushpushGame_Panel.SetActive(false);

        currentPage = 1;
        maxPage = moldIcon_List.Count;
        Page_Text.text = $"{currentPage}/{maxPage}";
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

        //Catetory Icon리스트의 원소이름기준 오름차순 재정렬
        // categoryIcon_List = categoryIcon_List.OrderBy(x => x.name).ToList();
        
        // gameObject.SetActive(false);

      
        for (int i = 0; i < content_ScrollView.transform.childCount; i++)
        {
            //버튼 리스트 초기화
            categoryBtn_List.Add(content_ScrollView.transform.GetChild(i).transform.GetChild(0).GetComponent<Button>());
            categoryBtn_List[i].GetComponent<Image>().sprite = categoryIcon_List[i];


            //텍스트 리스트 초기화
            categoryText_List.Add(categoryBtn_List[i].transform.GetChild(0).GetComponent<TMP_Text>());
            categoryText_List[i].text = Mold_Dictionary.instance.category_Dictionary[int.Parse(categoryIcon_List[i].name)];

            //버튼 이벤트 초기화     
            int temp = i;
            categoryBtn_List[i].onClick.AddListener(delegate { CategoryIcon_Clicked(int.Parse(categoryIcon_List[temp].name)); });

        }

    }

    //카테고리 아이콘(버튼) 클릭 시 호출되는 메소드
    public void CategoryIcon_Clicked(int key)
    {
        if (!help_Canvas.bisHelpPanelOn)
        {     
            //key는 카테고리 딕셔너리 key값
            moldIcon_List.Clear();


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
            blurPanel.SetActive(true);
            selectedMoldIcon_Image.sprite = moldIcon_List[currentPage - 1];
            selectedMoldIcon_Text.text = Mold_Dictionary.instance.icon_Dictionry[int.Parse(moldIcon_List[currentPage - 1].name)];
            Page_Text.text = $"{currentPage}/{maxPage}";


            //버튼 enable = false 함수
            Disable_Objects();

            help_Canvas.Back_Btn.enabled = false;
            help_Canvas.Help_Btn.enabled = false;
        }

    }

    //몰드 선택 패널에서 다음 버튼을 클릭 시 호출되는 메소드
    public void NextBtn_Clicked()
    {
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

    //몰드 선택 패널에서 이전 버튼을 클릭 시 호출되는 메소드
    public void PreviousBtn_Clicked()
    {
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
        blurPanel.SetActive(false);
        selectCategory_Panel.SetActive(false);
        selectMold_Panel.SetActive(false);
        help_Canvas.gameObject.SetActive(false);
    }

    //몰드 선택 패널에서 게임시작 버튼을 누르면 호출되는 메소드
    public void GameStartBtn_Clicked()
    {
        SelectedMold = selectedMoldIcon_Image.sprite;

        //pushpushGame_Panel.SetActive(true);
        /*selectCategory_Panel.SetActive(false);
        selectMold_Panel.SetActive(false);
        help_Canvas.gameObject.SetActive(false);*/
        //PushPush 게임 진입
        int puzzleIDIndex = int.Parse(moldIcon_List[currentPage - 1].name);
        puzzleLozic.SelectPuzzleButton(puzzleIDIndex);
        GameManager.Instance.PushPushMode();
    }

    //몰드 선택 패널에서 x버튼을 누르면 호출되는 메소드
    public void SelectMold_BackBtn_Clicked()
    {
        if (!help_Canvas.bisHelpPanelOn)
        {
            //버튼 enable = true 함수
            Enable_Objects();

            help_Canvas.Back_Btn.enabled = true;
            help_Canvas.Help_Btn.enabled = true;

            selectMold_Panel.SetActive(false);
            moldIcon_List.Clear();
            next_Btn.enabled = true;
            Back_Btn.enabled = true;
        }

    }

    //좌측 하단 뒤로가기 버튼 클릭 시 호출되는 함수
    public void BackBtn_Clicked()
    {
        main_Canvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }


    //우측 하단 도움말 버튼 클릭 시 호출되는 함수 :  도움말 버튼 눌리면 그 외 버튼 비활성화
    public void Disable_Objects()
    {
        for (int i = 0; i < categoryBtn_List.Count; i++)
        {
            categoryBtn_List[i].enabled = false;
        }

        selectCategory_ScrollView.enabled = false;
        selectCategory_ScrollView.transform.GetChild(1).GetComponent<Scrollbar>().interactable = false;
    }


    //도움말 창의 뒤로가기 버튼 클릭 시 호출되는 함수 :  그 외 버튼 활성화
    public void Enable_Objects()
    {
        for (int i = 0; i < categoryBtn_List.Count; i++)
        {
            categoryBtn_List[i].enabled = true;
        }

        selectCategory_ScrollView.enabled = true;
        selectCategory_ScrollView.transform.GetChild(1).GetComponent<Scrollbar>().interactable = true;
    }


   

    #endregion

}
