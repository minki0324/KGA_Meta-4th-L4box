using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PushPopButton : MonoBehaviour
{    
    // Push Pop Button click method
    public void PushPopClick()
    {
        GameObject clickButton = EventSystem.current.currentSelectedGameObject;
        clickButton.SetActive(false);
        PushPop.instance.activePos.Remove(clickButton);
    }
}
