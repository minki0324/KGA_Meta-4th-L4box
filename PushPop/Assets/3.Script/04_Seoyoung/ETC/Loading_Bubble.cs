using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//�ö󰡴� ����
public class Loading_Bubble : MonoBehaviour
{  
    RectTransform rectTransform;

    MoveMode moveMode;
 
    Vector2 randomPos;  //�������� ������ġ
    int randomSize; //�������� �� ������ ���������� x,y ��

    float upSpeed;  //�񴰹�� �ö󰡴� �ӵ�
    float moveRange;    //�¿�� �����̴� ����
    float sizeRandom;   //Ŀ���� �۾Ơ��� ũ�� ����

    bool bisIncrease = false;   //false�� �۾����� ��, true�� Ŀ���� ��

    float screenHeight;
    float screenWidth;

    bool bisStart = true;   //���� �������ڸ������� �Ǵ�

    private int bubbleSizeMin;  //������ �� ���� �ּ�ũ�� -> ���߿� �ϰ������ �ڷ�ƾ������ �ּ�/�ִ�ũ�� ������ ��.
    private int bubbleSizeMax;  //������ �� ���� �ִ�ũ��


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
                {//ó�� ���� �����Ҷ� �� ��ġ�������� �ڷ�ƾ ����
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
            {//Ŀ���� ���̿����� �۾����ô�             
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x - sizeRandom, rectTransform.sizeDelta.y - sizeRandom);      
            }
            else
            {//�۾����� ���̿����� Ŀ���ô�     
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x + sizeRandom, rectTransform.sizeDelta.y + sizeRandom);            
            }


            //���� �ö�
            rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y + upSpeed, rectTransform.position.z);
            //�¿�� ���� ������
            rectTransform.position = new Vector3(rectTransform.position.x + moveRange, rectTransform.position.y, rectTransform.position.z);


            //ȭ�� ����� ����
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
