using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemoryManager : MonoBehaviour, IGame
{
    public static MemoryManager Instance;
    [Header("Canvas")]
    [SerializeField] private MemoryCanvas memoryCanvas = null;

    [Header("Panel")]
    [SerializeField] private GameObject resultPanel = null;
    [SerializeField] private GameObject warngingPanel = null;   
    [SerializeField] private GameObject gameReadyPanel = null;
    
    [Header("Profile Info")]
    [SerializeField] private TMP_Text profileName;
    [SerializeField] private Image profileImage;

    [Header("Game Info")]
    [SerializeField] private Transform SapwnPos; // Board 생성 위치
    [SerializeField] private MemoryStageData[] stages; // Stage ScriptableObjects
    [SerializeField] private Animator stageStartImage = null; // animation 없어지면 gameObject로 받을 것... todo
    [SerializeField] private TMP_Text stageStartText = null;
    [SerializeField] private GameObject[] lifeObject; // 목숨 나타내는 하트오브젝트 배열
    [HideInInspector] public MemoryBoard CurrentBoard;
    [HideInInspector] public int CurrentStage = 1;
    [HideInInspector] public int EndStageIndex = 0; // Stage 전체 개수
    [HideInInspector] public int Life = 3;
    [HideInInspector] public int Score = 0;

    [Header("Game Result")]
    [SerializeField] private TMP_Text resultScoreText_del = null;
    public TMP_Text StageText = null;
    [SerializeField] private TMP_Text resultMassageText = null;
    [SerializeField] private TMP_Text resultScoreText;

    [SerializeField] private TMP_Text ReadyPanel_Text_del;

    [Header("버튼")]
    [SerializeField] public Button Hintbutton;//힌트버튼
    [SerializeField] public Button Backbutton;//뒤로가기버튼
    [SerializeField] public Image hintbuttonIamge;//힌트버튼

    [Header("로비OB")]
    [SerializeField] private GameObject Lobby; //푸시푸시 스피드 메모리 선택창
    [SerializeField] private Animator startAni; //게임시작 ,훌륭해요 재생해주는 판넬 Ani

    private int clearMessage;
    private Coroutine readyGameCoroutine = null;

    private void Awake()
    {
        Instance = this;
        MaxStageCount();
    }
    private void OnEnable()
    {
        GameSetting();
    }

    private void OnDisable()
    {
        Init();
    }
    #region Game Interface
    public void Init()
    { // OnDisable(), check list: coroutine, list, array, variables 초기화 관련
        // life setting
        Life = 3;
        for (int i = 0; i < lifeObject.Length; i++)
        {
            if (!lifeObject[2 - i].activeSelf)
            {
                lifeObject[2 - i].SetActive(true);
            }
        }

        // stage setting
        CurrentStage = 1;
        StageText.text = $"{CurrentStage} 단계";

        // socre setting
        Score = 0;
        resultScoreText_del.text = $"{Score}";
        HintButtonActive(); // hint button reset
        
        // coroutine 초기화
        StopAllCoroutines();
    }

    public void GameSetting()
    { // OnEnable() bubble size, board size, pushpopbutton size, pushpop percentage, etc. setting 관련
        // Stage 시작 시 어느 스테이지부터 시작할지 추가된다면 필요
    }

    public void GameStart()
    { // MultiCanvas에서 호출할 Game Start
        StartCoroutine(GameStart_Co());
    }

    public IEnumerator GameStart_Co()
    { // gameready coroutine -> gamestart 게임 시작 관련 코루틴
      // ready
        yield return new WaitForSeconds(0.5f);
        AudioManager.instance.SetCommonAudioClip_SFX(1);
        memoryCanvas.GameReadyPanelText.text = "준비~";
        memoryCanvas.GameReadyPanel.SetActive(true);

        yield return new WaitForSeconds(2f);
        AudioManager.instance.SetCommonAudioClip_SFX(2);
        memoryCanvas.GameReadyPanelText.text = "시작~";

        yield return new WaitForSeconds(0.8f);
        memoryCanvas.GameReadyPanel.SetActive(false);

        readyGameCoroutine = null;
        GameReadyStart();
    }

    public void GameReadyStart()
    {
        CreatBoard(); // Game start 이후 Memory Board에서 알아서 해줌
    }

    public void GameEnd()
    {
        StartCoroutine(Result_Co());
    }

    private IEnumerator Result_Co()
    {
        AudioManager.instance.SetAudioClip_SFX(5, false);
        // PlayStartPanel("게임 종료");

        yield return new WaitForSeconds(2f); // animation

        // 게임 종료, 결과 저장
        Ranking.Instance.SetScore(ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileName, ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex, Score);
        profileImage.sprite = ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileImage;
        profileName.text = ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileName;
        resultScoreText.text = $"{Score}";
        clearMessage = (int)Ranking.Instance.CompareRanking(); // 점수 비교
        resultMassageText.text = Ranking.Instance.ResultDialog.memoryResult[clearMessage];

        resultPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    #endregion
    #region Memory Game Setting
    public void MaxStageCount()
    { // 최대 스테이지 개수
        EndStageIndex = stages.Length;
    }

    public void PlayStartPanel(string _text)
    { // 스테이지 시작 멘트
        stageStartText.text = _text;
        stageStartImage.SetTrigger("isStart"); // animation 말고 직접 코루틴으로 바꿔줄 것... to. 민준
    }

    public void CreatBoard()
    { // 현재 스테이지에 맞는 보드판 소환
        Instantiate(stages[CurrentStage - 1].board, SapwnPos.position, Quaternion.identity, gameObject.transform);
    }

    public MemoryStageData GetStage()
    { // PushPopBoard 생성 될 때 현재 스테이지 관련 ScriptableObject 데이터 가져오기
        return stages[CurrentStage - 1];
    }

    public void SetStageIndex()
    { // 스테이지 바뀔때 text 변경
        StageText.text = $"{CurrentStage} 단계";
    }
    #endregion
    #region Game Life, Score, Hint
    public void LifeRemove()
    { // 틀렸을때 라이프 감소 메소드
        for (int i = 0; i < lifeObject.Length; i++)
        {
            if (lifeObject[2 - i].activeSelf)
            {
                lifeObject[2 - i].SetActive(false);
                break;
            }
        }
    }

    public void HintButtonActive()
    { // 점수 변동 시 점수에 따라 힌트 버튼 활성화 변경
        if (Score >= 300)
        { // 버튼 활성화
            Hintbutton.interactable = true;
        }
        else
        { // 버튼 비활성화
            Hintbutton.interactable = false;
        }
    }

    public void HintButtonBlinkRePlay()
    { // 힌트 버튼, 힌트 사용
        AddScore(-300); //300점 차감
        HintButtonActive();
        CurrentBoard.Blink(true);
    }

    public void AddScore(int _score)
    { // 스코어 _score만큼 추가하고 text변경 메소드
        Score += _score;
        resultScoreText_del.text = $"점수 : {Score}";
        HintButtonActive();
    }
    /*public void BlinkRePlay()
    {
        Score -= 300;
        ScoreText.text = $"점수 : {Score}";
        HintBtnActive();
        currentBoard.Blink(true);
    }*/
    #endregion
    #region Result Panel
    public void ResultExitButton()
    { // Result Panel - 나가기
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        AudioManager.instance.SetAudioClip_BGM(1);
        Time.timeScale = 1f;
        // PlayStartPanel("게임 종료");

        memoryCanvas.Ready.SetActive(true);
        memoryCanvas.BackButton.SetActive(true);
        memoryCanvas.HelpButton.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ResultRestartButton()
    { // Result Panel - 다시하기
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        Time.timeScale = 1f;

        Init();
        GameSetting();
        GameStart();
    }
    #endregion
}

