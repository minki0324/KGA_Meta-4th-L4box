using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ranking : MonoBehaviour
{
    public static Ranking instance = null;

    [Header("score")]
    [SerializeField] private TMP_Text score_txt;
    public int score;

    [Header("timer")]
    [SerializeField] private TMP_Text timer_txt;
    public float timer;

    [Header("Rank")]
    [SerializeField] private TMP_Text[] Name_Text;
    [SerializeField] private TMP_Text[] Rank_Text;

    #region Unity Callback
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    #endregion

    #region Other Method
    // ���ھ� ���� btn���� �׽�Ʈ �޼ҵ�
    public void test_Set_Score()
    {
        score = int.Parse(score_txt.text.ToString());

        SQL_Manager.instance.SQL_SetScore(GameManager.Instance.ProfileName, GameManager.Instance.ProfileIndex, score, null, GameManager.Instance.UID);
    }

    // Ÿ�̸� ���� btn���� �׽�Ʈ �޼ҵ�
    public void UpdateTimerScore(float timer)
    {
        SQL_Manager.instance.SQL_SetScore(GameManager.Instance.ProfileName, GameManager.Instance.ProfileIndex, null, timer, GameManager.Instance.UID);
    }

    // Ư�� ������ � mode����(speed, memory) �Ű������� �޾Ƽ� ��ŷ�� ȣ�����ִ� �޼ҵ�
    public void test_Print_Rank(string mode)
    {
        // 1. ��ŷ ���� ��������
        SQL_Manager.instance.SQL_PrintRanking();

        List<Rank> sortedRankList = new List<Rank>();

        if (mode == "Speed")
        {
            // Speed ���: Ÿ�̸Ӱ� 0���� ū �׸� �����Ͽ� Ÿ�̸Ӱ� ���� ������ ����
            sortedRankList = SQL_Manager.instance.Rank_List
                .Where(r => r.timer > 0) // Ÿ�̸Ӱ� 0���� ū �׸� ����
                .OrderBy(r => r.timer)
                .ThenByDescending(r => r.score)
                .Take(3)
                .ToList();
        }
        else if (mode == "Memory")
        {
            // Memory ���: ������ 0���� ū �׸� �����Ͽ� ������ ���� ������ ����
            sortedRankList = SQL_Manager.instance.Rank_List
                .Where(r => r.score > 0) // ������ 0���� ū �׸� ����
                .OrderByDescending(r => r.score)
                .ThenBy(r => r.timer)
                .Take(3)
                .ToList();
        }

        // 2. UI�� ���
        for (int i = 0; i < sortedRankList.Count; i++)
        {
            if (i < Name_Text.Length)
            {
                Name_Text[i].text = sortedRankList[i].name;
                // Mode�� ���� �ٸ� ���� ���
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

        // ������ UI �׸���� ����
        for (int i = sortedRankList.Count; i < Name_Text.Length; i++)
        {
            Name_Text[i].text = "";
            Rank_Text[i].text = "";
        }
    }
    #endregion

}
