using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Speed_Timer : MonoBehaviour
{
    [Header("캔버스")]
    [SerializeField] private Speed_Canvas speed_Canvas;
    [SerializeField] private Help_Canvas help_Canvas;

    [Header("패널")]
    [SerializeField] private GameObject SelectDifficulty_Panel;

    [Header("타이머")]
    [SerializeField] private TMP_Text time_Text;
    [SerializeField] private Slider time_Slider;

    [Header("아이콘 이미지")]
    [SerializeField] private Image Mold_Image;

    [Header("뒤로가기 버튼")]
    [SerializeField] private Button Back_Btn;

    public int currentTime;
    int sec;
    int min;

    #region Unity Callback

    private void OnEnable()
    {
        //시간 초기화
       currentTime = GameManager.Instance.TimerTime + 1;
       // currentTime = 10 +1;
        SetText();

        //슬라이더 초기화
        time_Slider.maxValue = currentTime;
        time_Slider.minValue = 0f;
        time_Slider.value = time_Slider.maxValue;

        //몰드 아이콘 이미지 초기화
        Mold_Image.sprite = speed_Canvas.moldIcon;

        //타이머 코루틴 시작
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
    //타이머 코루틴
    private IEnumerator Timer_co()
    {
        int cashing = 1;

        while(true)
        {      
            currentTime -= cashing;
            SetText();
          
            if (currentTime <= 0)
            {
                //경고문 띄우기
                yield break;
            }
            yield return new WaitForSeconds(cashing);
        }
    }


    //슬라이더 값 변화 코루틴
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
                //경고문 띄우기
                yield break;
            }
            yield return new WaitForSeconds(cashing);
        }
    }



    //시분초 변환 & 텍스트 포맷 지정 함수
    public void SetText()
    {
        sec = currentTime % 60;    //60으로 나눈 나머지 = 초
        min = currentTime / 60;
        time_Text.text = $"{string.Format("{0:0}", min)}분 {sec}초";
    }
    #endregion
}
