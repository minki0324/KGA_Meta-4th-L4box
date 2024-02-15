using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PushPopButton : MonoBehaviour
{
    private Button pushButton;
    private void Awake()
    {
        pushButton = GetComponent<Button>();
    }
    // Push Pop Button click method
    public void PushPopClick()
    {
        pushButton.interactable = false;
        PushPop.instance.activePos.Remove(gameObject);
    }
}
