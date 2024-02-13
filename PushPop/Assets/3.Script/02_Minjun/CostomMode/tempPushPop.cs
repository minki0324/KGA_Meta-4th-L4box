using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class tempPushPop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{
    
    private bool isCanMakePush;
    public bool isSet;
    public bool isCheckOverlap;
    public bool isOverlap;
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
        if (isOverlap /*|| !isCanMakePush*/)
        {
            Destroy(gameObject);
        }
        else
        {
            isSet = true;
            isCheckOverlap = false;
  
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.selectedObject.CompareTag("Puzzle"))
        {
            isCanMakePush = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.selectedObject.CompareTag("Puzzle"))
        {
            isCanMakePush = false;
        }
    }
}
