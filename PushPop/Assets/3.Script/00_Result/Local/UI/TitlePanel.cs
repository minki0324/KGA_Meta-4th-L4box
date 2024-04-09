using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum MoveMode
{
    Main = 0, 
    Loading
}

public class TitlePanel : MonoBehaviour
{
    [Header("캔버스")]
    [SerializeField] private Canvas LoadingCanvas;  //로딩 캔버스(오브젝트명 : LoadingCanvas) 오브젝트
    [SerializeField] private Canvas ParticleCanvas; //터치이펙트(오브젝트명 : ParticleCanvas) 캔버스 오브젝트

    [Header("비눗방울 오브젝트 관련")]
    [SerializeField] private GameObject Bubbles;       //부모 오브젝트
    [SerializeField] private GameObject bubblePrefab;   //버블 프리팹
    [SerializeField] private Loading_Bubble[] bubble_Array; //오브젝트 풀링용

    [Header("비눗방울 최대 생성 갯수")]
    public int maxBubble = 5;       //생성할 비눗방울의 갯수

    [Header("비눗방울 속도")]
    public int upSpeed_Min = 2;     //비눗방울 올라가는 속도 : 최소 속도
    public int upSpeed_Max = 5;     //비눗방울 올라가는 속도 : 최대 속도


    [Header("비눗방울 좌우 속도")]
    public float moveRange_Min;     //비눗방울 좌우 이동 속도 : 최소 속도
    public float moveRange_Max;     //비눗방울 좌우 이동 속도 : 최대 속도

    [Header("비눗방울 커지고 작아지는 정도")]
    public float sizeRandom_Min;   //비눗방울 크기 조절 : 최소 크기 (width, height 값)
    public float sizeRandom_Max;   ////비눗방울 크기 조절 : 최대 크기 (width, height 값)

    [Header("ETC")]
    [SerializeField] Animator TitleTextAnimator;
    [SerializeField] private Button StartBtn;   //타이틀 패널의 버튼
    int screenHeight;     //화면 세로 길이
    int screenWidth;      //화면 가로 길이

    private void OnEnable()
    {
        StartCoroutine(TitleActive());
    }

    private void Start()
    {
        Init();
        StartCoroutine(Init_co());
        AudioManager.Instance.SetAudioClip_BGM(0);
    }


    private void Update()
    { 
        //비눗방울 계속 소환하는 코드
        for (int i = 0; i < bubble_Array.Length; i++)
        {
            if (!bubble_Array[i].gameObject.activeSelf)
            {
                bubble_Array[i].gameObject.SetActive(true);
            }
        }
    }

    #region Other Method
    private IEnumerator TitleActive()
    {//렉/버그 방지용 텀두는 코루틴
        yield return null;
        if (GameManager.Instance.GameMode.Equals(GameMode.Lobby))
        {
            gameObject.SetActive(false);
        }
    }
    private void Init()
    {//초기화 메소드

        //화면 크기(픽셀단위)초기화
        screenHeight = Camera.main.pixelHeight;
        screenWidth = Camera.main.pixelWidth;

        bubble_Array = new Loading_Bubble[maxBubble];

        for (int i = 0; i < maxBubble; i++)
        {
            int rangeX = Random.Range(100, screenWidth - 100);
            int rangeY = Random.Range(100, screenHeight - 100);
            GameObject bub = Instantiate(bubblePrefab, new Vector3(rangeX, rangeY), Quaternion.identity);
            bub.transform.SetParent(Bubbles.transform);

            bubble_Array[i] = bub.GetComponent<Loading_Bubble>();
            bubble_Array[i].MoveMode = MoveMode.Main;

            bubble_Array[i].UpSpeedMin = upSpeed_Min;
            bubble_Array[i].UpSpeedMax = upSpeed_Max;
            bubble_Array[i].UpSpeed = Random.Range(upSpeed_Min, upSpeed_Max);

            bubble_Array[i].MoveRangeMin = moveRange_Min;
            bubble_Array[i].MoveRangeMax = moveRange_Max;

            bubble_Array[i].SizeRandomMin = sizeRandom_Min;
            bubble_Array[i].SizeRandomMax = sizeRandom_Max;

            //bubble_Array[i].gameObject.SetActive(false);
            bubble_Array[i].gameObject.SetActive(true);
        }
        ParticleCanvas.gameObject.SetActive(true);
    }



    private IEnumerator Init_co()
    { //타이틀 텍스트 애니메이션 코루틴
        yield return new WaitForSeconds(0.4f);
        TitleTextAnimator.speed = 1;

        yield return new WaitForSeconds(1.3f);
        TitleTextAnimator.SetTrigger("PopPop");

        //yield return new WaitForSeconds(0.2f);
        //StartBtn.interactable = true;
    }

    public void StartGame()
    {//타이틀 화면 터치 시 호출될 함수
        AudioManager.Instance.SetCommonAudioClip_SFX(2);

        //LoadingCanvas.gameObject.SetActive(false);
        LoadingPanel.Instance.gameObject.SetActive(true);

        for(int i = 0; i<maxBubble; i++)
        {
            Destroy(bubble_Array[i].gameObject);
        }

        gameObject.SetActive(false);
    }

    public void GameQuitButton()
    { // 게임 종료
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    #endregion
}
