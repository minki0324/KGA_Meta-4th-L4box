using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PushPushCanvas : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private MainCanvas mainCanvas = null;

    [Header("Side Panel")]
    public GameObject BackButton = null;
    public GameObject HelpButton = null;

    [Header("PushPush Game")]
    [SerializeField] private PushPushManager pushpushManager = null;
    public SelectListSetting SelectListSetting = null;
    public GameObject PushpushGame = null;
    public GameObject CustomMode = null;

    [Header("Select Panel")]
    public GameObject SelectCategoryPanel = null;
    public GameObject SelectBoardPanel = null;

    [Header("Panel")]
    public GameObject ResultPanel = null;
    public GameObject WarningPanel = null;
    public GameObject GameReadyPanel = null;
    public GameObject HelpPanel = null;
    public TMP_Text GameReadyPanelText = null;

    public void GameStartButton()
    {
        AudioManager.Instance.SetAudioClip_BGM(3);
        AudioManager.Instance.SetCommonAudioClip_SFX(0);

        SelectCategoryPanel.SetActive(false);
        SelectBoardPanel.SetActive(false);
        HelpButton.SetActive(false);
        PushpushGame.SetActive(true);
        pushpushManager.GameStart();
    }

    public void PushPushBackButton()
    { // �ڷΰ��� ��ư
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        if (SelectCategoryPanel.activeSelf)
        { // ī�װ� ���� ���� ��
            AudioManager.Instance.SetAudioClip_BGM(0);
            GameManager.Instance.GameMode = GameMode.None;
            GameManager.Instance.InGame = false;
            mainCanvas.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        else if (PushpushGame.activeSelf)
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
    {
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        HelpPanel.SetActive(false);
    }
}
