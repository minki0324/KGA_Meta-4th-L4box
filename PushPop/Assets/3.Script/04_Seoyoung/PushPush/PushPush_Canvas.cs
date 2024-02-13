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



    [SerializeField] private Dictionary<int, string> category_Dictionary = new Dictionary<int, string>();
    [SerializeField] private Dictionary<int, string> icon_Dictionry = new Dictionary<int, string>();
   



    int currentPage;
    int maxPage;

    //도움말/뒤로가기 패널 버튼들 비활성화 추가하기

    #region Unity Callback

    private void Start()
    {
        CategoryDictionary();
        IconDictionary();
        Init();
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
        background_Canvas.gameObject.SetActive(false);
    }

    #endregion

    #region Other Method

  
    public void CategoryDictionary()
    {
        category_Dictionary = new Dictionary<int, string>();
        category_Dictionary.Add(10, "해산물");
        category_Dictionary.Add(11, "곤충");
        category_Dictionary.Add(12, "공룡");
        category_Dictionary.Add(13, "과일");
        category_Dictionary.Add(14, "동물");
        category_Dictionary.Add(15, "인형");
        category_Dictionary.Add(16, "새");
        category_Dictionary.Add(17, "차");
        category_Dictionary.Add(18, "채소");
        category_Dictionary.Add(19, "배&비행기");
    }

    public void IconDictionary()
    {
        icon_Dictionry = new Dictionary<int, string>();

        icon_Dictionry.Add(1001, "문어");
        icon_Dictionry.Add(1002, "키조개");
        icon_Dictionry.Add(1003, "키조개");
        icon_Dictionry.Add(1004, "키조개");
        icon_Dictionry.Add(1005, "키조개");
        icon_Dictionry.Add(1006, "키조개");
        icon_Dictionry.Add(1007, "키조개");
        icon_Dictionry.Add(1008, "키조개");
        icon_Dictionry.Add(1009, "키조개");
        icon_Dictionry.Add(1010, "키조개");
        icon_Dictionry.Add(1012, "키조개");
        icon_Dictionry.Add(1013, "키조개");
        icon_Dictionry.Add(1014, "키조개");
        icon_Dictionry.Add(1015, "키조개");
        icon_Dictionry.Add(1016, "키조개");
        icon_Dictionry.Add(1017, "키조개");
        icon_Dictionry.Add(1018, "키조개");
        icon_Dictionry.Add(1019, "키조개");
        icon_Dictionry.Add(1020, "키조개");
        icon_Dictionry.Add(1021, "키조개");
        //곤충
        icon_Dictionry.Add(1101, "키조개");
        icon_Dictionry.Add(1102, "키조개");
        icon_Dictionry.Add(1103, "키조개");
        icon_Dictionry.Add(1104, "키조개");
        icon_Dictionry.Add(1105, "키조개");
        icon_Dictionry.Add(1106, "키조개");
        icon_Dictionry.Add(1107, "키조개");
        icon_Dictionry.Add(1108, "키조개");
        icon_Dictionry.Add(1109, "키조개");
        icon_Dictionry.Add(1110, "키조개");
        icon_Dictionry.Add(1111, "키조개");
        //공룡
        icon_Dictionry.Add(1201, "키조개");
        icon_Dictionry.Add(1202, "키조개");
        icon_Dictionry.Add(1203, "키조개");
        icon_Dictionry.Add(1204, "키조개");
        icon_Dictionry.Add(1205, "키조개");
        icon_Dictionry.Add(1207, "키조개");
        icon_Dictionry.Add(1208, "키조개");
        icon_Dictionry.Add(1209, "키조개");
        icon_Dictionry.Add(1210, "키조개");
        icon_Dictionry.Add(1211, "키조개");
        icon_Dictionry.Add(1212, "키조개");
        icon_Dictionry.Add(1213, "키조개");
        icon_Dictionry.Add(1214, "키조개");
        icon_Dictionry.Add(1215, "키조개");
        icon_Dictionry.Add(1216, "키조개");
        icon_Dictionry.Add(1217, "키조개");
        icon_Dictionry.Add(1218, "키조개");
        icon_Dictionry.Add(1219, "키조개");
        icon_Dictionry.Add(1220, "키조개");
        icon_Dictionry.Add(1221, "키조개");
        icon_Dictionry.Add(1222, "키조개");
        icon_Dictionry.Add(1223, "키조개");
        icon_Dictionry.Add(1224, "키조개");
        icon_Dictionry.Add(1225, "키조개");
        icon_Dictionry.Add(1226, "키조개");
        icon_Dictionry.Add(1227, "키조개");
        icon_Dictionry.Add(1228, "키조개");
        //과일
        icon_Dictionry.Add(1301, "파인애플");
        icon_Dictionry.Add(1302, "포도");
        icon_Dictionry.Add(1303, "바나나");
        icon_Dictionry.Add(1304, "사과");
        icon_Dictionry.Add(1305, "배");
        icon_Dictionry.Add(1306, "수박");
        icon_Dictionry.Add(1307, "레몬");
        //동물 
        icon_Dictionry.Add(1401, "치타");
        icon_Dictionry.Add(1402, "당나귀");
        icon_Dictionry.Add(1403, "미어캣");
        icon_Dictionry.Add(1404, "악어");
        icon_Dictionry.Add(1405, "기린");
        icon_Dictionry.Add(1406, "돼지");
        icon_Dictionry.Add(1407, "젖소");
        icon_Dictionry.Add(1408, "나무늘보");
        icon_Dictionry.Add(1409, "판다");
        icon_Dictionry.Add(1410, "곰");
        icon_Dictionry.Add(1411, "하마");
        icon_Dictionry.Add(1412, "사자");
        icon_Dictionry.Add(1413, "치타");
        icon_Dictionry.Add(1414, "코끼리");
        icon_Dictionry.Add(1415, "사슴");
        icon_Dictionry.Add(1416, "여우");
        icon_Dictionry.Add(1417, "얼룩말");
        icon_Dictionry.Add(1418, "코뿔소");
        icon_Dictionry.Add(1419, "캥거루");
        icon_Dictionry.Add(1420, "코알라");
        icon_Dictionry.Add(1421, "다람쥐");
        icon_Dictionry.Add(1422, "오리너구리");
        icon_Dictionry.Add(1423, "호랑이");
        icon_Dictionry.Add(1424, "염소");
        icon_Dictionry.Add(1425, "쿼카");
        //인형
        icon_Dictionry.Add(1501, "로봇");
        icon_Dictionry.Add(1502, "곰인형");
        //새
        icon_Dictionry.Add(1601, "오리");
        icon_Dictionry.Add(1602, "참새");
        icon_Dictionry.Add(1603, "딱따구리");
        icon_Dictionry.Add(1604, "독수리");
        icon_Dictionry.Add(1605, "까치");
        icon_Dictionry.Add(1606, "제비");
        icon_Dictionry.Add(1607, "부엉이");
        icon_Dictionry.Add(1608, "공작새");
        icon_Dictionry.Add(1609, "닭");
        icon_Dictionry.Add(1610, "오리");
        //탈것
        icon_Dictionry.Add(1701, "덤프트럭");
        icon_Dictionry.Add(1702, "2층버스");
        icon_Dictionry.Add(1703, "구급차");
        icon_Dictionry.Add(1704, "소방차");
        icon_Dictionry.Add(1705, "불도저");
        icon_Dictionry.Add(1706, "굴착기");
        icon_Dictionry.Add(1707, "택배차");
        icon_Dictionry.Add(1708, "크레인");
        icon_Dictionry.Add(1709, "자동차");
        icon_Dictionry.Add(1710, "오토바이");
        icon_Dictionry.Add(1711, "레미콘");
        icon_Dictionry.Add(1712, "청소차");
        icon_Dictionry.Add(1713, "택시");
        icon_Dictionry.Add(1714, "캠핑카");
        icon_Dictionry.Add(1715, "트럭");
        icon_Dictionry.Add(1716, "버스");
        icon_Dictionry.Add(1717, "경찰차");
        icon_Dictionry.Add(1718, "지하철");
        icon_Dictionry.Add(1719, "기차");
        //채소
        icon_Dictionry.Add(1801, "호박");
        icon_Dictionry.Add(1802, "브로콜리");
        icon_Dictionry.Add(1803, "고구마");
        icon_Dictionry.Add(1804, "감자");
        icon_Dictionry.Add(1805, "가지");
        icon_Dictionry.Add(1806, "당근");
        //해공
        icon_Dictionry.Add(1901, "돛단배");
        icon_Dictionry.Add(1902, "보트");
        icon_Dictionry.Add(1903, "요트");
        icon_Dictionry.Add(1904, "여객선");
        icon_Dictionry.Add(1905, "잠수함");
        icon_Dictionry.Add(1906, "유람선");
        icon_Dictionry.Add(1907, "비행기");
        icon_Dictionry.Add(1908, "헬리콥터");
        icon_Dictionry.Add(1909, "열기구");


        Debug.Log(icon_Dictionry[1307]);
    }



    private void Init()
    {

        gameObject.SetActive(false);

        categoryBtn_List = new List<Button>();
        moldIcon_List = new List<Sprite>();

        //Catetory Icon리스트의 원소이름기준 오름차순 재정렬
       // categoryIcon_List = categoryIcon_List.OrderBy(x => x.name).ToList();
        for (int i = 0; i < content_ScrollView.transform.childCount; i++)
        {
            //버튼 리스트 초기화
            categoryBtn_List.Add(content_ScrollView.transform.GetChild(i).GetChild(0).GetComponent<Button>());
            categoryBtn_List[i].GetComponent<Image>().sprite = categoryIcon_List[i];

            //텍스트 리스트 초기화
            categoryText_List.Add(categoryBtn_List[i].transform.GetChild(0).GetComponent<TMP_Text>());
            categoryText_List[i].text = category_Dictionary[int.Parse(categoryIcon_List[i].name)];

            //버튼 이벤트 초기화
            int temp = i;
            categoryBtn_List[i].onClick.AddListener(delegate { CategoryIcon_Clicked(int.Parse(categoryIcon_List[temp].name)); });

        }
  
    }





    //카테고리 아이콘(버튼) 클릭 시 호출되는 메소드
    public void CategoryIcon_Clicked(int key)
    {
        //key는 카테고리 딕셔너리 key값
        moldIcon_List.Clear();
        Debug.Log("CategoryIcon_Clicked : "+ key);
     

        switch(key)
        {
            case 10:
                for(int i = 0; i < icon_List10.Count; i++)
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

        //selectedCategory_Text.text = categoryText_List[index].name;
        selectMold_Panel.SetActive(true);

        currentPage = 1;

        maxPage = moldIcon_List.Count;
        selectedCategory_Text.text = category_Dictionary[key];

        selectedMoldIcon_Image.sprite = moldIcon_List[currentPage - 1];
        selectedMoldIcon_Text.text = icon_Dictionry[int.Parse(moldIcon_List[currentPage - 1].name)];
        Page_Text.text = $"{currentPage}/{maxPage}";
    }

    //몰드 선택 패널에서 다음 버튼을 클릭 시 호출되는 메소드
    public void NextBtn_Clicked()
    {
        if(currentPage < maxPage)
        {
            prievious_Btn.enabled = true;
            currentPage += 1;

            selectedMoldIcon_Image.sprite = moldIcon_List[currentPage - 1];
            selectedMoldIcon_Text.text = icon_Dictionry[int.Parse(moldIcon_List[currentPage - 1].name)];
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
            selectedMoldIcon_Text.text = icon_Dictionry[int.Parse(moldIcon_List[currentPage - 1].name)];
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

    }

    //몰드 선택 패널에서 x버튼을 누르면 호출되는 메소드
    public void BackBtn_Clicked()
    {
        selectMold_Panel.SetActive(false);
        moldIcon_List.Clear();
        next_Btn.enabled = true;
        Back_Btn.enabled = true;
    }



    #endregion

}
