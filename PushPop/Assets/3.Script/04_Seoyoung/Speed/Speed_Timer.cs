using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Speed_Timer : MonoBehaviour
{
    [Header("ĵ����")]
    [SerializeField] private Speed_Canvas speed_Canvas;
    [SerializeField] private Help_Canvas help_Canvas;

    [Header("�г�")]
    [SerializeField] private GameObject SelectDifficulty_Panel;
    [SerializeField] private GameObject Warning_Panel;

    [Header("Ÿ�̸�")]
    [SerializeField] private TMP_Text time_Text;
    public Slider time_Slider;

    [Header("������ �̹���")]
    [SerializeField] private Image Mold_Image;

    [Header("�ڷΰ��� ��ư")]
    [SerializeField] private Button Back_Btn;

    public int currentTime;
    private int sec;
    private int min;
    public Coroutine timer = null;

    #region Unity Callback

    private void OnEnable()
    {
        //�ð� �ʱ�ȭ
        // currentTime = GameManager.Instance.ShutdownTime + 1;
        currentTime = 0; // 0���� ���ѽð����� +
                         // currentTime = 10 +1;
        SetText();

        //�����̴� �ʱ�ȭ
        time_Slider.maxValue = 1f;
        time_Slider.minValue = 0f;
        time_Slider.value = 0f;
        time_Slider.gameObject.SetActive(false);

        //���� ������ �̹��� �ʱ�ȭ
        // Mold_Image.sprite = speed_Canvas.moldIcon;

        //Ÿ�̸� �ڷ�ƾ ����
        timer = StartCoroutine(Timer_co());
        // StartCoroutine(SliderLerp_co());

        if(Warning_Panel.activeSelf)
        {
            Warning_Panel.SetActive(false);
        }
    }


    #endregion


    #region Other Method
    //Ÿ�̸� �ڷ�ƾ
    private IEnumerator Timer_co()
    {
        // game ready
        StartCoroutine(GameManager.Instance.GameReady_Co());
        int cashing = 1;

        while (true)
        {
            currentTime += cashing;
            GameManager.Instance.TimeScore = currentTime; // score ����
            SetText();

            if (currentTime <= 0)
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
        time_Text.text = $"{string.Format("{0:00}", min)}:{string.Format("{0:00}", sec)}";
    }

    public void BackBtn_Clicked()
    {
        Time.timeScale = 0;
        Warning_Panel.SetActive(true);
    }

    public void GoOutBtn_Clicked()
    {
        Time.timeScale = 1;
        help_Canvas.gameObject.SetActive(true);
        help_Canvas.Button_Enable();
        SelectDifficulty_Panel.SetActive(true);
        speed_Canvas.Enable_Objects();
        Warning_Panel.SetActive(false);
        gameObject.SetActive(false);
    }

    public void CancelBtn_Clicked()
    {
        Time.timeScale = 1;
        Warning_Panel.SetActive(false);
    }

    #endregion
}
