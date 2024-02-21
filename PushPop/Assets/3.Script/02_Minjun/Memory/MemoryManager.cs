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
    public void onStageFail()
    {//게임종료메소드
        //현재보드 꺼주기
        Destroy(currentBoard.gameObject);
        //stage초기화
        currentStage = 1;
        SetStageIndex();
        //라이프초기화
        Life = 3;
        ResetLife();
        //점수 기록하기
        SQL_Manager.instance.SQL_SetScore(
            GameManager.Instance.ProfileName, GameManager.Instance.ProfileIndex, Score, null, GameManager.Instance.UID);
        //스코어초기화
        ResetScore();
        //메모리 로비로 나가기
       // StartCoroutine(ExitToLobby());
    }
    public void onStageFail(bool isGiveUp)
    {//게임종료메소드
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
        //StartCoroutine(ExitToLobby());
        SQL_Manager.instance.SQL_ProfileListSet();
    }
    private IEnumerator ExitToLobby()
    {
        PlayStartPanel("왤케못하니?");
        yield return new WaitForSeconds(2f);
        Lobby.SetActive(true);
        gameObject.SetActive(false);
    }
}
