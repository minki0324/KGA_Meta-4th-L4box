using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{ // speed, multi game timer
    public TMP_Text TimerText;
    [HideInInspector] public float CurrentTime = 60f; // ���� �ð�
    [HideInInspector] public bool isTenCount = false; // ���� �ð��� 10�� ������ �� true
    [HideInInspector] public bool isEndTimer = false; // ���� ����� true

    private float compareTime = 0;
    private int setSign = 0;

    public Coroutine TimerCoroutine = null;

    public void TimerStart()
    { // StartCoroutine
        if (GameManager.Instance.GameMode.Equals(GameMode.Speed))
        {
            // ���̵��� ���� �� ����
            compareTime = (float)GameManager.Instance.Difficulty;
            setSign = 1;
        }
        else
        { // multi
            compareTime = 10f;
            setSign = -1;
        }
        TimerCoroutine = StartCoroutine(Timer_Co());
    }

    private IEnumerator Timer_Co()
    {
        while (true)
        {
            CurrentTime += Time.deltaTime * setSign;

            if (GameManager.Instance.GameMode.Equals(GameMode.Speed))
            {
                if (CurrentTime >= (float)GameManager.Instance.Difficulty + 10f) break;
                if (CurrentTime >= (float)GameManager.Instance.Difficulty)
                {
                    TimerText.color = TimerCountColorChange("#FF0000");

                    if (!isTenCount)
                    { // 10�� ������ �� Ÿ�̸� �Ҹ� �� �� ���
                        isTenCount = true;
                        AudioManager.Instance.SetAudioClip_SFX((int)GameMode.Speed, true);
                    }
                }
            }
            else
            { // multi
                if (CurrentTime <= 0f) break;
                if (CurrentTime <= compareTime)
                { 
                    TimerText.color = TimerCountColorChange("#FF0000");

                    if (!isTenCount)
                    { // 10�� ������ �� Ÿ�̸� �Ҹ� �� �� ���
                        isTenCount = true;
                        AudioManager.Instance.SetAudioClip_SFX((int)GameMode.Multi, true);
                    }
                }
            }

            SetTime();
            yield return null;
        }

        isEndTimer = true; // game end
        TimerCoroutine = null;
    }

    private void SetTime()
    { // �����ð� ǥ��
        if (GameManager.Instance.GameMode.Equals(GameMode.Speed))
        {
            int sec = (int)CurrentTime % 60; // 60���� ���� ������ = ��
            int min = (int)CurrentTime / 60;
            TimerText.text = $"{string.Format("{00:00}", min)}:{string.Format("{00:00}", sec)}";
        }
        else
        { // multi mode
            TimerText.text = $"�����ð�\n{string.Format("{0:0}", CurrentTime)}";
        }
    }

    private Color TimerCountColorChange(string _colorCode)
    { // text color change
        if (ColorUtility.TryParseHtmlString(_colorCode, out Color newColor))
        {
            return newColor;
        }
        return newColor;
    }
}
