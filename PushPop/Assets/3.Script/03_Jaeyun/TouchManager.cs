using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.VFX;

public class TouchManager : MonoBehaviour
{
    //This is a simple script that does a raycast and plays the VFX on a prefab
    public GameObject VFXPrefab;
    public GameObject TouchMovePrefab;
    VisualEffect[] visualEffects;
    VisualEffect[] visualEffect_Pooling;
    public Camera effectCamera;
    Coroutine touchTimer;
    public Transform particleCanvas;
    [SerializeField] private RawImage rawImage;

    private Vector2 nowPos;
    private Vector2 prePos;
    private Vector3 movePos;
    private float speed = 0.25f;
    public Transform touchTransform;

    public bool bisDrag = false;
    public bool bCanCreate = true;

    float touchTime = 0;
    float createTime = 0;


    public List<GameObject> DragPool = new List<GameObject>();

    public int MaxCount = 15;
    private int CurrentCount = 0;

    private void Awake()
    {
        Application.targetFrameRate = 30;
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
        //if (Input.touchCount == 1)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        prePos = touch.position = touch.deltaPosition;
        //    }
        //    else if (touch.phase == TouchPhase.Moved)
        //    {
        //        nowPos = touch.position - touch.deltaPosition;
        //        movePos = (Vector3)(prePos - nowPos) * Time.deltaTime * speed;
        //        touchTransform.Translate(movePos);
        //        prePos = touch.position - touch.deltaPosition;
        //    }
        //}

        ClickEffect();
    }

    private void ClickEffect()
    {
        //ui상 클릭한 좌표를 world로 바꿔서 그 위치에 생성

        //마우스 클릭된 상태에서 마우스 클릭위치와 현재위치가 다르면 이펙트 변경
        //position은 현재 터치 위치
        //deltaposition은 전 프레임에서의 터치 위치와 이번 프레임에서 터치위치의 차이, 터치의 이동량

        if (Input.touchCount == 1)
        {
           
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                nowPos = touch.position;
                TouchEffect();
            }  
            else if (touch.phase == TouchPhase.Stationary)
            {
                //터치중이면
              
                touchTime += Time.deltaTime;
                if (touchTime > 0.4f)
                {
                    bisDrag = true;             
                }
                if (bisDrag)
                {
                    nowPos = touch.position - touch.deltaPosition;
                    if(bCanCreate)
                    {
                        DragEffect();
                    }
                    
                }
            }
            else if(touch.phase == TouchPhase.Moved)
            {
                if(bisDrag)
                {
                    nowPos = touch.position - touch.deltaPosition;
                    if (bCanCreate)
                    {
                        DragEffect();
                    }
                }
            }
            else if(touch.phase == TouchPhase.Ended)
            {
              
                bisDrag = false;
                touchTime = 0f;
                createTime = 0f;
            }

            createTime += Time.deltaTime;
            if(createTime > 0.5f)
            {
                bCanCreate = true;
            }
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
        //Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //pos.z = 1;


        //GameObject vfxEffect = Instantiate(VFXPrefab, pos, Quaternion.identity);
        //vfxEffect.transform.parent = particleCanvas;


        //visualEffects = vfxEffect.GetComponentsInChildren<VisualEffect>();

        //for (int i = 0; i < visualEffects.Length; i++)
        //{
        //    visualEffects[i].SendEvent("Click");
        //}
        //Destroy(vfxEffect, 1.5f);
    }

    private IEnumerator ClickStartTimer_Co()
    {
        effectCamera.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        effectCamera.gameObject.SetActive(false);
    }


}
