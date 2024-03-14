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

    public void SelectDifficultyButton(int _difficulty)
    { // ���̵� ��ư
        AudioManager.instance.SetAudioClip_BGM(1);
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        GameManager.Instance.Difficulty = (Difficulty)_difficulty;

        SelectCategoryPanelScrollView.normalizedPosition = new Vector2(1f, 1f);
        SelectCategoryPanel.SetActive(true);
        SelectDifficultyPanel.SetActive(false);
    }

    public void GameStartButton()
    { // Ready - ���� ����
        AudioManager.instance.SetCommonAudioClip_SFX(0);
        AudioManager.instance.SetAudioClip_BGM(3);
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
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        if (SelectDifficultyPanel.activeSelf)
        { // ���̵� ���� ���� ��
            AudioManager.instance.SetAudioClip_BGM(0);
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
            Time.timeScale = 0f;
            WarningPanel.SetActive(true);
        }
    }

    public void ReadyPanelExitButton()
    { // Ready panel
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        SelectListSetting.Ready.SetActive(false);
    }

    public void HelpPanelButton()
    { // ����
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        HelpPanel.SetActive(true);
    }

    public void HelpPanelBackButton()
    { // ���� �ݱ�
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        HelpPanel.SetActive(false);
    }
}
