using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShutdownCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text GameReadyPanelText = null;
    [SerializeField] private GameObject GameReadyPanel = null;

    private void Start()
    {
        GameManager.Instance.ShutdownAlarm += ShutdownAlarm;
    }

    public void ShutdownAlarm()
    {
        GameManager.Instance.OnShutdownAlarm = false;
        float shutdownTime = GameManager.Instance.ShutdownTimer / 60f;
        GameReadyPanelText.text = $"게임 시간이 {string.Format("{0:0}", shutdownTime)}분 남았습니다.";
        GameReadyPanel.SetActive(true);
        AudioManager.Instance.Pause_SFX(true);
        Time.timeScale = 0f;
    }
}
