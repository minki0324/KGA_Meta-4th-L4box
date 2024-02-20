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
        Button btn = transform.GetComponent<Button>();
        btn.interactable = true;
        if(!GameManager.Instance.gameMode.Equals(Mode.PushPush))
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
        PushPop.Instance.pushPopButton.Remove(clickButton);
        if(GameManager.Instance.gameMode.Equals(Mode.Bomb))
        {
            if (player.Equals(0))
            { // 1P ¼ÒÀ¯ ÆË ¹öÆ°
                GameManager.Instance.bombScript.popList1P.Remove(clickButton);
            }
            else if (player.Equals(1))
            { // 2P ¼ÒÀ¯ ÆË ¹öÆ°
                GameManager.Instance.bombScript.popList2P.Remove(clickButton);
            }
        }
        GameManager.Instance.GameClear();
        clickButton.GetComponent<Button>().interactable = false;
    }
}
