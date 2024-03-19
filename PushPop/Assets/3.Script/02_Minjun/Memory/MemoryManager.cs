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
    [SerializeField] private LoadingPanel loadingCanvas = null;

    [Header("Panel")]
    [SerializeField] private GameObject resultPanel = null;
    [SerializeField] private GameObject warningPanel = null;   
    [SerializeField] private GameObject gameReadyPanel = null;
    [SerializeField] public GameObject hintGuidePanel = null;

    [Header("Game Info")]
    public TMP_Text StageText = null;
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
    [SerializeField] private TMP_Text currentScoreText = null;

    [Header("Game Result")]
    [SerializeField] private TMP_Text profileName = null;
    [SerializeField] private Image profileImage = null;
    [SerializeField] private TMP_Text resultMassageText = null;
    [SerializeField] private TMP_Text resultScoreText = null;

    [Header("Button")]
    public GameObject BackButton = null;
    [SerializeField] public Button Hintbutton;//힌트버튼

    public int saveStage;

    private int clearMessage;
   
    public bool isSave = true;
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
        // coroutine 초기화
        StopAllCoroutines();
        Destroy(CurrentBoard.gameObject);

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
      

        // socre setting
        Score = 0;
        currentScoreText.text = $"{Score}";
    }

    public void GameSetting()
    { // OnEnable() bubble size, board size, pushpopbutton size, pushpop percentage, etc. setting 관련
        Ranking.Instance.SettingPreviousScore(); // old score
        HintButtonActive(); // hint button Setting
        StageText.text = $"{CurrentStage} 단계";
    }

    public void GameStart()
    { // MultiCanvas에서 호출할 Game Start
        StartCoroutine(GameStart_Co());
    }

    public IEnumerator GameStart_Co()
    { // gameready coroutine -> gamestart 게임 시작 관련 코루틴
      // ready
        yield return new WaitForSeconds(0.5f);
        AudioManager.Instance.SetCommonAudioClip_SFX(1);
        memoryCanvas.GameReadyPanel.SetActive(true);
        memoryCanvas.GameReadyPanelText.text = "준비~";

        yield return new WaitForSeconds(2f);
        AudioManager.Instance.SetCommonAudioClip_SFX(2);
        memoryCanvas.GameReadyPanelText.text = "시작~";

        yield return new WaitForSeconds(0.8f);
        memoryCanvas.GameReadyPanel.SetActive(false);

        GameReadyStart();
    }

    public void GameReadyStart()
    {
        CreatBoard(); // Game start 이후 Memory Board에서 알아서 해줌
    }

    public void GameEnd()
    {
        if (isSave)
        {
            isSave = false;
            SavePoint.Instance.SetStage(ProfileManager.Instance.myProfile.name, ProfileManager.Instance.myProfile.index, CurrentStage - 1);
        }
        // 게임 종료, 결과 저장
        Ranking.Instance.SetScore(ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileName, ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex, Score);
        profileImage.sprite = ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileImage;
        profileName.text = ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileName;
        resultScoreText.text = $"{Score}점";
        clearMessage = (int)Ranking.Instance.CompareRanking(Score); // 점수 비교
        resultMassageText.text = Ranking.Instance.ResultDialog.memoryResult[clearMessage];
        isSave = true;
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
        Instantiate(stages[CurrentStage - 1].board, SapwnPos.position, Quaternion.identity, SapwnPos.transform);
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
    { // 틀렸을때 라이프 감소
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
        if (Score >= 800)
        { // 버튼 활성화
            Hintbutton.interactable = true;
        }
        else
        { // 버튼 비활성화
            Hintbutton.interactable = false;
        }
    }

    public void HintPanelActiveButton(bool _active)
    { // 힌트 버튼, 힌트 - 나가기 버튼
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        hintGuidePanel.SetActive(_active);
    }

    public void HintButtonBlinkRePlay()
    { // 힌트 버튼 - 힌트 사용
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        hintGuidePanel.SetActive(false);
        
        AddScore(-800); //800점 차감
        HintButtonActive();
        CurrentBoard.Blink(true);

        if (isSave)
        {
            isSave = false;
            SavePoint.Instance.SetStage(ProfileManager.Instance.myProfile.name, ProfileManager.Instance.myProfile.index, CurrentStage - 1);
        }

    }

    public void AddScore(int _score)
    { // 스코어 _score만큼 추가하고 text 변경
        Score += _score;
        currentScoreText.text = $"{Score}";
        HintButtonActive();
    }
    #endregion
    #region Result Panel
    public void ResultExitButton()
    { // Result Panel - 나가기
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        AudioManager.Instance.SetAudioClip_BGM(1);

        Time.timeScale = 1f;

        loadingCanvas.gameObject.SetActive(true);
        memoryCanvas.Ready.SetActive(true);
        memoryCanvas.HelpButton.SetActive(true);
        BackButton.SetActive(true);
        resultPanel.SetActive(false);
        gameObject.SetActive(false);
    }

    public void ResultRestartButton()
    { // Result Panel - 다시하기
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        
        if (GameManager.Instance.IsShutdown) return;
        
        loadingCanvas.gameObject.SetActive(true);
        resultPanel.SetActive(false);
        Time.timeScale = 1f;

        Init();
        GameSetting();
        GameStart();
    }
    #endregion
    #region Warning Panel, BackButton , ContinueBtn
    public void WarningPanelGoOutButton()
    { // 나가기
        AudioManager.Instance.Stop_SFX();
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        Time.timeScale = 1f;

        Init();
        loadingCanvas.gameObject.SetActive(true);
        warningPanel.SetActive(false);
        memoryCanvas.Ready.SetActive(true);
        memoryCanvas.HelpButton.SetActive(true);
        BackButton.SetActive(true);
        gameObject.SetActive(false);
    }

    public void WarningPanelCancelButton()
    { // 취소
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        Time.timeScale = 1f;

        warningPanel.SetActive(false);
    }

   
    #endregion

}

