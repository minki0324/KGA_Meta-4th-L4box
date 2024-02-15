using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CostomPushpopManager costom;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");
        costom.isOnArea = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
        costom.isOnArea = false;
    }
}
