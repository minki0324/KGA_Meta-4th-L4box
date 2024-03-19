using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Difficulty // Only Speed Mode
{
    Easy = 50,
    Normal = 40,
    Hard = 30
}

public class SpeedManager : MonoBehaviour, IGame
{ // speed game
    [Header("Canvas")]
    [SerializeField] private SpeedCanvas speedCanvas = null;
    [SerializeField] private LoadingPanel loadingCanvas = null;

    [Header("Panel")]
    [SerializeField] private GameObject resultPanel = null;
    [SerializeField] private GameObject warningPanel = null;

    [Header("Game Info")]
    public Difficulty Difficulty = Difficulty.Easy;
    public float difficultyCount = 0;
    [SerializeField] private Transform boardTrans = null;
    [SerializeField] private Transform buttonTrans = null;
    private GameObject touchBubble = null;
    private bool firstSetting = true;
    private Vector2[] bubblePos = { new Vector2(0f, 0f), new Vector2(0f, -20f) };

    [Header("Game Result")]
    [SerializeField] private TMP_Text resultTitle = null;
    [SerializeField] private Image resultImage = null;
    [SerializeField] private TMP_Text resultMassageText = null;
    [SerializeField] private TMP_Text resultScoreText = null;

    [Header("Timer And Count")]
    [SerializeField] private GameTimer gameTimer = null;
    [SerializeField] private Slider countSlider = null;

    private int clearMessage = 0;
    private bool isEndGame = false;

    private void OnEnable()
    {
        GameSetting();
    }

    private void Update()
    {
        if (isEndGame) return;
        GameEndSliderAfter();
    }

    private void OnDisable()
    {
        Init();
    }

    public void Init()
    { // OnDisable(), check list: coroutine, list, array, variables 초기화 관련
        GameManager.Instance.OnDestroyBubble -= BubbleOnDestroy; // action 삭제
        GameManager.Instance.GameEnd -= GameEnd;
        GameManager.Instance.LiveBubbleCount = 0;
        GameManager.Instance.bubbleObject.Clear();

        gameTimer.TimerText.color = new Color(0, 0, 0, 1);
        gameTimer.TenCount = false;
        gameTimer.EndTimer = false;
        countSlider.value = 0f;

        gameTimer.gameObject.SetActive(false);
        countSlider.gameObject.SetActive(false);
        firstSetting = true;
        isEndGame = false;

        PushPop.Instance.BoardPos = Vector2.zero;
        PushPop.Instance.Turning = false;
        PushPop.Instance.Init();
        if (touchBubble != null)
        {
            Destroy(touchBubble);
        }

        StopAllCoroutines();

        if (gameTimer.TimerCoroutine != null)
        {
            gameTimer.StopCoroutine(gameTimer.TimerCoroutine);
        }
    }
    public void GameSetting()
    { // OnEnable() bubble size, board size, pushpopbutton size, pushpop percentage, etc. setting 관련
        // action 추가
        GameManager.Instance.OnDestroyBubble += BubbleOnDestroy;
        GameManager.Instance.GameEnd += GameEnd;

        Ranking.Instance.SettingPreviousScore(); // old score
        PushPop.Instance.BoardSize = new Vector2(300f, 300f);
        PushPop.Instance.ButtonSize = new Vector2(80f, 80f);
        PushPop.Instance.Percentage = 0.67f;

        gameTimer.CurrentTime = 0f;
        float sec = gameTimer.CurrentTime % 60; // 60으로 나눈 나머지 = 초
        float min = gameTimer.CurrentTime / 60;
        gameTimer.TimerText.text = $"{string.Format("{0:00}", min)}:{string.Format("{0:00}", sec)}";

        Difficulty = GameManager.Instance.Difficulty;
        switch (Difficulty)
        {
            case Difficulty.Easy:
                difficultyCount = 1 / 3f;
                break;
            case Difficulty.Normal:
                difficultyCount = 1 / 4f;
                break;
            case Difficulty.Hard:
                difficultyCount = 1 / 5f;
                break;
        }
    }

    public void GameStart()
    {
        BoardInBubbleSetting();
    }

    public IEnumerator GameStart_Co()
    {
        Difficulty = GameManager.Instance.Difficulty;
        countSlider.gameObject.SetActive(true);
        gameTimer.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        AudioManager.Instance.SetCommonAudioClip_SFX(1);
        speedCanvas.GameReadyPanel.SetActive(true);
        speedCanvas.GameReadyPanelText.text = "준비~";

        yield return new WaitForSeconds(2f);

        AudioManager.Instance.SetCommonAudioClip_SFX(2);
        speedCanvas.GameReadyPanelText.text = "시작!";

        yield return new WaitForSeconds(0.8f);

        speedCanvas.GameReadyPanel.SetActive(false);
        // ready
        GameReadyStart();
    }

    public void GameReadyStart()
    {
        PushPop.Instance.BoardSize = new Vector2(700f, 700f);

        StartCoroutine(BoardCreate_Co());
        gameTimer.TimerStart(); // timer start
    }

    public void GameEnd()
    {
        if (PushPop.Instance.ActivePosCount.Equals(0) && !firstSetting)
        {
            if (!gameTimer.EndTimer)
            { // timer true or countslider.value >= 0.9f일 경우 게임 종료
                StartCoroutine(SpeedSlider_Co());
                StartCoroutine(BoardCreate_Co());
            }
        }
    }

