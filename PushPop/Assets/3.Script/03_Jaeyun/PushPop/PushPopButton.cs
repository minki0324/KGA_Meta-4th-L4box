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
        switch (GameManager.Instance.GameMode)
        {
            case GameMode.PushPush:
                GameManager.Instance.pushPush.custom.StackPops.Push(gameObject);
                break;
            case GameMode.Memory:
                break;

        }
        Button btn = transform.GetComponent<Button>();
        btn.interactable = true;
        if (!GameManager.Instance.GameMode.Equals(GameMode.PushPush))
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
        if (GameManager.Instance.GameMode.Equals(GameMode.PushPush))
        {
            GameManager.Instance.pushPush.pushCount++;
            gameObject.GetComponent<Image>().raycastTarget = false;
        }
        else if (GameManager.Instance.GameMode.Equals(GameMode.Multi))
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
        if (clickButton.GetComponent<Button>().interactable)
        {
            AudioManager.instance.SetCommonAudioClip_SFX(4);
            GameManager.Instance.buttonActive--;
            clickButton.GetComponent<Button>().interactable = false;
        }
        GameManager.Instance.GameClear();
    }

}
