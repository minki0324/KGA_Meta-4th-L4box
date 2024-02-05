using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 프로필 이름과 접속관련 스크립트
/// </summary>
public class Profile_Information : MonoBehaviour
{
    public TMP_Text Profile_name;

    #region Unity Callback
    #endregion

    #region Other Method
    // 본인이 몇번째 자식인지 확인하는 Method
    public int Receive_Index()
    {
        return gameObject.transform.GetSiblingIndex();
    }

    // 프로필 선택 후 Join Btn 연동 Method
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
