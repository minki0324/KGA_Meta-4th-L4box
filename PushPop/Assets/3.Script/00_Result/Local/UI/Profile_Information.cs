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

    // 프로필 선택 후 Join Btn 연동 Method
    public void Join()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        if (GameManager.Instance.gameMode == Mode.Bomb)
        { // Bomb 모드에서 2번째 Player를 선택했을 때
            GameManager.Instance.ProfileName2P = SQL_Manager.instance.Profile_list[Receive_Index()].name;
            GameManager.Instance.ProfileIndex2P = SQL_Manager.instance.Profile_list[Receive_Index()].index;
            GameManager.Instance.IsimageMode2P = SQL_Manager.instance.Profile_list[Receive_Index()].imageMode;

            Bomb bomb = FindObjectOfType<Bomb>();
            bomb.SelectProfile.SetActive(false);

            if(!GameManager.Instance.IsimageMode2P)
            { // 사진 찍기를 선택한 플레이어
                Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(GameManager.Instance.UID, GameManager.Instance.ProfileIndex2P);
                Sprite profileSprite = GameManager.Instance.TextureToSprite(profileTexture);
                bomb.tempPlayerImage2P.sprite = profileSprite;
            }
            else if(GameManager.Instance.IsimageMode2P)
            { // 이미지 고르기를 선택한 플레이어
                GameManager.Instance.DefaultImage2P = SQL_Manager.instance.Profile_list[Receive_Index()].defaultImage;
                Sprite profileSprite = GameManager.Instance.ProfileImages[SQL_Manager.instance.Profile_list[Receive_Index()].defaultImage];
                bomb.tempPlayerImage2P.sprite = profileSprite;
            }
            bomb.tempPlayerName2P.text = GameManager.Instance.ProfileName2P;
            bomb.CurrentProfile.SetActive(true);
        }
        else
        { // 그 외에 모든 경우 본인의 프로필을 선택했을 때
            GameManager.Instance.ProfileName = SQL_Manager.instance.Profile_list[Receive_Index()].name;
            GameManager.Instance.ProfileIndex = SQL_Manager.instance.Profile_list[Receive_Index()].index;
            GameManager.Instance.UID = SQL_Manager.instance.UID;
            GameManager.Instance.IsImageMode = SQL_Manager.instance.Profile_list[Receive_Index()].imageMode;

            Profile_ profile = FindObjectOfType<Profile_>();
            profile.SelectProfilePanel.SetActive(false);
            if (!GameManager.Instance.IsImageMode)
            { // 사진 찍기를 선택한 플레이어
                Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(GameManager.Instance.UID, GameManager.Instance.ProfileIndex);
                Sprite profileSprite = GameManager.Instance.TextureToSprite(profileTexture);
                profile._profileImage.sprite = profileSprite;
            }
            else if (GameManager.Instance.IsImageMode)
            { // 이미지 고르기를 선택한 플레이어
                GameManager.Instance.DefaultImage = SQL_Manager.instance.Profile_list[Receive_Index()].defaultImage;
                Sprite profileSprite = GameManager.Instance.ProfileImages[SQL_Manager.instance.Profile_list[Receive_Index()].defaultImage];
                profile._profileImage.sprite = profileSprite;
            }
            profile.SelectName.text = GameManager.Instance.ProfileName;
            profile.CurrnetProfilePanel.SetActive(true);
        }
    }

    // 선택한 버튼의 name, UID GameManager에 넘겨주면서 삭제 확인 PopUp창 켜주는 Method
    public void DeleteInfo()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        GameManager.Instance.ProfileName = SQL_Manager.instance.Profile_list[Receive_Index()].name;
        GameManager.Instance.ProfileIndex = SQL_Manager.instance.Profile_list[Receive_Index()].index;
        GameManager.Instance.UID = SQL_Manager.instance.UID;

        Profile_ profile = FindObjectOfType<Profile_>();
        Bomb bomb = FindObjectOfType<Bomb>();
        if(profile != null)
        {
            profile._deletePanel.SetActive(true);
        }
        else if(bomb != null)
        {
            bomb.deletePanel.SetActive(true);
        }
    }
    #endregion
}
