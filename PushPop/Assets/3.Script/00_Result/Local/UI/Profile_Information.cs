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

    // ������ ���� �� Join Btn ���� Method
    public void Join()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        if (GameManager.Instance.gameMode == Mode.Bomb)
        { // Bomb ��忡�� 2��° Player�� �������� ��
            Bomb bomb = FindObjectOfType<Bomb>();
            bomb.SelectProfile.SetActive(false);

            // ������ �̹��� ���
            SQL_Manager.instance.PrintProfileImage(SQL_Manager.instance.Profile_list[Receive_Index()].imageMode, bomb.tempPlayerImage2P, SQL_Manager.instance.Profile_list[Receive_Index()].index);
           
            bomb.tempPlayerName2P.text = SQL_Manager.instance.Profile_list[Receive_Index()].name;
            bomb.CurrentProfile.SetActive(true);
            bomb.player2PInfo = this;
        }
        else
        { // �� �ܿ� ��� ��� ������ �������� �������� ��
            GameManager.Instance.UID = SQL_Manager.instance.UID;
            GameManager.Instance.ProfileName = SQL_Manager.instance.Profile_list[Receive_Index()].name;
            GameManager.Instance.ProfileIndex = SQL_Manager.instance.Profile_list[Receive_Index()].index;
            GameManager.Instance.IsImageMode = SQL_Manager.instance.Profile_list[Receive_Index()].imageMode;
            GameManager.Instance.DefaultImage = SQL_Manager.instance.Profile_list[Receive_Index()].defaultImage;

            Profile_ profile = FindObjectOfType<Profile_>();
            profile.SelectProfilePanel.SetActive(false);
            // ������ �̹��� ���
            SQL_Manager.instance.PrintProfileImage(GameManager.Instance.IsImageMode, profile.ProfileImage, GameManager.Instance.ProfileIndex);
           
            profile.SelectName.text = GameManager.Instance.ProfileName;
            profile.CurrnetProfilePanel.SetActive(true);
        }
    }

    // ������ ��ư�� name, UID GameManager�� �Ѱ��ָ鼭 ���� Ȯ�� PopUpâ ���ִ� Method
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
