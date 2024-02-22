using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Memory_Game : MonoBehaviour
{
    [Header("캔버스")]   
    [SerializeField] Canvas memory_Canvas;
    [SerializeField] Help_Canvas help_Canvas;

    [Header("패널")]
    [SerializeField] GameObject gameSet_Panel;
    [SerializeField] GameObject Warning_Panel;
    [SerializeField] private Button back_Btn;

  

    #region Unity Callback
    private void OnEnable()
    {
        if (Warning_Panel.activeSelf)
        {
            Warning_Panel.SetActive(false);
        }
    }

    private void Start()
    {
      
    }


    #endregion

    #region Other Method
    //좌측 하단 뒤로가기 버튼 클릭 시 호출되는 메소드
    public void BackBtn_Clicked()
    {
        Warning_Panel.SetActive(true);
        back_Btn.interactable = false;
        Time.timeScale = 0f;
    }


    //나가기 경고 패널의 나가기 버튼 클릭 시 호출되는 메소드
    public void GoOutBtn_Clicked()
    {
        Debug.Log("ㅇㅇ");
        back_Btn.interactable = true;
        gameSet_Panel.SetActive(true);
        help_Canvas.gameObject.SetActive(true);


       // Warning_Panel.SetActive(false);
        gameObject.SetActive(false);
        //main_Canvas.gameObject.SetActive(true);
        //memory_Canvas.gameObject.SetActive(false);
    }

    //나가기 경고 패널의 취소 버튼 클릭 시 호출되는 메소드
    public void CancelBtn_Clicked()
    {
        back_Btn.interactable = true;
        Warning_Panel.SetActive(false);
        Time.timeScale = 1f;

    }

    #endregion
}
