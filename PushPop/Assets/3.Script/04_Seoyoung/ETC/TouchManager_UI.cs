using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//VFX Graph�� �����ڽ� ��⿡�� �۵����� �ʾ� UI�� ���� ��ġ/�巡�� ����Ʈ..

public class TouchManager_UI : MonoBehaviour
{
    class TouchEvent
    { //��ġ�̺�Ʈ Ŭ���� : ���� ��ġ �� �巡�� ������
        public Touch touch;
        public bool bisDrag = false;
    }

    [Header("Camera")]
    public Camera effectCamera;     //����Ʈ �Կ� ī�޶�

    [Header("Prefabs")]
    [SerializeField] private GameObject TouchEffectPrefab;  //��ġ���� �� ������ Visual Effect ������
    [SerializeField] private GameObject DragEffectPrefab;   //�巡������ �� ������ Visual Effect ������

    [Header("List & Array")]
    [SerializeField] private GameObject[] TouchEffect_Pooling;    //�巡�� �� �����Ǵ� ������ ������Ʈ Ǯ���� �迭

    [SerializeField] private List<TouchEvent> touchEvent_List = new List<TouchEvent>();     //TouchEvent ���� ����Ʈ
    [SerializeField] private List<Vector2> nowPos_List = new List<Vector2>();       //TouchEvent�� Touch.position�� ���� ����Ʈ


    [Header("ETC")]
    // Coroutine touchTimer;
    [SerializeField] private Transform particleCanvas;
    [SerializeField] private Transform DragEffectsObj; //��ġ&�巡�� ���� �� ��ӵ� ������Ʈ Transform

    public int maxTouchCount = 10;      //�ִ� ��ġ ��� ��

    public float dragTime = 0.7f;   //�巡�׷� ������ �ð� ����
    private float[] touchTime_arr;

    public float createCoolTime = 0.1f;  //������ ���� ��Ÿ�� ����
    private float createTime = 0;   //������ ���� ��Ÿ�� ������ ����
    public bool bCanCreate = true;   //������ ������ �����Ѱ� ������ ����

    public int MaxCount = 60;   //�� ������ �� �����Ǵ� ������ �ִ� ����
    private int CurrentCount = 0;   //�� ������ �� �����Ǵ� ������ ���� ����

    #region Unity Callback
    private void Awake()
    {
        //������ �ӵ� ����
        Application.targetFrameRate = 60;
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
        TouchEffect_Pooling = new GameObject[MaxCount];
        touchTime_arr = new float[MaxCount];

        for (int i = 0; i < 10; i++)
        {
            TouchEvent touchEvent = new TouchEvent();
            touchEvent_List.Add(touchEvent);

            Vector2 pos = new Vector2();
            nowPos_List.Add(pos);

            touchTime_arr[i] = 0f;
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
                touchEvent_List[i].touch = touch;

                //��ġ ���̵� ��ġ ������ ���� ����Ʈ ����
                switch (touchEvent_List[i].touch.phase)
                {
                    case TouchPhase.Began:
                        nowPos_List[i] = touchEvent_List[i].touch.position;
                        //  Vector2 localPoint;
                        //RectTransformUtility.ScreenPointToLocalPointInRectangle(particleCanvas.GetComponent<RectTransform>(), touchEvent_List[i].touch.position, effectCamera, out localPoint);

                        TouchEffect_Multi(i);
                        break;

                    case TouchPhase.Stationary:
                        touchTime_arr[i] += Time.deltaTime;
                        if (touchTime_arr[i] > dragTime)
                        {
                            touchEvent_List[i].bisDrag = true;
                        }
                        if (touchEvent_List[i].bisDrag)
                        {
                            nowPos_List[i] = touchEvent_List[i].touch.position - touchEvent_List[i].touch.deltaPosition;
                            if (bCanCreate)
                            {
                                DragEffect_Multi(i);
                            }
                        }
                        break;

                    case TouchPhase.Moved:
                        if (touchEvent_List[i].bisDrag)
                        {
                            nowPos_List[i] = touchEvent_List[i].touch.position - touchEvent_List[i].touch.deltaPosition;
                            if (bCanCreate)
                            {
                                DragEffect_Multi(i);
                            }
                        }
                        break;

                    case TouchPhase.Ended:
                        touchEvent_List[i].bisDrag = false;
                        touchTime_arr[i] = 0f;
                        createTime = 0f;
                        break;
                }

                //�巡�� ����Ʈ ���� ��Ÿ�� ����
                createTime += Time.deltaTime;
                if (createTime >= createCoolTime)
                {
                    bCanCreate = true;
                }
            }
        }
    }

    public void TouchEffect_Multi(int _index)
    {//��ġ ����Ʈ ���� �޼ҵ� -> �굵 Ǯ������ �ұ��?

        GameObject touchEffect = Instantiate(TouchEffectPrefab, nowPos_List[_index], Quaternion.identity);
        touchEffect.transform.SetParent(DragEffectsObj);

        Destroy(touchEffect, 1f);
    }

    public void DragEffect_Multi(int _index)
    {//�巡�� ����Ʈ ���� �޼ҵ� 
        Vector3 worldPos = new Vector3();
        worldPos = Camera.main.ScreenToWorldPoint(nowPos_List[_index]);
        worldPos.z = 1;

        //������Ʈ Ǯ��
        if (CurrentCount < MaxCount)
        {//������Ʈ Ǯ�� ��������� ����
            GameObject touchEffect = Instantiate(DragEffectPrefab, nowPos_List[_index], Quaternion.identity);

            TouchEffect_Pooling[CurrentCount] = touchEffect;
            touchEffect.transform.SetParent(DragEffectsObj);
            TouchEffect_Pooling[CurrentCount].gameObject.SetActive(true);

            CurrentCount += 1;
        }
        else
        {
            for (int i = 0; i < MaxCount; i++)
            {//������Ʈ Ǯ�� ���� �������� Ǯ��

                if (!TouchEffect_Pooling[i].gameObject.activeSelf)
                {//������Ʈ�� ����������
                    TouchEffect_Pooling[i].gameObject.SetActive(true);
                    TouchEffect_Pooling[i].transform.position = nowPos_List[_index];

                    break;
                }
            }
        }

        //�巡�� ����Ʈ ���� ��Ÿ�� �ʱ�ȭ
        bCanCreate = false;
        createTime = 0f;
    }

    #endregion

}

