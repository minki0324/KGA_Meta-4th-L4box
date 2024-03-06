using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCanvas : MonoBehaviour, IGameCanvas
{
    [Header("Canvas")]
    [SerializeField] private MainCanvas mainCanvas = null;
    [SerializeField] private ProfileCanvas profileCanvas = null;

    [Header("Side Panel")]
    public GameObject BackButton = null;
    public GameObject HelpButton = null;

    [Header("Ready")]
    public GameObject Ready = null;
    [SerializeField] private ReadyProfileSetting readyProfileSetting = null;

    [Header("Profile Info")]
    public GameObject SelectButton = null;
    public GameObject ChangeButton = null;

    [Header("Multi Game")]
    public GameObject MultiGame = null;

    [Header("Panel")]
    public GameObject ResultPanel = null;
    public GameObject WarningPanel = null;
    public GameObject GameReadyPanel = null;
    public GameObject HelpPanel = null;

    #region Ready
    public void ProfileSelectButton()
    { // 프로필 - 프로필 선택, 변경
        profileCanvas.gameObject.SetActive(true);
        profileCanvas.BlockPanel.SetActive(true);
    }
    #endregion

    public void GameStartButton()
    {
        throw new System.NotImplementedException();
    }

    public void GameExitButton()
    {
        throw new System.NotImplementedException();
    }

    public void GameCancelButton()
    {
        throw new System.NotImplementedException();
    }

    public void GameBackButton()
    {
        throw new System.NotImplementedException();
    }

    public void HelpPanelButton()
    {
        throw new System.NotImplementedException();
    }
}
