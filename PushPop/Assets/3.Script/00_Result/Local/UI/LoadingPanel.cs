using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//�ѳ��� �����ϱ�
public class LoadingPanel : MonoBehaviour
{
    public static LoadingPanel Instance;

    [Header("��ġ ����Ʈ ĵ����")]
    [SerializeField] private Canvas ParticleCanvas;

    [Header("�񴰹�� ������Ʈ ����")]
    [SerializeField] private GameObject Bubbles;    //������ ��ӽ�ų �θ� ������Ʈ
    [SerializeField] private GameObject bubblePrefab;   //�񴰹�� �̹��� ������
    [SerializeField] private Loading_Bubble[] bubble_Array;     //������Ʈ Ǯ

    [Header("Fade Background")]
    [SerializeField] private Image FadeBackground;

    [Header("�񴰹�� �ִ� ���� ����")]
    [SerializeField] private  int maxBubble = 100;     //�ִ� ���� ��

    [Header("�񴰹�� �ö󰡴� �ӵ�")]
    [SerializeField] private int upSpeed_Min = 40;
    [SerializeField] private int upSpeed_Max = 65;

    [Header("�񴰹�� �¿� �ӵ�")]
    public float moveRange_Min = -3f;
    public float moveRange_Max = 3f;

    [Header("�񴰹�� Ŀ���� �۾����� ����")]
    public float sizeRandom_Min = 0.1f;
    public float sizeRandom_Max = 0.3f;

    [Header("ETC")]
    private bool isLoadingEnd = false;   //�ε��� �����°�
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
                //������ �ϳ��� ���������� ���� �ε�ȭ�� �ȳ���
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

            // shutdown loading ���� ��
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
