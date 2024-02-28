using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 프로필 이름과 접속관련 Class
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

    public void Receive_Infomation()
    {
        GameManager.Instance.ProfileName2P = SQL_Manager.instance.Profile_list[Receive_Index()].name;
        GameManager.Instance.ProfileIndex2P = SQL_Manager.instance.Profile_list[Receive_Index()].index;
        GameManager.Instance.IsimageMode2P = SQL_Manager.instance.Profile_list[Receive_Index()].imageMode;
        if(GameManager.Instance.IsimageMode2P)
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
            Bomb bomb = FindObjectOfType<Bomb>();
            bomb.SelectProfile.SetActive(false);

            // 프로필 이미지 출력
            SQL_Manager.instance.PrintProfileImage(SQL_Manager.instance.Profile_list[Receive_Index()].imageMode, bomb.tempPlayerImage2P, SQL_Manager.instance.Profile_list[Receive_Index()].index);
           
            bomb.tempPlayerName2P.text = SQL_Manager.instance.Profile_list[Receive_Index()].name;
            bomb.CurrentProfile.SetActive(true);
            bomb.player2PInfo = this;
        }
        else
        { // 그 외에 모든 경우 본인의 프로필을 선택했을 때
            GameManager.Instance.UID = SQL_Manager.instance.UID;
            GameManager.Instance.ProfileName = SQL_Manager.instance.Profile_list[Receive_Index()].name;
            GameManager.Instance.ProfileIndex = SQL_Manager.instance.Profile_list[Receive_Index()].index;
            GameManager.Instance.IsImageMode = SQL_Manager.instance.Profile_list[Receive_Index()].imageMode;
            GameManager.Instance.DefaultImage = SQL_Manager.instance.Profile_list[Receive_Index()].defaultImage;

            Profile_ profile = FindObjectOfType<Profile_>();
            profile.SelectProfilePanel.SetActive(false);
            // 프로필 이미지 출력
            SQL_Manager.instance.PrintProfileImage(GameManager.Instance.IsImageMode, profile.ProfileImage, GameManager.Instance.ProfileIndex);
           
            profile.SelectName.text = GameManager.Instance.ProfileName;
            profile.CurrnetProfilePanel.SetActive(true);
        }
    }

    // 선택한 버튼의 name, UID GameManager에 넘겨주면서 삭제 확인 PopUp창 켜주는 Method
    public void DeleteInfo()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        if(GameManager.Instance.gameMode != Mode.Bomb)
        {
            GameManager.Instance.ProfileName = SQL_Manager.instance.Profile_list[Receive_Index()].name;
            GameManager.Instance.ProfileIndex = SQL_Manager.instance.Profile_list[Receive_Index()].index;
            GameManager.Instance.UID = SQL_Manager.instance.UID;

            Profile_ profile = FindObjectOfType<Profile_>();
            if (profile != null)
            {
                profile.DeletePanel.SetActive(true);
            }
        }
        else if(GameManager.Instance.gameMode == Mode.Bomb)
        {
            Bomb bomb = FindObjectOfType<Bomb>();
            bomb.player2PInfo = this;
            bomb.deletePanel.SetActive(true);
        }
    }
    #endregion
}
