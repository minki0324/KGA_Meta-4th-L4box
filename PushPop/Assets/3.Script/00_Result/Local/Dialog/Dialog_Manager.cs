using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog_Manager : MonoBehaviour
{
    public static Dialog_Manager instance;

    #region Unity Callback
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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
    public void Print_Dialog(int index, TMP_Text text)
    {

    }
    #endregion

    /* dialog index
         0. 
    */
}
