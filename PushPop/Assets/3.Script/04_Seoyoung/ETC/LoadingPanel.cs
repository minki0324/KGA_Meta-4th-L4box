using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�ѳ��� �����ϱ�
public class LoadingPanel : MonoBehaviour
{
    [Header("��ġ ����Ʈ ĵ����")]
    [SerializeField] private Canvas ParticleCanvas;

    [Header("�񴰹�� ������Ʈ ����")]
    [SerializeField] private GameObject Bubbles;    //������ ��ӽ�ų �θ� ������Ʈ
    [SerializeField] private GameObject bubblePrefab;   //�񴰹�� �̹��� ������
    [SerializeField] private Loading_Bubble[] bubble_Array;     //������Ʈ Ǯ

    [Header("Fade Background")]
    [SerializeField] private Image FadeBackground;

    [Header("�񴰹�� �ִ� ���� ����")]
    public int maxBubble = 150;     //�ִ� ���� ��

    [Header("�񴰹�� �ö󰡴� �ӵ�")]
    public int upSpeed_Min = 10;
    public int upSpeed_Max = 25;

    [Header("�񴰹�� �¿� �ӵ�")]
    public float moveRange_Min;
    public float moveRange_Max;

    [Header("�񴰹�� Ŀ���� �۾����� ����")]
    public float sizeRandom_Min;
    public float sizeRandom_Max;

    [Header("ETC")]
    public bool bisLoaded = false;  //�ε��� �� Fade Background �� �ö��� �� true -> �񴰹�� ���� ���̻� �ȵǵ��� ��
    public bool isLoadingEnd = false;   //�ε��� �����°�
    #region Unity Callback
    private void Awake()
    {
        Init();
       //gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        
        bisLoaded = false;
        FadeBackground.material.SetFloat("_Visibility", 0.1f);
        ParticleCanvas.gameObject.SetActive(false);

        for (int i = 0; i < maxBubble; i++)
        {
            bubble_Array[i].moveMode = MoveMode.Loading;
            bubble_Array[i].transform.position = new Vector3(Random.Range(0, Camera.main.pixelWidth - 100), Random.Range(-850f, -150f), 0f);

            bubble_Array[i].upSpeedMin = upSpeed_Min;
            bubble_Array[i].upSpeedMax = upSpeed_Max;

            bubble_Array[i].moveRangeMin = moveRange_Min;
            bubble_Array[i].moveRangeMax = moveRange_Max;

            bubble_Array[i].sizeRandomMin = sizeRandom_Min;
            bubble_Array[i].sizeRandomMax = sizeRandom_Max;

            bubble_Array[i].gameObject.SetActive(true);
        }


        StartCoroutine(BackgroundFadeOut_co());

    }

    private void OnDisable()
    {
        for (int i = 0; i < maxBubble; i++)
        {
            bubble_Array[i].gameObject.SetActive(false);
        }
        if(!ParticleCanvas.gameObject.activeSelf)
        {
            ParticleCanvas.gameObject.SetActive(true);
        }
       

    }

    private void Update()
    {
        if (!bisLoaded)
        {
            for (int i = 0; i < maxBubble; i++)
            {
                if (!bubble_Array[i].gameObject.activeSelf)
                {
                    bubble_Array[i].gameObject.SetActive(true);
                    bubble_Array[i].transform.position = new Vector3(Random.Range(0, Camera.main.pixelWidth - 100), -100f, 0f);
                }
            }
        }
        else
        {
            CheckBubbleEnd();
        }
    }
    #endregion

    #region Other Method

    private void Init()
    {
        bubble_Array = new Loading_Bubble[maxBubble];

        for (int i = 0; i < maxBubble; i++)
        {
            //GameObject bub = Instantiate(bubblePrefab, new Vector3(Random.Range(0, Camera.main.pixelWidth - 100), Random.Range(-400f, -150f), 0f), Quaternion.identity);
            GameObject bub = Instantiate(bubblePrefab, Vector3.zero, Quaternion.identity);
            bub.transform.parent = Bubbles.transform;
            bubble_Array[i] = bub.GetComponent<Loading_Bubble>();
            bubble_Array[i].gameObject.SetActive(false);
        }

        
    }

    private void CheckBubbleEnd()
    {
        isLoadingEnd = true;

        for (int i = 0; i < maxBubble; i++)
        {
            if (bubble_Array[i].gameObject.activeSelf)
            {
                //������ �ϳ��� ���������� ���� �ε�ȭ�� �ȳ���
                isLoadingEnd = false;
            }
        }

        if (isLoadingEnd)
        {
            isLoadingEnd = false;
            ParticleCanvas.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private IEnumerator BackgroundFadeOut_co()
    {
        float visibility = 0.1f;
        FadeBackground.material.SetFloat("_Visibility", visibility);
        yield return new WaitForSeconds(0.5f);
       
        float cashing = 0.1f;
        while(true)
        {

            if (visibility <= 0.35f)
            {
                visibility += 0.03f;
                FadeBackground.material.SetFloat("_Visibility", visibility);
                // yield return null;
                yield return new WaitForSeconds(cashing);
            }
            else if (visibility > 0.3f && visibility < 5f)
            {
                visibility += 0.1f;
                FadeBackground.material.SetFloat("_Visibility", visibility);
                yield return null;
            }
            else
            {
                FadeBackground.material.SetFloat("_Visibility", 7f);
                bisLoaded = true;
                yield break;
            }
        }
    }

    private IEnumerator BackgroundFade_co()
    {
        //�Ʒ������� �ö���� ���̴��� ����� ���ȭ�� Fade �ڷ�ƾ
        float visibility = 15f;

        while (true)
        {
            if (visibility <= 0)
            {
                bisLoaded = true;
                yield break;
            }

            FadeBackground.material.SetFloat("_Visibility", visibility);

            visibility -= 0.1f;
            //visibility -= Time.deltaTime;
            yield return null;
        }
    }
    #endregion
}
