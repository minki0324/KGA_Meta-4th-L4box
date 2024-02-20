using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Memory_Game : MonoBehaviour
{
    [Header("�ڷΰ���")]
    [SerializeField] Canvas main_Canvas;
    [SerializeField] Canvas memory_Canvas;
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
    //���� �ϴ� �ڷΰ��� ��ư Ŭ�� �� ȣ��Ǵ� �޼ҵ�
    public void BackBtn_Clicked()
    {
        Warning_Panel.SetActive(true);
        back_Btn.interactable = false;
    }


    //������ ��� �г��� ������ ��ư Ŭ�� �� ȣ��Ǵ� �޼ҵ�
    public void GoOutBtn_Clicked()
    {
        back_Btn.interactable = true;

        main_Canvas.gameObject.SetActive(true);
        memory_Canvas.gameObject.SetActive(false);
    }

    //������ ��� �г��� ��� ��ư Ŭ�� �� ȣ��Ǵ� �޼ҵ�
    public void CancelBtn_Clicked()
    {
        back_Btn.interactable = true;

        Warning_Panel.SetActive(false);
      
    }

    #endregion
}
