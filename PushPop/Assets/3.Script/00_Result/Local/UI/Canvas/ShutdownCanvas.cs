using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShutdownCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text gameReadyPanelText = null;
    [SerializeField] private GameObject gameReadyPanel = null;

    private void Start()
    {
        GameManager.Instance.Shutdown += ShutdownInit;
        GameManager.Instance.ShutdownAlarm += ShutdownAlarm;
    }

    private void OnDestroy()
    {
        GameManager.Instance.Shutdown -= ShutdownInit;
        GameManager.Instance.ShutdownAlarm -= ShutdownAlarm;
    }

    private void ShutdownInit()
    {
        gameReadyPanel.SetActive(false);
    }

    private void ShutdownAlarm()
    {
        GameManager.Instance.OnShutdownAlarm = false;
        float shutdownTime = GameManager.Instance.ShutdownTimer / 60f;
        gameReadyPanelText.text = $"���� �ð��� {string.Format("{0:0}", shutdownTime)}�� ���ҽ��ϴ�.";
        gameReadyPanel.SetActive(true);
        AudioManager.Instance.Pause_SFX(true);
        Time.timeScale = 0f;
    }
}
