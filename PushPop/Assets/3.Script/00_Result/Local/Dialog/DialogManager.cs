using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    public Coroutine log;

    #region Unity Callback
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    #endregion

    #region Other Method
    /// <summary>
    /// Dialog 출력하는 Method.
    /// 매개 변수로 Index와 text를 입력하면 Dialog S.O의 EventID에 맞는 Log 출력
    /// </summary>
    public IEnumerator Print_Dialog(TMP_Text text, string log)
    {
        text.gameObject.SetActive(true);
        text.text = log;

        yield return new WaitForSeconds(3f);

        text.gameObject.SetActive(false);

        log = null;
    }
    #endregion

    /* dialog index
         0. 
    */
}
