using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FramePuzzle : MonoBehaviour,  IPointerDownHandler , IPointerUpHandler
{
    private Image _myImage;
    private CustomPushpopManager costomPushpop;
    private RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        costomPushpop = FindObjectOfType<CustomPushpopManager>();
        _myImage = GetComponent<Image>();
        //Sprite���� Alpha ���� 0.1 ���� �Ͻ� �ν����� �ʰ���
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
            // i��° �ڽ� ����� Image ������Ʈ�� raycastTarget�� true�� �����մϴ�.
            transform.GetChild(i).GetComponent<Image>().raycastTarget = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CustomPushpopManager.Instance.ClickDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CustomPushpopManager.Instance.ClickUp();
    }

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    GameObject clickObject = EventSystem.current.currentSelectedGameObject;
    //    Debug.Log(clickObject.name);
    //}

    /*  public void OnPointerDown(PointerEventData eventData)
      {
          // ��ġ�� ������ ��ũ�� ��ǥ�� RectTransform���� ��ȯ�Ͽ� Ȯ��
          RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);

          // ��ȯ�� ������ CustomArea �ȿ� �ִ��� Ȯ��
          if (rect.rect.Contains(localPoint))
          {
              // rect �ȿ� ��ġ�� �߻��� ���
              Debug.Log("Touch inside CustomArea");
              costomPushpop.isCanMakePush = true;
              costomPushpop.isOnArea = true;
          }
          else
          {
              // rect �ۿ��� ��ġ�� �߻��� ���
              costomPushpop.isCanMakePush = false;
              Debug.Log("Touch outside CustomArea");
          }

      }*/
}
