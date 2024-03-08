using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemoryManager : MonoBehaviour
{
    public static MemoryManager Instance;
    [Header("Panel")]
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
    [SerializeField] public Image hintbuttonIamge;//힌트버튼

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
        Gameinit(); 
        GameManager.Instance.StartCoroutine(GameManager.Instance.GameReady_Co(ReadyPanel, ReadyPanel_Text));
    }
    #region 초기화 관련 메소드
    public void ResetLife()
    {
        for (int i = 0; i < Heart.Length; i++)
        {
            if (!Heart[2 - i].activeSelf)
            {
                Heart[2 - i].SetActive(true);
            }
        }
    } //게임끝났을때 라이프 초기화
    public void ResetScore()
    {
        Score = 0;
        ScoreText.text = $"점수 : {Score}";
    } //게임끝났을때 스코어 초기화
    public void SetStageIndex()
    {
        StageText.text = $"{currentStage} 스테이지";
    }  //스테이지 바뀔때 text 변경 
    #endregion
    #region 게임로직 관련 메소드
 
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
    }  //틀렸을때 라이프 감소 메소드
    public void AddScore(int _score)
    {
        Score += _score;
        ScoreText.text = $"점수 : {Score}";
        HintBtnActive();


    }//스코어 _score만큼 추가하고 text변경 메소드
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
    } // 점수변동 될때마다 불림. 점수에 따라 힌트 버튼 활성화변경
    public void BlinkRePlay()
    {
        AddScore(-300); //300점 차감
        HintBtnActive();
        currentBoard.Blink(true);
    } //힌트사용시 메소드

    #endregion
    #region 스테이지 시작 관련
    public void PlayStartPanel(string Text) //스테이지 시작할때 멘트 .
    { //넣어준 매
        StartAni.transform.GetChild(0).GetComponent<TMP_Text>().text = Text;
        StartAni.SetTrigger("isStart");
    }
    public void CreatBoard()
    {//현재 스테이지에 맞는 보드판 소환
        Instantiate(stages[currentStage - 1].board, SapwnPos.position, Quaternion.identity, gameObject.transform);
        DebugLog.instance.Adding_Message("되는데? 짱인데~");
    } // 게임시작 , 스테이지넘어갈때 새로운 PushPopBoard생성
    public MemoryStageData GetStage()
    {//다른곳에서 현재스테이지 가져오기
        return stages[currentStage - 1];
    } // PushPopBoard생성될때 현재 스테이지 관련 ScriptableObject 데이터 가져오기

    #endregion
    #region 게임종료 , 초기화 
    public void GameEnd(string _string)
    {//ResultPanel 다시시작버튼  : 저장하고 다시시작
        Memory_Canvas canvas = transform.root.GetComponent<Memory_Canvas>();

        //현재보드 꺼주기
        Destroy(currentBoard.gameObject);
     

        switch (_string)
        {
            case "Retry": // 라이프소진, 클리어시 결과판넬에서 다시하기 눌렀을때
                //점수 기록하기
                Ranking.Instance.SetScore(ProfileManager.Instance.ProfileName1P, ProfileManager.Instance.ProfileIndex1P, Score);
                //다시시작
                //다시시작할땐 OnEnable 호출안되서 수동으로 초기화 해줘야함
                Gameinit();
                GameManager.Instance.StartCoroutine(GameManager.Instance.GameReady_Co(ReadyPanel, ReadyPanel_Text));
                break;
            case "End"://라이프소진, 클리어시 나가기 눌렀을때
                //점수 기록하기
                Ranking.Instance.SetScore(ProfileManager.Instance.ProfileName1P, ProfileManager.Instance.ProfileIndex1P, Score);
                //로비로나가기
                StartCoroutine(ExitToLobby());
                break;

            case "GiveUp": // 게임도중 포기하고 나갔을때
                //로비로나가기
                StartCoroutine(ExitToLobby());
                break;

        }
        canvas.RankingLoad();
    } //게임종료
    public void Gameinit()
    {

        //stage초기화
        currentStage = 1;
        SetStageIndex();

        //라이프초기화
        Life = 3;
        ResetLife();

        //스코어초기화
        ResetScore();
        HintBtnActive();
        Time.timeScale = 1f;
    } // 초기화
    #endregion
    #region 게임종료시 ResultPanel 관련
    public void ResultPanelInit() //Result Panel 정보 Init
    {
        profileImage.sprite = ProfileManager.Instance.CacheProfileImage;
        profileName.text = ProfileManager.Instance.ProfileName1P;
        resultScore.text = $"{Score}";
        clearMessage = (int)Ranking.Instance.CompareRanking();
        resultMassage.text = Ranking.Instance.ResultDialog.memoryResult[clearMessage];
    } //
    public void Result() //라이프소진 , 스테이지 올 클리어시 결과판넬 관련 메소드
    {
        ResultPanelInit();
        AudioManager.instance.SetAudioClip_SFX(5, false);
        ResultPanel.SetActive(true);
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
    } //로비이동 코루틴

}

