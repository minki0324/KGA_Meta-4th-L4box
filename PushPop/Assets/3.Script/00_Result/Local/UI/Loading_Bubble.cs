using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//올라가는 버블
public class Loading_Bubble : MonoBehaviour
{
    private RectTransform rectTransform;

    [HideInInspector] public MoveMode MoveMode;

    [Header("비눗방울 상승 속도")]
    [HideInInspector] public float UpSpeed = 3;  //비눗방울 올라가는 속도
    [HideInInspector] public int UpSpeedMin;        // 비눗방울 상승 속도 최소치
    [HideInInspector] public int UpSpeedMax;        //비눗방울 상승 속도 최대치

    [Header("비눗방울 좌우 범위")]
    [HideInInspector] public float MoveRangeMin;  // 비눗방울 좌우 범위 최소치
    [HideInInspector] public float MoveRangeMax;  //비눗방울 좌우 범위 최대치
    private float moveRange;    //좌우로 움직이는 범위

    [Header("비눗방울 크기")]
    [HideInInspector] public float SizeRandomMin;     // 비눗방울 크기 최소치
    [HideInInspector] public float SizeRandomMax;     //비눗방울 크기 최대치
    private float sizeRandom;   // 비눗방울 사이즈 범위

    [Header("생성 됐을 때 랜덤 수치")]
    private Vector2 randomPos;  //리스폰될 랜덤위치
    private int randomSize; //리스폰될 때 정해질 랜덤사이즈 x,y 값
    private int bubbleSizeMin;  //생성될 때 버블 최소크기
    private int bubbleSizeMax;  //생성될 떄 버블 최대크기

    private float screenHeight;     //화면 세로 길이
    private float screenWidth;      //화면 가로 길이

    private bool isIncrease = false;   //false면 작아지는 중, true면 커지는 중
    private bool isStart = true;   //게임 시작하자마자인지 판단

    #region Unity Callback
    private void Awake()
    {
        UpSpeed = 3;
        rectTransform = GetComponent<RectTransform>();
        screenHeight = Camera.main.pixelHeight;
        screenWidth = Camera.main.pixelWidth;
    }

    private void OnEnable()
    {
        switch (MoveMode)
        {
            case MoveMode.Main:
                bubbleSizeMin = 270;
                bubbleSizeMax = 300;
   
                if (!isStart)
                {
                    randomPos = new Vector2(Random.Range(0, screenWidth), 0);
                    rectTransform.position = randomPos;
                }
                else
                {//처음 게임 시작할때 그 위치에서부터 코루틴 시작           
                    isStart = false;
                }
                break;
            case MoveMode.Loading:
                bubbleSizeMin = 100;
                bubbleSizeMax = 400;
                break;
        }

        randomSize = Random.Range(bubbleSizeMin, bubbleSizeMax);
        rectTransform.sizeDelta = new Vector2(randomSize, randomSize);

        StartCoroutine(MoveUp_co());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    #endregion

    #region Other Method
    private IEnumerator MoveUp_co()
    {//비눗방을 상승 코루틴

        float cashing = 0.01f;
        float moveTime = 0f;    //움직이는 방향, 올라가는속도 바꾸는 시간
        float sizeTime = 0f;    //커짐/작아짐 바꾸는 시간
        UpSpeed = Random.Range(UpSpeedMin, UpSpeedMax);
        moveRange = Random.Range(MoveRangeMin, MoveRangeMax);

        while (true)
        {
            if (moveTime >= Random.Range(1f, 3f))
            {//움직이는 방향, 올라가는 속도 변경
                UpSpeed = Random.Range(UpSpeedMin, UpSpeedMax);
                moveRange = Random.Range(MoveRangeMin, MoveRangeMax);
                moveTime = 0f;
            }

            if (sizeTime >= Random.Range(0.5f, 3f))
            {//커지면 작아지게 작아지면 커지게 변경 및 사이즈 변경 수치값 재설정
                sizeRandom = Random.Range(SizeRandomMin, SizeRandomMax);
                if (isIncrease) isIncrease = false;
                else isIncrease = true;
                sizeTime = 0f;
            }

            //위로 올라감
            rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y + UpSpeed, rectTransform.position.z);
            //좌우로 방향 움직임
            rectTransform.position = new Vector3(rectTransform.position.x + moveRange, rectTransform.position.y, rectTransform.position.z);

            if (isIncrease)
            {//커지는 중이였으면 작아집시다             
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x - sizeRandom, rectTransform.sizeDelta.y - sizeRandom);
                if (rectTransform.sizeDelta.x <= bubbleSizeMin)
                {   //최소크기가 되면 커지도록 변경
                    isIncrease = false;
                    sizeTime = 0f;
                }
            }
            else
            {//작아지는 중이였으면 커집시다     
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x + sizeRandom, rectTransform.sizeDelta.y + sizeRandom);
              
                if (rectTransform.sizeDelta.x >= bubbleSizeMax)
                {   //최대크기가 되면 작아지도록 변경
                    isIncrease = true;
                    sizeTime = 0f;
                }
            }

            //화면 벗어나면 게임오브젝트 끄기(완벽히 벗어난 경우 꺼짐)
            if (rectTransform.position.x - (rectTransform.sizeDelta.x * 0.5) >= screenWidth || rectTransform.position.x + (rectTransform.sizeDelta.x * 0.5) <= 0)
            {//가로화면 벗어나면 끄기
                gameObject.SetActive(false);
            }
            if (rectTransform.position.y - (rectTransform.sizeDelta.y * 0.5) >= screenHeight)
            {//세로화면 벗어나면 끄기
                gameObject.SetActive(false);
            }

            moveTime += Time.deltaTime;
            sizeTime += Time.deltaTime;

            yield return new WaitForSeconds(cashing);
            yield return null;
        }
    }
    #endregion
}
