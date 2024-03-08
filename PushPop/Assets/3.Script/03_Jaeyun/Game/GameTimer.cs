using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{ // speed, multi game timer
    public TMP_Text TimerText = null;
    public float CurrentTime = 60f; // ���� �ð�
    public bool TenCount = false; // ���� �ð��� 10�� �������� üũ
    public bool EndTimer = false;
    public Coroutine TimerCoroutine = null;

    private float compareTime = 0;
    private int setSign = 0;

    public void TimerStart()
    { // StartCoroutine
        if (GameManager.Instance.GameMode.Equals(GameMode.Speed))
        {
            // ���̵��� ���� �� ����
            compareTime = 50f; // difficulty
            compareTime = CurrentTime - compareTime;
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
        while (CurrentTime >= 0f)
        {
            CurrentTime += Time.deltaTime * setSign;

            if (CurrentTime <= compareTime)
            {
                TimerText.color = TimerCountColorChange("#FF0000");

                if (!TenCount)
                { // 10�� ������ �� Ÿ�̸� �Ҹ� �� �� ���
                    TenCount = true;
                    AudioManager.instance.SetAudioClip_SFX((int)GameManager.Instance.GameMode, true);
                }
            }

            SetTIme();
            yield return null;
        }

        CurrentTime = 0f; // ���߿� speed�� 60f 50f 40f�� �ٲپ������
        EndTimer = true; // game end
        TimerCoroutine = null;
    }

    private void SetTIme()
    {
        if (GameManager.Instance.GameMode.Equals(GameMode.Speed))
        {
            float sec = CurrentTime % 60; //60���� ���� ������ = ��
            float min = CurrentTime / 60;
            TimerText.text = $"{string.Format("{0:00}", min)}:{string.Format("{0:00}", sec)}";
        }
        else
        { // multi mode
            TimerText.text = $"{string.Format("{0:0}", CurrentTime)}";
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
