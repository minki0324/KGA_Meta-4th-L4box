using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//VFX Graph가 엘포박스 기기에서 작동하지 않아 UI로 만든 터치/드래그 이펙트..

public class TouchManager_UI : MonoBehaviour
{
    class TouchEvent
    { //터치이벤트 클래스 : 개별 터치 및 드래그 판정용
        public Touch touch;
        public bool bisDrag = false;
    }

    [Header("Camera")]
    public Camera effectCamera;     //이펙트 촬영 카메라

    [Header("Prefabs")]
    [SerializeField] private GameObject TouchEffectPrefab;  //터치했을 때 나오는 Visual Effect 프리팹
    [SerializeField] private GameObject DragEffectPrefab;   //드래그했을 때 나오는 Visual Effect 프리팹

    [Header("List & Array")]
    [SerializeField] private GameObject[] TouchEffect_Pooling;    //드래그 시 생성되는 프리팹 오브젝트 풀링용 배열

    [SerializeField] private List<TouchEvent> touchEvent_List = new List<TouchEvent>();     //TouchEvent 담을 리스트
    [SerializeField] private List<Vector2> nowPos_List = new List<Vector2>();       //TouchEvent의 Touch.position을 담을 리스트


    [Header("ETC")]
    // Coroutine touchTimer;
    [SerializeField] private Transform particleCanvas;
    [SerializeField] private Transform DragEffectsObj; //터치&드래그 생성 시 상속될 오브젝트 Transform

    public int maxTouchCount = 10;      //최대 터치 허용 수

    public float dragTime = 0.7f;   //드래그로 판정될 시간 변수
    private float[] touchTime_arr;

    public float createCoolTime = 0.1f;  //프리팹 생성 쿨타임 변수
    private float createTime = 0;   //프리팹 생성 쿨타임 측정용 변수
    public bool bCanCreate = true;   //프리팹 생성이 가능한가 판정용 변수

    public int MaxCount = 60;   //꾹 눌렀을 떄 생성되는 프리팹 최대 갯수
    private int CurrentCount = 0;   //꾹 눌렀을 떄 생성되는 프리팹 현재 갯수

    #region Unity Callback
    private void Awake()
    {
        //프레임 속도 고정
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
    {//초기화 메소드      
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
    {//멀티터치 이벤트 (각 터치별 독립적 작동) 메소드
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.touchCount < maxTouchCount + 1)
            {
                //터치의 핑거아이디를 인덱스로하는 touchEvent를 넣기
                Touch touch = Input.GetTouch(i);
                touchEvent_List[i].touch = touch;

                //터치 아이디별 터치 판정에 따른 이펙트 생성
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

                //드래그 이펙트 생성 쿨타임 판정
                createTime += Time.deltaTime;
                if (createTime >= createCoolTime)
                {
                    bCanCreate = true;
                }
            }
        }
    }

    public void TouchEffect_Multi(int _index)
    {//터치 이펙트 생성 메소드 -> 얘도 풀링으로 할까요?

        GameObject touchEffect = Instantiate(TouchEffectPrefab, nowPos_List[_index], Quaternion.identity);
        touchEffect.transform.SetParent(DragEffectsObj);

        Destroy(touchEffect, 1f);
    }

    public void DragEffect_Multi(int _index)
    {//드래그 이펙트 생성 메소드 
        Vector3 worldPos = new Vector3();
        worldPos = Camera.main.ScreenToWorldPoint(nowPos_List[_index]);
        worldPos.z = 1;

        //오브젝트 풀링
        if (CurrentCount < MaxCount)
        {//오브젝트 풀이 비어있으면 생성
            GameObject touchEffect = Instantiate(DragEffectPrefab, nowPos_List[_index], Quaternion.identity);

            TouchEffect_Pooling[CurrentCount] = touchEffect;
            touchEffect.transform.SetParent(DragEffectsObj);
            TouchEffect_Pooling[CurrentCount].gameObject.SetActive(true);

            CurrentCount += 1;
        }
        else
        {
            for (int i = 0; i < MaxCount; i++)
            {//오브젝트 풀이 가득 차있으면 풀링

                if (!TouchEffect_Pooling[i].gameObject.activeSelf)
                {//오브젝트가 꺼져있으면
                    TouchEffect_Pooling[i].gameObject.SetActive(true);
                    TouchEffect_Pooling[i].transform.position = nowPos_List[_index];

                    break;
                }
            }
        }

        //드래그 이펙트 생성 쿨타임 초기화
        bCanCreate = false;
        createTime = 0f;
    }

    #endregion

}

