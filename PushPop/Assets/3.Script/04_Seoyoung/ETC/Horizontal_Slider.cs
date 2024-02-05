using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Horizontal_Slider : MonoBehaviour
{

    [SerializeField]
    private GameObject Slide_Panel;

    private void OnEnable()
    {
        Slide_Panel.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 100);
        //버튼 눌리면 코루틴 실행
        StartCoroutine(Panel_co());
    }


    private IEnumerator Panel_co()
    {
        while(true)
        {

        }
    }
}
