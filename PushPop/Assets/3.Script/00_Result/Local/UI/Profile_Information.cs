using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ������ �̸��� ���Ӱ��� ��ũ��Ʈ
/// </summary>
public class Profile_Information : MonoBehaviour
{
    public TMP_Text Profile_name;

    #region Unity Callback
    #endregion

    #region Other Method
    // ������ ���° �ڽ����� Ȯ���ϴ� Method
    public int Receive_Index()
    {
        return gameObject.transform.GetSiblingIndex();
    }

    // ������ ���� �� Join Btn ���� Method
    public void Join()
    {
        GameManager.instance.Profile_name = SQL_Manager.instance.Profile_list[Receive_Index()].name;
        GameManager.instance.Profile_Index = SQL_Manager.instance.Profile_list[Receive_Index()].index;
        GameManager.instance.UID = SQL_Manager.instance.info.UID;

        Profile_ profile = FindObjectOfType<Profile_>();
        profile.selectProfile_Panel.SetActive(false);
        profile.Select_Name.text = GameManager.instance.Profile_name;
        profile.currnetProfile_Panel.SetActive(true);
    }
    #endregion
}
