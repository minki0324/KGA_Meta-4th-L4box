using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoustomPushpop : MonoBehaviour 
{ 
    
    private Vector3 SelectPositon;
    private Vector3 RectPosition;
    
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Transform canvas;
    [SerializeField] private GameObject pushPop;
    [SerializeField]private GameObject RectPushPop;
    private GameObject newPush;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject())
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.GetTouch(i);
                    if ( (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved))
                    {
                        
                    }
                    //��ġ�� ������������ ���� ��ġ�� �����մϴ�.(UI�� ����)
                }
            }
      
        }
        else
        { // window , editor
          //���콺Ŭ���ϰ� Ŭ����ġ�� UI�� �ƴҶ�

            if (Input.GetMouseButtonDown(0) )
            {
                Vector2 localPosition;
                //�̵�����ġ����
                SelectPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform , Input.mousePosition , null , out localPosition);
                Vector2 newLocalPosition = localPosition + new Vector2(960, 540);
                newPush = Instantiate(pushPop, SelectPositon, Quaternion.identity);
                GameObject newpush =  Instantiate(RectPushPop, newLocalPosition, Quaternion.identity);
                newpush.transform.SetParent(canvas.transform);
                Debug.Log(newLocalPosition);



            }
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                //�̵�����ġ����
                SelectPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if(newPush != null) { 
                newPush.transform.position = SelectPositon;
                }
            }
            if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
            {

                tempPushPop push = newPush.GetComponent<tempPushPop>();
                push.isCheckOverlap = true;
                StartCoroutine(CheckDelay(push));
            }


        }
    }
    private IEnumerator CheckDelay(tempPushPop push)
    {
        yield return new WaitForSeconds(0.1f);
        push.CheckOverlap(SelectPositon);
    }

    
}
