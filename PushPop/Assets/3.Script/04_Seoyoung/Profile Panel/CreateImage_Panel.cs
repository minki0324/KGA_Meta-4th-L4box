using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateImage_Panel : MonoBehaviour
{

    [Header("ī�޶�/�������� �г�")]
    [SerializeField] GameObject Picture_Panel;

    [SerializeField] Button takeImage_Btn;
    [SerializeField] Button SelectImage_Btn;


    //ī�޶� ������ ������ �Ǵ��ϴ� ����
    public bool isPictrueTaken = false;

    [Header("������ ����� Ȯ���ϴ� �г�")]
    [SerializeField] GameObject check_Panel;

    [Header("������ ���� �г�")]
    [SerializeField] GameObject icon_Panel;

    [SerializeField] GameObject Content;


    [SerializeField] private List<Button> button_List;

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
            Debug.Log("����");
            icon_Panel.SetActive(true); 
        });

      

        for(int i = 0; i < button_List.Count; i++)
        {

        }
    }

    private void takeImageBtn_Clicked()
    {
        //������� ��ư�� �������� ��Ÿ�� �۾� �ֱ� :)
        //�ϴ� �׳� ������ �����峪�� �г��� ���°ɷ� �۾��߽��ϴ� :) ���̺귯�� �ּ��� ��ǥ��
        check_Panel.SetActive(true);
        
    }


}
