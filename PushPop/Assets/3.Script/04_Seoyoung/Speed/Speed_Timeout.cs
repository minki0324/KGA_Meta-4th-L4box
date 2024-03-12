using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Speed_Timeout : MonoBehaviour
{
    [SerializeField] private Canvas main_Canvas;
    [SerializeField] private SelectListSetting speed_Canvas;


    private RectTransform size;
    [SerializeField] TMP_Text Timeout_Text;


    #region Unity Callback
    private void OnEnable()
    {
        size = GetComponent<RectTransform>();
        Timeout_Text.gameObject.SetActive(false);
        size.sizeDelta = new Vector2(0f, 300f);

        StartCoroutine(Timeout_co());
    }

    #endregion

    #region Other Method

    private IEnumerator Timeout_co()
    {
        float cashing = 0.01f;
        float x = 0;
        
        while(true)
        {
            x += 50f;
            size.sizeDelta = new Vector2(x, 300f);

            if (x >= 1920)
            {
                Timeout_Text.gameObject.SetActive(true);
                yield return new WaitForSeconds(2f);

                BacktoMain();
                yield break;
            }
            yield return new WaitForSeconds(cashing);
        }
    }


    public void BacktoMain()
    {
        main_Canvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
        speed_Canvas.gameObject.SetActive(false);
    }
    #endregion
}
