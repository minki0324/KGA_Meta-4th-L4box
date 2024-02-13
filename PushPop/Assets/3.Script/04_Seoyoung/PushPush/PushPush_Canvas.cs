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

    //�̹����� �ҷ��� ����Ʈ �ϳ��� �������� ����Ʈ�� ����
    [Header("Mold Icon(Image) List")]
    [SerializeField] private List<Sprite> icon_List10;       
    [SerializeField] private List<Sprite> icon_List11;       
    [SerializeField] private List<Sprite> icon_List12;       
    [SerializeField] private List<Sprite> icon_List13;       
    [SerializeField] private List<Sprite> icon_List14;       
    [SerializeField] private List<Sprite> icon_List15;       
    [SerializeField] private List<Sprite> icon_List16;       
    [SerializeField] private List<Sprite> icon_List17;       
    [SerializeField] private List<Sprite> icon_List18;       
    [SerializeField] private List<Sprite> icon_List19;       

    [SerializeField] private List<Sprite> moldIcon_List;    //���õ� ī�װ� ���� ������ ����Ʈ



    [SerializeField] private Dictionary<int, string> category_Dictionary = new Dictionary<int, string>();
    [SerializeField] private Dictionary<int, string> icon_Dictionry = new Dictionary<int, string>();
   



    int currentPage;
    int maxPage;

    //����/�ڷΰ��� �г� ��ư�� ��Ȱ��ȭ �߰��ϱ�

    #region Unity Callback

    private void Start()
    {
        CategoryDictionary();
        IconDictionary();
        Init();
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
        background_Canvas.gameObject.SetActive(false);
    }

    #endregion

    #region Other Method

  
    public void CategoryDictionary()
    {
        category_Dictionary = new Dictionary<int, string>();
        category_Dictionary.Add(10, "�ػ깰");
        category_Dictionary.Add(11, "����");
        category_Dictionary.Add(12, "����");
        category_Dictionary.Add(13, "����");
        category_Dictionary.Add(14, "����");
        category_Dictionary.Add(15, "����");
        category_Dictionary.Add(16, "��");
        category_Dictionary.Add(17, "��");
        category_Dictionary.Add(18, "ä��");
        category_Dictionary.Add(19, "��&�����");
    }

    public void IconDictionary()
    {
        icon_Dictionry = new Dictionary<int, string>();

        icon_Dictionry.Add(1001, "����");
        icon_Dictionry.Add(1002, "Ű����");
        icon_Dictionry.Add(1003, "Ű����");
        icon_Dictionry.Add(1004, "Ű����");
        icon_Dictionry.Add(1005, "Ű����");
        icon_Dictionry.Add(1006, "Ű����");
        icon_Dictionry.Add(1007, "Ű����");
        icon_Dictionry.Add(1008, "Ű����");
        icon_Dictionry.Add(1009, "Ű����");
        icon_Dictionry.Add(1010, "Ű����");
        icon_Dictionry.Add(1012, "Ű����");
        icon_Dictionry.Add(1013, "Ű����");
        icon_Dictionry.Add(1014, "Ű����");
        icon_Dictionry.Add(1015, "Ű����");
        icon_Dictionry.Add(1016, "Ű����");
        icon_Dictionry.Add(1017, "Ű����");
        icon_Dictionry.Add(1018, "Ű����");
        icon_Dictionry.Add(1019, "Ű����");
        icon_Dictionry.Add(1020, "Ű����");
        icon_Dictionry.Add(1021, "Ű����");
        //����
        icon_Dictionry.Add(1101, "Ű����");
        icon_Dictionry.Add(1102, "Ű����");
        icon_Dictionry.Add(1103, "Ű����");
        icon_Dictionry.Add(1104, "Ű����");
        icon_Dictionry.Add(1105, "Ű����");
        icon_Dictionry.Add(1106, "Ű����");
        icon_Dictionry.Add(1107, "Ű����");
        icon_Dictionry.Add(1108, "Ű����");
        icon_Dictionry.Add(1109, "Ű����");
        icon_Dictionry.Add(1110, "Ű����");
        icon_Dictionry.Add(1111, "Ű����");
        //����
        icon_Dictionry.Add(1201, "Ű����");
        icon_Dictionry.Add(1202, "Ű����");
        icon_Dictionry.Add(1203, "Ű����");
        icon_Dictionry.Add(1204, "Ű����");
        icon_Dictionry.Add(1205, "Ű����");
        icon_Dictionry.Add(1207, "Ű����");
        icon_Dictionry.Add(1208, "Ű����");
        icon_Dictionry.Add(1209, "Ű����");
        icon_Dictionry.Add(1210, "Ű����");
        icon_Dictionry.Add(1211, "Ű����");
        icon_Dictionry.Add(1212, "Ű����");
        icon_Dictionry.Add(1213, "Ű����");
        icon_Dictionry.Add(1214, "Ű����");
        icon_Dictionry.Add(1215, "Ű����");
        icon_Dictionry.Add(1216, "Ű����");
        icon_Dictionry.Add(1217, "Ű����");
        icon_Dictionry.Add(1218, "Ű����");
        icon_Dictionry.Add(1219, "Ű����");
        icon_Dictionry.Add(1220, "Ű����");
        icon_Dictionry.Add(1221, "Ű����");
        icon_Dictionry.Add(1222, "Ű����");
        icon_Dictionry.Add(1223, "Ű����");
        icon_Dictionry.Add(1224, "Ű����");
        icon_Dictionry.Add(1225, "Ű����");
        icon_Dictionry.Add(1226, "Ű����");
        icon_Dictionry.Add(1227, "Ű����");
        icon_Dictionry.Add(1228, "Ű����");
        //����
        icon_Dictionry.Add(1301, "���ξ���");
        icon_Dictionry.Add(1302, "����");
        icon_Dictionry.Add(1303, "�ٳ���");
        icon_Dictionry.Add(1304, "���");
        icon_Dictionry.Add(1305, "��");
        icon_Dictionry.Add(1306, "����");
        icon_Dictionry.Add(1307, "����");
        //���� 
        icon_Dictionry.Add(1401, "ġŸ");
        icon_Dictionry.Add(1402, "�糪��");
        icon_Dictionry.Add(1403, "�̾�Ĺ");
        icon_Dictionry.Add(1404, "�Ǿ�");
        icon_Dictionry.Add(1405, "�⸰");
        icon_Dictionry.Add(1406, "����");
        icon_Dictionry.Add(1407, "����");
        icon_Dictionry.Add(1408, "�����ú�");
        icon_Dictionry.Add(1409, "�Ǵ�");
        icon_Dictionry.Add(1410, "��");
        icon_Dictionry.Add(1411, "�ϸ�");
        icon_Dictionry.Add(1412, "����");
        icon_Dictionry.Add(1413, "ġŸ");
        icon_Dictionry.Add(1414, "�ڳ���");
        icon_Dictionry.Add(1415, "�罿");
        icon_Dictionry.Add(1416, "����");
        icon_Dictionry.Add(1417, "��踻");
        icon_Dictionry.Add(1418, "�ڻԼ�");
        icon_Dictionry.Add(1419, "Ļ�ŷ�");
        icon_Dictionry.Add(1420, "�ھ˶�");
        icon_Dictionry.Add(1421, "�ٶ���");
        icon_Dictionry.Add(1422, "�����ʱ���");
        icon_Dictionry.Add(1423, "ȣ����");
        icon_Dictionry.Add(1424, "����");
        icon_Dictionry.Add(1425, "��ī");
        //����
        icon_Dictionry.Add(1501, "�κ�");
        icon_Dictionry.Add(1502, "������");
        //��
        icon_Dictionry.Add(1601, "����");
        icon_Dictionry.Add(1602, "����");
        icon_Dictionry.Add(1603, "��������");
        icon_Dictionry.Add(1604, "������");
        icon_Dictionry.Add(1605, "��ġ");
        icon_Dictionry.Add(1606, "����");
        icon_Dictionry.Add(1607, "�ξ���");
        icon_Dictionry.Add(1608, "���ۻ�");
        icon_Dictionry.Add(1609, "��");
        icon_Dictionry.Add(1610, "����");
        //Ż��
        icon_Dictionry.Add(1701, "����Ʈ��");
        icon_Dictionry.Add(1702, "2������");
        icon_Dictionry.Add(1703, "������");
        icon_Dictionry.Add(1704, "�ҹ���");
        icon_Dictionry.Add(1705, "�ҵ���");
        icon_Dictionry.Add(1706, "������");
        icon_Dictionry.Add(1707, "�ù���");
        icon_Dictionry.Add(1708, "ũ����");
        icon_Dictionry.Add(1709, "�ڵ���");
        icon_Dictionry.Add(1710, "�������");
        icon_Dictionry.Add(1711, "������");
        icon_Dictionry.Add(1712, "û����");
        icon_Dictionry.Add(1713, "�ý�");
        icon_Dictionry.Add(1714, "ķ��ī");
        icon_Dictionry.Add(1715, "Ʈ��");
        icon_Dictionry.Add(1716, "����");
        icon_Dictionry.Add(1717, "������");
        icon_Dictionry.Add(1718, "����ö");
        icon_Dictionry.Add(1719, "����");
        //ä��
        icon_Dictionry.Add(1801, "ȣ��");
        icon_Dictionry.Add(1802, "����ݸ�");
        icon_Dictionry.Add(1803, "����");
        icon_Dictionry.Add(1804, "����");
        icon_Dictionry.Add(1805, "����");
        icon_Dictionry.Add(1806, "���");
        //�ذ�
        icon_Dictionry.Add(1901, "���ܹ�");
        icon_Dictionry.Add(1902, "��Ʈ");
        icon_Dictionry.Add(1903, "��Ʈ");
        icon_Dictionry.Add(1904, "������");
        icon_Dictionry.Add(1905, "�����");
        icon_Dictionry.Add(1906, "������");
        icon_Dictionry.Add(1907, "�����");
        icon_Dictionry.Add(1908, "�︮����");
        icon_Dictionry.Add(1909, "���ⱸ");


        Debug.Log(icon_Dictionry[1307]);
    }



    private void Init()
    {

        gameObject.SetActive(false);

        categoryBtn_List = new List<Button>();
        moldIcon_List = new List<Sprite>();

        //Catetory Icon����Ʈ�� �����̸����� �������� ������
       // categoryIcon_List = categoryIcon_List.OrderBy(x => x.name).ToList();
        for (int i = 0; i < content_ScrollView.transform.childCount; i++)
        {
            //��ư ����Ʈ �ʱ�ȭ
            categoryBtn_List.Add(content_ScrollView.transform.GetChild(i).GetChild(0).GetComponent<Button>());
            categoryBtn_List[i].GetComponent<Image>().sprite = categoryIcon_List[i];

            //�ؽ�Ʈ ����Ʈ �ʱ�ȭ
            categoryText_List.Add(categoryBtn_List[i].transform.GetChild(0).GetComponent<TMP_Text>());
            categoryText_List[i].text = category_Dictionary[int.Parse(categoryIcon_List[i].name)];

            //��ư �̺�Ʈ �ʱ�ȭ
            int temp = i;
            categoryBtn_List[i].onClick.AddListener(delegate { CategoryIcon_Clicked(int.Parse(categoryIcon_List[temp].name)); });

        }
  
    }





    //ī�װ� ������(��ư) Ŭ�� �� ȣ��Ǵ� �޼ҵ�
    public void CategoryIcon_Clicked(int key)
    {
        //key�� ī�װ� ��ųʸ� key��
        moldIcon_List.Clear();
        Debug.Log("CategoryIcon_Clicked : "+ key);
     

        switch(key)
        {
            case 10:
                for(int i = 0; i < icon_List10.Count; i++)
                {           
                    moldIcon_List.Add(icon_List10[i]);
                }
                break;

            case 11:
                for (int i = 0; i < icon_List11.Count; i++)
                {
                    moldIcon_List.Add(icon_List11[i]);
                }
                break;

            case 12:
                for (int i = 0; i < icon_List12.Count; i++)
                {
                    moldIcon_List.Add(icon_List12[i]);
                }
                break;

            case 13:
                for (int i = 0; i < icon_List13.Count; i++)
                {
                    moldIcon_List.Add(icon_List13[i]);
                }
                break;

            case 14:
                for (int i = 0; i < icon_List14.Count; i++)
                {
                    moldIcon_List.Add(icon_List14[i]);
                }
                break;

            case 15:
                for (int i = 0; i < icon_List15.Count; i++)
                {
                    moldIcon_List.Add(icon_List15[i]);
                }
                break;

            case 16:
                for (int i = 0; i < icon_List16.Count; i++)
                {
                    moldIcon_List.Add(icon_List16[i]);
                }
                break;


            case 17:
                for (int i = 0; i < icon_List17.Count; i++)
                {
                    moldIcon_List.Add(icon_List17[i]);
                }
                break;

            case 18:
                for (int i = 0; i < icon_List18.Count; i++)
                {
                    moldIcon_List.Add(icon_List18[i]);
                }
                break;

            case 19:
                for (int i = 0; i < icon_List19.Count; i++)
                {
                    moldIcon_List.Add(icon_List19[i]);
                }
                break;
        }

        //selectedCategory_Text.text = categoryText_List[index].name;
        selectMold_Panel.SetActive(true);

        currentPage = 1;

        maxPage = moldIcon_List.Count;
        selectedCategory_Text.text = category_Dictionary[key];

        selectedMoldIcon_Image.sprite = moldIcon_List[currentPage - 1];
        selectedMoldIcon_Text.text = icon_Dictionry[int.Parse(moldIcon_List[currentPage - 1].name)];
        Page_Text.text = $"{currentPage}/{maxPage}";
    }

    //���� ���� �гο��� ���� ��ư�� Ŭ�� �� ȣ��Ǵ� �޼ҵ�
    public void NextBtn_Clicked()
    {
        if(currentPage < maxPage)
        {
            prievious_Btn.enabled = true;
            currentPage += 1;

            selectedMoldIcon_Image.sprite = moldIcon_List[currentPage - 1];
            selectedMoldIcon_Text.text = icon_Dictionry[int.Parse(moldIcon_List[currentPage - 1].name)];
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
            selectedMoldIcon_Text.text = icon_Dictionry[int.Parse(moldIcon_List[currentPage - 1].name)];
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

    }

    //���� ���� �гο��� x��ư�� ������ ȣ��Ǵ� �޼ҵ�
    public void BackBtn_Clicked()
    {
        selectMold_Panel.SetActive(false);
        moldIcon_List.Clear();
        next_Btn.enabled = true;
        Back_Btn.enabled = true;
    }



    #endregion

}
