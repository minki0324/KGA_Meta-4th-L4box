using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using System;

public enum Turn
{
    Turn1P = 0,
    Turn2P
}

/// <summary>
/// 2인 모드(폭탄 돌리기) 관련 Class
/// </summary>
public class MultiManager : MonoBehaviour, IGame
{ // multi game
    [Header("Quit")]
    [SerializeField] private Button[] quitButton;  // 나가기 버튼

    [Header("Panel")]
    [SerializeField] private GameObject gameReadyPanel = null;
    [SerializeField] private TMP_Text gameReadyPanelText = null; // ready and end 공용
    [SerializeField] private GameObject warningPanel = null;
    [SerializeField] private GameObject resultPanel = null;

    [Header("Player Info")]
    [SerializeField] private Image[] profileImage = null;
    [SerializeField] private TMP_Text[] profileName = null;
    private bool[] quitButtonClick = { false, false }; // 1P, 2P 뒤로가기 버튼 클릭 시 true

    [Header("Game Info")]
    [SerializeField] private Turn playerTurn = Turn.Turn1P; // 현재 턴 기준


    // poplist 0일 때 턴 넘김
    public List<GameObject> popList1P = new List<GameObject>(); // 1P 의 Pushpop button list -> object pooling 필요
    public List<GameObject> popList2P = new List<GameObject>(); // 2P 의 Pushpop List
    [SerializeField] private SpriteAtlas boardSprite = null; // pushPop board sprite
    [SerializeField] private Sprite[] sprites = null;   // atlas의 sprite들 배열

    // bubble
    private float upperTimer = 12f;
    [SerializeField] private Transform[] boardPos; // Pushpop Board가 소환될 Parent
    [SerializeField] private Vector2[] upperPos = new Vector2[2]; // player1, player2의 위 bubble pos
    [SerializeField] private Vector2[] bottomPos = new Vector2[2]; // player1, player2의 아래 bubble pos

    [SerializeField] private GameObject upperBubbleObject;    // 위 방울 오브젝트
    [SerializeField] private GameObject bottomBubbleObject;   // 아래 방울 오브젝트
    [SerializeField] private Image upperBubbleImage;
    [SerializeField] private Sprite[] upperBubbleSprite; // upperBubble에 들어있는 물 이미지들의 배열
    [SerializeField] private GameObject upperWaterfall;
    [SerializeField] private GameObject bottomWaterfall;

    [SerializeField] private Sprite quitPressed_Sprite; //quit 버튼 눌렷을 때 스프라이트

    [Header("Versus")] [Space(5)]
    [SerializeField] private TMP_Text winText; // Result의 1개의 결과 Text
    [SerializeField] private TMP_Text loseText; // Result의 1개의 결과 Text
    [SerializeField] private Image winProfileImage; // Result의 1개의 결과 Image
    [SerializeField] private Image loseProfileImage; // Result의 1개의 결과 Image

    [Header("Timer")]
    [SerializeField] private GameTimer gameTimer = null;
    private bool bNoTimePlaying = false;

    // Waterfall 회전 변수
    private bool rotateDirection = true; // true면 회전 방향이 +, false면 회전 방향이 -
    private float rotationZ = 0f; // 현재 Z 축 회전 각도

    private Coroutine readyGameCoroutine; // 게임 시작 코루틴
    private Coroutine upperBubbleCoroutine; // 물 차오르는 코루틴

    #region Unity Callback
    private void Awake()
    {
        GetBoardSprite();
    }

    private void OnEnable()
    {
        GameSetting();
    }

    private void Update()
    {
        GameEnd();
    }

    private void OnDisable()
    {
        Init();
    }
    #endregion

    #region Other Method
    #region Game Logic
    /// <summary>
    /// Multi게임 로직 반복 Method
    /// </summary>
    public void RepeatGameLogic()
    {
        switch(playerTurn)
        {
            case Turn.Turn1P:
                playerTurn = Turn.Turn2P;
                break;
            case Turn.Turn2P:
                playerTurn = Turn.Turn1P;
                break;
        }
        // 새로운 Sprite 생성 및 버블 위치 세팅
        PosSetting();

        // 버튼 interactable 설정하여 누구의 턴인지 설정
        TurnSetting();

        // upperBubble 초기화 및 코루틴 재실행
        if (upperBubbleCoroutine != null)
        {
            StopCoroutine(upperBubbleCoroutine);
        }
        upperTimer = 12f;
        upperBubbleCoroutine = StartCoroutine(UpperBubble_Co());
    }

