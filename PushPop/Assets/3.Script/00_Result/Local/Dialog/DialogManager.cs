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
    /// Dialog ����ϴ� Method.
    /// �Ű� ������ Index�� text�� �Է��ϸ� Dialog S.O�� EventID�� �´� Log ���
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
