using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemoryManager : MonoBehaviour
{
    public static MemoryManager Instance;
    [Header("판넬")]
    [SerializeField] public GameObject ResultPanel; //라이프소진 , AllClear시 뜨는 결과창
    [SerializeField] public GameObject WarngingPanel;   
    [SerializeField] private GameObject ReadyPanel;
    [Header("Text")]
    [SerializeField] public TMP_Text ScoreText; //점수텍스트
    [SerializeField] public TMP_Text StageText; //화면상 표시하는 스테이지
    [SerializeField] private TMP_Text ReadyPanel_Text;
    [SerializeField] private TMP_Text resultMassage;
    [SerializeField] private TMP_Text resultScore;

    [Header("게임플레이OB")]
    [SerializeField] private GameObject[] Heart; //목숨나타내는 하트오브젝트 배열
    [Header("버튼")]
    [SerializeField] public Button Hintbutton;//힌트버튼
    [SerializeField] public Button Backbutton;//뒤로가기버튼

    [Header("로비OB")]
    [SerializeField] private GameObject Lobby; //푸시푸시 스피드 메모리 선택창
    [SerializeField] private Animator StartAni; //게임시작 ,훌륭해요 재생해주는 판넬 Ani
    [Header("프로필관련")]
    [SerializeField] private TMP_Text profileName;
    [SerializeField] private Image profileImage;

    public MemoryBoard currentBoard; //현재 소환되있는 푸시팝보드판
    public MemoryStageData[] stages; //스테이지 ScriptableObject 배열 현재스테이지에따라 설정이다름 / 보드판,정답갯수, 스페셜스테이지여부
    public int currentStage = 1; //현재스테이지
    public int Life = 3; //현재라이프
    public int Score = 0; //현재스코어
    public Transform SapwnPos; //푸시팝보드판 소환위치
    public int endStageIndex = 0;

    private int clearMessage;

    private void Awake()
    {
        Instance = this;
        endStageIndex = stages.Length;
    }
    private void OnEnable()
    {
        //처음 Gameplay판넬 시작시 보드판소환(게임시작) 
     
        StartCoroutine(ReadyGame_Co());
    }
    public void CreatBoard()
    {//현재 스테이지에 맞는 보드판 소환
        Instantiate(stages[currentStage - 1].board, SapwnPos.position, Quaternion.identity, gameObject.transform);
    }
    public MemoryStageData GetStage()
    {//다른곳에서 현재스테이지 가져오기
        return stages[currentStage - 1];
    }
    public void PlayStartPanel(string Text)
    { //넣어준 매
        StartAni.transform.GetChild(0).GetComponent<TMP_Text>().text = Text;
        StartAni.SetTrigger("isStart");
    }
    public void SetStageIndex()
    {
        StageText.text = $"{currentStage} 스테이지";
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
        HintBtnActive();


    }
    public void HintBtnActive()
    {//점수가 늘어나거나 줄어들때(힌트를 볼때) 버튼을 활성화 하거나 비활성화시킴.
        if (Score >= 300)
        {
            //버튼활성화
            Hintbutton.interactable = true;
        }
        else
        {
            //버튼 비활성화
            Hintbutton.interactable = false;
        }
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
        Memory_Canvas canvas = transform.root.GetComponent<Memory_Canvas>();

        //현재보드 꺼주기
        Destroy(currentBoard.gameObject);
        //stage초기화
        currentStage = 1;
        SetStageIndex();
        //라이프초기화
        Life = 3;
        ResetLife();
        //점수 기록하기
        Ranking.Instance.SetScore(GameManager.Instance.ProfileName, GameManager.Instance.ProfileIndex, Score);
        //스코어초기화
        ResetScore();

        canvas.RankingLoad();

        if (isRetry)
        {//다시하기 버튼
            StartCoroutine(ReadyGame_Co());
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
   
        //스코어초기화
        ResetScore();
        Time.timeScale = 1f;
        //메모리 로비로 나가기
        StartCoroutine(ExitToLobby());
        //SQL_Manager.instance.SQL_ProfileListSet()
    }
    #endregion

    private IEnumerator ExitToLobby()
    {//로비나가기
        WarngingPanel.SetActive(false);
        ResultPanel.SetActive(false);

        PlayStartPanel("게임종료");
        Debug.Log("코루틴전");
        yield return new WaitForSeconds(2f);
        Debug.Log("코루틴후");

        GetComponent<Memory_Game>().GoOutBtn_Clicked();
    }
    public void BlinkRePlay()
    {
        Score -= 300;
        ScoreText.text = $"점수 : {Score}";
        HintBtnActive();
        currentBoard.Blink(true);
    }
    private IEnumerator ReadyGame_Co()
    {
        Ranking.Instance.SettingPreviousScore();
        ReadyPanel.SetActive(true);
        ReadyPanel_Text.text = "준비~";
        yield return new WaitForSeconds(2f);

        ReadyPanel_Text.text = "시작!";
        yield return new WaitForSeconds(0.8f);
        ReadyPanel.SetActive(false);
        CreatBoard();
        // upperBubble 코루틴 실행
    }

    public void OnGameEnd()
    {
        profileImage.sprite = GameManager.Instance.CacheProfileImage1P;
        profileName.text = GameManager.Instance.ProfileName;
        resultScore.text = $"{Score}";
        clearMessage = (int)Ranking.Instance.CompareRanking();
        resultMassage.text = Ranking.Instance.ResultDialog.memoryResult[clearMessage];
    }
}
