using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Mirror;
public class PlayerMove : NetworkBehaviour 
{
    //�ڹ������� ������ �ƹ�Ÿ ��ũ��Ʈ
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private float speed = 5; //�ƹ�Ÿ �̵��ӵ�
    private bool isMove; //�ùٸ� ������ ��ġ , Ŭ�� �� �÷��̾ �̵���Ű�� ���� bool
    private bool _isMouseStartUI;
    Vector2 touchPosition; // android ��ġ , WIndow  Ŭ���� �̵��� ��ġ ����( �÷��̾ �̵���Ű�� ���� ��ġ)
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
            if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject())
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.GetTouch(i);
                    if (!isMove && (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved))
                    {
                            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                            isMove = true;
                    }
                    //��ġ�� ������������ ���� ��ġ�� �����մϴ�.(UI�� ����)
                }



            }
            // ��ġ ��ġ�� �÷��̾ �̵���ŵ�ϴ�.
            if (isMove)
            {
                //�̵��ڵ�
                rb.position = Vector2.MoveTowards(rb.position, touchPosition, speed * Time.deltaTime);
                //�������� ���������� isMove =flase �Ҵ��� �̵� ����.
                if (Vector2.Distance(rb.position, touchPosition) < 0.01f)
                {
                    rb.position = touchPosition;
                    isMove = false;
                }
            }
        }
        else
        { // window , editor
            //���콺Ŭ���ϰ� Ŭ����ġ�� UI�� �ƴҶ�
      
            if ( Input.GetMouseButton(0) &&  !EventSystem.current.IsPointerOverGameObject())
                {
                    //�̵�����ġ����
                    touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    isMove = true;
                }
       
            //��ġ�� ��� ��ġ��ġ�� �̵���Ŵ
            if (isMove)
            {
                //�̵��ڵ�
                rb.position = Vector2.MoveTowards(rb.position, touchPosition, speed * Time.deltaTime);
                //�������� ���������� isMove�ٽ� false 
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
        //���� �̺�Ʈ �ý����� ������� �ϴ� ���ο� PointerEventData ��ü�� ����
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        //���� ������(���콺 �Ǵ� ��ġ)�� ��ġ ���� �Ҵ�
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
