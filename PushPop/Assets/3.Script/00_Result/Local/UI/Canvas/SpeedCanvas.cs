using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SpeedCanvas : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject mainCanvas = null;

    [Header("Side Panel")]
    public GameObject BackButton = null;
    public GameObject HelpButton = null;

    [Header("Select Panel")]
    [SerializeField] private GameObject selectDifficultyPanel = null;
    [SerializeField] private ScrollRect selectCategoryPanelScrollView = null;
    public SelectListSetting SelectListSetting = null;
    public GameObject SelectCategoryPanel = null;

    [Header("Speed Game")]
    [SerializeField] private SpeedManager speedManager = null;
    [SerializeField] private GameObject speedGame = null;

    [Header("Panel")]
    [SerializeField] private GameObject warningPanel = null;
    [SerializeField] private GameObject helpPanel = null;

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
        LoadingPanel.Instance.gameObject.SetActive(true);
        selectDifficultyPanel.SetActive(true);
        SelectCategoryPanel.SetActive(false);
        helpPanel.SetActive(false);
    }
    
    public void SelectDifficultyButton(int _difficulty)
    { // ���̵� ��ư
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        GameManager.Instance.Difficulty = (Difficulty)_difficulty;

        selectCategoryPanelScrollView.normalizedPosition = new Vector2(1f, 1f);
        selectDifficultyPanel.SetActive(false);
        SelectCategoryPanel.SetActive(true);
    }

    public void GameStartButton()
    { // Ready - ���� ����
        AudioManager.Instance.SetAudioClip_BGM(3);
        AudioManager.Instance.SetCommonAudioClip_SFX(0);
        LoadingPanel.Instance.gameObject.SetActive(true);

        GameManager.Instance.IsGameClear = false;
        PushPop.Instance.BoardSprite = SelectListSetting.BoardIcon; // pushpop

        speedGame.SetActive(true);
        selectDifficultyPanel.SetActive(false);
        SelectCategoryPanel.SetActive(false);
        SelectListSetting.Ready.SetActive(false);
        HelpButton.SetActive(false);
        speedManager.GameStart();
    }

    public void SpeedBackButton()
    { // �ڷΰ���
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        if (selectDifficultyPanel.activeSelf)
        { // ���̵� ���� ���� ��
            AudioManager.Instance.SetAudioClip_BGM(0);
            LoadingPanel.Instance.gameObject.SetActive(true);
            
            GameManager.Instance.GameMode = GameMode.Lobby;
            GameManager.Instance.InGame = false;
            GameManager.Instance.ShutdownCoroutineStop();

            mainCanvas.SetActive(true);
            SelectCategoryPanel.SetActive(false) ;
            selectDifficultyPanel.SetActive(true);
            gameObject.SetActive(false);
        }
        else if (SelectCategoryPanel.activeSelf)
        { // ī�װ� ���� ���� ��
            selectDifficultyPanel.SetActive(true);
            SelectCategoryPanel.SetActive(false);
        }
        else if (speedGame.activeSelf)
        { // ���� ���� ��
            Time.timeScale = 0f;
            warningPanel.SetActive(true);
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
        helpPanel.SetActive(true);
    }

    public void HelpPanelBackButton()
    { // ���� �ݱ�
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        helpPanel.SetActive(false);
    }
}