    private void PosSetting()
    { // 턴 넘어갔을 때 각 포지션들 설정하는 Method
        AudioManager.instance.SetAudioClip_SFX(2, false);
        bNoTimePlaying = false;

        if(playerTurn.Equals(Turn.Turn1P))
        { // 1P 턴
            // 버튼 리스트 초기화 및 삭제 (추후 풀링으로 구현한다면 setactive false로 바꾸면 될듯)
            popList2P.Clear();
            Destroy(boardPos[1].transform.GetChild(0).gameObject);    // 지금 오브젝트 풀링의 List를 받아올 수 없는 구조라서 일단 Destroy로 했음, 추후 수정해야함

            // Sprite 배열로 각 플레이어들에게 랜덤한 Sprite 부여 및 Pushpop 생성
            SetSpriteImage(boardPos[1], popList2P);

            // 새로운 sprite, popButton 포지션 설정
            boardPos[1].transform.GetChild(1).transform.localPosition = bottomPos[1]; // Destroy한 객체는 다음 프레임에 삭제됨
            upperBubbleObject.transform.localPosition = upperPos[0];
            bottomBubbleObject.transform.localPosition = bottomPos[1];
        }
        else if(playerTurn.Equals(Turn.Turn2P))
        { // 2P 턴
            // 버튼 리스트 초기화 및 삭제 (추후 풀링으로 구현한다면 setactive false로 바꾸면 될듯)
            popList1P.Clear();
            Destroy(boardPos[0].transform.GetChild(0).gameObject);

            // Sprite 배열로 각 플레이어들에게 랜덤한 Sprite 부여 및 Pushpop 생성
            SetSpriteImage(boardPos[0], popList1P);

            // 새로운 sprite, popButton 포지션 설정
            boardPos[0].transform.GetChild(1).transform.localPosition = bottomPos[0];
            upperBubbleObject.transform.localPosition = upperPos[1];
            bottomBubbleObject.transform.localPosition = bottomPos[0];
        }
    }

    private void TurnSetting()
    { // 턴 아닌 플레이어의 PushPop button 클릭 비활성화
        if(playerTurn.Equals(Turn.Turn1P))
        { // 1P 턴
            for (int i = 0; i < popList2P.Count; i++)
            {
                popList2P[i].GetComponent<Button>().interactable = false;
            }
            for(int i = 0; i < popList1P.Count; i++)
            {
                popList1P[i].GetComponent<Button>().interactable = true;
                popList1P[i].GetComponent<PushPopButton>().player = 0;
            }
        }
        else if(playerTurn.Equals(Turn.Turn2P))
        { // 2P 턴
            for (int i = 0; i < popList1P.Count; i++)
            {
                popList1P[i].GetComponent<Button>().interactable = false;
            }
            for (int i = 0; i < popList2P.Count; i++)
            {
                popList2P[i].GetComponent<Button>().interactable = true;
                popList2P[i].GetComponent<PushPopButton>().player = 1;
            }
        }
    }

    private void SetSpriteImage(Transform _parent, List<GameObject> _popList) // object pooling... todo 
    { // 매개변수를 이용해 각 Player의 Sprite와 PushPop Button 세팅
        int randomIndex = UnityEngine.Random.Range(0, sprites.Length);

        Sprite sprite = sprites[randomIndex];
        PushPop.Instance.boardSprite = sprite;
        // Sprite 이름에서 "(Clone)" 부분을 제거
        string spriteName = sprite.name.Replace("(Clone)", "").Trim();

        // 이름에서 숫자 부분만 추출하여 int로 변환
        // 이 부분 Pushpop에서 생성하는 부분이랑 많이 꼬여있음...
        if (int.TryParse(spriteName, out int spriteNumber))
        {
            GameManager.Instance.PushPopStage = spriteNumber;
            PushPop.Instance.CreatePushPopBoard(_parent);
            PushPop.Instance.CreateGrid(PushPop.Instance.pushPopBoardObject[0]);
            PushPop.Instance.PushPopButtonSetting(PushPop.Instance.PopParent.transform);
            for(int i = 0; i < PushPop.Instance.PopParent.transform.childCount; i++)
            {
                GameObject pop = PushPop.Instance.PopParent.transform.GetChild(i).gameObject;
                _popList.Add(pop);
            }
            PushPop.Instance.pushPopBoardObject[0].transform.SetParent(_parent, false); // worldPositionStays를 false로 설정하여 로컬 위치 유지
            GameObject temp = PushPop.Instance.pushPopBoardObject[0];
            PushPop.Instance.pushPopBoardObject.Remove(temp);
            Destroy(temp);
            for (int i = 0; i < PushPop.Instance.activePos.Count; i++)
            {
                PushPop.Instance.activePos[i].SetActive(false);
            }
            PushPop.Instance.activePos.Clear();
            PushPop.Instance.pushPopButton.Clear();
            PushPop.Instance.pushPopBoardObject.Clear();
            PushPop.Instance.pushPopBoardUIObject.Clear();
        }
    }

