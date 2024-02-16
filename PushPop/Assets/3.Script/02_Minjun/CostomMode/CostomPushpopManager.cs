using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CostomPushpopManager : MonoBehaviour
{
    [SerializeField] private RectTransform CustomArea;
    private Vector3 SelectPositon; //카메라에서보이는 world 포지션 저장할 Vector
    private Vector2 localPosition; //UI에 위치한 마우스위치 저장할 Vector
    [SerializeField] private RectTransform rectTransform; // UI상 마우스위치 구하기위한 기준 판넬
    [SerializeField] private GameObject pushPop; // OverLap검사하는 푸시팝(gameObject)
    [SerializeField] private Transform canvas; // UI푸시팝 소환하고 상속시킬 캔버스
    [SerializeField] private GameObject RectPushPop; //UI 푸시팝
    private GameObject newPush; //현재 소환중인 푸시팝
    private GameObject newRectPush;//현재 소환중인 푸시팝
    public bool isCanMakePush; //FramePuzzle 에서 전달받은 bool값을 설치할때 버튼이 오브젝트위에있는지 판단하기위해 씀.
    public bool isOnArea;
    public GameObject puzzleBoard;
    public Stack<GameObject> rectPopBtn = new Stack<GameObject>();
    void Update()
    {// 안드로이드는 구현안함
        if (Application.platform == RuntimePlatform.Android)
        {//안드로이드에서 실행 할 때
            AndroidPlatForm();

        }
        else
        { // window , editor
          //마우스클릭하고 클릭위치가 UI가 아닐때
            WindowPlatform();
        }
    }
    private IEnumerator CheckDelay(tempPushPop push)
    {
        yield return new WaitForSeconds(0.1f);
        push.CheckOverlap(SelectPositon);
    }
    private void WindowPlatform()
    {

        if (Input.GetMouseButtonDown(0) && isOnArea)
        {
            ClickDown();

        }
        if (newRectPush == null) return;
        if (Input.GetMouseButton(0))
        {
            Click();
        }
        if (Input.GetMouseButtonUp(0) && isOnArea)
        {
            ClickUp();
        }
    }
    private void AndroidPlatForm()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began && isOnArea)
                {
                    ClickDown();
                }
                if (newRectPush == null) return;
                if (touch.phase == TouchPhase.Moved)
                {
                    Click();
                }
                if (touch.phase == TouchPhase.Ended && isOnArea)
                {
                    Debug.Log("터치엔드");
                    ClickUp();
                }
                //터치를 누르고있을때 누른 위치를 저장합니다.(UI는 제외)
            }
        }
    }
    public void DestroyNewPush()
    {
        if (newPush != null && newRectPush != null)
        {
            Destroy(newPush);
            PushPop.Instance.pushPopButton.Remove(newRectPush);
            GameObject lastStack = rectPopBtn.Pop();
            Destroy(lastStack);
            newPush = null;
            newRectPush = null;
            return;
        }
    }
    public void DisableThisComponent()
    {
        this.enabled = false;
    }
    // 클릭 or 터치시 메소드들
    private void ClickDown()
    {
        Debug.Log("마우스클릭");
        SelectPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition); //카메라상의 좌표를 월드포지션으로구하기
                                                                             //판넬안에서 마우스혹은 터치위치의 RectTransform 구하기
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, null, out localPosition);
        //월드포지션 과 UI상 위치 보정
        Vector2 newLocalPosition = localPosition + new Vector2(960, 540);
        //UI에선 collider검사가 안되서 gameObject를 동시에 소환해서 안보이는 곳에서 겹침검사
        //월드포지션에 push소환하기(Collider 검사해서 push버튼 겹치는지 확인하기위해)
        newPush = Instantiate(pushPop, SelectPositon, Quaternion.identity);
        //UI상 위치에 push소환(실제로 보이는 push)
        newRectPush = Instantiate(RectPushPop, newLocalPosition, Quaternion.identity);
        newRectPush.transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);
        PushPop.Instance.pushPopButton.Add(newRectPush);
        rectPopBtn.Push(newRectPush);
        // pushpop Btn Parent 설정
        newRectPush.transform.SetParent(puzzleBoard.transform);

    } //마우스클릭 or 터치를 한 순간
    private void Click()
    {
        if (!isOnArea)
        {
            DestroyNewPush();
            return;
        }
        //드래그하는동안 마우스위치에 동기화
        SelectPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, null, out localPosition);

        newRectPush.transform.position = localPosition + new Vector2(960, 540);
        if (newPush != null)
        {
            newPush.transform.position = SelectPositon;
        }
    } // 마우스드래그 or 터치중일때
    private void ClickUp()
    {
        tempPushPop push = newPush.GetComponent<tempPushPop>();
        push.RectPush = newRectPush;
        //isCheckOverLap = ture일시 다른 푸시팝과 겹치는지 확인함
        push.isCheckOverlap = true ;
        //마우스가 퍼즐오브젝트 위에 있을시 true , 아니면 false -> 푸시팝설치를 오브젝트에만 하게하기위해
        push.isCanMakePush = isCanMakePush;
        //todo 버튼 interactable 설치할땐 꺼주고 설치완료되면 다시 켜주기
        //OverLap 체크후 겹친다면 Destroy , 아니면 위치 고정.
        StartCoroutine(CheckDelay(push));
        newRectPush = null;
        newPush = null;
    } // 마우스클릭을 뗏을때 or 터치를 뗏을때  

    public void ReturnBtn()
    {
        GameObject lastStack = rectPopBtn.Pop();
        Destroy(lastStack);
    }

    public void BtnAllClear()
    {
        while (rectPopBtn.Count > 0)
        {
            GameObject obj = rectPopBtn.Pop(); // Queue에서 오브젝트를 하나씩 제거
            Destroy(obj); // 해당 오브젝트를 파괴
        }
    }
    /* public void OnPointerDown(PointerEventData eventData)
     {
         // 터치한 지점의 스크린 좌표를 RectTransform으로 변환하여 확인
         RectTransformUtility.ScreenPointToLocalPointInRectangle(CustomArea, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);

         // 변환된 지점이 CustomArea 안에 있는지 확인
         if (CustomArea.rect.Contains(localPoint))
         {
             // CustomArea 안에 터치가 발생한 경우
             Debug.Log("Touch inside CustomArea");
             isOnArea = true;
         }
         else
         {
             // CustomArea 밖에서 터치가 발생한 경우
             isOnArea = false;
             Debug.Log("Touch outside CustomArea");
         }

     }*/
}