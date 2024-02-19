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
        switch (GameManager.Instance.gameMode)
        {
            case Mode.PushPush:
                CustomPushpopManager.Instance.StackPops.Push(gameObject);
                break;
            case Mode.Memory:
                break;

        }
        Button btn = transform.GetComponent<Button>();
        btn.interactable = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (GameManager.Instance.gameMode)
        {
            case Mode.PushPush:
                PushPopClick();
                break;
            case Mode.Memory:
                break;
        }
    }

    // Push Pop Button click method
    public void PushPopClick()
    {
        GameObject clickButton = this.gameObject;
        PushPop.Instance.pushPopButton.Remove(clickButton);
        GameManager.Instance.GameClear();
        clickButton.GetComponent<Button>().interactable = false;
    }
}
