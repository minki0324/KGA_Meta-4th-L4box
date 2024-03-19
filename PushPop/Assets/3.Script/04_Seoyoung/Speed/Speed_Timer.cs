using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class Speed_Timer : MonoBehaviour
{
    [Header("캔버스")]
    [SerializeField] private SelectListSetting speed_Canvas;
    [SerializeField] private HelpScriptManager help_Canvas;

    [Header("패널")]
    [SerializeField] private GameObject SelectDifficulty_Panel;
    [SerializeField] private GameObject Warning_Panel;
    public GameObject resultPanel;

    [Header("타이머")]
    public GameObject TimerObj;
    [SerializeField] private TMP_Text time_Text;
    public Slider time_Slider;
    public Difficulty difficult;

    [Header("아이콘 이미지")]
    [SerializeField] private Image Mold_Image;

    [Header("뒤로가기 버튼")]
    [SerializeField] private Button Back_Btn;

    [Header("Result")]
    [SerializeField] private Image resultImage;
    [SerializeField] private TMP_Text resultTimer;
    [SerializeField] private TMP_Text resultLog;
    [SerializeField] private TMP_Text resultTitle;

    [Header("Ready")]
    public GameObject readyPanel;
    public TMP_Text readyText;


    public int currentTime;
    private int sec;
    private int min;
    public Coroutine timer = null;
    public bool bNoTimePlaying = false;

    #region Unity Callback
    private void OnEnable()
    {
        Init();
    }
    #endregion


    #region Other Method
    public void TimerStart()
    {
        timer = StartCoroutine(Timer_co());
    }

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

        if (Warning_Panel.activeSelf)
        {
            Warning_Panel.SetActive(false);
        }

        // Back Method
        if (PushPop.Instance.PushPopBoardObject.Count > 0)
        {
            Destroy(PushPop.Instance.PushPopBoardObject[0]);
            PushPop.Instance.PushPopBoardObject.Clear();
        }

        if (GameManager.Instance.bubbleObject.Count > 0)
        {
            Destroy(GameManager.Instance.bubbleObject[0]);
            GameManager.Instance.bubbleObject.Clear();
        }

        // PushPop.Instance.PushPopClear();
    }

    //타이머 코루틴
    private IEnumerator Timer_co()
    {
        yield return new WaitForSeconds(2f);
        // game ready
        int cashing = 1;

        while (true)
        {
            currentTime += cashing;
            SetText();

            if (currentTime.Equals((int)difficult-10))
            {
                time_Text.color = TimerCountColorChange("#FF0000");
                if(!bNoTimePlaying)
                {
                    bNoTimePlaying = true;
                    AudioManager.Instance.SetAudioClip_SFX(1, true);
                }
            }
            if (currentTime.Equals((int)difficult))
            {
                // GameManager.Instance.GameClear();
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
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        if(bNoTimePlaying)
        {
            AudioManager.Instance.Pause_SFX(true);
        }

        Time.timeScale = 0;
        Warning_Panel.SetActive(true);
    }

    public void GoOutBtn_Clicked()
    {
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        AudioManager.Instance.SetAudioClip_BGM(1);

        Time.timeScale = 1;
        AudioManager.Instance.Stop_SFX();

        // Back Method
        if (PushPop.Instance.PushPopBoardUIObject.Count > 0)
        {
            Destroy(PushPop.Instance.PushPopBoardUIObject[0]);
            PushPop.Instance.PushPopBoardUIObject.Clear();
        }

        if (PushPop.Instance.PushPopBoardObject.Count > 0)
        {
            Destroy(PushPop.Instance.PushPopBoardObject[0]);
            PushPop.Instance.PushPopBoardObject.Clear();
        }

        if (GameManager.Instance.bubbleObject.Count > 0)
        {
            Destroy(GameManager.Instance.bubbleObject[0]);
            GameManager.Instance.bubbleObject.Clear();
        }

        // PushPop.Instance.PushPopClear();
        if(timer != null)
        {
            StopCoroutine(timer);
        }

        help_Canvas.gameObject.SetActive(true);
        SelectDifficulty_Panel.SetActive(true);
        // speed_Canvas.Enable_Objects();
        Warning_Panel.SetActive(false);
        resultPanel.SetActive(false);
        gameObject.SetActive(false);
        TimerObj.SetActive(false);
    }

    public void CancelBtn_Clicked()
    {
        Time.timeScale = 1;

        if(bNoTimePlaying)
        {
            AudioManager.Instance.Pause_SFX(false);
        }

        Warning_Panel.SetActive(false);
    }

    public void Result()
    {
        int clearTitle;
        resultImage.sprite = speed_Canvas.BoardIcon;
        resultTimer.text = $"{string.Format("{0:00}", min)}:{string.Format("{0:00}", sec)}";
        bNoTimePlaying = false;
        AudioManager.Instance.Stop_SFX();

        if (currentTime.Equals((int)difficult))
        {
            clearTitle = (int)ClearTitle.Fail;
            AudioManager.Instance.SetCommonAudioClip_SFX(8);
        }
        else
        {
            // clearTitle = (int)Ranking.Instance.CompareRanking();
            AudioManager.Instance.SetCommonAudioClip_SFX(7);
        }

        //resultTitle.text = $"{Ranking.Instance.ResultDialog.title[clearTitle]}";
        //resultLog.text = $"{Ranking.Instance.ResultDialog.speedResult[clearTitle]}";
    }
    #endregion
}
