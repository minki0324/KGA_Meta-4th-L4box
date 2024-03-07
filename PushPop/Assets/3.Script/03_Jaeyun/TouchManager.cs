using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.VFX;

public class TouchManager : MonoBehaviour
{
    struct TouchEvent
    {
        public Touch touch;
        public bool isDrag;
    }

    //This is a simple script that does a raycast and plays the VFX on a prefab
    public GameObject VFXPrefab;
    public GameObject TouchMovePrefab;
    VisualEffect[] visualEffects;
    VisualEffect[] visualEffect_Pooling;
    public Camera effectCamera;
    Coroutine touchTimer;
    public Transform particleCanvas;
    [SerializeField] private RawImage rawImage;

    [SerializeField] private List<Touch> touch_List = new List<Touch>();
    [SerializeField] private List<Vector2> nowPos_List = new List<Vector2>();
    private int maxTouchCount = 10;
    private int preTouchCout = 0;

    private Vector2 nowPos; //현재 터치 포지션
    public Transform touchTransform;


    public bool bisDrag = false;
    public bool bCanCreate = true;

    float touchTime = 0;    //꾹 눌렀을 때 판정 시간
    float createTime = 0;   //생성 쿨타임


    public List<GameObject> DragPool = new List<GameObject>();

    public int MaxCount = 15;   //꾹 눌렀을 떄 생성되는 프리팹 최대 갯수
    private int CurrentCount = 0;   //꾹 눌렀을 떄 생성되는 프리팹 현재 갯수

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {

        visualEffect_Pooling = new VisualEffect[MaxCount];
        visualEffects = VFXPrefab.GetComponentsInChildren<VisualEffect>();
        rawImage.texture.width = Screen.width;
        rawImage.texture.height = Screen.height;
    }

    void Update()
    {      
        if (Input.touchCount > 0)
        {
            Debug.Log("터치 발생");
            // ClickEffect();
            MultiTouchEvent();
        }         
    }

    private void SingleTouchEvent()
    {
        if (Input.touchCount == 1)
        {       
            Touch touch = Input.GetTouch(0);

            switch(touch.phase)
            {
                case TouchPhase.Began:
                    nowPos = touch.position;
                    TouchEffect();
                    break;

                case TouchPhase.Stationary:
                    touchTime += Time.deltaTime;
                    if (touchTime > 0.4f)
                    {
                        bisDrag = true;
                    }
                    if (bisDrag)
                    {
                        nowPos = touch.position - touch.deltaPosition;
                        if (bCanCreate)
                        {
                            DragEffect();
                        }

                    }
                    break;

                case TouchPhase.Moved:
                    if (bisDrag)
                    {
                        nowPos = touch.position - touch.deltaPosition;
                        if (bCanCreate)
                        {
                            DragEffect();
                        }
                    }
                    break;

                case TouchPhase.Ended:
                    bisDrag = false;
                    touchTime = 0f;
                    createTime = 0f;
                    break;
            }
            createTime += Time.deltaTime;
            if (createTime >= 0.5f)
            {
                bCanCreate = true;
            }
        
        }
       
        
    }
    
    private void MultiTouchEvent()
    {
        //if (Input.touchCount > 0 ) -> update에 박기전에 넣기
 
        for (int i = 0; i < Input.touchCount; i++)
        {
            if(Input.touchCount < maxTouchCount + 1)
            {
                Touch touch = Input.GetTouch(i);
                touch_List.Add(touch);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        nowPos = touch.position;
                       
                        TouchEffect();
                        break;

                    case TouchPhase.Stationary:
                        touchTime += Time.deltaTime;
                        if (touchTime > 0.4f)
                        {
                            bisDrag = true;
                        }
                        if (bisDrag)
                        {
                            nowPos = touch.position - touch.deltaPosition;
                            if (bCanCreate)
                            {
                                DragEffect();
                            }
                        }

                        break;

                    case TouchPhase.Moved:
                        if (bisDrag )
                        {
                            nowPos = touch.position - touch.deltaPosition;
                            if (bCanCreate)
                            {
                                DragEffect();
                            }
                        }

                        break;

                    case TouchPhase.Ended:
                        bisDrag = false;
                        touchTime = 0f;
                        createTime = 0f;


                        break;
                }

                createTime += Time.deltaTime;
                if (createTime >= 0.5f)
                {
                    bCanCreate = true;
                }
            }

            //if (Input.touchCount == 0)
            //{
            //    bisDrag = false;
            //    touchTime = 0f;
            //    createTime = 0f;

            //}

        }



    }

    public void TouchEffect()
    {

        Vector3 worldPos = new Vector3();

        worldPos = Camera.main.ScreenToWorldPoint(nowPos);
        worldPos.z = 1;
        GameObject vfxEffect = Instantiate(VFXPrefab, worldPos, Quaternion.identity);

        vfxEffect.transform.parent = particleCanvas;
        visualEffects = vfxEffect.GetComponentsInChildren<VisualEffect>();

        for (int i = 0; i < visualEffects.Length; i++)
        {
            visualEffects[i].SendEvent("Click");
        }
        Destroy(vfxEffect, 1.5f);
    }

    public void DragEffect()
    {   
        if(bisDrag)
        {
            Vector3 worldPos = new Vector3();
            worldPos = Camera.main.ScreenToWorldPoint(nowPos);
            worldPos.z = 1;
          
            if(CurrentCount < MaxCount)
            {//오브젝트 풀이 비어있으면 생성
                GameObject vfxEffect = Instantiate(TouchMovePrefab, worldPos, Quaternion.identity);
                vfxEffect.transform.parent = particleCanvas;

                visualEffect_Pooling[CurrentCount] = vfxEffect.GetComponent<VisualEffect>();

                visualEffect_Pooling[CurrentCount].SendEvent("Click");
                CurrentCount += 1;
                Debug.Log(CurrentCount);
            }
            else
            {
                for (int i = 0; i < MaxCount; i++)
                {//오브젝트 풀이 가득 차있으면 풀링

                    if(!visualEffect_Pooling[i].gameObject.activeSelf)
                    {//오브젝트가 꺼져있으면
                        visualEffect_Pooling[i].gameObject.SetActive(true);
                        visualEffect_Pooling[i].transform.position = worldPos;
                        visualEffect_Pooling[i].SendEvent("Click"); 
                        break;
                    }
                }
            }

            createTime = 0f;

        }

    }



    public void OriginalCode()
    {
        //if (touch.phase == TouchPhase.Began)
        //{
        //    nowPos = touch.position;
        //    TouchEffect();
        //}
        //else if (touch.phase == TouchPhase.Stationary)
        //{
        //    //터치중이면

        //    touchTime += Time.deltaTime;
        //    if (touchTime > 0.4f)
        //    {
        //        bisDrag = true;
        //    }
        //    if (bisDrag)
        //    {
        //        nowPos = touch.position - touch.deltaPosition;
        //        if (bCanCreate)
        //        {
        //            DragEffect();
        //        }

        //    }
        //}
        //else if (touch.phase == TouchPhase.Moved)
        //{
        //    if (bisDrag)
        //    {
        //        nowPos = touch.position - touch.deltaPosition;
        //        if (bCanCreate)
        //        {
        //            DragEffect();
        //        }
        //    }
        //}
        //else if (touch.phase == TouchPhase.Ended)
        //{

        //    bisDrag = false;
        //    touchTime = 0f;
        //    createTime = 0f;
        //}
    }

    private IEnumerator ClickStartTimer_Co()
    {
        effectCamera.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        effectCamera.gameObject.SetActive(false);
    }


}
