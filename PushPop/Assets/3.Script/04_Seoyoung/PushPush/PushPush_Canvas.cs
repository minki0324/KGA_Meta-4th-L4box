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
    [SerializeField] private Canvas background_Canvas;

    [Header("Panel")]
    [SerializeField] private GameObject selectCategory_Panel;
    [SerializeField] private GameObject selectMold_Panel;

    [Header("ScrollView")]
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

    

    //이 친구는 Resource폴더에서 이미지를 불러와 리스트 하나로 동적으로 리스트를 변경할지 or 그냥 카테고리별 몰드 아이콘 리스트를 다 만들어 쓸지 생각할 것. 임시로 해산물 넣었슴당
    [Header("Mold Icon(Image) List")]
    [SerializeField] private List<Sprite> moldIcon_List;

    [SerializeField] private PuzzleLozic puzzleLozic;


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
        background_Canvas.gameObject.SetActive(true);
        selectMold_Panel.SetActive(false);

        currentPage = 1;
        maxPage = moldIcon_List.Count;
        Page_Text.text = $"{currentPage}/{maxPage}";
    }


    private void OnDisable()
    {
        //background_Canvas.gameObject.SetActive(false);
    }

    #endregion

    #region Other Method

    private void Init()
    {
        
        // gameObject.SetActive(false);

      
        for (int i = 0; i < content_ScrollView.transform.childCount; i++)
        {
            //버튼 리스트 초기화
            categoryBtn_List.Add(content_ScrollView.transform.GetChild(i).GetComponent<Button>());
            categoryBtn_List[i].GetComponent<Image>().sprite = categoryIcon_List[i];
     
        }

        //Catetory Icon 리스트의 원소이름으로 재정렬하기..
        

        for (int i = 0; i < content_ScrollView.transform.childCount; i++)
        {
            //텍스트 리스트 초기화
            categoryText_List.Add(categoryBtn_List[i].transform.GetChild(0).GetComponent<TMP_Text>());
            categoryText_List[i].text = categoryIcon_List[i].name;

            //버튼 이벤트 초기화
            categoryBtn_List[i].onClick.AddListener(delegate { CategoryIcon_Clicked(i); });
        }




    }


    public void SortText(string a, string b)
    {

    }

    //카테고리 아이콘(버튼) 클릭 시 호출되는 메소드
    public void CategoryIcon_Clicked(int index)
    {
        //일단 무조건 해산물 리스트 호출
        selectMold_Panel.SetActive(true);

        Debug.Log(index);
        selectedCategory_Text.text = categoryText_List[index].name;
        

        selectedMoldIcon_Image.sprite = moldIcon_List[currentPage - 1];
        selectedMoldIcon_Text.text = moldIcon_List[currentPage - 1].name;
        Page_Text.text = $"{currentPage}/{maxPage}";

    }

    //몰드 선택 패널에서 다음 버튼을 클릭 시 호출되는 메소드
    public void NextBtn_Clicked()
    {
        if (currentPage < maxPage)
        {
            prievious_Btn.enabled = true;
            currentPage += 1;

            selectedMoldIcon_Image.sprite = moldIcon_List[currentPage - 1];
            selectedMoldIcon_Text.text = moldIcon_List[currentPage - 1].name;
            Page_Text.text = $"{currentPage}/{maxPage}";
        }

        if(currentPage == maxPage)
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
        if(currentPage > 0)
        {
            next_Btn.enabled = true;
            currentPage -= 1;

            selectedMoldIcon_Image.sprite = moldIcon_List[currentPage - 1];
            selectedMoldIcon_Text.text = moldIcon_List[currentPage - 1].name;
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


    //몰드 선택 패널에서 게임시작 버튼을 누르면 호출되는 메소드
    public void GameStartBtn_Clicked()
    {
        //PushPush 게임 진입
        int puzzleIDIndex = int.Parse(moldIcon_List[currentPage - 1].name);
        Debug.Log(puzzleIDIndex);
        puzzleLozic.SelectPuzzleButton(puzzleIDIndex);

    }

    //몰드 선택 패널에서 x버튼을 누르면 호출되는 메소드
    public void BackBtn_Clicked()
    {
        selectMold_Panel.SetActive(false);
    }



    #endregion

}
