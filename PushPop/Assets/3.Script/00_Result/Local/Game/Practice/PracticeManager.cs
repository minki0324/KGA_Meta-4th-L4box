using System.Collections;
using TMPro;
using UnityEngine;

public class PracticeManager : MonoBehaviour, IGame
{
    [SerializeField] private MainCanvas mainCanvas;
    [SerializeField] private PracticeCanvas practiceCanvas;
    public GameObject WarningPanel;

    [SerializeField] private Sprite[] boardSprite;
    private Sprite selectSprite = null;
    [SerializeField] private Transform boardTrans;
    [SerializeField] private Transform buttonTrans;
    [SerializeField] private TMP_Text switchButtonText;
    private Vector2[] boardSize = { new Vector2(850f, 850f), new Vector2(800f, 800f) }; // 0: 네모, 1: 동그라미
    private bool isCircle = false; // false는 네모, true는 동그라미
    private bool isSwitchBoard = true; // true 때 처음 switch

    private void OnEnable()
    {
        GameSetting();
    }

    private void OnDisable()
    {
        Init();
    }

    public void Init()
    {
        GameManager.Instance.GameEnd -= GameEnd;
        PushPop.Instance.Init();
        PushPop.Instance.Turning = false;
        PushPop.Instance.BoardPos = Vector2.zero;

        StopAllCoroutines();
    }

    public void GameSetting()
    {
        GameManager.Instance.GameEnd += GameEnd;
        selectSprite = boardSprite[0];
        PushPop.Instance.BoardSize = boardSize[0];
        PushPop.Instance.ButtonSize = new Vector2(120f, 120f);
        PushPop.Instance.Percentage = 1.2f;

        GameStart();
    }

    public void GameStart()
    {
        PushPop.Instance.BoardPos = Vector2.zero;
        PushPop.Instance.BoardSprite = selectSprite;
        PushPop.Instance.CreatePushPopBoard(boardTrans);
        PushPop.Instance.CreateGrid();
        PushPop.Instance.PushPopButtonSetting(buttonTrans);
        PushPop.Instance.Turning = !PushPop.Instance.Turning; // prefab 생성 시 rotation 결정
    }

    private void GameEnd()
    {
        isSwitchBoard = false;
        if (PushPop.Instance.ActivePosCount.Equals(0))
        {
            StartCoroutine(GameStart_Co());
        }
    }

    public IEnumerator GameStart_Co()
    { // switch button Click
        if (!isSwitchBoard)
        {
            if(!PushPop.Instance.PushPopBoardObject.Count.Equals(0))
            {
                for (int i = 0; i < PushPop.Instance.PushPopBoardObject.Count; i++)
                {
                    Destroy(PushPop.Instance.PushPopBoardObject[i]);
                }
                PushPop.Instance.PushPopBoardObject.Clear();
            }
            
            if (!PushPop.Instance.activePos.Count.Equals(0))
            {
                for (int i = 0; i < PushPop.Instance.activePos.Count; i++)
                {
                    Destroy(PushPop.Instance.activePos[i]);
                }
                PushPop.Instance.activePos.Clear();
            }
            if (!PushPop.Instance.pushPopButton.Count.Equals(0))
            {
                for (int i = 0; i < PushPop.Instance.pushPopButton.Count; i++)
                {
                    Destroy(PushPop.Instance.pushPopButton[i]);
                }
                PushPop.Instance.pushPopButton.Clear();
            }

            BoardTurningAnimation();
            yield return new WaitForSeconds(0.8f);

            for (int i = 0; i < PushPop.Instance.PushPopBoardUIObject.Count; i++)
            {
                Destroy(PushPop.Instance.PushPopBoardUIObject[i]);
            }
            PushPop.Instance.PushPopBoardUIObject.Clear();

            AudioManager.Instance.SetAudioClip_SFX(0, false);
        }

        PushPop.Instance.BoardSprite = selectSprite;
        PushPop.Instance.CreatePushPopBoard(boardTrans);
        PushPop.Instance.CreateGrid();
        PushPop.Instance.PushPopButtonSetting(buttonTrans);
        PushPop.Instance.Turning = !PushPop.Instance.Turning; // prefab 생성 시 rotation 결정
    }

    public void SwitchBoardImage()
    { // board swtich button
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        isCircle = !isCircle;
        isSwitchBoard = true;

        if (!isCircle)
        {
            PushPop.Instance.BoardSize = boardSize[0];
            selectSprite = boardSprite[0];
            switchButtonText.text = "동그라미";
        }
        else
        {
            PushPop.Instance.BoardSize = boardSize[1];
            selectSprite = boardSprite[1];
            switchButtonText.text = $"네모";
        }

        PushPop.Instance.Init();
        PushPop.Instance.Turning = false;
        StopAllCoroutines();
        StartCoroutine(GameStart_Co());
    }

    private void BoardTurningAnimation()
    { // board 회전 animation
        Animator boardAni = PushPop.Instance.PushPopBoardUIObject[0].GetComponent<Animator>();
        boardAni.SetTrigger("Turning");
    }

    #region Warning Panel
    public void WarningPanelGoOutButton()
    { // Warning panel - 나가기
        AudioManager.Instance.SetAudioClip_BGM(0);
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        LoadingPanel.Instance.gameObject.SetActive(true);
        GameManager.Instance.GameMode = GameMode.Lobby;
        Time.timeScale = 1f;
        Init();
        WarningPanel.SetActive(false);
        mainCanvas.gameObject.SetActive(true);
        practiceCanvas.gameObject.SetActive(false);
    }

    public void WarningPanelCancelButton()
    { // Warning panel - 취소
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        Time.timeScale = 1f;
        WarningPanel.SetActive(false);
    }
    #endregion
}
