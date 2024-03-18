using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.VFX;

//update-> coroutine으로 바꿀지 생각할 것

public class TouchManager : MonoBehaviour
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
    [SerializeField] private VisualEffect[] visualEffect_Pooling;    //드래그 시 생성되는 프리팹 오브젝트 풀링용 배열

    [SerializeField] private List<TouchEvent> touchEvent_List = new List<TouchEvent>();     //TouchEvent 담을 리스트
    [SerializeField] private List<Vector2> nowPos_List = new List<Vector2>();       //TouchEvent의 Touch.position을 담을 리스트


    [Header("ETC")]
    // Coroutine touchTimer;
    [SerializeField] private Transform particleCanvas;  //터치&드래그 생성 시 상속될 오브젝트 Transform
    [SerializeField] private RawImage rawImage;     //EffectCamera가 촬영하는 RawImage 오브젝트
    //[SerializeField] private Transform DragPoolPos;

    public int maxTouchCount = 10;      //최대 터치 허용 수

    public float dragTime = 0.4f;   //드래그로 판정될 시간 변수
    private float touchTime = 0;    //터치 꾹 눌렀을 때 드래그로 판정되는 시간 측정용 변수

    public float createCoolTime = 0.1f;  //프리팹 생성 쿨타임 변수
    private float createTime = 0;   //프리팹 생성 쿨타임 측정용 변수
    public bool bCanCreate = true;   //프리팹 생성이 가능한가 판정용 변수

    public int MaxCount = 60;   //꾹 눌렀을 떄 생성되는 프리팹 최대 갯수
    private int CurrentCount = 0;   //꾹 눌렀을 떄 생성되는 프리팹 현재 갯수

    private float timeCool = 0.3f;
    #region Unity Callback
    private void Awake()
    {
        //프레임 속도 고정
        Application.targetFrameRate = 300;
        Screen.SetResolution(1920, 1080, false);

    }



    private void OnEnable()
    {
        rawImage.texture.width = Camera.main.pixelWidth;
        rawImage.texture.height = Camera.main.pixelHeight;
    }

    void Start()
    {
        //SetResolution();
        //RawImage의 텍스처 크기 변경
        //rawImage.texture.width = 5760;
        //rawImage.texture.height = 3240;

        //VFX Graph 나타나는 사양되는지 확인하는 코드
        // bool supported = SystemInfo.supportsComputeShaders && SystemInfo.maxComputeBufferInputsVertex != 0;

        bool computeShaderSupporting = SystemInfo.supportsComputeShaders;
        Debug.Log("Compute Shader is Supported : " + computeShaderSupporting);

        //SSBO : Shader Storage Buffer Objects
        bool SSBOSupporting = SystemInfo.maxComputeBufferInputsVertex != 0;
        Debug.Log("SSBO is Supported :" + SSBOSupporting + SystemInfo.maxComputeBufferInputsVertex);


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
        visualEffect_Pooling = new VisualEffect[MaxCount];

        for (int i = 0; i < 10; i++)
        {
            TouchEvent touchEvent = new TouchEvent();
            touchEvent_List.Add(touchEvent);

            Vector2 pos = new Vector2();
            nowPos_List.Add(pos);
        }


        //for (int i = 0; i < MaxCount; i++)
        //{//오브젝트 풀이 비어있으면 생성
        //    GameObject vfxEffect = Instantiate(DragEffectPrefab, Vector3.zero, Quaternion.identity);
        //    //vfxEffect.transform.parent = particleCanvas;

        //    visualEffect_Pooling[i] = vfxEffect.GetComponent<VisualEffect>();
        //    visualEffect_Pooling[i].gameObject.SetActive(false);
        //    visualEffect_Pooling[i].SendEvent("Click");
        //    // CurrentCount += 1;
        //}
    }

    public void SetResolution()
    {
        int setWidth = 1920; // 사용자 설정 너비
        int setHeight = 1080; // 사용자 설정 높이

        int deviceWidth = Screen.width; // 기기 너비 저장
        int deviceHeight = Screen.height; // 기기 높이 저장

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution 함수 제대로 사용하기

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // 새로운 너비
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }

        rawImage.texture.width = (int)Camera.main.pixelRect.width;
        rawImage.texture.height = (int)Camera.main.pixelRect.height;
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
                        TouchEffect_Multi(i);
                        break;

                    case TouchPhase.Stationary:
                        touchTime += Time.deltaTime;
                        if (touchTime > dragTime)
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
                        touchTime = 0f;
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
        Vector3 worldPos = new Vector3();

        worldPos = Camera.main.ScreenToWorldPoint(nowPos_List[_index]);
        worldPos.z = 1;
        GameObject vfxEffect = Instantiate(TouchEffectPrefab, worldPos, Quaternion.identity);

        //vfxEffect.transform.parent = particleCanvas;

        vfxEffect.GetComponent<VisualEffect>().SendEvent("Click");
     
        Destroy(vfxEffect, 1.5f);
    }

    public void DragEffect_Multi(int _index)
    {//드래그 이펙트 생성 메소드 
        Vector3 worldPos = new Vector3();
        worldPos = Camera.main.ScreenToWorldPoint(nowPos_List[_index]);
        worldPos.z = 1;

        //오브젝트 풀링
        if (CurrentCount < MaxCount)
        {//오브젝트 풀이 비어있으면 생성
            GameObject vfxEffect = Instantiate(DragEffectPrefab, worldPos, Quaternion.identity);
            //vfxEffect.transform.parent = particleCanvas;

            visualEffect_Pooling[CurrentCount] = vfxEffect.GetComponent<VisualEffect>();
            //visualEffect_Pooling[CurrentCount].transform.parent = DragPoolPos;
            visualEffect_Pooling[CurrentCount].gameObject.SetActive(true);
            visualEffect_Pooling[CurrentCount].SendEvent("Click");
            CurrentCount += 1;
        }
        else
        {
            for (int i = 0; i < MaxCount; i++)
            {//오브젝트 풀이 가득 차있으면 풀링

                if (!visualEffect_Pooling[i].gameObject.activeSelf)
                {//오브젝트가 꺼져있으면
                    visualEffect_Pooling[i].gameObject.SetActive(true);
                    visualEffect_Pooling[i].transform.position = worldPos;
                    visualEffect_Pooling[i].SendEvent("Click");

                    break;
                }
            }
        }

        //드래그 이펙트 생성 쿨타임 초기화
        bCanCreate = false;
        createTime = 0f;
    }

    public void DragEffect_Multi_NonePool(int _index)
    {
        Vector3 worldPos = new Vector3();
        worldPos = Camera.main.ScreenToWorldPoint(nowPos_List[_index]);
        worldPos.z = 1;

        GameObject vfxEffect = Instantiate(DragEffectPrefab, worldPos, Quaternion.identity);

        vfxEffect.GetComponent<VisualEffect>().SendEvent("Click");

        Destroy(vfxEffect, 1.5f);
        //드래그 이펙트 생성 쿨타임 초기화
        bCanCreate = false;
        createTime = 0f;

        

       
    }


}

    #endregion