    public void GameEndSliderAfter()
    {
        if (gameTimer.EndTimer || countSlider.value >= 0.9f)
        {
            AudioManager.Instance.Stop_SFX();
            AudioManager.Instance.SetCommonAudioClip_SFX(6);

            isEndGame = true;
            Ranking.Instance.SetTimer(ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileName, ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex, int.Parse(speedCanvas.SelectListSetting.BoardIcon.name), (int)gameTimer.CurrentTime);
            resultImage.sprite = speedCanvas.SelectListSetting.BoardIcon;

            int sec = (int)gameTimer.CurrentTime % 60;
            int min = (int)gameTimer.CurrentTime / 60;
            resultScoreText.text = $"{string.Format("{00:00}", min)}:{string.Format("{00:00}", sec)}";
            clearMessage = (int)Ranking.Instance.CompareRanking((int)gameTimer.CurrentTime); // 점수 비교
            resultTitle.text = Ranking.Instance.ResultDialog.title[clearMessage];
            resultMassageText.text = Ranking.Instance.ResultDialog.speedResult[clearMessage];
            if (gameTimer.TimerCoroutine != null)
            {
                gameTimer.StopCoroutine(gameTimer.TimerCoroutine);
            }

            resultPanel.SetActive(true);
        }
    }

    private void BoardInBubbleSetting()
    { // board in bubble setting
        touchBubble = Instantiate(PushPop.Instance.BoardPrefabUI, boardTrans); // image setting
        touchBubble.GetComponent<Image>().sprite = PushPop.Instance.BoardSprite;
        touchBubble.GetComponent<RectTransform>().sizeDelta = PushPop.Instance.BoardSize;
        GameManager.Instance.CreateBubble(PushPop.Instance.BoardSize, bubblePos[0], touchBubble); // board.transform.localPosition
        GameManager.Instance.LiveBubbleCount++;
    }

    public void BubbleOnDestroy()
    { // Bubble OnDestroy 시 호출되는 method
        Destroy(touchBubble);
        GameManager.Instance.LiveBubbleCount--;
        StartCoroutine(GameStart_Co());
    }

    private IEnumerator BoardCreate_Co()
    {
        if (!firstSetting)
        {
            for (int i = 0; i < PushPop.Instance.PushPopBoardObject.Count; i++)
            {
                Destroy(PushPop.Instance.PushPopBoardObject[i]);
            }
            PushPop.Instance.PushPopBoardObject.Clear();
            if (!PushPop.Instance.activePos.Count.Equals(0))
            {
                for (int i = 0; i < PushPop.Instance.activePos.Count; i++)
                {
                    Destroy(PushPop.Instance.activePos[i]);
                }
                PushPop.Instance.activePos.Clear();
            }
            for (int i = 0; i < PushPop.Instance.pushPopButton.Count; i++)
            {
                PushPop.Instance.pushPopButton[i].SetActive(false);
            }

            BoardTurningAnimation();
            yield return new WaitForSeconds(1f);

            for (int i = 0; i < PushPop.Instance.PushPopBoardUIObject.Count; i++)
            {
                Destroy(PushPop.Instance.PushPopBoardUIObject[i]);
            }
            PushPop.Instance.PushPopBoardUIObject.Clear();

            AudioManager.Instance.SetAudioClip_SFX(0, false);
        }

        PushPop.Instance.BoardPos = bubblePos[1];
        PushPop.Instance.CreatePushPopBoard(boardTrans);
        PushPop.Instance.CreateGrid(PushPop.Instance.PushPopBoardObject[0]);
        PushPop.Instance.PushPopButtonSetting(buttonTrans);
        firstSetting = false;
        PushPop.Instance.Turning = !PushPop.Instance.Turning; // prefab 생성 시 rotation 결정
    }

    private void BoardTurningAnimation()
    {
        Animator boardAni = PushPop.Instance.PushPopBoardUIObject[0].GetComponent<Animator>();
        boardAni.SetTrigger("Turning");
    }

    private IEnumerator SpeedSlider_Co()
    { // slider count
        float duration = 1f;
        float endValue = countSlider.value + difficultyCount;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            countSlider.value = Mathf.Lerp(countSlider.value, endValue, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        countSlider.value = endValue;
        GameEndSliderAfter();
    }
    #region Result Panel
    public void ResultExitButton()
    { // Result Panel - 나가기
        AudioManager.Instance.Stop_SFX();
        AudioManager.Instance.SetAudioClip_BGM(1);
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        Time.timeScale = 1f;

        loadingCanvas.gameObject.SetActive(true);
        speedCanvas.SelectCategoryPanel.SetActive(true);
        speedCanvas.HelpButton.SetActive(true);
        speedCanvas.BackButton.SetActive(true);
        resultPanel.SetActive(false);
        gameObject.SetActive(false);
    }

    public void ResultRestartButton()
    { // Result Panel - 다시하기
        AudioManager.Instance.Stop_SFX();
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        loadingCanvas.gameObject.SetActive(true);
        resultPanel.SetActive(false);

        Time.timeScale = 1f;

        Init();
        GameSetting();
        GameStart();
    }
    #endregion
    #region Warning Panel
    public void WarningPanelGoOutButton()
    { // Warning panel - 나가기
        AudioManager.Instance.Stop_SFX();
        AudioManager.Instance.SetAudioClip_BGM(1);
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        Time.timeScale = 1f;

        Init();
        loadingCanvas.gameObject.SetActive(true);
        warningPanel.SetActive(false);
        speedCanvas.SelectCategoryPanel.SetActive(true);
        speedCanvas.HelpButton.SetActive(true);
        speedCanvas.BackButton.SetActive(true);
        gameObject.SetActive(false);
    }

    public void WarningPanelCancelButton()
    { // Warning panel - 취소
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        AudioManager.Instance.Pause_SFX(false);

        Time.timeScale = 1f;

        warningPanel.SetActive(false);
    }
    #endregion

}
