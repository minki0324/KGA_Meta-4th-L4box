using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{ // speed, multi game timer
    public TMP_Text TimerText = null;
    public float CurrentTime = 60f; // 현재 시간
    public bool TenCount = false; // 남은 시간이 10초 이하인지 체크
    public bool EndTimer = false;

    private float compareTime = 0;
    private int setSign = 0;

    public Coroutine TimerCoroutine = null;

    public void TimerStart()
    { // StartCoroutine
        if (GameManager.Instance.GameMode.Equals(GameMode.Speed))
        {
            // 난이도에 따라 또 나뉨
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
                if (CurrentTime >= compareTime)
                {
                    TimerText.color = TimerCountColorChange("#FF0000");

                    if (!TenCount)
                    { // 10초 이하일 때 타이머 소리 한 번 재생
                        TenCount = true;
                        AudioManager.instance.SetAudioClip_SFX((int)GameManager.Instance.GameMode, true);
                    }
                }
            }
            else
            { // multi
                if (CurrentTime <= 0f) break;
                if (CurrentTime <= compareTime)
                { 
                    TimerText.color = TimerCountColorChange("#FF0000");

                    if (!TenCount)
                    { // 10초 이하일 때 타이머 소리 한 번 재생
                        TenCount = true;
                        AudioManager.instance.SetAudioClip_SFX((int)GameManager.Instance.GameMode, true);
                    }
                }
            }

            SetTime();
            yield return null;
        }

        EndTimer = true; // game end
        TimerCoroutine = null;
    }

    private void SetTime()
    {
        if (GameManager.Instance.GameMode.Equals(GameMode.Speed))
        {
            float sec = CurrentTime % 60; // 60으로 나눈 나머지 = 초
            float min = CurrentTime / 60;
            TimerText.text = $"{string.Format("{0:00}", min)}:{string.Format("{0:00}", sec)}";
        }
        else
        { // multi mode
            TimerText.text = $"남은시간\n{string.Format("{0:0}", CurrentTime)}";
        }
    }

    private Color TimerCountColorChange(string _colorCode)
    { // text color change
        Color newColor = new Color(0, 0, 0, 1);
        if (ColorUtility.TryParseHtmlString(_colorCode, out newColor))
        {
            return newColor;
        }
        return newColor;
    }
}
