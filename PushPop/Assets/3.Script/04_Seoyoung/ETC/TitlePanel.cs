using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum MoveMode
{
    Main = 0, 
    Loading
}

public class TitlePanel : MonoBehaviour
{
    [Header("ĵ����")]
    [SerializeField] private Canvas LoadingCanvas;
    [SerializeField] private Canvas ParticleCanvas;

    [Header("�񴰹�� ������Ʈ ����")]
    [SerializeField] private GameObject Bubbles;       //�θ� ������Ʈ
    [SerializeField] private GameObject bubblePrefab;   //���� ������
    [SerializeField] private Loading_Bubble[] bubble_Array; //������Ʈ Ǯ����

    [Header("�񴰹�� �ִ� ���� ����")]
    public int maxBubble = 5;

    [Header("�񴰹�� �ӵ�")]
    public int upSpeed_Min = 2;
    public int upSpeed_Max = 5;


    [Header("�񴰹�� �¿� �ӵ�")]
    public float moveRange_Min;
    public float moveRange_Max;

    [Header("�񴰹�� Ŀ���� �۾����� ����")]
    public float sizeRandom_Min;
    public float sizeRandom_Max;

    [Header("ETC")]
    [SerializeField] Animator TitleTextAnimator;
    [SerializeField] private Button StartBtn;   //Ÿ��Ʋ �г��� ��ư
    int screenHeight;     //ȭ�� ���� ����
    int screenWidth;      //ȭ�� ���� ����

    #region Unity Callback

    private void Awake()
    {
        Init();
        ParticleCanvas.gameObject.SetActive(true);
       // StartBtn.interactable = false;

        StartCoroutine(Init_co());
    }

    private void Start()
    {
        AudioManager.instance.SetAudioClip_BGM(0);
    }


    private void Update()
    {
        for (int i = 0; i < bubble_Array.Length; i++)
        {
            if (!bubble_Array[i].gameObject.activeSelf)
            {
                bubble_Array[i].gameObject.SetActive(true);
            }
        }
    }

    #endregion

    #region Other Method

    private void Init()
    {
        //ȭ�� ũ��(�ȼ�����)�ʱ�ȭ
        screenHeight = Camera.main.pixelHeight;
        screenWidth = Camera.main.pixelWidth;

        bubble_Array = new Loading_Bubble[maxBubble];

        for (int i = 0; i < maxBubble; i++)
        {
            int rangeX = Random.Range(100, screenWidth - 100);
            int rangeY = Random.Range(100, screenHeight - 100);
            GameObject bub = Instantiate(bubblePrefab, new Vector3(rangeX, rangeY), Quaternion.identity);
            bub.transform.parent = Bubbles.transform;

            bubble_Array[i] = bub.GetComponent<Loading_Bubble>();
            bubble_Array[i].moveMode = MoveMode.Main;

            bubble_Array[i].upSpeedMin = upSpeed_Min;
            bubble_Array[i].upSpeedMax = upSpeed_Max;
            bubble_Array[i].upSpeed = Random.Range(upSpeed_Min, upSpeed_Max);

            bubble_Array[i].moveRangeMin = moveRange_Min;
            bubble_Array[i].moveRangeMax = moveRange_Max;

            bubble_Array[i].sizeRandomMin = sizeRandom_Min;
            bubble_Array[i].sizeRandomMax = sizeRandom_Max;

            //bubble_Array[i].gameObject.SetActive(false);
            bubble_Array[i].gameObject.SetActive(true);
        }
    }
    


    private IEnumerator Init_co()
    {
        yield return new WaitForSeconds(0.4f);
        TitleTextAnimator.speed = 1;

        yield return new WaitForSeconds(1.3f);
        TitleTextAnimator.SetTrigger("PopPop");

        //yield return new WaitForSeconds(0.2f);
        //StartBtn.interactable = true;
    }

    public void StartGame()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(2);

        //LoadingCanvas.gameObject.SetActive(false);
        LoadingCanvas.gameObject.SetActive(true);

        for(int i = 0; i<maxBubble; i++)
        {
            Destroy(bubble_Array[i].gameObject);
        }

        gameObject.SetActive(false);
    }

#endregion
}
