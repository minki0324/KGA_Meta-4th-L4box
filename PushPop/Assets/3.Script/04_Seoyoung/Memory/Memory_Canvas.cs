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
    [SerializeField] private GameObject memoryGame_Canvas;
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
    [SerializeField] private Image[] profileImage_Array;
    [SerializeField] private TMP_Text[] profileName_Array;
    [SerializeField] private TMP_Text[] scoreText_Array;
    [SerializeField] private Image progileImage_Personal;
    [SerializeField] private TMP_Text profileName_Personal;
    [SerializeField] private TMP_Text scoreText_Personal;


    #region Unity Callback
    private void OnEnable()
    {
        AudioManager.instance.SetAudioClip_BGM(1);
        if (!gameSet_Panel.activeSelf)
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


    //게임 시작 버튼 클릭 시 호출되는 함수
    public void GameStartBtn_Clicked()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(0);
        gameSet_Panel.SetActive(false);
        help_Canvas.gameObject.SetActive(false);
        memoryGame_Canvas.gameObject.SetActive(true);    
    }


    public void BackBtn_Clicked()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        gameSet_Panel.gameObject.SetActive(false);
        help_Canvas.gameObject.SetActive(false);

        AudioManager.instance.SetAudioClip_BGM(0);
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

    public void RankingLoad()
    {
        Ranking.Instance.LoadScore(scoreText_Array, profileImage_Array, profileName_Array);
        Ranking.Instance.LoadScore_Personal(profileName_Personal, scoreText_Personal, progileImage_Personal);
    }

    #endregion

}
