using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeCanvas : MonoBehaviour
{
    [SerializeField] private PracticeManager practiceManager;

    public void PracticeBackButton()
    { // �ڷΰ��� ��ư
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        Time.timeScale = 0f;
        practiceManager.WarningPanel.SetActive(true);
    }
}
