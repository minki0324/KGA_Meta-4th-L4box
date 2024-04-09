using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PushPushManager : MonoBehaviour, IGame
{ // pushpush game
    [Header("Canvas")]
    [SerializeField] private PushPushCanvas pushpushCanvas;

    [Header("Panel")]
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private GameObject warningPanel;

    [Header("Game Info")]
    public CustomPushpopManager customManager;
    public PuzzleLozic puzzleManager;

    [Header("PushPop Object")]
    [SerializeField] private FramePuzzle frame;

    private void OnEnable()
    {
        GameSetting();
    }

    private void OnDisable()
    {
        Init();
    }

    #region PushPush Next Method
    public void OnAllBubblesPopped()
    { // 버블이 모두 터지고 땅에 닿았을때 호출
        customManager.StageTitle.text = "그림에 맞게 퍼즐을 맞춰 보세요!";

        puzzleManager.SettingGame(); // puzzle mode start

        GameManager.Instance.NextMode -= OnAllBubblesPopped;
        GameManager.Instance.NextMode += OnPuzzleSolved;
    }

    public void OnPuzzleSolved()
    { // 퍼즐을 모두 맞췄을 때 호출
        AudioManager.Instance.SetAudioClip_SFX(1, false);
        customManager.StageTitle.text = "내 마음대로 그림을 꾸며보자!";

        customManager.CustomMode.SetActive(true);
        puzzleManager.DestroyChildren();
        puzzleManager.CraetBoard();

        GameManager.Instance.NextMode -= OnPuzzleSolved;
    }

    public void OnCustomEndButton()
    { // Custom mode - 다음으로
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        customManager.StageTitle.text = "모든 푸쉬팝을 눌러보자!";

        // End Game
        if (PushPop.Instance.StackPops.Count.Equals(0))
        {
            GameEnd();
            return;
        }

        customManager.EndCustom();

        frame.FrameImage.alphaHitTestMinimumThreshold = 0f;
    }
    #endregion
    #region Game Interface
    public void Init()
    { // OnDisable(), check list: coroutine, list, array, variables 초기화 관련
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        GameManager.Instance.GameEnd -= GameEnd;
        GameManager.Instance.IsGameClear = true;
        GameManager.Instance.NextMode = null;
        GameManager.Instance.LiveBubbleCount = 0;
        GameManager.Instance.bubbleObject.Clear();
        GameManager.Instance.IsCustomMode = false;

        PushPop.Instance.Init();
        customManager.CustomModeInit();
        puzzleManager.PuzzleModeInit();

        StopAllCoroutines();
    }

    public void GameSetting()
    { // OnEnable() bubble size, board size, pushpopbutton size, pushpop percentage, etc. setting 관련
        GameManager.Instance.GameEnd += GameEnd;
        GameManager.Instance.NextMode += OnAllBubblesPopped;
        PushPop.Instance.BoardSize = new Vector2(520f, 400f);
        PushPop.Instance.ButtonSize = new Vector2(80f, 80f);

        puzzleManager.SettingPuzzleInfo();
        customManager.StageTitle.text = "비눗방울을 터트려보세요!";
    }

    public void GameStart()
    {
        StartCoroutine(GameStart_Co());
    }

    public IEnumerator GameStart_Co()
    {
        yield return new WaitForSeconds(0.5f);

        AudioManager.Instance.SetCommonAudioClip_SFX(1);
        pushpushCanvas.GameReadyPanel.SetActive(true);
        pushpushCanvas.GameReadyPanelText.text = "준비~";

        yield return new WaitForSeconds(2f);

        AudioManager.Instance.SetCommonAudioClip_SFX(2);
        pushpushCanvas.GameReadyPanelText.text = "시작!";

        yield return new WaitForSeconds(0.8f);

        pushpushCanvas.GameReadyPanel.SetActive(false);
        
        GameReadyStart();
    }

    public void GameReadyStart()
    {
        puzzleManager.SettingPuzzlePos();
    }

    public void GameEnd()
    { // popbutton click 시 호출
        if (PushPop.Instance.pushPopButton.Count.Equals(PushPop.Instance.PushCount))
        {
            AudioManager.Instance.SetCommonAudioClip_SFX(6);
            PushPop.Instance.PushCount = 0;
            PushPop.Instance.pushPopButton.Clear();

            int[] spriteIndexs = new int[customManager.PuzzleBoard.transform.childCount];
            Vector2[] childPos = new Vector2[customManager.PuzzleBoard.transform.childCount];
            for (int i = 0; i < customManager.PuzzleBoard.transform.childCount; i++)
            {
                PushPopButton pop = customManager.PuzzleBoard.transform.GetChild(i).GetComponent<PushPopButton>();
                spriteIndexs[i] = pop.SpriteIndex;
                childPos[i] = pop.transform.localPosition;
            }

            // Save
            PushPushObject newPush = new PushPushObject(puzzleManager.CurrentPuzzle.PuzzleID, PushPop.Instance.StackPops.Count, spriteIndexs, childPos);
            string json = JsonUtility.ToJson(newPush);
            SQL_Manager.instance.SQL_AddPushpush(json, ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex);

            // Result Setting
            List<PushPushObject> pushlist = SQL_Manager.instance.SQL_SetPushPush(ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex);
            customManager.ResultText.text = DataManager.Instance.IconDict[puzzleManager.CurrentPuzzle.PuzzleID];
            customManager.ResultImage.sprite = DataManager.Instance.pushPopAtlas.GetSprite(pushlist[0].spriteName.ToString());
            customManager.ResultImage.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

            for (int i = 0; i < pushlist[0].childIndex; i++)
            { // button setting
                GameObject pop = Instantiate(PushPop.Instance.PushPopButtonPrefab, customManager.ResultImage.transform);
                pop.GetComponent<Image>().sprite = customManager.PushPopButtonSprite[pushlist[0].childSpriteIndex[i]];
                pop.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                pop.transform.localPosition = pushlist[0].childPosition[i];
            }

            // end setting
            customManager.ResultImage.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            for (int i = 0; i < customManager.PuzzleBoard.transform.childCount; i++)
            {
                customManager.PuzzleBoard.transform.GetChild(i).GetComponent<Button>().interactable = true;
            }

            resultPanel.SetActive(true);
        }
    }
    #endregion
    #region Result Panel
    public void ResultExitButton()
    { // Result Panel - 나가기
        AudioManager.Instance.SetAudioClip_BGM(1);
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        Time.timeScale = 1f;

        LoadingPanel.Instance.gameObject.SetActive(true);
        pushpushCanvas.SelectCategoryPanel.SetActive(true);
        pushpushCanvas.HelpButton.SetActive(true);
        pushpushCanvas.BackButton.SetActive(true);
        resultPanel.SetActive(false);
        gameObject.SetActive(false);
    }

    public void ResultRestartButton()
    { // Result Panel - 다시하기
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        
        if (GameManager.Instance.IsShutdown) return;

        LoadingPanel.Instance.gameObject.SetActive(true);
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
        AudioManager.Instance.SetAudioClip_BGM(1);
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        LoadingPanel.Instance.gameObject.SetActive(true);

        Time.timeScale = 1f;

        Init();

        warningPanel.SetActive(false);
        pushpushCanvas.SelectCategoryPanel.SetActive(true);
        pushpushCanvas.HelpButton.SetActive(true);
        pushpushCanvas.BackButton.SetActive(true);
        gameObject.SetActive(false);
    }

    public void WarningPanelCancelButton()
    { // Warning panel - 취소
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        Time.timeScale = 1f;

        warningPanel.SetActive(false);
    }
    #endregion
}