    public void BottomBubbleTouch()
    { // 밑에 큰 방울을 터치할 때마다 상단 방울의 시간이 줄어듬
        AudioManager.instance.SetCommonAudioClip_SFX(5);
        upperTimer -= 0.1f;
    }

    private IEnumerator UpperBubble_Co()
    { // 시간 흐름에 따라 waterfall sprite를 결정, uppertimer가 0보다 작아지면 게임 종료
        upperBubbleImage.sprite = upperBubbleSprite[0]; // sprite를 초기화
        int lastSpriteIndex = 0; // 마지막으로 변경된 스프라이트 인덱스를 추적

        while (upperTimer > 0)
        { // upper bubble timer
            upperTimer -= Time.deltaTime;
            int spriteIndex = GetSpriteIndexByTimer(upperTimer); // waterfall sprite change
            if (spriteIndex != lastSpriteIndex)
            {
                upperBubbleImage.sprite = upperBubbleSprite[spriteIndex];
                lastSpriteIndex = spriteIndex; // 마지막으로 변경된 스프라이트 인덱스 업데이트
            }

            // Z축 회전 처리
            if (rotateDirection)
            {
                rotationZ += Time.deltaTime * 30; // 속도 조절
                if (rotationZ > 15)
                {
                    rotateDirection = !rotateDirection;
                }
            }
            else
            {
                rotationZ -= Time.deltaTime * 30; // 속도 조절
                if (rotationZ < -15)
                {
                    rotateDirection = !rotateDirection;
                }
            }

            upperBubbleImage.transform.rotation = Quaternion.Euler(0, 0, rotationZ);

            yield return null;
        }

        // 게임 종료
        upperBubbleCoroutine = null;
        gameTimer.EndTimer = true;
        AudioManager.instance.SetAudioClip_SFX(0, false);
    }

    private int GetSpriteIndexByTimer(float timer)
    { // waterfall sprite index return
        if (timer < 2) return 5;
        if (timer < 4) return 4;
        if (timer < 6) return 3;
        if (timer < 8) return 2;
        if (timer < 10) return 1;
        return 0; // Default sprite
    }

    private IEnumerator Result_Co()
    { // 결과창 출력 코루틴
        yield return new WaitForSeconds(2f); // waterfall animation 기다림 
        WaterfallAnimatorSet(playerTurn, false);

        // 결과창 출력
        AudioManager.instance.Stop_SFX();
        AudioManager.instance.SetCommonAudioClip_SFX(7);

        resultPanel.SetActive(true);
        Ranking.Instance.LoadVersusResult_Personal(winText, loseText, winProfileImage, loseProfileImage);
        
        yield return new WaitForSeconds(0.1f);
        
        // 오브젝트들 삭제
        ResetGame(); // delete
        yield return null;
    }

    private void WaterfallAnimatorSet(Turn _turn, bool _play)
    { // 패배 Animation
        if(_play)
        {
            upperWaterfall.SetActive(true);
            bottomWaterfall.SetActive(true);
            SetAnimatorSpeed(upperWaterfall, 1f);
            SetAnimatorSpeed(bottomWaterfall, 1f);
            if (_turn.Equals(Turn.Turn1P))
            {
                upperWaterfall.transform.localPosition = upperPos[0];
                bottomWaterfall.transform.localPosition = bottomPos[0];
            }
            else if (_turn.Equals(Turn.Turn2P))
            {
                upperWaterfall.transform.localPosition = upperPos[1];
                bottomWaterfall.transform.localPosition = bottomPos[1];
            }
        }
        else if (!_play)
        {
            SetAnimatorSpeed(upperWaterfall, 0f);
            SetAnimatorSpeed(bottomWaterfall, 0f);
        }
    }

