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

    [Header("�����")]
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


    //���� �ϴ� �ڷΰ��� ��ư Ŭ�� �� ȣ��Ǵ� �޼ҵ�
    public void BackBtn_Clicked()
    {
        Warning_Panel.SetActive(true);
        back_Btn.interactable = false;
    }


    //������ ��� �г��� ������ ��ư Ŭ�� �� ȣ��Ǵ� �޼ҵ�
    public void GoOutBtn_Clicked()
    {
        main_Canvas.gameObject.SetActive(true);
        memory_Canvas.gameObject.SetActive(false);
    }

    //������ ��� �г��� ��� ��ư Ŭ�� �� ȣ��Ǵ� �޼ҵ�
    public void CancelBtn_Clicked()
    {
        Warning_Panel.SetActive(false);
        back_Btn.interactable = true;
    }

}
