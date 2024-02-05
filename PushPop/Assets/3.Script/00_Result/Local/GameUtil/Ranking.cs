using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ranking : MonoBehaviour
{
    [Header("score")]
    [SerializeField] private TMP_InputField score_txt;
    public int score;

    [Header("timer")]
    [SerializeField] private TMP_InputField timer_txt;
    public float timer;

    [Header("Rank")]
    [SerializeField] private TMP_Text[] Name_Text;
    [SerializeField] private TMP_Text[] Rank_Text;

    #region Unity Callback
    #endregion

    #region Other Method
    // 스코어 저장 btn연동 테스트 메소드
    public void test_Set_Score()
    {
        score = int.Parse(score_txt.text.ToString());

        SQL_Manager.instance.SQL_Set_Score(GameManager.instance.Profile_name, GameManager.instance.Profile_Index, score, null, GameManager.instance.UID);
    }

    // 타이머 저장 btn연동 테스트 메소드
    public void test_Set_Timer()
    {
        timer = float.Parse(timer_txt.text.ToString());

        SQL_Manager.instance.SQL_Set_Score(GameManager.instance.Profile_name, GameManager.instance.Profile_Index, null, timer, GameManager.instance.UID);
    }

    // 특정 시점에 어떤 mode인지(speed, memory) 매개변수를 받아서 랭킹을 호출해주는 메소드
    public void test_Print_Rank(string mode)
    {
        // 1. 랭킹 정보 가져오기
        SQL_Manager.instance.SQL_Print_Ranking();

        List<Rank> sortedRankList = new List<Rank>();

        if (mode == "Speed")
        {
            // Speed 모드: 타이머가 0보다 큰 항목만 포함하여 타이머가 낮은 순으로 정렬
            sortedRankList = SQL_Manager.instance.Rank_List
                .Where(r => r.timer > 0) // 타이머가 0보다 큰 항목만 선택
                .OrderBy(r => r.timer)
                .ThenByDescending(r => r.score)
                .Take(3)
                .ToList();
        }
        else if (mode == "Memory")
        {
            // Memory 모드: 점수가 0보다 큰 항목만 포함하여 점수가 높은 순으로 정렬
            sortedRankList = SQL_Manager.instance.Rank_List
                .Where(r => r.score > 0) // 점수가 0보다 큰 항목만 선택
                .OrderByDescending(r => r.score)
                .ThenBy(r => r.timer)
                .Take(3)
                .ToList();
        }

        // 2. UI에 출력
        for (int i = 0; i < sortedRankList.Count; i++)
        {
            if (i < Name_Text.Length)
            {
                Name_Text[i].text = sortedRankList[i].name;
                // Mode에 따라 다른 정보 출력
                if (mode == "Speed")
                {
                    Rank_Text[i].text = "Timer: " + sortedRankList[i].timer.ToString("F3");
                }
                else if (mode == "Memory")
                {
                    Rank_Text[i].text = "Score: " + sortedRankList[i].score.ToString();
                }
            }
        }

        // 나머지 UI 항목들을 비우기
        for (int i = sortedRankList.Count; i < Name_Text.Length; i++)
        {
            Name_Text[i].text = "";
            Rank_Text[i].text = "";
        }
    }
    #endregion

}
