using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//VFX Graph�� �����ڽ� ��⿡�� �۵����� �ʾ� UI�� ���� ��ġ/�巡�� ����Ʈ..

public class EffectManager : MonoBehaviour
{
    class TouchEvent
    { //��ġ�̺�Ʈ Ŭ���� : ���� ��ġ �� �巡�� ������
        public Touch touch;
        public bool isDrag = false;
    }

    [Header("Prefabs")]
    [SerializeField] private GameObject touchEffectPrefab;  //��ġ���� �� ������ Visual Effect ������
    [SerializeField] private GameObject dragEffectPrefab;   //�巡������ �� ������ Visual Effect ������

    [Header("List & Array")]
    private GameObject[] touchEffectPooling;    //�巡�� �� �����Ǵ� ������ ������Ʈ Ǯ���� �迭
    private List<TouchEvent> touchEventList = new List<TouchEvent>();     //TouchEvent ���� ����Ʈ
    private List<Vector2> nowPosList = new List<Vector2>();       //TouchEvent�� Touch.position�� ���� ����Ʈ

    [Header("ETC")]
    // Coroutine touchTimer;
    [SerializeField] private Transform particleCanvas;
    [SerializeField] private Transform dragEffectsObj; //��ġ&�巡�� ���� �� ��ӵ� ������Ʈ Transform

    private int maxTouchCount = 10;      //�ִ� ��ġ ��� ��

    private float dragTime = 0.7f;   //�巡�׷� ������ �ð� ����
    private float[] touchTimeArray;

    private float createCoolTime = 0.1f;  //������ ���� ��Ÿ�� ����
    private float createTime = 0;   //������ ���� ��Ÿ�� ������ ����
    private bool isCanCreate = true;   //������ ������ �����Ѱ� ������ ����

    private int maxCount = 80;   //�� ������ �� �����Ǵ� ������ �ִ� ����
    private int currentCount = 0;   //�� ������ �� �����Ǵ� ������ ���� ����

    #region Unity Callback
    private void Awake()
    {
        Screen.SetResolution(1920, 1080, false);
    }


    void Start()
    {
        Init();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            MultiTouchEvent_Independent();
        }
    }


    #endregion

    #region Other Method
    private void Init()
    {//�ʱ�ȭ �޼ҵ�      
        touchEffectPooling = new GameObject[maxCount];
        touchTimeArray = new float[maxCount];

        for (int i = 0; i < 10; i++)
        {
            TouchEvent touchEvent = new TouchEvent();
            touchEventList.Add(touchEvent);

            Vector2 pos = new Vector2();
            nowPosList.Add(pos);

            touchTimeArray[i] = 0f;
        }
    }


    private void MultiTouchEvent_Independent()
    {//��Ƽ��ġ �̺�Ʈ (�� ��ġ�� ������ �۵�) �޼ҵ�
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.touchCount < maxTouchCount + 1)
            {
                //��ġ�� �ΰž��̵� �ε������ϴ� touchEvent�� �ֱ�
                Touch touch = Input.GetTouch(i);
                touchEventList[i].touch = touch;

                //��ġ ���̵� ��ġ ������ ���� ����Ʈ ����
                switch (touchEventList[i].touch.phase)
                {
                    case TouchPhase.Began:
                        nowPosList[i] = touchEventList[i].touch.position;
                        //  Vector2 localPoint;
                        //RectTransformUtility.ScreenPointToLocalPointInRectangle(particleCanvas.GetComponent<RectTransform>(), touchEvent_List[i].touch.position, effectCamera, out localPoint);

                        TouchEffect_Multi(i);
                        break;

                    case TouchPhase.Stationary:
                        touchTimeArray[i] += Time.deltaTime;
                        if (touchTimeArray[i] > dragTime)
                        {
                            touchEventList[i].isDrag = true;
                        }
                        if (touchEventList[i].isDrag)
                        {
                            nowPosList[i] = touchEventList[i].touch.position - touchEventList[i].touch.deltaPosition;
                            if (isCanCreate)
                            {
                                DragEffect_Multi(i);
                            }
                        }
                        break;

                    case TouchPhase.Moved:
                        if (touchEventList[i].isDrag)
                        {
                            nowPosList[i] = touchEventList[i].touch.position - touchEventList[i].touch.deltaPosition;
                            if (isCanCreate)
                            {
                                DragEffect_Multi(i);
                            }
                        }
                        break;

                    case TouchPhase.Ended:
                        touchEventList[i].isDrag = false;
                        touchTimeArray[i] = 0f;
                        createTime = 0f;
                        break;
                }

                //�巡�� ����Ʈ ���� ��Ÿ�� ����
                createTime += Time.deltaTime;
                if (createTime >= createCoolTime)
                {
                    isCanCreate = true;
                }
            }
        }
    }

    public void TouchEffect_Multi(int _index)
    {//��ġ ����Ʈ ���� �޼ҵ� -> �굵 Ǯ������ �ұ��?

        GameObject touchEffect = Instantiate(touchEffectPrefab, nowPosList[_index], Quaternion.identity);
        touchEffect.transform.SetParent(dragEffectsObj);

        Destroy(touchEffect, 1f);
    }

    public void DragEffect_Multi(int _index)
    {//�巡�� ����Ʈ ���� �޼ҵ� 
        Vector3 worldPos = new Vector3();
        worldPos = Camera.main.ScreenToWorldPoint(nowPosList[_index]);
        worldPos.z = 1;

        //������Ʈ Ǯ��
        if (currentCount < maxCount)
        {//������Ʈ Ǯ�� ��������� ����
            GameObject touchEffect = Instantiate(dragEffectPrefab, nowPosList[_index], Quaternion.identity);

            touchEffectPooling[currentCount] = touchEffect;
            touchEffect.transform.SetParent(dragEffectsObj);
            touchEffectPooling[currentCount].gameObject.SetActive(true);

            currentCount += 1;
        }
        else
        {
            for (int i = 0; i < maxCount; i++)
            {//������Ʈ Ǯ�� ���� �������� Ǯ��

                if (!touchEffectPooling[i].gameObject.activeSelf)
                {//������Ʈ�� ����������
                    touchEffectPooling[i].gameObject.SetActive(true);
                    touchEffectPooling[i].transform.position = nowPosList[_index];

                    break;
                }
            }
        }

        //�巡�� ����Ʈ ���� ��Ÿ�� �ʱ�ȭ
        isCanCreate = false;
        createTime = 0f;
    }

    #endregion

}