    private void SetAnimatorSpeed(GameObject bubble, float speed)
    { 
        for (int i = 0; i < bubble.transform.childCount; i++)
        {
            Animator animator = bubble.transform.GetChild(i).GetComponent<Animator>();
            if (animator != null)
            {
                animator.speed = speed;
            }
        }
    }

    

    private void ResetGame()
    { // 오브젝트들 삭제하는 메소드
        // 리스트 초기화 및 Sprite 삭제
        popList1P.Clear();
        popList2P.Clear();
        if(boardPos[0].transform.childCount > 0)
        {
            Destroy(boardPos[0].transform.GetChild(0).gameObject);
        }
        if(boardPos[1].transform.childCount > 0)
        {
            Destroy(boardPos[1].transform.GetChild(0).gameObject);
        }

        upperWaterfall.SetActive(false);
        bottomWaterfall.SetActive(false);
    }

    public void PrintVersus()
    {
        Ranking.Instance.LoadVersusResult(winTexts, loseTexts, winProfileImages, loseProfileImages);
    }
    #endregion

    private void PrintErrorLog(TMP_Text logObj, string log)
    {
        if (DialogManager.instance.log_co != null)
        {
            StopCoroutine(DialogManager.instance.log_co);
        }
        DialogManager.instance.log_co = StartCoroutine(DialogManager.instance.Print_Dialog_Co(logObj, log));
    }

    #region Button Set
    //나가기 전 경고패널 나가기 버튼
    public void GoOutBtn_Clicked()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        AudioManager.instance.Stop_SFX();

        bNoTimePlaying = false;

        QuitGame();

