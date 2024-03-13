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
    [SerializeField] private Canvas LoadingCanvas;
    [SerializeField] private Canvas ParticleCanvas;

    [Header("비눗방울 오브젝트 관련")]
    [SerializeField] private GameObject Bubbles;       //부모 오브젝트
    [SerializeField] private GameObject bubblePrefab;   //버블 프리팹
    [SerializeField] private Loading_Bubble[] bubble_Array; //오브젝트 풀링용

    [Header("비눗방울 최대 생성 갯수")]
    public int maxBubble = 5;

    [Header("비눗방울 속도")]
    public int upSpeed_Min = 2;
    public int upSpeed_Max = 5;


    [Header("비눗방울 좌우 속도")]
    public float moveRange_Min;
    public float moveRange_Max;

    [Header("비눗방울 커지고 작아지는 정도")]
    public float sizeRandom_Min;
    public float sizeRandom_Max;

    [Header("ETC")]
    [SerializeField] private Button StartBtn;   //타이틀 패널의 버튼
    int screenHeight;     //화면 세로 길이
    int screenWidth;      //화면 가로 길이

    #region Unity Callback

    private void Awake()
    {
        Init();
        ParticleCanvas.gameObject.SetActive(true);
        StartBtn.interactable = false;
        StartCoroutine(Init_co());
    }



    private void Update()
    {
        for (int i = 0; i < bubble_Array.Length; i++)
        {
            if (!bubble_Array[i].gameObject.activeSelf)
            {
                bubble_Array[i].gameObject.SetActive(true);
            }
        }
    }

    #endregion

    #region Other Method

    private void Init()
    {
        //화면 크기(픽셀단위)초기화
        screenHeight = Camera.main.pixelHeight;
        screenWidth = Camera.main.pixelWidth;

        bubble_Array = new Loading_Bubble[maxBubble];

        for (int i = 0; i < maxBubble; i++)
        {
            int rangeX = Random.Range(100, screenWidth - 100);
            int rangeY = Random.Range(100, screenHeight - 100);
            GameObject bub = Instantiate(bubblePrefab, new Vector3(rangeX, rangeY), Quaternion.identity);
            bub.transform.parent = Bubbles.transform;

            bubble_Array[i] = bub.GetComponent<Loading_Bubble>();
            bubble_Array[i].moveMode = MoveMode.Main;

            bubble_Array[i].upSpeedMin = upSpeed_Min;
            bubble_Array[i].upSpeedMax = upSpeed_Max;

            bubble_Array[i].moveRangeMin = moveRange_Min;
            bubble_Array[i].moveRangeMax = moveRange_Max;

            bubble_Array[i].sizeRandomMin = sizeRandom_Min;
            bubble_Array[i].sizeRandomMax = sizeRandom_Max;
            bubble_Array[i].gameObject.SetActive(true);
        }
           

    }
    


    private IEnumerator Init_co()
    {
       
        yield return new WaitForSeconds(1.5f);
        StartBtn.interactable = true;
    }

    public void StartGame()
    {
        LoadingCanvas.gameObject.SetActive(false);
        LoadingCanvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

#endregion
}
