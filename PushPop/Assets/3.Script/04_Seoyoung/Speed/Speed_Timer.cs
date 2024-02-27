using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Speed_Timer : MonoBehaviour
{
    [Header("캔버스")]
    [SerializeField] private Speed_Canvas speed_Canvas;
    [SerializeField] private Help_Canvas help_Canvas;

    [Header("패널")]
    [SerializeField] private GameObject SelectDifficulty_Panel;
    [SerializeField] private GameObject Warning_Panel;
    public GameObject resultPanel;

    [Header("타이머")]
    [SerializeField] private TMP_Text time_Text;
    public Slider time_Slider;

    [Header("아이콘 이미지")]
    [SerializeField] private Image Mold_Image;

    [Header("뒤로가기 버튼")]
    [SerializeField] private Button Back_Btn;

    [Header("Result")]
    [SerializeField] private Image resultImage;
    [SerializeField] private TMP_Text resultTimer;
    [SerializeField] private TMP_Text resultLog;
    [SerializeField] private TMP_Text resultTitle;

    public int currentTime;
    private int sec;
    private int min;
    public Coroutine timer = null;
    public bool bNoTimePlaying = false;

    #region Unity Callback

    private void OnEnable()
    {
        //시간 초기화
        Init();
    }
    #endregion


    #region Other Method
    public void Init()
    {
        currentTime = 0; // 0부터 제한시간까지 +
                         // currentTime = 10 +1;
        bNoTimePlaying = false;
        SetText();

        //슬라이더 초기화
        time_Slider.maxValue = 1f;
        time_Slider.minValue = 0f;
        time_Slider.value = 0f;
        time_Slider.gameObject.SetActive(false);

        //몰드 아이콘 이미지 초기화
        // Mold_Image.sprite = speed_Canvas.moldIcon;

        //타이머 코루틴 시작
        time_Text.color = new Color(0, 0, 0, 1); // black으로 초기화
        timer = StartCoroutine(Timer_co());

        if (Warning_Panel.activeSelf)
        {
            Warning_Panel.SetActive(false);
        }

        // Back Method
        if (PushPop.Instance.pushPopBoardObject.Count > 0)
        {
            Destroy(PushPop.Instance.pushPopBoardObject[0]);
            PushPop.Instance.pushPopBoardObject.Clear();
        }

        if (GameManager.Instance.bubbleObject.Count > 0)
        {
            Destroy(GameManager.Instance.bubbleObject[0]);
            GameManager.Instance.bubbleObject.Clear();
        }

        GameManager.Instance.bubblePos.Clear(); // bubble transform mode에 따라 달라짐
        PushPop.Instance.PushPopClear();
        GameManager.Instance.pushpushCreate_Co = null;
    }

    //타이머 코루틴
    private IEnumerator Timer_co()
    {
        // game ready
        StartCoroutine(GameManager.Instance.GameReady_Co());
        int cashing = 1;

        while (true)
        {
            currentTime += cashing;
            GameManager.Instance.TimeScore = currentTime; // score 저장
            SetText();

            if (currentTime.Equals(50))
            {
                time_Text.color = TimerCountColorChange("#FF0000");
                if(!bNoTimePlaying)
                {
                    bNoTimePlaying = true;
                    AudioManager.instance.SetAudioClip_SFX(1, true);
                }
            }
            if (currentTime.Equals(60))
            {
                GameManager.Instance.GameClear();
                yield break;
            }
            yield return new WaitForSeconds(cashing);
        }
    }

    private Color TimerCountColorChange(string _colorCode)
    {
        Color newColor = new Color(0, 0, 0, 1);
        if (ColorUtility.TryParseHtmlString(_colorCode, out newColor))
        {
            return newColor;
        }
        return newColor;
    }

    //시분초 변환 & 텍스트 포맷 지정 함수
    public void SetText()
    {
        sec = currentTime % 60;    //60으로 나눈 나머지 = 초
        min = currentTime / 60;
        time_Text.text = $"{string.Format("{0:00}", min)}:{string.Format("{0:00}", sec)}";
    }

    public void BackBtn_Clicked()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        if(bNoTimePlaying)
        {
            AudioManager.instance.Pause_SFX(true);
        }

        Time.timeScale = 0;
        Warning_Panel.SetActive(true);
    }

    public void GoOutBtn_Clicked()
    {
        GameManager.Instance.StopCoroutine(GameManager.Instance.speedCreate);
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        AudioManager.instance.SetAudioClip_BGM(1);

        Time.timeScale = 1;
        AudioManager.instance.Stop_SFX();

        // Back Method
        if (PushPop.Instance.pushPopBoardUIObject.Count > 0)
        {
            Destroy(PushPop.Instance.pushPopBoardUIObject[0]);
            PushPop.Instance.pushPopBoardUIObject.Clear();
        }

        if (PushPop.Instance.pushPopBoardObject.Count > 0)
        {
            Destroy(PushPop.Instance.pushPopBoardObject[0]);
            PushPop.Instance.pushPopBoardObject.Clear();
        }

        if (GameManager.Instance.bubbleObject.Count > 0)
        {
            Destroy(GameManager.Instance.bubbleObject[0]);
            GameManager.Instance.bubbleObject.Clear();
        }

        GameManager.Instance.bubblePos.Clear(); // bubble transform mode에 따라 달라짐
        PushPop.Instance.PushPopClear();
        StopCoroutine(timer);
        if (GameManager.Instance.pushpushCreate_Co != null)
        {
            GameManager.Instance.StopCoroutine(GameManager.Instance.pushpushCreate_Co); // speed pushpop create 초기화
        }

        help_Canvas.gameObject.SetActive(true);
        help_Canvas.Button_Enable();
        SelectDifficulty_Panel.SetActive(true);
        speed_Canvas.Enable_Objects();
        Warning_Panel.SetActive(false);
        resultPanel.SetActive(false);
        gameObject.SetActive(false);
    }

    public void CancelBtn_Clicked()
    {
        Time.timeScale = 1;

        if(bNoTimePlaying)
        {
            AudioManager.instance.Pause_SFX(false);
        }

        Warning_Panel.SetActive(false);
    }

    public void Result()
    {
        int clearTitle;
        resultImage.sprite = speed_Canvas.moldIcon;
        resultTimer.text = $"{string.Format("{0:00}", min)}:{string.Format("{0:00}", sec)}";

        if (currentTime.Equals(60))
        {
            clearTitle = (int)ClearTitle.Fail;
        }
        else
        {
            clearTitle = (int)Ranking.instance.CompareRanking();
        }

        resultTitle.text = $"{Ranking.instance.ResultDialog.title[clearTitle]}";
        resultLog.text = $"{Ranking.instance.ResultDialog.speedResult[clearTitle]}";
    }
    #endregion
}
