using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public struct IconButton
{
    //���� ��ư ����ü
    public Button button;
    public bool isSelected;
}


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
            //Content ���� ������Ʈ�� ������ ������
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
        //������� ��ư�� �������� ��Ÿ�� �۾� �ֱ� :)
        //�ϴ� �׳� ������ �����峪�� �г��� ���°ɷ� �۾��߽��ϴ� :) ���̺귯�� �ּ��� ��ǥ��
        check_Panel.SetActive(true);
        
    }


    private void SelectIconBtn_Clicked()
    {

    }

    private void Icon_Clicked(int indexnum)
    {
        //������ Ŭ������ ��
        //Ŭ���� ������ ��Ȱ��ȭ
        SelectIndex = indexnum;
        Debug.Log(button_List[indexnum].name);
    }

}
