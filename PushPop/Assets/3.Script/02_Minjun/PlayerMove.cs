using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class PlayerMove : NetworkBehaviour
{
    //박물관에서 움직일 아바타 스크립트
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private float speed = 5; //아바타 이동속도
    private bool isTouch; //플레이어를 이동시키기 위해 터치를 했는지 확인하는 bool값
    Vector2 mousePosition; // window 마우스클릭 위치저장 ( 플레이어를 이동시키기 위한 위치)
    Vector2 touchPosition; // android 터치시 이동할 위치 저장( 플레이어를 이동시키기 위한 위치)
    private void Start()
    {
        if (isLocalPlayer)
        {
            TryGetComponent(out rb);
        }
    }
    private void Update()
    {
        if (isLocalPlayer) { 
        PlayerMoveMent();
        }


    }

    public void PlayerMoveMent()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0); // 첫 번째 터치만 고려합니다.

                touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                isTouch = true;
              
            }
            // 터치 위치로 플레이어를 이동시킵니다.
            if (isTouch)
            {
                //이동코드
                rb.position = Vector2.MoveTowards(rb.position, touchPosition, speed * Time.deltaTime);
                //목적지에 도착했을시 isTouch =flase 할당후 이동 정지.
                if (Vector2.Distance(rb.position, mousePosition) < 0.01f)
                {
                    rb.position = mousePosition;
                    isTouch = false;
                }
            }
        }
        else
        { // window , editor

            if (Input.GetMouseButton(0))
            {
                isTouch = true;
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


                // 터치 위치로 플레이어를 이동시킵니다.
                
            }
            //터치한 경우 터치위치로 이동시킴
            if (isTouch) { 
                //이동코드
            rb.position = Vector2.MoveTowards(rb.position, mousePosition, speed * Time.deltaTime);
                //목적지에 도착했을시 isTouch 다시 false 
                if(Vector2.Distance(rb.position,mousePosition)< 0.01f)
                {
                    rb.position = mousePosition;
                    isTouch = false;
                }
            }
        }
        
    }
}
