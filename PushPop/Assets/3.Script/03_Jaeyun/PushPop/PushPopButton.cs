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
        GameObject clickButton = this.gameObject;
        PushPop.Instance.pushPopButton.Remove(clickButton);
        GameManager.Instance.GameClear();
        clickButton.SetActive(false);
    }
}
