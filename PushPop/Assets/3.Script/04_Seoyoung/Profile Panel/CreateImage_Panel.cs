using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public struct IconButton
{
    //아콘 버튼 구조체
    public Button button;
    public bool isSelected;
}


public class CreateImage_Panel : MonoBehaviour
{

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

    [SerializeField] private List<IconButton> Icon_List;

    public int SelectIndex = 0;


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

        for (int i = 0; i < button_List.Count; i++)
        {
            button_List[i].onClick.AddListener(delegate { Icon_Clicked(i); });
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
    }

    private void takeImageBtn_Clicked()
    {
        //사진찍기 버튼을 눌렀을때 나타날 작업 넣기 :)
        //일단 그냥 사진이 맘에드나요 패널을 띄우는걸로 작업했습니다 :) 라이브러리 주세요 대표님
        check_Panel.SetActive(true);
        
    }


    private void SelectIconBtn_Clicked()
    {

    }

    private void Icon_Clicked(int indexnum)
    {
        //아이콘 클릭했을 때
        //클릭된 아이콘 비활성화
        SelectIndex = indexnum;
        Debug.Log(button_List[indexnum].name);
    }

}
