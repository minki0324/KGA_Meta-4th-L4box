using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ranking : MonoBehaviour
{
    [SerializeField] private TMP_InputField score_txt;
    public int score;
    #region Unity Callback
    #endregion

    #region Other Method
    // ���ھ� ���� btn���� �׽�Ʈ �޼ҵ�
    public void test_Set_Score()
    {
        score = int.Parse(score_txt.text.ToString());
    }
    #endregion

}
