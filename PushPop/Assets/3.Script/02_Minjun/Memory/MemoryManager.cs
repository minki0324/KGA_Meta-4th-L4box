using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemoryManager : MonoBehaviour
{
    public static MemoryManager Instance;
    [SerializeField] private Animator StartPanel;
    [SerializeField] public TMP_Text StageIndex;
    [SerializeField] public TMP_Text ScoreText;
    [SerializeField] private GameObject Lobby;
    [SerializeField] public GameObject ResultPanel;
    [SerializeField] private GameObject[] Heart;
    
    public MemoryBoard currentBoard;
    public int currentStage = 1;
    public int Life = 3;
    public int Score = 0;
    public MemoryStageData[] stages;
    public Transform SapwnPos;
    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        CreatBoard();
    }
    public void CreatBoard()
    {
        Instantiate(stages[currentStage - 1].board, SapwnPos.position, Quaternion.identity, gameObject.transform);
    }
    public MemoryStageData GetStage()
    {
        return stages[currentStage - 1];
    }
    public void PlayStartPanel(string Text)
    {
        StartPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = Text;
        StartPanel.SetTrigger("isStart");
    }
    public void SetStageIndex()
    {
        StageIndex.text = $"{currentStage} 스테이지";
    }
    public void LifeRemove()
    {
        for (int i = 0; i < Heart.Length; i++)
        {
            if (Heart[2 - i].activeSelf)
            {
                Heart[2 - i].SetActive(false);
                break;
            }
        }
    }

    public void ResetLife()
    {
        for (int i = 0; i < Heart.Length; i++)
        {
            if (!Heart[2 - i].activeSelf)
            {
                Heart[2 - i].SetActive(true);
            }
        }
    }
    public void AddScore()
    {
        Score += 100;
        ScoreText.text = $"점수 : {Score}";
    }
    public void ResetScore()
    {
        Score = 0;
        ScoreText.text = $"점수 : {Score}";
    }
    #region 게임이 끝났을때 승리패배 or 포기
    //Stage 단위 판단은 MemoryPushpop에 있음
    public void onStageEnd(bool isRetry =false)
    {//ResultPanel 다시시작버튼  : 저장하고 다시시작
        //현재보드 꺼주기

        Destroy(currentBoard.gameObject);
        //stage초기화
        currentStage = 1;
        SetStageIndex();
        //라이프초기화
        Life = 3;
        ResetLife();
        //점수 기록하기 //머지후 주석풀기
        //Ranking.instance.SetScore(GameManager.Instance.ProfileName, GameManager.Instance.ProfileIndex, Score);
        //스코어초기화
        ResetScore();
       
        if (isRetry)
        {//다시하기 버튼
            CreatBoard();
        }
        else
        {//나가기 버튼
            StartCoroutine(ExitToLobby());
        }
    }
    public void onStageFail(bool isGiveUp)
    {//게임종료메소드 (포기) 게임도중 Back버튼 : 저장안되고 나가짐
        //현재보드 꺼주기
        Destroy(currentBoard.gameObject);
        //stage초기화
        currentStage = 1;
        SetStageIndex();

        //라이프초기화
        Life = 3;
        ResetLife();
        Ranking.instance.score = Score;
        Ranking.instance.test_Set_Score();
        Ranking.instance.test_Print_Rank("Memory");
        //스코어초기화
        ResetScore();
        //메모리 로비로 나가기
        StartCoroutine(ExitToLobby());
        //SQL_Manager.instance.SQL_ProfileListSet()

    }
    #endregion

    private IEnumerator ExitToLobby()
    {
        PlayStartPanel("게임종료");
        yield return new WaitForSeconds(2f);
        Lobby.SetActive(true);
        gameObject.SetActive(false);
    }
}
