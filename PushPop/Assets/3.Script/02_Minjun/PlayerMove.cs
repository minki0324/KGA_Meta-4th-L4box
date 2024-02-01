using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class PlayerMove : NetworkBehaviour
{
    //�ڹ������� ������ �ƹ�Ÿ ��ũ��Ʈ
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private float speed = 5; //�ƹ�Ÿ �̵��ӵ�
    private bool isTouch; //�÷��̾ �̵���Ű�� ���� ��ġ�� �ߴ��� Ȯ���ϴ� bool��
    Vector2 mousePosition; // window ���콺Ŭ�� ��ġ���� ( �÷��̾ �̵���Ű�� ���� ��ġ)
    Vector2 touchPosition; // android ��ġ�� �̵��� ��ġ ����( �÷��̾ �̵���Ű�� ���� ��ġ)
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
                Touch touch = Input.GetTouch(0); // ù ��° ��ġ�� ����մϴ�.

                touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                isTouch = true;
              
            }
            // ��ġ ��ġ�� �÷��̾ �̵���ŵ�ϴ�.
            if (isTouch)
            {
                //�̵��ڵ�
                rb.position = Vector2.MoveTowards(rb.position, touchPosition, speed * Time.deltaTime);
                //�������� ���������� isTouch =flase �Ҵ��� �̵� ����.
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


                // ��ġ ��ġ�� �÷��̾ �̵���ŵ�ϴ�.
                
            }
            //��ġ�� ��� ��ġ��ġ�� �̵���Ŵ
            if (isTouch) { 
                //�̵��ڵ�
            rb.position = Vector2.MoveTowards(rb.position, mousePosition, speed * Time.deltaTime);
                //�������� ���������� isTouch �ٽ� false 
                if(Vector2.Distance(rb.position,mousePosition)< 0.01f)
                {
                    rb.position = mousePosition;
                    isTouch = false;
                }
            }
        }
        
    }
}
