using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//켜놓고 시작하기
public class LoadingPanel : MonoBehaviour
{
    [Header("비동기 로딩 스크립트")]
    [SerializeField] private AsyncLoading asyncLoading;

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

    public bool bisSceneLoading = false;    //비동기 씬로딩중인가 아닌가 판별용 변수 (MainGame에선 false, AsyncLoading에선 true)

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        Loading();
    }

    private void OnDisable()
    {
        BubbleSetting();
    }

    private void Update()
    {
        CheckBubbleEnd();
    }
    
    private void Init()
    {
        bubble_Array = new Loading_Bubble[maxBubble];

        for (int i = 0; i < maxBubble; i++)
        {
            //GameObject bub = Instantiate(bubblePrefab, new Vector3(Random.Range(0, Camera.main.pixelWidth - 100), Random.Range(-400f, -150f), 0f), Quaternion.identity);
            GameObject bub = Instantiate(bubblePrefab, Vector3.zero, Quaternion.identity);
            bub.transform.SetParent(Bubbles.transform);
            bubble_Array[i] = bub.GetComponent<Loading_Bubble>();
            bubble_Array[i].gameObject.SetActive(false);
        }
    }

    private void Loading()
    {
        bisLoaded = false;
        FadeBackground.material.SetFloat("_Horizontal", 1f);
        FadeBackground.material.SetFloat("_Visibility", 0.001f);
        if(!SceneManager.GetActiveScene().Equals("01_Network Minki"))
        {
            bisStart = false;       
        }
        else if(SceneManager.GetActiveScene().Equals("02_Async_Loading"))
        {
            asyncLoading = GetComponent<AsyncLoading>();
            bisSceneLoading = true;
        }
        else
        {
            ParticleCanvas.gameObject.SetActive(false);
        }

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
            if(!bisSceneLoading)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void BubbleSetting()
    {
        StopAllCoroutines();
        for (int i = 0; i < maxBubble; i++)
        {
            bubble_Array[i].gameObject.SetActive(false);
        }

        if(!bisSceneLoading)
        {
            if (!ParticleCanvas.gameObject.activeSelf)
            {
                ParticleCanvas.gameObject.SetActive(true);
            }
        }
    }

    private void BubblePooling()
    {//버블 계속 생산하는 코드

        if (bisSceneLoading)
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
        if (SceneManager.GetActiveScene().Equals("02_Async_Loading"))
        {
            bisSceneLoading = asyncLoading.bisLoading;
        }

        if (bisSceneLoading)
        {
            BubblePooling();
        }
        
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
            if(!bisSceneLoading)
            {
                ParticleCanvas.gameObject.SetActive(true);
            }
           
            gameObject.SetActive(false);

            // shutdown loading 끝난 뒤
            if (GameManager.Instance.OnShutdownAlarm)
            {
                GameManager.Instance.ShutdownAlarm?.Invoke();
            }
            GameManager.Instance.IsLoading = false;
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
}
