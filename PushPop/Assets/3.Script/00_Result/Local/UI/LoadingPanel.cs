using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//켜놓고 시작하기
public class LoadingPanel : MonoBehaviour
{
    public static LoadingPanel Instance;

    [Header("터치 이펙트 캔버스")]
    [SerializeField] private Canvas ParticleCanvas;

    [Header("비눗방울 오브젝트 관련")]
    [SerializeField] private GameObject Bubbles;    //프리팹 상속시킬 부모 오브젝트
    [SerializeField] private GameObject bubblePrefab;   //비눗방울 이미지 프리팹
    [SerializeField] private Loading_Bubble[] bubble_Array;     //오브젝트 풀

    [Header("Fade Background")]
    [SerializeField] private Image FadeBackground;

    [Header("비눗방울 최대 생성 갯수")]
    [SerializeField] private  int maxBubble = 100;     //최대 버블 수

    [Header("비눗방울 올라가는 속도")]
    [SerializeField] private int upSpeed_Min = 40;
    [SerializeField] private int upSpeed_Max = 65;

    [Header("비눗방울 좌우 속도")]
    public float moveRange_Min = -3f;
    public float moveRange_Max = 3f;

    [Header("비눗방울 커지고 작아지는 정도")]
    public float sizeRandom_Min = 0.1f;
    public float sizeRandom_Max = 0.3f;

    [Header("ETC")]
    private bool isLoadingEnd = false;   //로딩이 끝났는가
    public bool bisStart = false;
   

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        Init();
    }

    private void OnEnable()
    {
        if (!bisStart)
        {
            gameObject.SetActive(false);
            bisStart = !bisStart;
            return;
        }
        Loading();
    }
    private void Start()
    {
        SceneManager.sceneLoaded += OnNetworkScene;
    }

    private void OnDisable()
    {
        BubbleSetting();
    }

    private void Update()
    {
        CheckBubbleEnd();
    }

    private void OnNetworkScene(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals("01_Network"))
        {
            gameObject.SetActive(true);
        }
        else if (scene.name.Equals("00_Maingame") && bisStart)
        {
            gameObject.SetActive(true);
        }
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
        FadeBackground.material.SetFloat("_Horizontal", 1f);
        FadeBackground.material.SetFloat("_Visibility", 0.001f);

        if (ParticleCanvas != null)
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
        StartCoroutine(BackgroundFadeOut_co());
    }

    public void BubbleSetting()
    {
        StopAllCoroutines();
        for (int i = 0; i < bubble_Array.Length; i++)
        {
            if(bubble_Array[i].gameObject.activeSelf)
            {
                bubble_Array[i].gameObject.SetActive(false);
            }
        }

        if (ParticleCanvas != null)
        {
            if (!ParticleCanvas.gameObject.activeSelf)
            {
                ParticleCanvas.gameObject.SetActive(true);
            }
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
            if(ParticleCanvas != null)
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
        GameManager.Instance.IsLoading = true;

        float visibility = 0.05f;
        FadeBackground.material.SetFloat("_Visibility", visibility);

        yield return new WaitForSeconds(0.5f);

        float cashing1 = 0.1f;
        float cashing2 = 0.05f;
        while (true)
        {
            if (visibility <= 0.35f)
            {
                visibility += 0.1f;
                FadeBackground.material.SetFloat("_Visibility", visibility);
                // yield return null;
                yield return new WaitForSeconds(cashing1);
            }
            else if (visibility > 0.3f && visibility < 4f)
            {
                visibility += 0.25f;
                FadeBackground.material.SetFloat("_Visibility", visibility);
                yield return new WaitForSeconds(cashing2);
            }
            else
            {
                FadeBackground.material.SetFloat("_Visibility", 7f);
                yield break;
            }
        }
    }
}
