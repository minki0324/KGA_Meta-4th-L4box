using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//올라가는 버블
public class Loading_Bubble : MonoBehaviour
{  
    RectTransform rectTransform;

    MoveMode moveMode;
 
    Vector2 randomPos;  //리스폰될 랜덤위치
    int randomSize; //리스폰될 때 정해질 랜덤사이즈 x,y 값

    float upSpeed;  //비눗방울 올라가는 속도
    float moveRange;    //좌우로 움직이는 범위
    float sizeRandom;   //커졋다 작아졋다 크기 범위

    bool bisIncrease = false;   //false면 작아지는 중, true면 커지는 중

    float screenHeight;
    float screenWidth;

    bool bisStart = true;   //게임 시작하자마자인지 판단

    private int bubbleSizeMin;  //생성될 때 버블 최소크기 -> 나중에 하고싶으면 코루틴에서도 최소/최대크기 제한할 것.
    private int bubbleSizeMax;  //생성될 떄 버블 최대크기


    #region Unity Callback


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        screenHeight = Camera.main.pixelHeight;
        screenWidth = Camera.main.pixelWidth;

        moveMode = MoveMode.Main;
    }


    private void OnEnable()
    {
        switch(moveMode)
        {
            case MoveMode.Main:
                bubbleSizeMin = 270;
                bubbleSizeMax = 300;

                if (!bisStart)
                {
                    randomPos = new Vector2(Random.Range(0, screenWidth), 0);
                    rectTransform.position = randomPos;
                    StartCoroutine(Main_MoveUp_co());
                }
                else
                {//처음 게임 시작할때 그 위치에서부터 코루틴 시작
                    StartCoroutine(Loading_MoveUp_co());
                    bisStart = false;
                }
                break;

            case MoveMode.Loading:
                randomPos = new Vector2(Random.Range(0, screenWidth), 0);
                bubbleSizeMin = 100;
                bubbleSizeMax = 300;
                break;

        }

        randomSize = Random.Range(bubbleSizeMin, bubbleSizeMax);
        rectTransform.sizeDelta = new Vector2(randomSize, randomSize);

    }
    #endregion

    #region Other Method
    private IEnumerator Main_MoveUp_co()
    {
        float moveTime = 0f;
        float sizeTime = 0f;

        while (true)
        {


            if (moveTime >= Random.Range(1, 3))
            {
                upSpeed = Random.Range(2f, 5f);
                moveRange = Random.Range(-3f, 3f);
                moveTime = 0f;
            }

            if (sizeTime >= Random.Range(0.5f, 3f))
            {
                sizeRandom =+ Random.Range(0.5f, 1f);
                if (bisIncrease) bisIncrease = false;      
                else bisIncrease = true;

                sizeTime = 0f;
            }

            if (bisIncrease)
            {//커지는 중이였으면 작아집시다             
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x - sizeRandom, rectTransform.sizeDelta.y - sizeRandom);      
            }
            else
            {//작아지는 중이였으면 커집시다     
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x + sizeRandom, rectTransform.sizeDelta.y + sizeRandom);            
            }


            //위로 올라감
            rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y + upSpeed, rectTransform.position.z);
            //좌우로 방향 움직임
            rectTransform.position = new Vector3(rectTransform.position.x + moveRange, rectTransform.position.y, rectTransform.position.z);


            //화면 벗어나면 끄기
            if(rectTransform.position.x - (rectTransform.sizeDelta.x * 0.5) >= screenWidth || rectTransform.position.x + (rectTransform.sizeDelta.x * 0.5) <= 0)
            {
                gameObject.SetActive(false);
            }

            if(rectTransform.position.y - (rectTransform.sizeDelta.y *0.5) >= screenHeight)
            {
                gameObject.SetActive(false);
            }


            moveTime += Time.deltaTime;
            sizeTime += Time.deltaTime;
            yield return null;

        }
    }
    private IEnumerator Loading_MoveUp_co()
    {
        while (true)
        {

        }
    }


    #endregion
}
