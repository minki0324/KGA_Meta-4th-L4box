using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{

    [SerializeField]
    private GameObject Slide_Panel;

    [SerializeField]
    private Button btn1;

    [SerializeField]
    private Button btn2;

    bool isclicked = false;

    private void OnEnable()
    {
        Slide_Panel.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 100);
        GetComponent<Button>().onClick.AddListener(buttonClicked);

        btn1.gameObject.SetActive(false);
        btn2.gameObject.SetActive(false);
    }

    public void buttonClicked()
    {
        if (!isclicked)
        {
            isclicked = true;
            StartCoroutine(Panel_co());

        }
        else
        {
            isclicked = false;
            Slide_Panel.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 100);
            btn1.gameObject.SetActive(false);
            btn2.gameObject.SetActive(false);

        }
    }


    private IEnumerator Panel_co()
    {
        float PanelWidth = Slide_Panel.GetComponent<RectTransform>().sizeDelta.x;
        while (true)
        {
            if (!isclicked)
            {
                Slide_Panel.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 100);
                btn1.gameObject.SetActive(false);
                btn2.gameObject.SetActive(false);
                yield break;
            }

            if (PanelWidth >= 220)
            {
                btn2.gameObject.SetActive(true);
                yield break;
            }

            if (PanelWidth >= 110 && PanelWidth < 111)
            {
                btn1.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.05f);
            }

            PanelWidth += 0.5f;
            Slide_Panel.GetComponent<RectTransform>().sizeDelta = new Vector2(PanelWidth, 100);
            Slide_Panel.GetComponent<RectTransform>().anchoredPosition = new Vector3(PanelWidth / 2, 0.5f, 0f);

            yield return new WaitForSeconds(0.00001f);
        }
    }

}