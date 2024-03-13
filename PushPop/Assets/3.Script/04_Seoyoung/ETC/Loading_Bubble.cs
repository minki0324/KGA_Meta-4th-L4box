using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//올라가는 버블
public class Loading_Bubble : MonoBehaviour
{
    RectTransform rectTransform;

    public MoveMode moveMode;

    Vector2 randomPos;  //리스폰될 랜덤위치
    int randomSize; //리스폰될 때 정해질 랜덤사이즈 x,y 값

    [SerializeField] private float upSpeed;  //비눗방울 올라가는 속도
    public int upSpeedMin;        //로딩일 때 10, 메인일 때 2
    public int upSpeedMax;        //로딩일 때 25, 메인일 때 5


    [SerializeField] private float moveRange;    //좌우로 움직이는 범위
    public float moveRangeMin;  //-3f
    public float moveRangeMax;  //3f

    
    [SerializeField] private float sizeRandom;   //커졋다 작아졋다 크기 범위
    public float sizeRandomMin;     //0.2f  
    public float sizeRandomMax;     //0.6f


    bool bisIncrease = false;   //false면 작아지는 중, true면 커지는 중

    float screenHeight;     //화면 세로 길이
    float screenWidth;      //화면 가로 길이

    bool bisStart = true;   //게임 시작하자마자인지 판단

    public int bubbleSizeMin;  //생성될 때 버블 최소크기
    public int bubbleSizeMax;  //생성될 떄 버블 최대크기


    #region Unity Callback


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        screenHeight = Camera.main.pixelHeight;
        screenWidth = Camera.main.pixelWidth;
    }


    private void OnEnable()
    {
        switch (moveMode)
        {
            case MoveMode.Main:
                bubbleSizeMin = 270;
                bubbleSizeMax = 300;
   
                if (!bisStart)
                {
                    randomPos = new Vector2(Random.Range(0, screenWidth), 0);
                    rectTransform.position = randomPos;
                }
                else
                {//처음 게임 시작할때 그 위치에서부터 코루틴 시작           
                    bisStart = false;
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
    #endregion

    #region Other Method

    private IEnumerator MoveUp_co()
    {//비눗방을 상승 코루틴

        float cashing = 0.005f;

        float moveTime = 0f;    //움직이는 방향, 올라가는속도 바꾸는 시간
        float sizeTime = 0f;    //커짐/작아짐 바꾸는 시간

        while (true)
        {
            if (moveTime >= Random.Range(1f, 3f))
            {//움직이는 방향, 올라가는 속도 변경
                upSpeed = Random.Range(upSpeedMin, upSpeedMax);
                moveRange = Random.Range(moveRangeMin, moveRangeMax);

                moveTime = 0f;
            }

            if (sizeTime >= Random.Range(0.5f, 3f))
            {//커지면 작아지게 작아지면 커지게 변경 및 사이즈 변경 수치값 재설정
                sizeRandom = Random.Range(sizeRandomMin, sizeRandomMax);
                if (bisIncrease) bisIncrease = false;
                else bisIncrease = true;

                sizeTime = 0f;
            }



            //위로 올라감
            rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y + upSpeed, rectTransform.position.z);
            //좌우로 방향 움직임
            rectTransform.position = new Vector3(rectTransform.position.x + moveRange, rectTransform.position.y, rectTransform.position.z);



            if (bisIncrease)
            {//커지는 중이였으면 작아집시다             
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x - sizeRandom, rectTransform.sizeDelta.y - sizeRandom);
                if (rectTransform.sizeDelta.x <= bubbleSizeMin)
                {   //최소크기가 되면 커지도록 변경
                    bisIncrease = false;
                    sizeTime = 0f;
                }
            }
            else
            {//작아지는 중이였으면 커집시다     
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x + sizeRandom, rectTransform.sizeDelta.y + sizeRandom);
              
                if (rectTransform.sizeDelta.x >= bubbleSizeMax)
                {   //최대크기가 되면 작아지도록 변경
                    bisIncrease = true;
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
