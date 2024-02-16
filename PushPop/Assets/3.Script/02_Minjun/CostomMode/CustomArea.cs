using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CustomPushpopManager costom;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Debug.Log("Custom : 들어왔음");
        costom.isOnArea = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Debug.Log("Custom : 나갔음");
        costom.isOnArea = false;
    }
}
