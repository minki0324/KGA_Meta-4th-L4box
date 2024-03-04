using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FramePuzzle : MonoBehaviour,  IPointerDownHandler , IPointerUpHandler
{
    private Image _myImage;
    [SerializeField] private CustomPushpopManager custom;
    private void OnEnable()
    {
        custom.onCustomEnd += PushButtonActiveOn; // 설치한 버튼들 활성화
        custom.onCustomEnd += CustomModeEnd; // 버튼 설치할 수 있게하는 bool true;
    }
    private void OnDisable()
    {
        custom.onCustomEnd -= PushButtonActiveOn; // 설치한 버튼들 활성화
        custom.onCustomEnd -= CustomModeEnd; // 버튼 설치할 수 있게하는 bool true;
    }
    void Start()
    {
        _myImage = GetComponent<Image>();
        //Sprite에서 Alpha 값이 0.1 이하 일시 인식하지 않게함
        _myImage.alphaHitTestMinimumThreshold = 0.1f;
    }

   
    // Update is called once per frame
    void Update()
    {
   
    }
   
    public void PushButtonActiveOn()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            // i번째 자식 요소의 Image 컴포넌트의 raycastTarget을 true로 설정합니다.
            transform.GetChild(i).GetComponent<Image>().raycastTarget = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!CustomPushpopManager.Instance.isCustomMode) return;
        if (CustomPushpopManager.Instance.isCool) return;
        CustomPushpopManager.Instance.ClickDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!CustomPushpopManager.Instance.isCustomMode) return;
        CustomPushpopManager.Instance.ClickUp();
    }

    public void CustomModeEnd()
    {
        CustomPushpopManager.Instance.isCustomMode = false;
    }

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    GameObject clickObject = EventSystem.current.currentSelectedGameObject;
    //    Debug.Log(clickObject.name);
    //}

    /*  public void OnPointerDown(PointerEventData eventData)
      {
          // 터치한 지점의 스크린 좌표를 RectTransform으로 변환하여 확인
          RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);

          // 변환된 지점이 CustomArea 안에 있는지 확인
          if (rect.rect.Contains(localPoint))
          {
              // rect 안에 터치가 발생한 경우
              Debug.Log("Touch inside CustomArea");
              costomPushpop.isCanMakePush = true;
              costomPushpop.isOnArea = true;
          }
          else
          {
              // rect 밖에서 터치가 발생한 경우
              costomPushpop.isCanMakePush = false;
              Debug.Log("Touch outside CustomArea");
          }

      }*/
}
