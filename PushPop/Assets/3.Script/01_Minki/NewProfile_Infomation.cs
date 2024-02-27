using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewProfile_Infomation : MonoBehaviour
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

    public void Receive_Infomation()
    {
        GameManager.Instance.ProfileName2P = SQL_Manager.instance.Profile_list[Receive_Index()].name;
        GameManager.Instance.ProfileIndex2P = SQL_Manager.instance.Profile_list[Receive_Index()].index;
        GameManager.Instance.IsimageMode2P = SQL_Manager.instance.Profile_list[Receive_Index()].imageMode;
        if (GameManager.Instance.IsimageMode2P)
        {
            GameManager.Instance.DefaultImage2P = SQL_Manager.instance.Profile_list[Receive_Index()].defaultImage;
        }

    }

    // 프로필 선택 후 Join Btn 연동 Method
    public void Join()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        if (GameManager.Instance.gameMode == Mode.Bomb)
        { // Bomb 모드에서 2번째 Player를 선택했을 때
            /*Bomb bomb = FindObjectOfType<Bomb>();
            bomb.SelectProfile.SetActive(false);

            // 프로필 이미지 출력
            SQL_Manager.instance.PrintProfileImage(SQL_Manager.instance.Profile_list[Receive_Index()].imageMode, bomb.tempPlayerImage2P, SQL_Manager.instance.Profile_list[Receive_Index()].index);

            bomb.tempPlayerName2P.text = SQL_Manager.instance.Profile_list[Receive_Index()].name;
            bomb.CurrentProfile.SetActive(true);
            bomb.player2PInfo = this;*/
        }
        else
        { // 그 외에 모든 경우 본인의 프로필을 선택했을 때
            ProfileManager.Instance.ProfileName1P = SQL_Manager.instance.Profile_list[Receive_Index()].name;
            ProfileManager.Instance.ProfileIndex1P = SQL_Manager.instance.Profile_list[Receive_Index()].index;
            ProfileManager.Instance.UID = SQL_Manager.instance.UID;
            ProfileManager.Instance.IsImageMode1P = SQL_Manager.instance.Profile_list[Receive_Index()].imageMode;

            NewProfileCanvas profile = FindObjectOfType<NewProfileCanvas>();
            profile.SelectProfilePanel.SetActive(false);
            // 프로필 이미지 출력
            SQL_Manager.instance.PrintProfileImage(ProfileManager.Instance.IsImageMode1P, profile.SelectProfileImage, ProfileManager.Instance.ProfileIndex1P);

            profile.SelectProfileText.text = ProfileManager.Instance.ProfileName1P;
            profile.CurrnetProfilePanel.SetActive(true);
        }
    }

    // 선택한 버튼의 name, UID GameManager에 넘겨주면서 삭제 확인 PopUp창 켜주는 Method
    public void DeleteInfo()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        if (GameManager.Instance.gameMode != Mode.Bomb)
        {
            ProfileManager.Instance.ProfileName1P = SQL_Manager.instance.Profile_list[Receive_Index()].name;
            ProfileManager.Instance.ProfileIndex1P = SQL_Manager.instance.Profile_list[Receive_Index()].index;

            NewProfileCanvas profile = FindObjectOfType<NewProfileCanvas>();
            if (profile != null)  profile.DeletePanel.SetActive(true);
        }
        else if (GameManager.Instance.gameMode == Mode.Bomb)
        {
           /* Bomb bomb = FindObjectOfType<Bomb>();
            bomb.player2PInfo = this;
            bomb.deletePanel.SetActive(true);*/
        }
    }
    #endregion
}
