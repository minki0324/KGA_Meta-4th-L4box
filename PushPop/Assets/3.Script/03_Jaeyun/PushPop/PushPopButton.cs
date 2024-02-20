using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PushPopButton : MonoBehaviour, IPointerDownHandler
{
    public int spriteIndex;
    public int player = 0;

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
        Debug.Log("왜지");
        GameObject clickButton = this.gameObject;
        if (GameManager.Instance.gameMode.Equals(Mode.PushPush))
        {
            PushPop.Instance.pushPopButton.Remove(clickButton);
        }
        if (GameManager.Instance.gameMode.Equals(Mode.Bomb))
        {
            if (player.Equals(0))
            { // 1P 소유 팝 버튼
                GameManager.Instance.bombScript.popList1P.Remove(clickButton);
            }
            else if (player.Equals(1))
            { // 2P 소유 팝 버튼
                GameManager.Instance.bombScript.popList2P.Remove(clickButton);
            }
        }
        if (clickButton.GetComponent<Button>().interactable)
        {
            GameManager.Instance.buttonActive--;
            clickButton.GetComponent<Button>().interactable = false;
        }
        GameManager.Instance.GameClear();
    }
}
