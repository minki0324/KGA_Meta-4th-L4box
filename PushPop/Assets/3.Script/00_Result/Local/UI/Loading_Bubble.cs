using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�ö󰡴� ����
public class Loading_Bubble : MonoBehaviour
{
    private RectTransform rectTransform;

    [HideInInspector] public MoveMode MoveMode;

    [Header("�񴰹�� ��� �ӵ�")]
    [HideInInspector] public float UpSpeed = 3;  //�񴰹�� �ö󰡴� �ӵ�
    [HideInInspector] public int UpSpeedMin;        // �񴰹�� ��� �ӵ� �ּ�ġ
    [HideInInspector] public int UpSpeedMax;        //�񴰹�� ��� �ӵ� �ִ�ġ

    [Header("�񴰹�� �¿� ����")]
    [HideInInspector] public float MoveRangeMin;  // �񴰹�� �¿� ���� �ּ�ġ
    [HideInInspector] public float MoveRangeMax;  //�񴰹�� �¿� ���� �ִ�ġ
    private float moveRange;    //�¿�� �����̴� ����

    [Header("�񴰹�� ũ��")]
    [HideInInspector] public float SizeRandomMin;     // �񴰹�� ũ�� �ּ�ġ
    [HideInInspector] public float SizeRandomMax;     //�񴰹�� ũ�� �ִ�ġ
    private float sizeRandom;   // �񴰹�� ������ ����

    [Header("���� ���� �� ���� ��ġ")]
    private Vector2 randomPos;  //�������� ������ġ
    private int randomSize; //�������� �� ������ ���������� x,y ��
    private int bubbleSizeMin;  //������ �� ���� �ּ�ũ��
    private int bubbleSizeMax;  //������ �� ���� �ִ�ũ��

    private float screenHeight;     //ȭ�� ���� ����
    private float screenWidth;      //ȭ�� ���� ����

    private bool isIncrease = false;   //false�� �۾����� ��, true�� Ŀ���� ��
    private bool isStart = true;   //���� �������ڸ������� �Ǵ�

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
                {//ó�� ���� �����Ҷ� �� ��ġ�������� �ڷ�ƾ ����           
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
    {//�񴰹��� ��� �ڷ�ƾ

        float cashing = 0.01f;
        float moveTime = 0f;    //�����̴� ����, �ö󰡴¼ӵ� �ٲٴ� �ð�
        float sizeTime = 0f;    //Ŀ��/�۾��� �ٲٴ� �ð�
        UpSpeed = Random.Range(UpSpeedMin, UpSpeedMax);
        moveRange = Random.Range(MoveRangeMin, MoveRangeMax);

        while (true)
        {
            if (moveTime >= Random.Range(1f, 3f))
            {//�����̴� ����, �ö󰡴� �ӵ� ����
                UpSpeed = Random.Range(UpSpeedMin, UpSpeedMax);
                moveRange = Random.Range(MoveRangeMin, MoveRangeMax);
                moveTime = 0f;
            }

            if (sizeTime >= Random.Range(0.5f, 3f))
            {//Ŀ���� �۾����� �۾����� Ŀ���� ���� �� ������ ���� ��ġ�� �缳��
                sizeRandom = Random.Range(SizeRandomMin, SizeRandomMax);
                if (isIncrease) isIncrease = false;
                else isIncrease = true;
                sizeTime = 0f;
            }

            //���� �ö�
            rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y + UpSpeed, rectTransform.position.z);
            //�¿�� ���� ������
            rectTransform.position = new Vector3(rectTransform.position.x + moveRange, rectTransform.position.y, rectTransform.position.z);

            if (isIncrease)
            {//Ŀ���� ���̿����� �۾����ô�             
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x - sizeRandom, rectTransform.sizeDelta.y - sizeRandom);
                if (rectTransform.sizeDelta.x <= bubbleSizeMin)
                {   //�ּ�ũ�Ⱑ �Ǹ� Ŀ������ ����
                    isIncrease = false;
                    sizeTime = 0f;
                }
            }
            else
            {//�۾����� ���̿����� Ŀ���ô�     
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x + sizeRandom, rectTransform.sizeDelta.y + sizeRandom);
              
                if (rectTransform.sizeDelta.x >= bubbleSizeMax)
                {   //�ִ�ũ�Ⱑ �Ǹ� �۾������� ����
                    isIncrease = true;
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
