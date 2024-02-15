using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Speed_Timer : MonoBehaviour
{
    [Header("ĵ����")]
    [SerializeField] private Speed_Canvas speed_Canvas;
    [SerializeField] private Help_Canvas help_Canvas;

    [Header("�г�")]
    [SerializeField] private GameObject SelectDifficulty_Panel;

    [Header("Ÿ�̸�")]
    [SerializeField] private TMP_Text time_Text;
    [SerializeField] private Slider time_Slider;

    [Header("������ �̹���")]
    [SerializeField] private Image Mold_Image;

    [Header("�ڷΰ��� ��ư")]
    [SerializeField] private Button Back_Btn;

    public int currentTime;
    int sec;
    int min;

    #region Unity Callback

    private void OnEnable()
    {
        //�ð� �ʱ�ȭ
       currentTime = GameManager.Instance.TimerTime + 1;
       // currentTime = 10 +1;
        SetText();

        //�����̴� �ʱ�ȭ
        time_Slider.maxValue = currentTime;
        time_Slider.minValue = 0f;
        time_Slider.value = time_Slider.maxValue;

        //���� ������ �̹��� �ʱ�ȭ
        Mold_Image.sprite = speed_Canvas.moldIcon;

        //Ÿ�̸� �ڷ�ƾ ����
        StartCoroutine(Timer_co());
        StartCoroutine(SliderLerp_co());
    }

    public void BackBtn_Clicked()
    {
        help_Canvas.gameObject.SetActive(true);
        SelectDifficulty_Panel.SetActive(true);
        speed_Canvas.Enable_Objects();


        gameObject.SetActive(false);
    }

    #endregion


    #region Other Method
    //Ÿ�̸� �ڷ�ƾ
    private IEnumerator Timer_co()
    {
        int cashing = 1;

        while(true)
        {      
            currentTime -= cashing;
            SetText();
          
            if (currentTime <= 0)
            {
                //��� ����
                yield break;
            }
            yield return new WaitForSeconds(cashing);
        }
    }


    //�����̴� �� ��ȭ �ڷ�ƾ
    private IEnumerator SliderLerp_co()
    {
        float cashing = 0.05f;
        float currentT = currentTime;

        while(true)
        {
            currentT -= cashing;
            time_Slider.value = currentT;

            if (currentT <= 0)
            {
                //��� ����
                yield break;
            }
            yield return new WaitForSeconds(cashing);
        }
    }



    //�ú��� ��ȯ & �ؽ�Ʈ ���� ���� �Լ�
    public void SetText()
    {
        sec = currentTime % 60;    //60���� ���� ������ = ��
        min = currentTime / 60;
        time_Text.text = $"{string.Format("{0:0}", min)}�� {sec}��";
    }
    #endregion
}
