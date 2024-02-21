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
    [SerializeField] private Canvas memoryGame_Canvas;
    [SerializeField] private Help_Canvas help_Canvas;

    [Header("���� ���")] 
    [SerializeField] private Image profile_Image;
    [SerializeField] private TMP_Text profileName_Text;
    [SerializeField] private TMP_Text score_Text;


    [Header("��ư")]
    [SerializeField] private Button gameStart_Btn;


    [Header("��ŷ")]
    [SerializeField] private GameObject rankObject;
   // [SerializeField] private List<Rank> rank_List = new List<Rank>();
    [SerializeField] private Image[] profileImage_Array;
    [SerializeField] private TMP_Text[] profileName_Array;
    [SerializeField] private TMP_Text[] scoreText_Array;


    #region Unity Callback
    private void OnEnable()
    {
        if(!gameSet_Panel.activeSelf)
        {
            gameSet_Panel.SetActive(true);
        }

        if (memoryGame_Canvas.gameObject.activeSelf)
        {
            memoryGame_Canvas.gameObject.SetActive(false);
        }
        if(!help_Canvas.gameObject.activeSelf)
        {
            help_Canvas.gameObject.SetActive(true);
        }
    }

  
    private void OnDisable()
    {
        //help_Canvas.gameObject.SetActive(false);
    }

    #endregion

    #region Other Method


    //���� ���� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void GameStartBtn_Clicked()
    {
        gameSet_Panel.SetActive(false);
        help_Canvas.gameObject.SetActive(false);

        memoryGame_Canvas.gameObject.SetActive(true);    
    }


    public void BackBtn_Clicked()
    {
        gameSet_Panel.gameObject.SetActive(false);
        help_Canvas.gameObject.SetActive(false);

        main_Canvas.gameObject.SetActive(true);
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
    public void RangkinLoad()
    {
        //Ranking.instance.LoadScore(scoreText_Array, profileImage_Array, profileName_Array);
    }

    #endregion

}
