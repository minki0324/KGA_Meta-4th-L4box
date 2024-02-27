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

    [Header("패널")]
    [SerializeField] private GameObject selectDifficulty_Panel;
    [SerializeField] private GameObject selectCategory_Panel;
    [SerializeField] private GameObject ready_Panel;
    [SerializeField] private GameObject speedGame_Panel;
    [SerializeField] private GameObject resultPanel;

    [Header("캔버스")]
    [SerializeField] private Canvas main_Canvas;
    [SerializeField] private Help_Canvas help_Canvas;

    [Header("게임 오브젝트/프리팹")]
    [SerializeField] private GameObject Content;
    [SerializeField] private GameObject Mold_Prefab;

    [Header("텍스트")]
    [SerializeField] private TMP_Text Difficulty_Text;
    [SerializeField] private TMP_Text SelectMold_Text;

    [Header("스크롤뷰")]
    [SerializeField] private ScrollRect SelectCategory_ScrollView;

    [Header("버튼")]
    [SerializeField] private List<Button> Difficulty_Btn;
    [SerializeField] private Button previous_Btn;
    [SerializeField] private Button next_Btn;


    [Header("Ready패널 관련")]
    [SerializeField] private Image selected_Image;
    [SerializeField] private TMP_Text selected_Text;

    [Header("난이도 별 아이콘 리스트")]
    [SerializeField] private List<Sprite> easyIcon_List;
    [SerializeField] private List<Sprite> normalIcon_List;
    [SerializeField] private List<Sprite> hardIcon_List;

    [SerializeField] private List<Button> iconButton_List;

    [SerializeField] private Speed_Timer speedTimer;

   
    //스피드게임에 넘겨줄 변수
    public Sprite moldIcon { get; private set; }



    //카테고리 선택창이 떠잇으면 카테고리를 끄고, 꺼져있으면 메인캔버스로 돌아감
    public bool bSelectCategoryPanel_On = false;

    public int currentIcon;


    #region Unity Callback

    private void OnEnable()
    {
        AudioManager.instance.SetAudioClip_BGM(1);
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

    //난이도 비눗방울 (쉬움/보통/어려움)버튼 눌렀을 때 호출되는 함수 : 쉬움(0), 보통(1), 어려움(2)을 매개변수로 줌
    public void DifficultyBtn_Clicked(int index)
    {
        SelectCategory_ScrollView.normalizedPosition = new Vector2(1f, 1f);
        if (!help_Canvas.bisHelpPanelOn)
        {
            AudioManager.instance.SetCommonAudioClip_SFX(3);
            selectCategory_Panel.SetActive(true);
            bSelectCategoryPanel_On = true;

            //-----------뒤로가기에 넣을 것----------------------------------
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

                    Difficulty_Text.text = "쉬움";
                    SelectMold_Text.text = "쉬움";
                    for (int i = 0; i < easyIcon_List.Count; i++)
                    {
                        GameObject a = Instantiate(Mold_Prefab, Content.transform);

                        //버튼 리스트 초기화
                        iconButton_List.Add(a.transform.GetChild(0).GetComponent<Button>());


                        //스프라이트 변경
                        a.transform.GetChild(0).GetComponent<Image>().sprite = easyIcon_List[i];

                        //텍스트 변경 : 스프라이트 이름을 키값으로 value가져오기
                        a.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = DataManager2.instance.iconDict[int.Parse(easyIcon_List[i].name)];
                    }

                    break;

                case 1:
                    difficulty = Difficulty.Normal;

                    Difficulty_Text.text = "보통";
                    SelectMold_Text.text = "보통";
                    for (int i = 0; i < normalIcon_List.Count; i++)
                    {
                        GameObject a = Instantiate(Mold_Prefab, Content.transform);

                        //버튼 리스트 초기화
                        iconButton_List.Add(a.transform.GetChild(0).GetComponent<Button>());

                        //스프라이트 변경
                        a.transform.GetChild(0).GetComponent<Image>().sprite = normalIcon_List[i];

                        //텍스트 변경
                        a.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = DataManager2.instance.iconDict[int.Parse(normalIcon_List[i].name)];
                    }
                    break;

                case 2:
                    difficulty = Difficulty.Hard;

                    Difficulty_Text.text = "어려움";
                    SelectMold_Text.text = "어려움";
                    for (int i = 0; i < hardIcon_List.Count; i++)
                    {
                        GameObject a = Instantiate(Mold_Prefab, Content.transform);

                        //버튼 리스트 초기화
                        iconButton_List.Add(a.transform.GetChild(0).GetComponent<Button>());

                        //스프라이트 변경
                        a.transform.GetChild(0).GetComponent<Image>().sprite = hardIcon_List[i];

                        //텍스트 변경
                        a.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = DataManager2.instance.iconDict[int.Parse(hardIcon_List[i].name)];
                    }
                    break;
            }

            for (int i = 0; i < iconButton_List.Count; i++)
            {
                int temp = i;
                //currentIcon = temp;
                iconButton_List[temp].onClick.AddListener(delegate { IconBtn_Clicked(iconButton_List[temp].gameObject, temp); });
            }
        }


    }
    public void NextBtn_Clicked()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        if (currentIcon >= iconButton_List.Count - 1)
        {
            next_Btn.interactable = false;
        }
        else
        {
            previous_Btn.interactable = true;
            currentIcon += 1;
            selected_Image.sprite = iconButton_List[currentIcon].GetComponent<Image>().sprite;
            selected_Text.text = iconButton_List[currentIcon].transform.GetChild(0).GetComponent<TMP_Text>().text;
            moldIcon = selected_Image.sprite;
        }

     
    }


    public void PreviousBtn_Clicked()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        if (currentIcon <= 0)
        {
            previous_Btn.interactable = false;
        }
        else
        {
            next_Btn.interactable = true;
            currentIcon -= 1;
            selected_Image.sprite = iconButton_List[currentIcon].GetComponent<Image>().sprite;
            selected_Text.text = iconButton_List[currentIcon].transform.GetChild(0).GetComponent<TMP_Text>().text;
            moldIcon = selected_Image.sprite;
        }
       
    }


    //몰드 아이콘 눌렀을 때 호출되는 함수
    public void IconBtn_Clicked(GameObject button, int _temp)
    {
       
        if (!help_Canvas.bisHelpPanelOn)
        {
            AudioManager.instance.SetCommonAudioClip_SFX(3);
            currentIcon = _temp;
            if (currentIcon <= 0)
            {
                previous_Btn.interactable = false;
            }
            else
            {
                previous_Btn.interactable = true;
            }

            if(currentIcon >= iconButton_List.Count - 1)
            {
                next_Btn.interactable = false;
            }
            else
            {
                next_Btn.interactable = true;
            }

    
            ready_Panel.SetActive(true);

            help_Canvas.Back_Btn.interactable = false;
            help_Canvas.Help_Btn.interactable = false;
           

            selected_Image.sprite = button.GetComponent<Image>().sprite;
            selected_Text.text = button.transform.GetChild(0).GetComponent<TMP_Text>().text;

            moldIcon = selected_Image.sprite;
            GameManager.Instance.boardName = int.Parse(moldIcon.name);
            GameManager.Instance.PrintSpeed(int.Parse(moldIcon.name));
        }
    }

    public void PrintScore()
    {
        GameManager.Instance.PrintSpeed(int.Parse(moldIcon.name));
    }

    //준비창의 x를 눌렀을 때 호출되는 함수
    public void ReadyPanel_BackBtn_Clicked()
    {
        if (!help_Canvas.bisHelpPanelOn)
        {
            AudioManager.instance.SetCommonAudioClip_SFX(3);
            ready_Panel.SetActive(false);
            help_Canvas.Back_Btn.interactable = true;
            help_Canvas.Help_Btn.interactable = true;
        }
    }


    //준비창의 게임시작 버튼 눌렀을 때 호출되는 함수
    public void GameStartBtn_Clicked()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(0);
        AudioManager.instance.SetAudioClip_BGM(3);

        speedGame_Panel.SetActive(true);
        selectCategory_Panel.SetActive(false);
        selectDifficulty_Panel.SetActive(false);
        ready_Panel.SetActive(false);
        bSelectCategoryPanel_On = false;
        help_Canvas.gameObject.SetActive(false);

        PushPop.Instance.boardSprite = moldIcon; // pushpop
        GameManager.Instance.GameStart();
    }


    //좌측하단 뒤로가기 버튼을 눌렀을 때 호출되는 함수
    public void BackBtn_Clicked()
    {
        //난이도 선택창이 켜져있고 카테고리 선택창이 꺼져있으면 메인화면으로가기
        //카테고리 선택창이 켜져있으면 카테고리 선택창 끄기

        AudioManager.instance.SetCommonAudioClip_SFX(3);
        if (!help_Canvas.bisHelpPanelOn)
        {
            if (bSelectCategoryPanel_On)
            {
                selectCategory_Panel.SetActive(false);
                bSelectCategoryPanel_On = false;
            }
            else
            {
                AudioManager.instance.SetAudioClip_BGM(0);

                help_Canvas.transform.SetParent(null);
                help_Canvas.transform.SetAsLastSibling();
                main_Canvas.gameObject.SetActive(true);
                gameObject.SetActive(false);
               
            }
        }

    }


    //좌측 하단 뒤로가기 버튼 클릭 시 호출되는 함수 :  도움말 버튼 눌리면 그 외 버튼 비활성화
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


    //도움말 창의 뒤로가기 버튼 클릭 시 호출되는 함수 :  그 외 버튼 활성화
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

    public void RestartButton()
    {
        resultPanel.SetActive(false);
        speedTimer.Init();
        GameManager.Instance.SpeedMode(); // Speed Mode restart
    }



  


    #endregion

}
