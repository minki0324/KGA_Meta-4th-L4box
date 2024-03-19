using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PushPushManager : MonoBehaviour, IGame
{ // pushpush game
    [Header("Canvas")]
    [SerializeField] private PushPushCanvas pushpushCanvas = null;
    [SerializeField] private LoadingPanel loadingCanvas = null;

    [Header("Panel")]
    [SerializeField] private GameObject resultPanel = null;
    [SerializeField] private GameObject warningPanel = null;

    [Header("Game Info")]
    public CustomPushpopManager customManager = null;
    public PuzzleLozic puzzleManager = null;
    [SerializeField] private TMP_Text stageTitle = null;
    [SerializeField] private FramePuzzle framePuzzle = null;

    [Header("Game Result")]
    [SerializeField] private Image resultImage = null;
    [SerializeField] private TMP_Text resultMassageText = null;

    [Header("PushPop Object")]
    public Stack<GameObject> StackPops = new Stack<GameObject>(); //UI�� ���̴� ��ư��� ����
    [SerializeField] private FramePuzzle frame;
    public int PushCount;

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
    { // ������ ��� ������ ���� ������� ȣ��
        customManager.StageTitle.text = "�׸��� �°� ������ ���� ������!";

        puzzleManager.SettingGame(); // puzzle mode start

        GameManager.Instance.NextMode -= OnAllBubblesPopped;
        GameManager.Instance.NextMode += OnPuzzleSolved;
    }

    public void OnPuzzleSolved()
    { // ������ ��� ������ �� ȣ��
        AudioManager.Instance.SetAudioClip_SFX(1, false);
        customManager.StageTitle.text = "�� ������� �׸��� �ٸ纸��!";

        customManager.CustomMode.SetActive(true);
        puzzleManager.DestroyChildren();
        puzzleManager.CraetBoard();

        GameManager.Instance.NextMode -= OnPuzzleSolved;
    }

    public void OnCustomEndButton()
    { // Custom mode - ��������
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        customManager.StageTitle.text = "��� Ǫ������ ��������!";

        // End Game
        if (PushPop.Instance.StackPops.Count.Equals(0))
        {
            GameEnd();
            return;
        }

        customManager.EndCustom();

        frame.FrameImage.alphaHitTestMinimumThreshold = 0f;
    }

    public void OnPushPushEnd() //Ǫ��Ǫ�ð� ��� ������ �ʱ�ȭ �Ұ͵� ��Ƶ� �޼ҵ�
    {
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
    }
    #endregion

    public void Init()
    { // OnDisable(), check list: coroutine, list, array, variables �ʱ�ȭ ����
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        GameManager.Instance.GameEnd -= GameEnd;
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
    { // OnEnable() bubble size, board size, pushpopbutton size, pushpop percentage, etc. setting ����
        GameManager.Instance.GameEnd += GameEnd;
        GameManager.Instance.NextMode += OnAllBubblesPopped;
        PushPop.Instance.BoardSize = new Vector2(520f, 400f);
        PushPop.Instance.ButtonSize = new Vector2(80f, 80f);

        puzzleManager.SettingPuzzleInfo();
        customManager.StageTitle.text = "�񴰹���� ��Ʈ��������!";
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
        pushpushCanvas.GameReadyPanelText.text = "�غ�~";

        yield return new WaitForSeconds(2f);

        AudioManager.Instance.SetCommonAudioClip_SFX(2);
        pushpushCanvas.GameReadyPanelText.text = "����!";

        yield return new WaitForSeconds(0.8f);

        pushpushCanvas.GameReadyPanel.SetActive(false);
        // ready
        GameReadyStart();
    }

    public void GameReadyStart()
    {
        puzzleManager.SettingPuzzlePos();
    }

    public void GameEnd()
    { // popbutton click �� ȣ��
        if (PushPop.Instance.pushPopButton.Count.Equals(PushPop.Instance.PushCount))
        {
            AudioManager.Instance.SetCommonAudioClip_SFX(6);
            PushPop.Instance.PushCount = 0;
            PushPop.Instance.pushPopButton.Clear();

            int[] spriteIndexs = new int[customManager.puzzleBoard.transform.childCount];
            Vector2[] childPos = new Vector2[customManager.puzzleBoard.transform.childCount];
            for (int i = 0; i < customManager.puzzleBoard.transform.childCount; i++)
            {
                PushPopButton pop = customManager.puzzleBoard.transform.GetChild(i).GetComponent<PushPopButton>();
                spriteIndexs[i] = pop.spriteIndex;
                childPos[i] = pop.transform.localPosition;
            }

            // Save
            PushPushObject newPush = new PushPushObject(puzzleManager.CurrentPuzzle.PuzzleID, PushPop.Instance.StackPops.Count, spriteIndexs, childPos);
            string json = JsonUtility.ToJson(newPush);
            SQL_Manager.instance.SQL_AddPushpush(json, ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex);

            // Result Setting
            List<PushPushObject> pushlist = SQL_Manager.instance.SQL_SetPushPush(ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex);
            customManager.ResultText.text = DataManager.Instance.iconDict[puzzleManager.CurrentPuzzle.PuzzleID];
            customManager.ResultImage.sprite = DataManager.Instance.pushPopAtlas.GetSprite(pushlist[0].spriteName.ToString());
            customManager.ResultImage.SetNativeSize();
            customManager.ResultImage.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

            for (int i = 0; i < pushlist[0].childIndex; i++)
            { // button setting
                GameObject pop = Instantiate(PushPop.Instance.pushPopButtonPrefab, customManager.ResultImage.transform);
                pop.GetComponent<Image>().sprite = customManager.pushPopButtonSprite[pushlist[0].childSpriteIndex[i]];
                pop.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                pop.transform.localPosition = pushlist[0].childPosition[i];
            }

            // end setting
            customManager.ResultImage.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            for (int i = 0; i < customManager.puzzleBoard.transform.childCount; i++)
            {
                customManager.puzzleBoard.transform.GetChild(i).GetComponent<Button>().interactable = true;
            }

            resultPanel.SetActive(true);
        }
    }

    #region Result Panel
    public void ResultExitButton()
    { // Result Panel - ������
        AudioManager.Instance.SetAudioClip_BGM(1);
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        Time.timeScale = 1f;

        loadingCanvas.gameObject.SetActive(true);
        pushpushCanvas.SelectCategoryPanel.SetActive(true);
        pushpushCanvas.HelpButton.SetActive(true);
        pushpushCanvas.BackButton.SetActive(true);
        resultPanel.SetActive(false);
        gameObject.SetActive(false);
    }

    public void ResultRestartButton()
    { // Result Panel - �ٽ��ϱ�
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
    #region Warning Panel
    public void WarningPanelGoOutButton()
    { // Warning panel - ������
        AudioManager.Instance.SetAudioClip_BGM(1);
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        loadingCanvas.gameObject.SetActive(true);

        Time.timeScale = 1f;

        Init();

        warningPanel.SetActive(false);
        pushpushCanvas.SelectCategoryPanel.SetActive(true);
        pushpushCanvas.HelpButton.SetActive(true);
        pushpushCanvas.BackButton.SetActive(true);
        gameObject.SetActive(false);
    }

    public void WarningPanelCancelButton()
    { // Warning panel - ���
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        Time.timeScale = 1f;

        warningPanel.SetActive(false);
    }
    #endregion
}
