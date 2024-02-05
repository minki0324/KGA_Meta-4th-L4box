using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;





public class CreateImage_Panel : MonoBehaviour
{
    public struct IconButton
    {
        //������ ��ư ����ü
        public Button button;
        public bool isSelected;
    }

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

    [SerializeField] private IconButton[] Icon_List;

    public int SelectIndex = 0;


    [SerializeField] private Image SelectedImage;   //���� ���õ� �̹���

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
        //������� ��ư�� �������� ��Ÿ�� �۾� �ֱ� :)
        //�ϴ� �׳� ������ �����峪�� �г��� ���°ɷ� �۾��߽��ϴ� :) ���̺귯�� �ּ��� ��ǥ��
        check_Panel.SetActive(true);
        
    }


    private void SelectIconBtn_Clicked()
    {
        //���� ��ư Ŭ������ ��
        if(SelectIndex != 1000)
        {
            SelectedImage = button_List[SelectIndex].transform.GetComponent<Image>();
        }
        else
        {
            Debug.Log("���õ� �������� ����");
        }
        
    }


    //indexnum�� 12�� ��..�� count���� �ߴ°���?
    private void Icon_Clicked(int indexnum)
    {
        //������ Ŭ������ ��
        

        if(Icon_List[indexnum].isSelected.Equals(false))
        {
            //���õ� �������� �ƴ� ��� isSelected���� false�� ����
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
            //���� �������� �� ������� ���� ����
            Icon_List[indexnum].isSelected = false;
            SelectIndex = 1000;
        }




    }

}
