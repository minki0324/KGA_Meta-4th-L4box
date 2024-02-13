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


    [Header("난이도 버튼")]
    [SerializeField] private List<Button> Difficulty_Btn;



    [Header("난이도 별 아이콘 리스트")]
    [SerializeField] private List<Sprite> easyIcon_List;
    [SerializeField] private List<Sprite> normalIcon_List;
    [SerializeField] private List<Sprite> hardIcon_List;


    [SerializeField] private List<TMP_Text> easyText_List; 
    [SerializeField] private List<TMP_Text> normalText_List; 
    [SerializeField] private List<TMP_Text> hardText_List; 



    public void DifficultyBtn_Clicked(int index)
    {
        switch(index)
        {
            case 0:
                difficulty = Difficulty.Easy;
                break;

            case 1:
                difficulty = Difficulty.Normal;
                break;

            case 2:
                difficulty = Difficulty.Hard;
                break;
        }

    }
}
