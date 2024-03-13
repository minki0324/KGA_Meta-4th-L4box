using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//�ö󰡴� ����
public class Loading_Bubble : MonoBehaviour
{
    RectTransform rectTransform;

    public MoveMode moveMode;

    Vector2 randomPos;  //�������� ������ġ
    int randomSize; //�������� �� ������ ���������� x,y ��

    [SerializeField] private float upSpeed;  //�񴰹�� �ö󰡴� �ӵ�
    public int upSpeedMin;        //�ε��� �� 10, ������ �� 2
    public int upSpeedMax;        //�ε��� �� 25, ������ �� 5


    [SerializeField] private float moveRange;    //�¿�� �����̴� ����
    public float moveRangeMin;  //-3f
    public float moveRangeMax;  //3f

    
    [SerializeField] private float sizeRandom;   //Ŀ���� �۾Ơ��� ũ�� ����
    public float sizeRandomMin;     //0.2f  
    public float sizeRandomMax;     //0.6f


    bool bisIncrease = false;   //false�� �۾����� ��, true�� Ŀ���� ��

    float screenHeight;     //ȭ�� ���� ����
    float screenWidth;      //ȭ�� ���� ����

    bool bisStart = true;   //���� �������ڸ������� �Ǵ�

    public int bubbleSizeMin;  //������ �� ���� �ּ�ũ��
    public int bubbleSizeMax;  //������ �� ���� �ִ�ũ��


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
                {//ó�� ���� �����Ҷ� �� ��ġ�������� �ڷ�ƾ ����           
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
    {//�񴰹��� ��� �ڷ�ƾ

        float cashing = 0.005f;

        float moveTime = 0f;    //�����̴� ����, �ö󰡴¼ӵ� �ٲٴ� �ð�
        float sizeTime = 0f;    //Ŀ��/�۾��� �ٲٴ� �ð�

        while (true)
        {
            if (moveTime >= Random.Range(1f, 3f))
            {//�����̴� ����, �ö󰡴� �ӵ� ����
                upSpeed = Random.Range(upSpeedMin, upSpeedMax);
                moveRange = Random.Range(moveRangeMin, moveRangeMax);

                moveTime = 0f;
            }

            if (sizeTime >= Random.Range(0.5f, 3f))
            {//Ŀ���� �۾����� �۾����� Ŀ���� ���� �� ������ ���� ��ġ�� �缳��
                sizeRandom = Random.Range(sizeRandomMin, sizeRandomMax);
                if (bisIncrease) bisIncrease = false;
                else bisIncrease = true;

                sizeTime = 0f;
            }



            //���� �ö�
            rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y + upSpeed, rectTransform.position.z);
            //�¿�� ���� ������
            rectTransform.position = new Vector3(rectTransform.position.x + moveRange, rectTransform.position.y, rectTransform.position.z);



            if (bisIncrease)
            {//Ŀ���� ���̿����� �۾����ô�             
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x - sizeRandom, rectTransform.sizeDelta.y - sizeRandom);
                if (rectTransform.sizeDelta.x <= bubbleSizeMin)
                {   //�ּ�ũ�Ⱑ �Ǹ� Ŀ������ ����
                    bisIncrease = false;
                    sizeTime = 0f;
                }
            }
            else
            {//�۾����� ���̿����� Ŀ���ô�     
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x + sizeRandom, rectTransform.sizeDelta.y + sizeRandom);
              
                if (rectTransform.sizeDelta.x >= bubbleSizeMax)
                {   //�ִ�ũ�Ⱑ �Ǹ� �۾������� ����
                    bisIncrease = true;
                    sizeTime = 0f;
                }
            }

            //ȭ�� ����� ���ӿ�����Ʈ ����(�Ϻ��� ��� ��� ����)
            if (rectTransform.position.x - (rectTransform.sizeDelta.x * 0.5) >= screenWidth || rectTransform.position.x + (rectTransform.sizeDelta.x * 0.5) <= 0)
            {//����ȭ�� ����� ����
                gameObject.SetActive(false);
            }
            if (rectTransform.position.y - (rectTransform.sizeDelta.y * 0.5) >= screenHeight)
            {//����ȭ�� ����� ����
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
