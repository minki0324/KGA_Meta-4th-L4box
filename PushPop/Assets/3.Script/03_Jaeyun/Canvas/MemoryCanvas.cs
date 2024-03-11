using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MemoryCanvas : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject mainCanvas = null;

    [Header("Side Panel")]
    public GameObject BackButton = null;
    public GameObject HelpButton = null;

    [Header("Ready")]
    public GameObject Ready = null;
    public ReadyProfileSetting ReadyProfileSetting = null;

    [Header("Memory Game")]
    public GameObject MemoryGame = null;
    [SerializeField] private MemoryManager memoryManager = null;

    [Header("Panel")]
    public GameObject HintGuidePanel = null;
    public GameObject ResultPanel = null;
    public GameObject WarningPanel = null;
    public GameObject GameReadyPanel = null;
    public GameObject HelpPanel = null;

    [Header("Game Ready Panel")]
    public TMP_Text GameReadyPanelText = null;

    #region Ready
    public void GameStartButton()
    { // ��� - ���� ����
        AudioManager.instance.SetCommonAudioClip_SFX(0);
        AudioManager.instance.SetAudioClip_BGM(5);

        MemoryGame.SetActive(true);
        HelpButton.SetActive(false);
        BackButton.SetActive(false);
        Ready.SetActive(false);
        memoryManager.GameStart();
    }
    #endregion
    #region Side Panel
    public void MultiBackButton()
    { // �ڷΰ���
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.SelectPlayer = Player.Player1;
        GameManager.Instance.GameMode = GameMode.None;
        GameManager.Instance.InGame = false;

        mainCanvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void HelpPanelButton()
    { // ����
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        HelpPanel.SetActive(true);
    }

    public void HelpPanelBackButton()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        HelpPanel.SetActive(false);
    }
    #endregion
}
