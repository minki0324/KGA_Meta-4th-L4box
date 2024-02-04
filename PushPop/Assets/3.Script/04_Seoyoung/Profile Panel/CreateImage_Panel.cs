using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;





public class CreateImage_Panel : MonoBehaviour
{
    public struct IconButton
    {
        //아이콘 버튼 구조체
        public Button button;
        public bool isSelected;
    }

    [Header("카메라/사진선택 패널")]
    [SerializeField] GameObject Picture_Panel;

    [SerializeField] Button takeImage_Btn;
    [SerializeField] Button SelectImage_Btn;


    //카메라 사진이 찍혔나 판단하는 변수
    public bool isPictrueTaken = false;

    [Header("마음에 드는지 확인하는 패널")]
    [SerializeField] GameObject check_Panel;

    [Header("아이콘 선택 패널")]
    [SerializeField] GameObject icon_Panel;

    [SerializeField] Button SelectIcon_Btn;

    [SerializeField] Button Back_Btn;

    [SerializeField] GameObject Content;

    [SerializeField] private List<Button> button_List;

    [SerializeField] private IconButton[] Icon_List;

    public int SelectIndex = 0;


    [SerializeField] private Image SelectedImage;   //최종 선택된 이미지

    private void Start()
    {
        gameObject.SetActive(false);
        for (int i = 0; i < Content.transform.childCount; i++)
        {
            //Content 게임 오브젝트의 갯수를 가져옴
            for (int j = 0; j < 4; j++)
            {
                button_List.Add(Content.transform.GetChild(i).transform.GetChild(j).GetComponent<Button>());
                
            }
        }

        Icon_List = new IconButton[button_List.Count];


        for (int i = 0; i < button_List.Count; i++)
        {
          //  button_List[i].onClick.AddListener(delegate { Icon_Clicked(i); });
            Icon_List[i].button = button_List[i];
            Icon_List[i].isSelected = false;
            Icon_List[i].button.onClick.AddListener(delegate { Icon_Clicked(i); });
         
        }
    }



    private void OnEnable()
    {
        check_Panel.SetActive(false);
        icon_Panel.SetActive(false);
        Init();
    }

    private void Init()
    {
        takeImage_Btn.onClick.AddListener(takeImageBtn_Clicked);
        SelectImage_Btn.onClick.AddListener(() => {
            icon_Panel.SetActive(true); 
        });

        SelectIcon_Btn.onClick.AddListener(SelectIconBtn_Clicked);
        Back_Btn.onClick.AddListener(() => {
            icon_Panel.SetActive(false);
        });
    }

    private void takeImageBtn_Clicked()
    {
        //사진찍기 버튼을 눌렀을때 나타날 작업 넣기 :)
        //일단 그냥 사진이 맘에드나요 패널을 띄우는걸로 작업했습니다 :) 라이브러리 주세요 대표님
        check_Panel.SetActive(true);
        
    }


    private void SelectIconBtn_Clicked()
    {
        //선택 버튼 클릭했을 때
        if(SelectIndex != 1000)
        {
            SelectedImage = button_List[SelectIndex].transform.GetComponent<Image>();
        }
        else
        {
            Debug.Log("선택된 아이콘이 없음");
        }
        
    }


    //indexnum이 12가 뜸..왜 count값이 뜨는거지?
    private void Icon_Clicked(int indexnum)
    {
        //아이콘 클릭했을 때
        

        if(Icon_List[indexnum].isSelected.Equals(false))
        {
            //선택된 아이콘이 아닌 경우 isSelected값을 false로 변경
            for (int i = 0; i<Icon_List.Length; i++)
            {            
                if(!Icon_List[i].Equals(Icon_List[indexnum]))
                {
                    Icon_List[i].isSelected = false;
                }

            }

            Icon_List[indexnum].isSelected = true;
            SelectIndex = indexnum;
        }
        else
        {
            //눌린 아이콘이 또 눌릴경우 선택 해제
            Icon_List[indexnum].isSelected = false;
            SelectIndex = 1000;
        }




    }

}
