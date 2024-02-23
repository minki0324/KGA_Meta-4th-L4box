using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ������ �̸��� ���Ӱ��� Class
/// </summary>
public class Profile_Information : MonoBehaviour
{
    public TMP_Text Profile_name;
    public Image ProfileImage;
    public GameObject DelBtn;

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
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        if (GameManager.Instance.gameMode == Mode.Bomb)
        { // Bomb ��忡�� 2��° Player�� �������� ��
            GameManager.Instance.ProfileName2P = SQL_Manager.instance.Profile_list[Receive_Index()].name;
            GameManager.Instance.ProfileIndex2P = SQL_Manager.instance.Profile_list[Receive_Index()].index;
            GameManager.Instance.IsimageMode2P = SQL_Manager.instance.Profile_list[Receive_Index()].imageMode;

            Bomb bomb = FindObjectOfType<Bomb>();
            bomb.SelectProfile.SetActive(false);

            if(!GameManager.Instance.IsimageMode2P)
            { // ���� ��⸦ ������ �÷��̾�
                Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(GameManager.Instance.UID, GameManager.Instance.ProfileIndex2P);
                Sprite profileSprite = GameManager.Instance.TextureToSprite(profileTexture);
                bomb.tempPlayerImage2P.sprite = profileSprite;
            }
            else if(GameManager.Instance.IsimageMode2P)
            { // �̹��� ���⸦ ������ �÷��̾�
                GameManager.Instance.DefaultImage2P = SQL_Manager.instance.Profile_list[Receive_Index()].defaultImage;
                Sprite profileSprite = GameManager.Instance.ProfileImages[SQL_Manager.instance.Profile_list[Receive_Index()].defaultImage];
                bomb.tempPlayerImage2P.sprite = profileSprite;
            }
            bomb.tempPlayerName2P.text = GameManager.Instance.ProfileName2P;
            bomb.CurrentProfile.SetActive(true);
        }
        else
        { // �� �ܿ� ��� ��� ������ �������� �������� ��
            GameManager.Instance.ProfileName = SQL_Manager.instance.Profile_list[Receive_Index()].name;
            GameManager.Instance.ProfileIndex = SQL_Manager.instance.Profile_list[Receive_Index()].index;
            GameManager.Instance.UID = SQL_Manager.instance.UID;
            GameManager.Instance.IsImageMode = SQL_Manager.instance.Profile_list[Receive_Index()].imageMode;

            Profile_ profile = FindObjectOfType<Profile_>();
            profile.SelectProfilePanel.SetActive(false);
            if (!GameManager.Instance.IsImageMode)
            { // ���� ��⸦ ������ �÷��̾�
                Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(GameManager.Instance.UID, GameManager.Instance.ProfileIndex);
                Sprite profileSprite = GameManager.Instance.TextureToSprite(profileTexture);
                profile._profileImage.sprite = profileSprite;
            }
            else if (GameManager.Instance.IsImageMode)
            { // �̹��� ���⸦ ������ �÷��̾�
                GameManager.Instance.DefaultImage = SQL_Manager.instance.Profile_list[Receive_Index()].defaultImage;
                Sprite profileSprite = GameManager.Instance.ProfileImages[SQL_Manager.instance.Profile_list[Receive_Index()].defaultImage];
                profile._profileImage.sprite = profileSprite;
            }
            profile.SelectName.text = GameManager.Instance.ProfileName;
            profile.CurrnetProfilePanel.SetActive(true);
        }
    }

    // ������ ��ư�� name, UID GameManager�� �Ѱ��ָ鼭 ���� Ȯ�� PopUpâ ���ִ� Method
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
