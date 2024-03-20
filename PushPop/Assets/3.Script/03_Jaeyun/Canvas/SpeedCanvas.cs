using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SpeedCanvas : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject mainCanvas = null;
    [SerializeField] private LoadingPanel loadingCanvas = null;

    [Header("Side Panel")]
    public GameObject BackButton = null;
    public GameObject HelpButton = null;

    [Header("Select Panel")]
    public SelectListSetting SelectListSetting = null;
    public GameObject SelectDifficultyPanel = null;
    public ScrollRect SelectCategoryPanelScrollView = null;
    public GameObject SelectCategoryPanel = null;

    [Header("Speed Game")]
    [SerializeField] private SpeedManager speedManager = null;
    public GameObject SpeedGame = null;
    public GameObject Timer = null;
    public GameObject CountSlider = null;

    [Header("Panel")]
    public GameObject Ready = null;
    public GameObject ResultPanel = null;
    public GameObject WarningPanel = null;
    public GameObject HelpPanel = null;

    [Header("Game Ready Panel")]
    public GameObject GameReadyPanel = null;
    public TMP_Text GameReadyPanelText = null;

    private void OnDisable()
    {
        ShutdownInit();
    }

    private void ShutdownInit()
    { // shutdown �� init
        if (!GameManager.Instance.IsShutdown) return;
        loadingCanvas.gameObject.SetActive(true);
        SelectDifficultyPanel.SetActive(true);
        SelectCategoryPanel.SetActive(false);
        HelpPanel.SetActive(false);
    }
    
    public void SelectDifficultyButton(int _difficulty)
    { // ���̵� ��ư
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        GameManager.Instance.Difficulty = (Difficulty)_difficulty;

        SelectCategoryPanelScrollView.normalizedPosition = new Vector2(1f, 1f);
        SelectCategoryPanel.SetActive(true);
    }

    public void GameStartButton()
    { // Ready - ���� ����
        AudioManager.Instance.SetAudioClip_BGM(3);
        AudioManager.Instance.SetCommonAudioClip_SFX(0);
        loadingCanvas.gameObject.SetActive(true);

        GameManager.Instance.IsGameClear = false;
        PushPop.Instance.BoardSprite = SelectListSetting.BoardIcon; // pushpop

        SpeedGame.SetActive(true);
        SelectDifficultyPanel.SetActive(false);
        SelectCategoryPanel.SetActive(false);
        SelectListSetting.Ready.SetActive(false);
        HelpButton.SetActive(false);
        speedManager.GameStart();
    }

    public void SpeedBackButton()
    { // �ڷΰ���
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        if (SelectDifficultyPanel.activeSelf)
        { // ���̵� ���� ���� ��
            AudioManager.Instance.SetAudioClip_BGM(0);
            loadingCanvas.gameObject.SetActive(true);
            
            GameManager.Instance.GameMode = GameMode.Lobby;
            GameManager.Instance.InGame = false;
            GameManager.Instance.ShutdownCoroutineStop();

            mainCanvas.SetActive(true);
            gameObject.SetActive(false);
        }
        else if (SelectCategoryPanel.activeSelf)
        { // ī�װ� ���� ���� ��
            SelectDifficultyPanel.SetActive(true);
            SelectCategoryPanel.SetActive(false);
        }
        else if (SpeedGame.activeSelf)
        { // ���� ���� ��
            AudioManager.Instance.SetAudioClip_BGM(1);
            Time.timeScale = 0f;
            WarningPanel.SetActive(true);
        }
    }

    public void ReadyPanelExitButton()
    { // Ready panel
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        SelectListSetting.Ready.SetActive(false);
    }

    public void HelpPanelButton()
    { // ����
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        HelpPanel.SetActive(true);
    }

    public void HelpPanelBackButton()
    { // ���� �ݱ�
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        HelpPanel.SetActive(false);
    }
}
