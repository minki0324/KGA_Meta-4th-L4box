using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//켜놓고 시작하기
public class LoadingPanel : MonoBehaviour
{
    [Header("터치 이펙트 캔버스")]
    [SerializeField] private Canvas ParticleCanvas;

    [Header("비눗방울 오브젝트 관련")]
    [SerializeField] private GameObject Bubbles;    //프리팹 상속시킬 부모 오브젝트
    [SerializeField] private GameObject bubblePrefab;   //비눗방울 이미지 프리팹
    [SerializeField] private Loading_Bubble[] bubble_Array;     //오브젝트 풀

    [Header("Fade Background")]
    [SerializeField] private Image FadeBackground;

    [Header("비눗방울 최대 생성 갯수")]
    public int maxBubble = 100;     //최대 버블 수

    [Header("비눗방울 올라가는 속도")]
    public int upSpeed_Min = 15;
    public int upSpeed_Max = 26;

    [Header("비눗방울 좌우 속도")]
    public float moveRange_Min = -3f;
    public float moveRange_Max = 3f;

    [Header("비눗방울 커지고 작아지는 정도")]
    public float sizeRandom_Min = 0.1f;
    public float sizeRandom_Max = 0.3f;

    [Header("ETC")]
    private bool bisLoaded = false;  //로딩일 때 Fade Background 다 올라갔을 때 true -> 비눗방울 생성 더이상 안되도록 함
    private bool isLoadingEnd = false;   //로딩이 끝났는가
    private bool bisStart = true;

    #region Unity Callback
    private void Awake()
    {
        Init();
        //gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        
        bisLoaded = false;
        FadeBackground.material.SetFloat("_Horizontal", 1f);


        FadeBackground.material.SetFloat("_Visibility", 0.001f);
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

        if(!bisStart)
        {
            StartCoroutine(BackgroundFadeOut_co());
        }
        else
        {
            bisStart = false;
            gameObject.SetActive(false);
        }

        

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

        StopAllCoroutines();
    }

    private void Update()
    {
        // BubblePooling();

        CheckBubbleEnd();
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

    private void BubblePooling()
    {//버블 계속 생산하는 코드
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

    private void CheckBubbleEnd()
    {
        isLoadingEnd = true;

        for (int i = 0; i < maxBubble; i++)
        {
            if (bubble_Array[i].gameObject.activeSelf)
            {
                //버블이 하나라도 켜져있으면 아직 로딩화면 안끝남
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
        float visibility = 0.001f;
        FadeBackground.material.SetFloat("_Visibility", visibility);



        yield return new WaitForSeconds(0.5f);
       
        float cashing1 = 0.1f;
        float cashing2= 0.05f;
        while(true)
        {

            if (visibility <= 0.35f)
            {
                visibility += 0.05f;
                FadeBackground.material.SetFloat("_Visibility", visibility);
                // yield return null;
                yield return new WaitForSeconds(cashing1);
            }
            else if (visibility > 0.3f && visibility < 4f)
            {
                visibility += 0.15f;
                FadeBackground.material.SetFloat("_Visibility", visibility);
                yield return new WaitForSeconds(cashing2);
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
        //아래서부터 올라오는 쉐이더가 적용된 배경화면 Fade 코루틴
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
