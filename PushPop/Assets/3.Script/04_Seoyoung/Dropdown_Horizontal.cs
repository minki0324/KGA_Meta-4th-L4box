using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dropdown_Horizontal : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] RectTransform rect;

    [SerializeField] Dropdown dropDown;

    [SerializeField] Text label_text;

    [SerializeField] ScrollRect template;

    private void Start()
    {
      
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("¾Æ¿À ¤µ¤²");
        template = transform.GetChild(3).GetComponent<ScrollRect>();
        //DropdownList.horizontal = true;
        //DropdownList.vertical = false;
        template.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 300f);

    }
}
