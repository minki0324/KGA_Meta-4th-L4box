using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private SelectListSetting selectListSetting = null;
    public GameObject SelectDifficultyPanel = null;
    public ScrollRect SelectCategoryPanelScrollView = null;
    public GameObject SelectCategoryPanel = null;

    [Header("Speed Game")]
    [SerializeField] private SpeedManager speedManager = null;
    public GameObject SpeedGame = null;
    public GameObject Timer = null;
    public GameObject CountSlider = null;

    [Header("Panel")]
    public GameObject ResultPanel = null;
    public GameObject WarningPanel = null;
    public GameObject GameReadyPanel = null;
    public GameObject HelpPanel = null;

    public void SelectDifficultyButton(int _difficulty)
    { // 난이도 버튼
        AudioManager.instance.SetAudioClip_BGM(1);
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        GameManager.Instance.Difficulty = (Difficulty)_difficulty;

        SelectCategoryPanelScrollView.normalizedPosition = new Vector2(1f, 1f);
        SelectCategoryPanel.SetActive(true);
        SelectDifficultyPanel.SetActive(false);
    }

    public void GameStartButton()
    { // Ready - 게임 시작
        AudioManager.instance.SetCommonAudioClip_SFX(0);
        AudioManager.instance.SetAudioClip_BGM(3);
        PushPop.Instance.boardSprite = selectListSetting.BoardIcon; // pushpop

        SpeedGame.SetActive(true);
        SelectDifficultyPanel.SetActive(false);
        SelectCategoryPanel.SetActive(false);
        selectListSetting.Ready.SetActive(false);
        HelpButton.SetActive(false);
        speedManager.GameStart();
    }

    public void SpeedBackButton()
    { // 뒤로가기
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        if (SelectDifficultyPanel.activeSelf)
        { // 난이도 선택 중일 때
            AudioManager.instance.SetAudioClip_BGM(0);
            mainCanvas.SetActive(true);
            gameObject.SetActive(false);
        }
        else if (SelectCategoryPanel.activeSelf)
        { // 카테고리 선택 중일 때
            SelectDifficultyPanel.SetActive(true);
            SelectCategoryPanel.SetActive(false);
        }
        else if (SpeedGame.activeSelf)
        { // 게임 중일 때
            Time.timeScale = 0f;
            WarningPanel.SetActive(true);
        }
    }

    public void ReadyPanelExitButton()
    { // Ready panel
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        selectListSetting.Ready.SetActive(false);
    }

    public void HelpPanelButton()
    { // 도움말
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        HelpPanel.SetActive(true);
    }

    public void HelpPanelBackButton()
    { // 도움말 닫기
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        HelpPanel.SetActive(false);
    }
}
