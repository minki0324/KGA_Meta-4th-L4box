using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PushPopButton : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        PushPopClick();
    }

    // Push Pop Button click method
    public void PushPopClick()
    {
        GameObject clickButton = EventSystem.current.currentSelectedGameObject;
        PushPop.instance.pushPopButton.Remove(clickButton);
        GameManager.instance.GameClear();
        clickButton.SetActive(false);
    }
}
