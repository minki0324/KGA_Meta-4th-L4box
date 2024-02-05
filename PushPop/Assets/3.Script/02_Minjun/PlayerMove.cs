using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Mirror;
public class PlayerMove : NetworkBehaviour 
{
    //박물관에서 움직일 아바타 스크립트
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private float speed = 5; //아바타 이동속도
    private bool isMove; //올바른 조건의 터치 , 클릭 시 플레이어를 이동시키기 위한 bool
    Vector2 touchPosition; // android 터치 , WIndow  클릭시 이동할 위치 저장( 플레이어를 이동시키기 위한 위치)
    private void Awake()
    {
       
    }
    private void Start()
    {
        if (isLocalPlayer)
        {
            TryGetComponent(out rb);
        }
    }
    private void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            PlayerMoveMent();
        }
    }
    private void Update()
    {
        //if (isLocalPlayer)
        //{
        //    PlayerMoveMent();
        //}
    }

    public void PlayerMoveMent()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                //터치를 누르고있을때 누른 위치를 저장합니다.
                if (touch.phase == TouchPhase.Moved && !TouchOverUI())
                {
                    touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    isMove = true;
                }

            }
            // 터치 위치로 플레이어를 이동시킵니다.
            if (isMove)
            {
                //이동코드
                rb.position = Vector2.MoveTowards(rb.position, touchPosition, speed * Time.deltaTime);
                //목적지에 도착했을시 isMove =flase 할당후 이동 정지.
                if (Vector2.Distance(rb.position, touchPosition) < 0.01f)
                {
                    rb.position = touchPosition;
                    isMove = false;
                }
            }
        }
        else
        { // window , editor
            //마우스클릭하고 클릭위치가 UI가 아닐때
            if (Input.GetMouseButton(0) && !TouchOverUI())
            {
                //이동할위치저장
                touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                isMove = true;
            }
            //터치한 경우 터치위치로 이동시킴
            if (isMove)
            {
                //이동코드
                rb.position = Vector2.MoveTowards(rb.position, touchPosition, speed * Time.deltaTime);
                //목적지에 도착했을시 isMove다시 false 
                if (Vector2.Distance(rb.position, touchPosition) < 0.01f)
                {
                    rb.position = touchPosition;
                    isMove = false;
                }
            }
        }

    }
    public bool TouchOverUI()
    {
        //현재 이벤트 시스템을 기반으로 하는 새로운 PointerEventData 객체를 생성
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        //현재 포인터(마우스 또는 터치)의 위치 정보 할당
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
