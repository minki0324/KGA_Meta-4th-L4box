using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//������ ���� �г�


public class Profile_Panel : MonoBehaviour
{

    [SerializeField] Button Create_Btn;

    [SerializeField] Button Delete_Btn;

    [SerializeField] GameObject Content;

    [SerializeField] List<Button> Content_List;

    public int clickedIndex;


    private void Start()
    {
        Init();
        for (int i = 0; i < Content.transform.childCount; i++)
        {
            Content_List.Add(Content.transform.GetChild(i).GetComponent<Button>());
            Debug.Log(i);
            

            //..? Index������ 0-4�ε� 5�� ������ �����߻� ����
            Content_List[i].GetComponent<Button>().onClick.AddListener(() => {           
                clickedIndex = i; 
            });
        }
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        Create_Btn.onClick.AddListener(CreateBtn_Clicked);
        Delete_Btn.onClick.AddListener(DeleteBtn_Clicked);

  
    }


    private void CreateBtn_Clicked()
    {
        //������ ���� �Լ� ȣ�� ���ּ��� :)
    }

    private void DeleteBtn_Clicked()
    {
        //������ ���� �Լ� ȣ�� ���ּ���:)
    }
}
