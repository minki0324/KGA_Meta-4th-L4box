using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PushPushManager : MonoBehaviour, IGame
{ // pushpush game
    public CustomPushpopManager custom;
    public PuzzleLozic puzzle;
    [SerializeField] FramePuzzle frame;
    public GameObject decoPanel;
    [SerializeField] private TMP_Text stageTitle;
    public int pieceCount = 1; //버블터졋을때 중복호출을 방지하기위한 인덱스
    public int pushCount; //버튼눌렀을때 + 되는 카운트 pushCount == 버튼리스트.Count 비교해서 동일시 클리어 판정(GameManager)
    public event Action onPushPushGameEnd;

    private void OnEnable()
    {
        GameSetting();
    }

    private void OnDisable()
    {
        Init();
    }

    #region 푸시푸시게임 전체 콜백메소드 모음

    public void OnAllBubblesPopped() //버블이 모두 터지고 땅에닿았을때 불리는 메소드
    {
        puzzle.SettingGame();//퍼즐게임 시작
        TitleSet("그림에 맞게 퍼즐을 맞춰 보세요!");
        pieceCount = 1;
    }
    public void OnPuzzleSolved() //퍼즐을 모두 맞췄을때 불리는 메소드
    {
        AudioManager.instance.SetAudioClip_SFX(1, false);
        TitleSet("내 마음대로 그림을 꾸며보자!");
        DebugLog.instance.Adding_Message("재윤아 다했다고? 대박 최곤데?");
        puzzle.successCount = 0;
        DecoPanelSetActive(true);
        puzzle.onPuzzleClear?.Invoke();
        custom.gameObject.SetActive(true);
    }
    public void OnCustomEndmethod() //커스텀하다가 다음으로 버튼 눌렀을때 불리는 메소드
    {//다음으로 버튼에 onClick 참조되있음.
        GameManager.Instance.GameClear();
        TitleSet("모든 푸쉬팝을 눌러보자!");
        DecoPanelSetActive(false);
        custom.onCustomEnd?.Invoke();
        frame._myImage.alphaHitTestMinimumThreshold = 0f;
    }
    public void OnButtonAllPush() //커스텀 완료하고 모두 눌렀을때
    {//버튼 모두 눌렀을때 자세한로직 GameManager GameClear에 있음.
        pushCount = 0; //
    }
    public void OnPushPushEnd() //푸시푸시가 모두 끝나고 초기화 할것들 모아둔 메소드
    {
        GameManager.Instance.puzzleClass.Clear();
        onPushPushGameEnd?.Invoke();
        AudioManager.instance.SetCommonAudioClip_SFX(3);

    }
    #endregion

    public void DecoPanelSetActive(bool _bool)
    {
        decoPanel.SetActive(_bool);
    }

    public void Init()
    { // OnDisable(), check list: coroutine, list, array, variables 초기화 관련

    }
    public void TitleSet(string _string)
    {
        stageTitle.text = _string;
    }

    public void GameSetting()
    { // OnEnable() bubble size, board size, pushpopbutton size, pushpop percentage, etc. setting 관련
    
    }

    public void GameStart()
    {
        throw new NotImplementedException();
    }

    public IEnumerator GameStart_Co()
    {
        // ready
        throw new NotImplementedException();
        GameStart();
    }
}
