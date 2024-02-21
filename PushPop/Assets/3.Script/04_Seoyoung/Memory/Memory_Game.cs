using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Memory_Game : MonoBehaviour
{
    [Header("ĵ����")]   
    [SerializeField] Canvas memory_Canvas;
    [SerializeField] Help_Canvas help_Canvas;

    [Header("�г�")]
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

        gameSet_Panel.SetActive(true);
        help_Canvas.gameObject.SetActive(true);

        Warning_Panel.SetActive(false);
        gameObject.SetActive(false);
    }

    //������ ��� �г��� ��� ��ư Ŭ�� �� ȣ��Ǵ� �޼ҵ�
    public void CancelBtn_Clicked()
    {
        back_Btn.interactable = true;

        Warning_Panel.SetActive(false);
      
    }

    #endregion
}
