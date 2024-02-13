using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class PushPush_Canvas : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private Canvas main_Canvas;
    [SerializeField] private Canvas background_Canvas;

    [Header("Panel")]
    [SerializeField] private GameObject selectCategory_Panel;
    [SerializeField] private GameObject selectMold_Panel;

    [Header("ScrollView")]
    [SerializeField] private GameObject content_ScrollView;

    [Header("Button")]
    [SerializeField] private Button next_Btn;
    [SerializeField] private Button prievious_Btn;
    [SerializeField] private Button gameStart_Btn;
    [SerializeField] private Button Back_Btn;

    [Header("Selected Mold Icon Image & Text")]
    [SerializeField] private TMP_Text selectedCategory_Text;    //���õ� ī�װ� �ؽ�Ʈ

    [SerializeField] private Image selectedMoldIcon_Image;      //���õ� ���� ������ �̹���
    [SerializeField] private TMP_Text selectedMoldIcon_Text;    //���õ� ���� �ؽ�Ʈ
    [SerializeField] private TMP_Text Page_Text;                //������ �ؽ�Ʈ


    [Header("Category Icon(Btn) List")]
    [SerializeField] private List<Button> categoryBtn_List;

    [Header("Category Icon List(Iamge & Text)")]
    [SerializeField] private List<Sprite> categoryIcon_List;        
    [SerializeField] private List<TMP_Text> categoryText_List;

    

    //�� ģ���� Resource�������� �̹����� �ҷ��� ����Ʈ �ϳ��� �������� ����Ʈ�� �������� or �׳� ī�װ��� ���� ������ ����Ʈ�� �� ����� ���� ������ ��. �ӽ÷� �ػ깰 �־�����
    [Header("Mold Icon(Image) List")]
    [SerializeField] private List<Sprite> moldIcon_List;

    [SerializeField] private PuzzleLozic puzzleLozic;


    int currentPage;
    int maxPage;

  
    //����/�ڷΰ��� �г� ��ư�� ��Ȱ��ȭ �߰��ϱ�

    #region Unity Callback

    private void Start()
    {
        Init();
        maxPage = moldIcon_List.Count;
    }

    private void OnEnable()
    {
        background_Canvas.gameObject.SetActive(true);
        selectMold_Panel.SetActive(false);

        currentPage = 1;
        maxPage = moldIcon_List.Count;
        Page_Text.text = $"{currentPage}/{maxPage}";
    }


    private void OnDisable()
    {
        //background_Canvas.gameObject.SetActive(false);
    }

    #endregion

    #region Other Method

    private void Init()
    {
        
        // gameObject.SetActive(false);

      
        for (int i = 0; i < content_ScrollView.transform.childCount; i++)
        {
            //��ư ����Ʈ �ʱ�ȭ
            categoryBtn_List.Add(content_ScrollView.transform.GetChild(i).GetComponent<Button>());
            categoryBtn_List[i].GetComponent<Image>().sprite = categoryIcon_List[i];
     
        }

        //Catetory Icon ����Ʈ�� �����̸����� �������ϱ�..
        

        for (int i = 0; i < content_ScrollView.transform.childCount; i++)
        {
            //�ؽ�Ʈ ����Ʈ �ʱ�ȭ
            categoryText_List.Add(categoryBtn_List[i].transform.GetChild(0).GetComponent<TMP_Text>());
            categoryText_List[i].text = categoryIcon_List[i].name;

            //��ư �̺�Ʈ �ʱ�ȭ
            categoryBtn_List[i].onClick.AddListener(delegate { CategoryIcon_Clicked(i); });
        }




    }


    public void SortText(string a, string b)
    {

    }

    //ī�װ� ������(��ư) Ŭ�� �� ȣ��Ǵ� �޼ҵ�
    public void CategoryIcon_Clicked(int index)
    {
        //�ϴ� ������ �ػ깰 ����Ʈ ȣ��
        selectMold_Panel.SetActive(true);

        Debug.Log(index);
        selectedCategory_Text.text = categoryText_List[index].name;
        

        selectedMoldIcon_Image.sprite = moldIcon_List[currentPage - 1];
        selectedMoldIcon_Text.text = moldIcon_List[currentPage - 1].name;
        Page_Text.text = $"{currentPage}/{maxPage}";

    }

    //���� ���� �гο��� ���� ��ư�� Ŭ�� �� ȣ��Ǵ� �޼ҵ�
    public void NextBtn_Clicked()
    {
        if (currentPage < maxPage)
        {
            prievious_Btn.enabled = true;
            currentPage += 1;

            selectedMoldIcon_Image.sprite = moldIcon_List[currentPage - 1];
            selectedMoldIcon_Text.text = moldIcon_List[currentPage - 1].name;
            Page_Text.text = $"{currentPage}/{maxPage}";
        }

        if(currentPage == maxPage)
        {
            next_Btn.enabled = false;
        }
        else
        {
            next_Btn.enabled = true;
        }

    }

    //���� ���� �гο��� ���� ��ư�� Ŭ�� �� ȣ��Ǵ� �޼ҵ�
    public void PreviousBtn_Clicked()
    {
        if(currentPage > 0)
        {
            next_Btn.enabled = true;
            currentPage -= 1;

            selectedMoldIcon_Image.sprite = moldIcon_List[currentPage - 1];
            selectedMoldIcon_Text.text = moldIcon_List[currentPage - 1].name;
            Page_Text.text = $"{currentPage}/{maxPage}";
        }


        if (currentPage == 1)
        {
            prievious_Btn.enabled = false;
        }
        else
        {
            prievious_Btn.enabled = true;
        }

    }


    //���� ���� �гο��� ���ӽ��� ��ư�� ������ ȣ��Ǵ� �޼ҵ�
    public void GameStartBtn_Clicked()
    {
        //PushPush ���� ����
        int puzzleIDIndex = int.Parse(moldIcon_List[currentPage - 1].name);
        Debug.Log(puzzleIDIndex);
        puzzleLozic.SelectPuzzleButton(puzzleIDIndex);

    }

    //���� ���� �гο��� x��ư�� ������ ȣ��Ǵ� �޼ҵ�
    public void BackBtn_Clicked()
    {
        selectMold_Panel.SetActive(false);
    }



    #endregion

}
