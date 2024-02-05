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
    /// Dialog ����ϴ� Method.
    /// �Ű� ������ Index�� text�� �Է��ϸ� Dialog S.O�� EventID�� �´� Log ���
    /// </summary>
    public void Print_Dialog(int index, TMP_Text text)
    {

    }
    #endregion

    /* dialog index
         0. 
    */
}
