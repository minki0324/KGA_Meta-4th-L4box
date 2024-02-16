using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class tempPushPop : MonoBehaviour/*, IPointerEnterHandler, IPointerExitHandler*/
{
    public bool isCanMakePush;
    public bool isSet;
    public bool isCheckOverlap;
    public bool isOverlap;
    public GameObject RectPush;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isCheckOverlap)
        {
            if (collision.CompareTag("PushPop"))
            {
                if (!isSet)
                {
                    isOverlap = true;
                }
            }
        }
    }
   
    public void CheckOverlap(Vector3 spawnPosition)
    {
        if (isOverlap || !isCanMakePush)
        {
            CostomPushpopManager stack = FindObjectOfType<CostomPushpopManager>();
            GameObject lastFakeStack = stack.StackFakePops.Pop();
            Destroy(lastFakeStack);
            PushPop.Instance.pushPopButton.Remove(RectPush);
            GameObject lastStack = stack.StackPops.Pop();
            Destroy(lastStack);
            Destroy(RectPush);
        }
        else
        {
            isSet = true;
            isCheckOverlap = false;
  
        }
    }
    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    Debug.Log(isCanMakePush);
    //    if (eventData.selectedObject.CompareTag("Puzzle"))
    //    {
    //        isCanMakePush = true;
    //        Debug.Log(isCanMakePush);
    //    }
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    Debug.Log(isCanMakePush);
    //    if (eventData.selectedObject.CompareTag("Puzzle"))
    //    {
    //        isCanMakePush = false;
    //        Debug.Log(isCanMakePush);
    //    }
    //}
}