        result.SetActive(false);
        GamePanel.SetActive(false);
        MainPanel.SetActive(true);
        WarningPanel.SetActive(false);
        Time.timeScale = 1;
    }

    //나가기 전 경고패널 취소 버튼
    public void CancelBtn_Clicked()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        if (bNoTimePlaying)
        {
            AudioManager.instance.Pause_SFX(false);
        }
        bNoTimePlaying = false;
        Time.timeScale = 1;

        WarningPanel.SetActive(false);
        ButtonSetting();
    }
    #endregion

    public void GetBoardSprite()
    { // 초기 Sprite 배열 세팅
        Sprite[] tempSprites = new Sprite[boardSprite.spriteCount];
        sprites = tempSprites;
        boardSprite.GetSprites(sprites);
    }

    public void Init()
    { // OnDisable(), check list: coroutine, list, array, variables 초기화 관련
        // timer setting
        gameTimer.TimerText.color = new Color(0, 0, 0, 1);
        gameTimer.TenCount = false;
        gameTimer.EndTimer = false;
        upperTimer = 12f;
        gameTimer.CurrentTime = 60f;
        gameTimer.TimerText.text = $"{(int)gameTimer.CurrentTime}";

        // pushpop setting
        popList1P.Clear();
        popList2P.Clear();

        // quit button setting
        quitButtonClick[(int)Player.Player1] = false;
        quitButtonClick[(int)Player.Player2] = false;
        quitButton[(int)Player.Player1].interactable = true;
        quitButton[(int)Player.Player2].interactable = true;

        // board object setting
        if (boardPos[0].transform.childCount > 0)
        {
            Destroy(boardPos[0].transform.GetChild(0).gameObject);
        }
        if (boardPos[1].transform.childCount > 0)
        {
            Destroy(boardPos[1].transform.GetChild(0).gameObject);
        }

        // lose animation에서 true된 object 비활성화
        upperWaterfall.SetActive(false);
        bottomWaterfall.SetActive(false);
    }

    public void GameSetting()
    { // OnEnable() bubble size, board size, pushpopbutton size, pushpop percentage, etc. setting 관련
        AudioManager.instance.SetAudioClip_BGM(1);

        // 버튼 사이즈 설정
        PushPop.Instance.buttonSize = new Vector2(56.8f, 56.8f);
        PushPop.Instance.percentage = 0.476f;
        GameManager.Instance.BoardSize = new Vector2(500f, 500f);

        // Player Turn, Bubble 위치 부여
        playerTurn = (Turn)UnityEngine.Random.Range(0, 2); // 0일 때 1P 먼저 시작
        bottomBubbleObject.transform.localPosition = bottomPos[(int)Turn.Turn2P];
        upperBubbleObject.transform.localPosition = upperPos[(int)playerTurn];

        // board pos setting
        SetSpriteImage(boardPos[0], popList1P);
        SetSpriteImage(boardPos[1], popList2P);
        boardPos[0].transform.GetChild(0).transform.localPosition = bottomPos[0];
        boardPos[1].transform.GetChild(0).transform.localPosition = bottomPos[1];
        
        TurnSetting();
    }

    public void GameStart()
    { // MultiCanvas에서 호출할 Game Start
        readyGameCoroutine = StartCoroutine(GameStart_Co());
    }

    public IEnumerator GameStart_Co()
    { // gameready coroutine -> gamestart 게임 시작 관련 코루틴
        // Game Ready
        yield return new WaitForSeconds(0.5f);
        AudioManager.instance.SetCommonAudioClip_SFX(1);
        gameReadyPanelText.text = "준비~";
        gameReadyPanel.SetActive(true);

        yield return new WaitForSeconds(2f);
        AudioManager.instance.SetCommonAudioClip_SFX(2);
        gameReadyPanelText.text = "시작~";

        yield return new WaitForSeconds(0.8f);
        gameReadyPanel.SetActive(false);

        readyGameCoroutine = null;
        GameReadyStart();
    }

    private void GameReadyStart()
    {
        // timer start
        upperBubbleCoroutine = StartCoroutine(UpperBubble_Co()); // upper bubble timer
        gameTimer.TimerStart(); // remaining timer

        // 프로필 세팅, 이미지 caching으로 바꿔줄 것... todo
        SQL_Manager.instance.PrintProfileImage(profileImage[(int)Player.Player1], ProfileManager.Instance.PlayerInfo[(int)Player.Player1].imageMode, ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex);
        SQL_Manager.instance.PrintProfileImage(profileImage[(int)Player.Player2], ProfileManager.Instance.PlayerInfo[(int)Player.Player2].imageMode, ProfileManager.Instance.PlayerInfo[(int)Player.Player2].playerIndex);
        profileName[(int)Player.Player1].text = ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileName;
        profileName[(int)Player.Player1].text = ProfileManager.Instance.PlayerInfo[(int)Player.Player2].profileName;
    }

    public void GameEnd()
    {
        if (!gameTimer.EndTimer) return;
        // 자신의 턴일 때 게임 종료 시 패배, 결과 저장
        Ranking.Instance.SetBombVersus(
            ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex,
            ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileName,
            ProfileManager.Instance.PlayerInfo[(int)Player.Player2].playerIndex,
            ProfileManager.Instance.PlayerInfo[(int)Player.Player2].profileName, !playerTurn.Equals(Turn.Turn1P)
            );

        AudioManager.instance.SetAudioClip_SFX(1, false);
        WaterfallAnimatorSet(playerTurn, true);
        StartCoroutine(Result_Co());
    }

    #endregion
    #region Button Method
    public void QuitButton(int _player)
    { // 나가기 1P, 2P 버튼
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        quitButton[_player].interactable = !quitButton[_player].interactable;
        if (!quitButton[(int)Player.Player1].interactable && !quitButton[(int)Player.Player2])
        {
            Time.timeScale = 0f;
            if (gameTimer.TenCount)
            {
                AudioManager.instance.Pause_SFX(true);
            }
            warningPanel.SetActive(true);
        }
    }

    public void WarningPanelGoOutButton()
    { // 나가기 1P, 2P 둘다 눌렀을 때 - 나가기
        AudioManager.instance.SetAudioClip_SFX(1, false);
        AudioManager.instance.SetAudioClip_BGM(1);

        ResetGame();

        warningPanel.SetActive(false);
        gameObject.SetActive(false);
    }

    public void WarningPanelCancelButton()
    { // 나가기 1P, 2P 둘다 눌렀을 때 - 취소
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        Time.timeScale = 1f;
        quitButton[(int)Player.Player1].interactable = true;
        quitButton[(int)Player.Player2].interactable = true;

        warningPanel.SetActive(false);
    }
    #endregion
}
