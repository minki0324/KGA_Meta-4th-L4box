using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


//TimeSetting_Panel에 참조 넣기
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


    [Header("패널")]
    [SerializeField] private Canvas main_Canvas;
    [SerializeField] private GameObject gameSet_Panel; 
    [SerializeField] private GameObject memoryGame_Panel;
    [SerializeField] private Help_Canvas help_Canvas;

    [Header("개인 기록")] 
    [SerializeField] private Image profile_Image;
    [SerializeField] private TMP_Text profileName_Text;
    [SerializeField] private TMP_Text score_Text;


    [Header("버튼")]
    [SerializeField] private Button gameStart_Btn;


    [Header("랭킹")]
    [SerializeField] private GameObject rankObject;
   // [SerializeField] private List<Rank> rank_List = new List<Rank>();
    [SerializeField] private List<Image> rankIconImage_List = new List<Image>();
    [SerializeField] private List<Image> profileImage_List = new List<Image>();
    [SerializeField] private List<TMP_Text> profileName_List = new List<TMP_Text>();
    [SerializeField] private List<TMP_Text> scoreText_List = new List<TMP_Text>();


    private void OnEnable()
    {
        if(!gameSet_Panel.activeSelf)
        {
            gameSet_Panel.SetActive(true);
        }

        if (memoryGame_Panel.activeSelf)
        {
            memoryGame_Panel.SetActive(false);
        }
        if(!help_Canvas.gameObject.activeSelf)
        {
            help_Canvas.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        Init();
    }

    private void OnDisable()
    {
        //help_Canvas.gameObject.SetActive(false);
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

    //게임 시작 버튼 클릭 시 호출되는 함수
    public void GameStartBtn_Clicked()
    {
        gameSet_Panel.SetActive(false);
        help_Canvas.gameObject.SetActive(false);

        memoryGame_Panel.SetActive(true);    
    }


    public void BackBtn_Clicked()
    {
        main_Canvas.gameObject.SetActive(true);
        help_Canvas.gameObject.SetActive(false);

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
        //디비에서 개인 랭킹 읽어온 후 계산
    }



}
