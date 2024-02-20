using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PushPopButton : MonoBehaviour, IPointerDownHandler
{
    public int spriteIndex;

    private void OnEnable()
    {
        Button btn = transform.GetComponent<Button>();
        btn.interactable = true;
        if (!GameManager.Instance.gameMode.Equals(Mode.PushPush))
        {
            gameObject.GetComponent<Image>().raycastTarget = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PushPopClick();
    }

    // Push Pop Button click method
    public void PushPopClick()
    {
        GameObject clickButton = this.gameObject;
        // PushPop.Instance.pushPopButton.Remove(clickButton);
        GameManager.Instance.GameClear();
        if (clickButton.GetComponent<Button>().interactable)
        {
            GameManager.Instance.buttonActive--;
            clickButton.GetComponent<Button>().interactable = false;
        }
    }
}
