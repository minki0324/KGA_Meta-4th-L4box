using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MemoryCanvas : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject mainCanvas = null;

    [Header("Side Panel")]
    public GameObject HelpButton = null;

    [Header("Ready")]
    public GameObject Ready = null;
    public ReadyProfileSetting ReadyProfileSetting = null;

    [Header("Memory Game")]
    public GameObject MemoryGame = null;
    [SerializeField] private MemoryManager memoryManager = null;

    [Header("Panel")]
    public GameObject ResultPanel = null;
    public GameObject WarningPanel = null;
    public GameObject GameReadyPanel = null;
    public GameObject HelpPanel = null;

    [Header("Game Ready Panel")]
    public TMP_Text GameReadyPanelText = null;

    #region Ready
    public void GameStartButton()
    { // 대기 - 게임 시작
        AudioManager.Instance.SetCommonAudioClip_SFX(0);
        AudioManager.Instance.SetAudioClip_BGM(5);

        MemoryGame.SetActive(true);
        HelpButton.SetActive(false);
        Ready.SetActive(false);
        memoryManager.BackButton.SetActive(true);
        memoryManager.GameStart();
    }
    #endregion
    #region Side Panel
    public void MemoryBackButton()
    { // 뒤로가기
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        if (Ready.activeSelf)
        { // 대기 화면일 때
            AudioManager.Instance.SetAudioClip_BGM(0);
            GameManager.Instance.GameMode = GameMode.None;
            GameManager.Instance.InGame = false;

            mainCanvas.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        { // 게임 중일 때
            Time.timeScale = 0f;
            WarningPanel.SetActive(true);
        }
    }

    public void HelpPanelButton()
    { // 도움말
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        HelpPanel.SetActive(true);
    }

    public void HelpPanelBackButton()
    {
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        HelpPanel.SetActive(false);
    }
    #endregion
}
