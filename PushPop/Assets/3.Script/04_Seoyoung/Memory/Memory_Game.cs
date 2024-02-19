using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Memory_Game : MonoBehaviour
{
    [Header("뒤로가기")]
    [SerializeField] Canvas main_Canvas;
    [SerializeField] Canvas memory_Canvas;
    [SerializeField] GameObject Warning_Panel;
    [SerializeField] private Button back_Btn;

    [Header("생명력")]
    [SerializeField] private GameObject heartBox;
    [SerializeField] private List<Image> heartImage_List;
    [SerializeField] private TMP_Text score_Text;


    private void OnEnable()
    {
        if (Warning_Panel.activeSelf)
        {
            Warning_Panel.SetActive(false);
        }
    }

    private void Start()
    {
        Init();
    }



    public void Init()
    {
        for(int i = 0; i< heartBox.transform.childCount; i++)
        {
            heartImage_List.Add(heartBox.transform.GetChild(i).GetComponent<Image>());
        }
    }


    //좌측 하단 뒤로가기 버튼 클릭 시 호출되는 메소드
    public void BackBtn_Clicked()
    {
        Warning_Panel.SetActive(true);
        back_Btn.interactable = false;
    }


    //나가기 경고 패널의 나가기 버튼 클릭 시 호출되는 메소드
    public void GoOutBtn_Clicked()
    {
        main_Canvas.gameObject.SetActive(true);
        memory_Canvas.gameObject.SetActive(false);
    }

    //나가기 경고 패널의 취소 버튼 클릭 시 호출되는 메소드
    public void CancelBtn_Clicked()
    {
        Warning_Panel.SetActive(false);
        back_Btn.interactable = true;
    }

}
