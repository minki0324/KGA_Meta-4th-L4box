using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.VFX;

//update-> coroutine���� �ٲ��� ������ ��

public class TouchManager : MonoBehaviour
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
    [SerializeField] private VisualEffect[] visualEffect_Pooling;    //�巡�� �� �����Ǵ� ������ ������Ʈ Ǯ���� �迭

    [SerializeField] private List<TouchEvent> touchEvent_List = new List<TouchEvent>();     //TouchEvent ���� ����Ʈ
    [SerializeField] private List<Vector2> nowPos_List = new List<Vector2>();       //TouchEvent�� Touch.position�� ���� ����Ʈ

    [Header("ETC")]
    // Coroutine touchTimer;
    [SerializeField] private Transform particleCanvas;  //��ġ&�巡�� ���� �� ��ӵ� ������Ʈ Transform
    [SerializeField] private RawImage rawImage;     //EffectCamera�� �Կ��ϴ� RawImage ������Ʈ

    public int maxTouchCount = 10;      //�ִ� ��ġ ��� ��

    public float dragTime = 0.4f;   //�巡�׷� ������ �ð� ����
    private float touchTime = 0;    //��ġ �� ������ �� �巡�׷� �����Ǵ� �ð� ������ ����

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

        //RawImage�� �ؽ�ó ũ�� ����
        rawImage.texture.width = Screen.width;
        rawImage.texture.height = Screen.height;
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
        visualEffect_Pooling = new VisualEffect[MaxCount];

        for (int i = 0; i < 10; i++)
        {
            TouchEvent touchEvent = new TouchEvent();
            touchEvent_List.Add(touchEvent);

            Vector2 pos = new Vector2();
            nowPos_List.Add(pos);
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
                touchEvent_List[touch.fingerId].touch = touch;

                //��ġ ���̵� ��ġ ������ ���� ����Ʈ ����
                switch (touchEvent_List[touch.fingerId].touch.phase)
                {
                    case TouchPhase.Began:
                        nowPos_List[touch.fingerId] = touchEvent_List[touch.fingerId].touch.position;
                        TouchEffect_Multi(touch.fingerId);
                        break;

                    case TouchPhase.Stationary:
                        touchTime += Time.deltaTime;
                        if (touchTime > dragTime)
                        {
                            touchEvent_List[touch.fingerId].bisDrag = true;
                        }
                        if (touchEvent_List[touch.fingerId].bisDrag)
                        {
                            nowPos_List[touch.fingerId] = touchEvent_List[touch.fingerId].touch.position - touchEvent_List[touch.fingerId].touch.deltaPosition;
                            if (bCanCreate)
                            {
                                DragEffect_Multi(touch.fingerId);
                            }
                        }
                        break;

                    case TouchPhase.Moved:
                        if (touchEvent_List[touch.fingerId].bisDrag)
                        {
                            nowPos_List[touch.fingerId] = touchEvent_List[touch.fingerId].touch.position - touchEvent_List[touch.fingerId].touch.deltaPosition;
                            if (bCanCreate)
                            {
                                DragEffect_Multi(touch.fingerId);
                            }
                        }
                        break;

                    case TouchPhase.Ended:
                        touchEvent_List[touch.fingerId].bisDrag = false;
                        touchTime = 0f;
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
        Vector3 worldPos = new Vector3();

        worldPos = Camera.main.ScreenToWorldPoint(nowPos_List[_index]);
        worldPos.z = 1;
        GameObject vfxEffect = Instantiate(TouchEffectPrefab, worldPos, Quaternion.identity);

        //vfxEffect.transform.parent = particleCanvas;

        vfxEffect.GetComponent<VisualEffect>().SendEvent("Click");
     
        Destroy(vfxEffect, 1.5f);
    }

    public void DragEffect_Multi(int _index)
    {//�巡�� ����Ʈ ���� �޼ҵ� 
        Vector3 worldPos = new Vector3();
        worldPos = Camera.main.ScreenToWorldPoint(nowPos_List[_index]);
        worldPos.z = 1;

        //������Ʈ Ǯ��
        if (CurrentCount < MaxCount)
        {//������Ʈ Ǯ�� ��������� ����
            GameObject vfxEffect = Instantiate(DragEffectPrefab, worldPos, Quaternion.identity);
            //vfxEffect.transform.parent = particleCanvas;

            visualEffect_Pooling[CurrentCount] = vfxEffect.GetComponent<VisualEffect>();
            visualEffect_Pooling[CurrentCount].gameObject.SetActive(true);
            visualEffect_Pooling[CurrentCount].SendEvent("Click");
            CurrentCount += 1;
        }
        else
        {
            for (int i = 0; i < MaxCount; i++)
            {//������Ʈ Ǯ�� ���� �������� Ǯ��

                if (!visualEffect_Pooling[i].gameObject.activeSelf)
                {//������Ʈ�� ����������
                    visualEffect_Pooling[i].gameObject.SetActive(true);
                    visualEffect_Pooling[i].transform.position = worldPos;
                    visualEffect_Pooling[i].SendEvent("Click");
                    break;
                }
            }
        }

        //�巡�� ����Ʈ ���� ��Ÿ�� �ʱ�ȭ
        bCanCreate = false;
        createTime = 0f;
    }

}

    #endregion
