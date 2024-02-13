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
    public Image ProfileImage;
    public GameObject DelBtn;

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
        GameManager.instance.UID = SQL_Manager.instance.UID;

        Profile_ profile = FindObjectOfType<Profile_>();
        profile.SelectProfilePanel.SetActive(false);
        if(!GameManager.instance._isImageMode)  // 사진찍기 선택모드
        {
            Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(GameManager.instance.UID, GameManager.instance.Profile_Index);
            Sprite profileSprite = GameManager.instance.TextureToSprite(profileTexture);
            profile._profileImage.sprite = profileSprite;
        }
        else if(GameManager.instance._isImageMode)  // 이미지 선택 모드
        {
            GameManager.instance.DefaultImage = SQL_Manager.instance.Profile_list[Receive_Index()].defaultImage;
            Sprite profileSprite = GameManager.instance.ProfileImages[SQL_Manager.instance.Profile_list[Receive_Index()].defaultImage];
            profile._profileImage.sprite = profileSprite;
        }
        profile.SelectName.text = GameManager.instance.Profile_name;
        profile.CurrnetProfilePanel.SetActive(true);
    }

    // 선택한 버튼의 name, UID GameManager에 넘겨주면서 삭제 확인 PopUp창 켜주는 Method
    public void DeleteInfo()
    {
        GameManager.instance.Profile_name = SQL_Manager.instance.Profile_list[Receive_Index()].name;
        GameManager.instance.Profile_Index = SQL_Manager.instance.Profile_list[Receive_Index()].index;
        GameManager.instance.UID = SQL_Manager.instance.UID;

        Profile_ profile = FindObjectOfType<Profile_>();
        profile._deletePanel.SetActive(true);
    }
    #endregion
}
