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
    [Header("Canvas")]
    [SerializeField] private MultiCanvas multiCanvas = null;

    [Header("Panel")]
    [SerializeField] private GameObject warningPanel = null;
    [SerializeField] private GameObject resultPanel = null;

    [Header("Player Info")]
    [SerializeField] private Image[] profileImage = null;
    [SerializeField] private TMP_Text[] profileName = null;
    private bool[] quitButtonClick = { false, false }; // 1P, 2P 뒤로가기 버튼 클릭 시 true

    [Header("Game Info")]
    [SerializeField] private Turn playerTurn = Turn.Turn1P; // 현재 턴 기준
    public List<GameObject> popButtonList1P = new List<GameObject>(); // 1P 의 Pushpop button list -> object pooling 필요
    public List<GameObject> popButtonList2P = new List<GameObject>(); // 2P 의 Pushpop List
    [SerializeField] private List<Sprite> easyList = new List<Sprite>();
    [SerializeField] private List<Sprite> normalList = new List<Sprite>();
    [SerializeField] private List<Sprite> hardList = new List<Sprite>();
    [SerializeField] private List<int> spriteList1P = new List<int>() { 0, 1, 2 };
    [SerializeField] private List<int> spriteList2P = new List<int>() { 0, 1, 2 };
    private bool isFever = false;

    [Header("Bubble Info")]
    [SerializeField] private Transform[] boardTransform; // Pushpop Board가 소환될 Parent
    [SerializeField] private GameObject upperBubbleObject;    // 위 방울 오브젝트
    [SerializeField] private GameObject bottomBubbleObject;   // 아래 방울 오브젝트
    [SerializeField] private Image upperBubbleImage;
    [SerializeField] private Sprite[] upperBubbleSprite; // upperBubble에 들어있는 물 이미지들의 배열
    [SerializeField] private GameObject upperWaterfall;
    [SerializeField] private GameObject bottomWaterfall;
    private Vector2[] upperPos = { new Vector2(-500, 380), new Vector2(500, 380) }; // player1, player2의 위 bubble pos
    private Vector2[] bottomPos = { new Vector2(-500, -100), new Vector2(500, -100) }; // player1, player2의 아래 bubble pos

    [Header("Game Result")]
    [SerializeField] private TMP_Text winText; // Result의 1개의 결과 Text
    [SerializeField] private TMP_Text loseText; // Result의 1개의 결과 Text
    [SerializeField] private Image winProfileImage; // Result의 1개의 결과 Image
    [SerializeField] private Image loseProfileImage; // Result의 1개의 결과 Image
    private bool isEndGame = false;

    [Header("Timer")]
    [SerializeField] private GameTimer gameTimer = null;
    private float upperTimer = 12f;
    private bool bNoTimePlaying = false;

    [Header("Quit")]
    [SerializeField] private Button[] quitButton;  // 나가기 버튼
    [SerializeField] private Sprite[] quitButtonSprite; //quit 스프라이트

    // Waterfall 회전 변수
    private bool rotateDirection = true; // true면 회전 방향이 +, false면 회전 방향이 -
    private float rotationZ = 0f; // 현재 Z 축 회전 각도
    private Coroutine readyGameCoroutine = null; // 게임 시작 코루틴
    private Coroutine upperBubbleCoroutine = null; // 물 차오르는 코루틴
    private Coroutine resultCoroutine = null;
    public Coroutine FeverCoroutine = null;

    #region Unity Callback

    private void OnEnable()
    {
        GameSetting();
    }

    private void Update()
    {
        if (isEndGame) return;
        if(gameTimer.TenCount && !isFever)
        {
            FeverCoroutine = StartCoroutine(FeverMode());
        }
        GameEnd();
    }

    private void OnDisable()
    {
        Init();
    }
    #endregion
    #region BoardSprite Setting
    public void SpriteListSet()
    { // 만약 모든 spriteList를 다 사용한 플레이어가 있다면 초기화
        if(spriteList1P.Count.Equals(0))
        {
            for(int i = 0; i < 3; i++)
            {
                spriteList1P.Add(i);
            }
        }
        if(spriteList2P.Count.Equals(0))
        {
            for (int i = 0; i < 3; i++)
            {
                spriteList2P.Add(i);
            }
        }
    }
    private Sprite GetSpriteName(int _player)
    {
        Sprite sprite;
        if(!gameTimer.TenCount)
        {
            int randomList = -1;
            // 스프라이트 리스트를 저장하는 변수
            List<Sprite> playerSpriteList;
            List<int> spriteList = _player.Equals(1) ? spriteList1P : spriteList2P;

            while (true)
            {
                randomList = UnityEngine.Random.Range(0, 3);
                if (spriteList.Contains(randomList))
                {
                    break;
                }
            }
            playerSpriteList = randomList == 0 ? easyList : randomList == 1 ? normalList : hardList;
            spriteList.Remove(randomList);

            // 선택된 플레이어 스프라이트 리스트에서 랜덤한 스프라이트를 선택
            int randomIndex = UnityEngine.Random.Range(0, playerSpriteList.Count);
            sprite = playerSpriteList[randomIndex];
        }
        else
        {
            int randomIndex = UnityEngine.Random.Range(0, easyList.Count);
            sprite = easyList[randomIndex];
        }
        return sprite;
    }
    private void SetSpriteImage(Transform _parent, List<GameObject> _popList, int _player) // object pooling... todo 
    { // 매개변수를 이용해 각 Player의 Sprite와 PushPop Button 세팅
        SpriteListSet();

        Sprite sprite = GetSpriteName(_player);
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
            for (int i = 0; i < PushPop.Instance.PopParent.transform.childCount; i++)
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
    #endregion
    #region Game Interface
    public void Init()
    { // OnDisable(), check list: coroutine, list, array, variables 초기화 관련
        resultPanel.SetActive(false);

        // timer setting
        gameTimer.TimerText.color = new Color(0, 0, 0, 1);
        gameTimer.TenCount = false;
        gameTimer.EndTimer = false;
        upperTimer = 12f;
        gameTimer.CurrentTime = 60f;
        gameTimer.TimerText.text = $"{(int)gameTimer.CurrentTime}";
        bNoTimePlaying = false;
        isEndGame = false;
        isFever = false;

        // pushpop setting
        popButtonList1P.Clear();
        popButtonList2P.Clear();

        // spriteList setting
        spriteList1P.Clear();
        spriteList2P.Clear();
        SpriteListSet();

        // quit button setting
        quitButtonClick[(int)Player.Player1] = false;
        quitButtonClick[(int)Player.Player2] = false;
        quitButton[(int)Player.Player1].GetComponent<Image>().sprite = quitButtonSprite[0];
        quitButton[(int)Player.Player2].GetComponent<Image>().sprite = quitButtonSprite[0];

        // board object setting
        if (boardTransform[0].transform.childCount > 0)
        {
            Destroy(boardTransform[0].transform.GetChild(0).gameObject);
        }
        if (boardTransform[1].transform.childCount > 0)
        {
            Destroy(boardTransform[1].transform.GetChild(0).gameObject);
        }

        // lose animation에서 true된 object 비활성화
        upperWaterfall.SetActive(false);
        bottomWaterfall.SetActive(false);

        // coroutine 초기화
        if (gameTimer.TimerCoroutine != null)
        {
            gameTimer.StopCoroutine(gameTimer.TimerCoroutine);
        }
        if (upperBubbleCoroutine != null)
        {
            StopCoroutine(upperBubbleCoroutine);
        }
        if (resultCoroutine != null)
        {
            StopCoroutine(resultCoroutine);
        }
        if (FeverCoroutine != null)
        {
            StopCoroutine(FeverCoroutine);
        }
    }

    public void GameSetting()
    { // OnEnable() bubble size, board size, pushpopbutton size, pushpop percentage, etc. setting 관련
        // AudioManager.instance.SetAudioClip_BGM(1);
        // 버튼 사이즈 설정
        PushPop.Instance.buttonSize = new Vector2(56.7f, 56.7f);
        PushPop.Instance.percentage = 0.47f;
        GameManager.Instance.BoardSize = new Vector2(500f, 500f);

        // 프로필 세팅, 이미지 caching으로 바꿔줄 것, 처음 시작 시 1p imageMode = true, defaultIndex = 1일 때 boy로 뜸 왤까요... todo
        SQL_Manager.instance.PrintProfileImage(profileImage[(int)Player.Player1], ProfileManager.Instance.PlayerInfo[(int)Player.Player1].imageMode, ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex);
        SQL_Manager.instance.PrintProfileImage(profileImage[(int)Player.Player2], ProfileManager.Instance.PlayerInfo[(int)Player.Player2].imageMode, ProfileManager.Instance.PlayerInfo[(int)Player.Player2].playerIndex);
        profileName[(int)Player.Player1].text = ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileName;
        profileName[(int)Player.Player2].text = ProfileManager.Instance.PlayerInfo[(int)Player.Player2].profileName;
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
        multiCanvas.GameReadyPanelText.text = "준비~";
        multiCanvas.GameReadyPanel.SetActive(true);

        yield return new WaitForSeconds(2f);
        AudioManager.instance.SetCommonAudioClip_SFX(2);
        multiCanvas.GameReadyPanelText.text = "시작~";

        yield return new WaitForSeconds(0.8f);
        multiCanvas.GameReadyPanel.SetActive(false);

        readyGameCoroutine = null;
        GameReadyStart();
    }

    private void GameReadyStart()
    {
        // timer start
        upperBubbleCoroutine = StartCoroutine(UpperBubble_Co()); // upper bubble timer
        gameTimer.TimerStart(); // remaining timer

        // board pos setting
        SetSpriteImage(boardTransform[0], popButtonList1P, 1);
        SetSpriteImage(boardTransform[1], popButtonList2P, 2);
        boardTransform[0].transform.GetChild(0).transform.localPosition = bottomPos[0];
        boardTransform[1].transform.GetChild(0).transform.localPosition = bottomPos[1];

        // Player Turn, Bubble 위치 부여
        playerTurn = (Turn)UnityEngine.Random.Range(0, 2); // 0일 때 1P 먼저 시작
        int randomIndex = playerTurn.Equals(Turn.Turn1P) ? (int)Turn.Turn2P : (int)Turn.Turn1P;
        upperBubbleObject.transform.localPosition = upperPos[(int)playerTurn]; // 턴인 사람에게 줌
        bottomBubbleObject.transform.localPosition = bottomPos[randomIndex]; // 턴 아닌 사람에게 줌

        TurnSetting();
    }

    public void GameEnd()
    {
        if (!gameTimer.EndTimer) return; // timer 종료 시 gameTimer.EndTimer true
        if (popButtonList1P.Count.Equals(0) || popButtonList2P.Count.Equals(0))
        {
            RepeatGameLogic();
            return;
        }

        AudioManager.instance.SetAudioClip_SFX(1, false);
        WaterfallAnimatorSet(playerTurn, true);

        if (upperBubbleCoroutine != null)
        {
            StopCoroutine(upperBubbleCoroutine);
        }
        if (gameTimer.TimerCoroutine != null)
        {
            gameTimer.StopCoroutine(gameTimer.TimerCoroutine);
        }
        if (readyGameCoroutine != null)
        {
            StopCoroutine(readyGameCoroutine);
        }
        if (FeverCoroutine != null)
        {
            StopCoroutine(FeverCoroutine);
        }

        resultCoroutine = StartCoroutine(Result_Co());
        isEndGame = true;
    }

    private IEnumerator Result_Co()
    { // 결과창 출력 코루틴
        yield return new WaitForSeconds(2f); // waterfall animation 기다림 
        WaterfallAnimatorSet(playerTurn, false);
        
        AudioManager.instance.Stop_SFX();
        AudioManager.instance.SetCommonAudioClip_SFX(7);

        // 자신의 턴일 때 게임 종료 시 패배, 결과 저장
        Ranking.Instance.SetBombVersus(
            ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex,
            ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileName,
            ProfileManager.Instance.PlayerInfo[(int)Player.Player2].playerIndex,
            ProfileManager.Instance.PlayerInfo[(int)Player.Player2].profileName, !playerTurn.Equals(Turn.Turn1P)
            );
        Ranking.Instance.LoadVersusResult_Personal(winText, loseText, winProfileImage, loseProfileImage);

        // 결과창 출력
        resultCoroutine = null;
        Time.timeScale = 0f;
        resultPanel.SetActive(true);
    }
    #endregion
    #region Multi Game Setting
    public void RepeatGameLogic()
    { // 남은 시간이 있고, 플레이어가 PushPop button을 다 눌렀을 때
        playerTurn = playerTurn.Equals(Turn.Turn1P) ? Turn.Turn2P : Turn.Turn1P; // 턴 넘겨줌

        PosSetting();
        TurnSetting();

        // upperBubble 초기화 및 코루틴 재실행
        if (upperBubbleCoroutine != null)
        {
            StopCoroutine(upperBubbleCoroutine);
        }
        // TenCount가 true면 10초 미만이 남은 상태로 피버모드 돌입
        upperTimer = gameTimer.TenCount ? 8f : 12f;
        upperBubbleCoroutine = StartCoroutine(UpperBubble_Co());
    }

    private void PosSetting()
    { // 턴 넘어갔을 때 각 포지션들 설정하는 Method
        AudioManager.instance.SetAudioClip_SFX(2, false);
        bNoTimePlaying = false;

        if (playerTurn.Equals(Turn.Turn1P))
        { // 1P 턴
            // 버튼 리스트 초기화 및 삭제 (추후 풀링으로 구현한다면 setactive false로 바꾸면 될듯)
            popButtonList2P.Clear();
            Destroy(boardTransform[1].transform.GetChild(0).gameObject);    // 지금 오브젝트 풀링의 List를 받아올 수 없는 구조라서 일단 Destroy로 했음, 추후 수정해야함

            // Sprite 배열로 각 플레이어들에게 랜덤한 Sprite 부여 및 Pushpop 생성
            SetSpriteImage(boardTransform[1], popButtonList2P, 2);

            // 새로운 sprite, popButton 포지션 설정
            boardTransform[1].transform.GetChild(1).transform.localPosition = bottomPos[1]; // Destroy한 객체는 다음 프레임에 삭제됨
            upperBubbleObject.transform.localPosition = upperPos[0];
            bottomBubbleObject.transform.localPosition = bottomPos[1];
        }
        else
        { // 2P 턴
            // 버튼 리스트 초기화 및 삭제 (추후 풀링으로 구현한다면 setactive false로 바꾸면 될듯)
            popButtonList1P.Clear();
            Destroy(boardTransform[0].transform.GetChild(0).gameObject);

            // Sprite 배열로 각 플레이어들에게 랜덤한 Sprite 부여 및 Pushpop 생성
            SetSpriteImage(boardTransform[0], popButtonList1P, 1);

            // 새로운 sprite, popButton 포지션 설정
            boardTransform[0].transform.GetChild(1).transform.localPosition = bottomPos[0];
            upperBubbleObject.transform.localPosition = upperPos[1];
            bottomBubbleObject.transform.localPosition = bottomPos[0];
        }
    }

    private void TurnSetting()
    { // 턴 아닌 플레이어의 PushPop button 클릭 비활성화
        if (playerTurn.Equals(Turn.Turn1P))
        { // 1P 턴
            for (int i = 0; i < popButtonList2P.Count; i++)
            {
                popButtonList2P[i].GetComponent<Button>().interactable = false;
            }
            for (int i = 0; i < popButtonList1P.Count; i++)
            {
                popButtonList1P[i].GetComponent<Button>().interactable = true;
                popButtonList1P[i].GetComponent<PushPopButton>().player = 0;
            }
        }
        else if (playerTurn.Equals(Turn.Turn2P))
        { // 2P 턴
            for (int i = 0; i < popButtonList1P.Count; i++)
            {
                popButtonList1P[i].GetComponent<Button>().interactable = false;
            }
            for (int i = 0; i < popButtonList2P.Count; i++)
            {
                popButtonList2P[i].GetComponent<Button>().interactable = true;
                popButtonList2P[i].GetComponent<PushPopButton>().player = 1;
            }
        }
    }

    public IEnumerator FeverMode()
    {
        isFever = true;
        Vector2 shakePos;
        while(gameTimer.TenCount)
        {
            float shakePosX = UnityEngine.Random.Range(-5, 6);
            float shakePosY = UnityEngine.Random.Range(-5, 6);
            shakePos = new Vector2(shakePosX, shakePosY);
            upperBubbleObject.transform.localPosition = upperPos[(int)playerTurn] + shakePos;
            yield return null;
        }
        FeverCoroutine = null;
    }
    #endregion
    #region UpperBubble
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

            float rotSpeed = gameTimer.TenCount ? 360f : 30f;
            float rotAngle = gameTimer.TenCount ? 360f : 15f;
            // Z축 회전 처리
            if (rotateDirection)
            {
                rotationZ += Time.deltaTime * rotSpeed; // 속도 조절
                if (rotationZ > rotAngle)
                {
                    rotateDirection = !rotateDirection;
                }
            }
            else
            {
                rotationZ -= Time.deltaTime * rotSpeed; // 속도 조절
                if (rotationZ < -rotAngle)
                {
                    rotateDirection = !rotateDirection;
                }
            }

            upperBubbleImage.transform.rotation = Quaternion.Euler(0, 0, rotationZ);

            yield return null;
        }

        upperBubbleCoroutine = null;
        gameTimer.EndTimer = true; // 게임 종료

        AudioManager.instance.SetAudioClip_SFX(0, false);
    }
    private int GetSpriteIndexByTimer(float timer)
    { // upperBubble sprite index return
        float fever = gameTimer.TenCount ? 0.7f : 1f;
        if (timer < 2 * fever) return 5;
        if (timer < 4 * fever) return 4;
        if (timer < 6 * fever) return 3;
        if (timer < 8 * fever) return 2;
        if (timer < 10 * fever) return 1;
        return 0; // Default sprite
    }
    #endregion
    #region BottomBubble
    public void BottomBubbleTouch()
    { // Bottom Bubble, 밑에 큰 방울을 터치할 때마다 상단 방울의 시간이 줄어듦
        AudioManager.instance.SetCommonAudioClip_SFX(5);
        upperTimer -= 0.1f;
    }
    #endregion
    #region Waterfall Animation
    private void WaterfallAnimatorSet(Turn _turn, bool _play)
    { // 패배한 플레이어 위치에 나오는 Animation
        if (_play)
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

    private void SetAnimatorSpeed(GameObject _bubble, float _speed)
    { // waterfall animation speed setting
        for (int i = 0; i < _bubble.transform.childCount; i++)
        {
            Animator animator = _bubble.transform.GetChild(i).GetComponent<Animator>();
            if (animator != null)
            {
                animator.speed = _speed;
            }
        }
    }
    #endregion
    #region Result Panel
    public void ResultExitButton()
    { // Result Panel - 나가기
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        Time.timeScale = 1f;

        multiCanvas.Ready.SetActive(true);
        multiCanvas.BackButton.SetActive(true);
        multiCanvas.HelpButton.SetActive(true);
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

    public void ResultReStartButton()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        Time.timeScale = 1f;

        Init();
        GameSetting();
        GameStart();
    }
    #endregion
    #region Quit And Warning Panel
    public void QuitButton(int _player)
    { // 나가기 1P, 2P 버튼
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        quitButtonClick[_player] = !quitButtonClick[_player];
        int spriteIndex = quitButtonClick[_player] ? 1 : 0; // 눌렸을 때 1, 눌리지 않았을 때 0
        quitButton[_player].GetComponent<Image>().sprite = quitButtonSprite[spriteIndex];
        if (quitButtonClick[(int)Player.Player1] && quitButtonClick[(int)Player.Player2])
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
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        AudioManager.instance.Stop_SFX();

        Time.timeScale = 1f;

        warningPanel.SetActive(false);
        multiCanvas.Ready.SetActive(true);
        multiCanvas.BackButton.SetActive(true);
        multiCanvas.HelpButton.SetActive(true);
        gameObject.SetActive(false);
    }

    public void WarningPanelCancelButton()
    { // 나가기 1P, 2P 둘다 눌렀을 때 - 취소
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        Time.timeScale = 1f;

        quitButtonClick[(int)Player.Player1] = false;
        quitButtonClick[(int)Player.Player2] = false;
        quitButton[(int)Player.Player1].GetComponent<Image>().sprite = quitButtonSprite[0];
        quitButton[(int)Player.Player2].GetComponent<Image>().sprite = quitButtonSprite[0];

        warningPanel.SetActive(false);
    }
    #endregion
}
