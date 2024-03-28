using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//VFX Graph가 엘포박스 기기에서 작동하지 않아 UI로 만든 터치/드래그 이펙트..

public class EffectManager : MonoBehaviour
{
    class TouchEvent
    { //터치이벤트 클래스 : 개별 터치 및 드래그 판정용
        public Touch touch;
        public bool isDrag = false;
    }

    [Header("Prefabs")]
    [SerializeField] private GameObject touchEffectPrefab;  //터치했을 때 나오는 Visual Effect 프리팹
    [SerializeField] private GameObject dragEffectPrefab;   //드래그했을 때 나오는 Visual Effect 프리팹

    [Header("List & Array")]
    private GameObject[] touchEffectPooling;    //드래그 시 생성되는 프리팹 오브젝트 풀링용 배열
    private List<TouchEvent> touchEventList = new List<TouchEvent>();     //TouchEvent 담을 리스트
    private List<Vector2> nowPosList = new List<Vector2>();       //TouchEvent의 Touch.position을 담을 리스트

    [Header("ETC")]
    // Coroutine touchTimer;
    [SerializeField] private Transform particleCanvas;
    [SerializeField] private Transform dragEffectsObj; //터치&드래그 생성 시 상속될 오브젝트 Transform

    private int maxTouchCount = 10;      //최대 터치 허용 수

    private float dragTime = 0.7f;   //드래그로 판정될 시간 변수
    private float[] touchTimeArray;

    private float createCoolTime = 0.1f;  //프리팹 생성 쿨타임 변수
    private float createTime = 0;   //프리팹 생성 쿨타임 측정용 변수
    private bool isCanCreate = true;   //프리팹 생성이 가능한가 판정용 변수

    private int maxCount = 80;   //꾹 눌렀을 떄 생성되는 프리팹 최대 갯수
    private int currentCount = 0;   //꾹 눌렀을 떄 생성되는 프리팹 현재 갯수

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
    {//초기화 메소드      
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
    {//멀티터치 이벤트 (각 터치별 독립적 작동) 메소드
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.touchCount < maxTouchCount + 1)
            {
                //터치의 핑거아이디를 인덱스로하는 touchEvent를 넣기
                Touch touch = Input.GetTouch(i);
                touchEventList[i].touch = touch;

                //터치 아이디별 터치 판정에 따른 이펙트 생성
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

                //드래그 이펙트 생성 쿨타임 판정
                createTime += Time.deltaTime;
                if (createTime >= createCoolTime)
                {
                    isCanCreate = true;
                }
            }
        }
    }

    public void TouchEffect_Multi(int _index)
    {//터치 이펙트 생성 메소드 -> 얘도 풀링으로 할까요?

        GameObject touchEffect = Instantiate(touchEffectPrefab, nowPosList[_index], Quaternion.identity);
        touchEffect.transform.SetParent(dragEffectsObj);

        Destroy(touchEffect, 1f);
    }

    public void DragEffect_Multi(int _index)
    {//드래그 이펙트 생성 메소드 
        Vector3 worldPos = new Vector3();
        worldPos = Camera.main.ScreenToWorldPoint(nowPosList[_index]);
        worldPos.z = 1;

        //오브젝트 풀링
        if (currentCount < maxCount)
        {//오브젝트 풀이 비어있으면 생성
            GameObject touchEffect = Instantiate(dragEffectPrefab, nowPosList[_index], Quaternion.identity);

            touchEffectPooling[currentCount] = touchEffect;
            touchEffect.transform.SetParent(dragEffectsObj);
            touchEffectPooling[currentCount].gameObject.SetActive(true);

            currentCount += 1;
        }
        else
        {
            for (int i = 0; i < maxCount; i++)
            {//오브젝트 풀이 가득 차있으면 풀링

                if (!touchEffectPooling[i].gameObject.activeSelf)
                {//오브젝트가 꺼져있으면
                    touchEffectPooling[i].gameObject.SetActive(true);
                    touchEffectPooling[i].transform.position = nowPosList[_index];

                    break;
                }
            }
        }

        //드래그 이펙트 생성 쿨타임 초기화
        isCanCreate = false;
        createTime = 0f;
    }

    #endregion

}

