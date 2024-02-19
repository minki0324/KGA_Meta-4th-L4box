using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


//TimeSetting_Panel�� ���� �ֱ�
public class Memory_Canvas : MonoBehaviour
{
    //public struct Rank
    //{
    //    public Image rankIcon_Image;
    //    public Image profile_Image;
    //    public TMP_Text profileName_Text;
    //    public TMP_Text score_Text;

    //    public int score;
    //}


    [Header("�г�")]
    [SerializeField] private Canvas main_Canvas;
    [SerializeField] private GameObject gameSet_Panel; 
    [SerializeField] private GameObject memoryGame_Panel;

    [Header("���� ���")] 
    [SerializeField] private Image profile_Image;
    [SerializeField] private TMP_Text profileName_Text;
    [SerializeField] private TMP_Text score_Text;

    [Header("��ŷ")]
    [SerializeField] private GameObject rankObject;
   // [SerializeField] private List<Rank> rank_List = new List<Rank>();
    [SerializeField] private List<Image> rankIconImage_List = new List<Image>();
    [SerializeField] private List<Image> profileImage_List = new List<Image>();
    [SerializeField] private List<TMP_Text> profileName_List = new List<TMP_Text>();
    [SerializeField] private List<TMP_Text> scoreText_List = new List<TMP_Text>();

    [Header("��ư")]
    [SerializeField] private Button gameStart_Btn;


    private void OnEnable()
    {

        if (memoryGame_Panel.activeSelf)
        {
            memoryGame_Panel.SetActive(false);
        }
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        for (int i = 0; i < rankObject.transform.childCount; i++)
        {
            rankIconImage_List.Add(rankObject.transform.GetChild(i).GetChild(0).GetComponent<Image>());
            profileImage_List.Add(rankObject.transform.GetChild(i).GetChild(1).GetComponent<Image>());
            profileName_List.Add(rankObject.transform.GetChild(i).GetChild(2).GetComponent<TMP_Text>());
            scoreText_List.Add(rankObject.transform.GetChild(i).GetChild(3).GetComponent<TMP_Text>());
        }

    }

    //���� ���� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void GameStartBtn_Clicked()
    {
        gameSet_Panel.SetActive(false);
        memoryGame_Panel.SetActive(true);    
    }


    public void BackBtn_Clicked()
    {
        main_Canvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void EnableObjects()
    {
        gameStart_Btn.interactable = true;
    }

    public void DisalbeObjects()
    {
        gameStart_Btn.interactable = false;
    }

    public void Calculate_Rank()
    {
        //��񿡼� ���� ��ŷ �о�� �� ���
    }



}
