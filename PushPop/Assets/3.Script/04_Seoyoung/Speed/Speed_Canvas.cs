using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Speed_Canvas : MonoBehaviour
{

    public enum Difficulty
    {
        Easy = 0,
        Normal,
        Hard
    }

    public Difficulty difficulty;

    [Header("�г�")]
    [SerializeField] private GameObject selectDifficulty_Panel;
    [SerializeField] private GameObject selectCategory_Panel;
    [SerializeField] private GameObject ready_Panel;

    [Header("���� ������Ʈ/������")]
    [SerializeField] private GameObject Content;
    [SerializeField] private GameObject Mold_Prefab;
    

    [Header("���̵� ��ư")]
    [SerializeField] private List<Button> Difficulty_Btn;

    [Header("Ready�г� ����")]
    [SerializeField] private Image selected_Image;
    [SerializeField] private TMP_Text selected_Text;

    [Header("���̵� �� ������ ����Ʈ")]
    [SerializeField] private List<Sprite> easyIcon_List;
    [SerializeField] private List<Sprite> normalIcon_List;
    [SerializeField] private List<Sprite> hardIcon_List;

    [SerializeField] private List<Button> iconButton_List;


    #region Unity Callback

    private void Start()
    {
        iconButton_List = new List<Button>();
        selectCategory_Panel.SetActive(false);
        ready_Panel.SetActive(false);
    }

    #endregion

    #region Other Method


    public void DifficultyBtn_Clicked(int index)
    {
        selectCategory_Panel.SetActive(true);

        //-----------�ڷΰ��⿡ ���� ��----------------------------------
        for (int i = 0; i < Content.transform.childCount; i++)
        {
            Destroy(Content.transform.GetChild(i).gameObject);
        }
        iconButton_List.Clear();

        //-------------------------------------------------------------

        switch (index)
        {
            case 0:
                difficulty = Difficulty.Easy;
                for (int i = 0; i < easyIcon_List.Count; i++)
                {
                    GameObject a = Instantiate(Mold_Prefab, Content.transform);

                    //��ư ����Ʈ �ʱ�ȭ
                    iconButton_List.Add(a.transform.GetChild(0).GetComponent<Button>());
                

                    //��������Ʈ ����
                    a.transform.GetChild(0).GetComponent<Image>().sprite = easyIcon_List[i];

                    //�ؽ�Ʈ ���� : ��������Ʈ �̸��� Ű������ value��������
                    a.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = Mold_Dictionary.instance.icon_Dictionry[int.Parse(easyIcon_List[i].name)];
                }

                break;

            case 1:
                difficulty = Difficulty.Normal;
                for (int i = 0; i < normalIcon_List.Count; i++)
                {
                    GameObject a = Instantiate(Mold_Prefab, Content.transform);

                    //��ư ����Ʈ �ʱ�ȭ
                    iconButton_List.Add(a.transform.GetChild(0).GetComponent<Button>());

                    //��������Ʈ ����
                    a.transform.GetChild(0).GetComponent<Image>().sprite = normalIcon_List[i];

                    //�ؽ�Ʈ ����
                    a.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = Mold_Dictionary.instance.icon_Dictionry[int.Parse(normalIcon_List[i].name)];
                }
                break;

            case 2:
                difficulty = Difficulty.Hard;
                for (int i = 0; i < hardIcon_List.Count; i++)
                {
                    GameObject a = Instantiate(Mold_Prefab, Content.transform);

                    //��ư ����Ʈ �ʱ�ȭ
                    iconButton_List.Add(a.transform.GetChild(0).GetComponent<Button>());

                    //��������Ʈ ����
                    a.transform.GetChild(0).GetComponent<Image>().sprite = hardIcon_List[i];

                    //�ؽ�Ʈ ����
                    a.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = Mold_Dictionary.instance.icon_Dictionry[int.Parse(hardIcon_List[i].name)];
                }
                break;
        }

        for(int i = 0; i<iconButton_List.Count; i++)
        {
            int temp = i;
            iconButton_List[temp].onClick.AddListener(delegate { IconBtn_Clicked(iconButton_List[temp].gameObject); });
        }

    }



    public void IconBtn_Clicked(GameObject button)
    {
        ready_Panel.SetActive(true);
        selected_Image.sprite = button.GetComponent<Image>().sprite;
        selected_Text.text = button.transform.GetChild(0).GetComponent<TMP_Text>().text;
    }


  
    public void BackBtn_Clicked()
    {
        ready_Panel.SetActive(false);
    }


    #endregion

}
