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
        GameManager.Instance.ProfileName = SQL_Manager.instance.Profile_list[Receive_Index()].name;
        GameManager.Instance.ProfileIndex = SQL_Manager.instance.Profile_list[Receive_Index()].index;
        GameManager.Instance.UID = SQL_Manager.instance.UID;

        Profile_ profile = FindObjectOfType<Profile_>();
        profile.SelectProfilePanel.SetActive(false);
        if(!GameManager.Instance.IsImageMode)  // ������� ���ø��
        {
            Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(GameManager.Instance.UID, GameManager.Instance.ProfileIndex);
            Sprite profileSprite = GameManager.Instance.TextureToSprite(profileTexture);
            profile._profileImage.sprite = profileSprite;
        }
        else if(GameManager.Instance.IsImageMode)  // �̹��� ���� ���
        {
            GameManager.Instance.DefaultImage = SQL_Manager.instance.Profile_list[Receive_Index()].defaultImage;
            Sprite profileSprite = GameManager.Instance.ProfileImages[SQL_Manager.instance.Profile_list[Receive_Index()].defaultImage];
            profile._profileImage.sprite = profileSprite;
        }
        profile.SelectName.text = GameManager.Instance.ProfileName;
        profile.CurrnetProfilePanel.SetActive(true);
    }

    // ������ ��ư�� name, UID GameManager�� �Ѱ��ָ鼭 ���� Ȯ�� PopUpâ ���ִ� Method
    public void DeleteInfo()
    {
        GameManager.Instance.ProfileName = SQL_Manager.instance.Profile_list[Receive_Index()].name;
        GameManager.Instance.ProfileIndex = SQL_Manager.instance.Profile_list[Receive_Index()].index;
        GameManager.Instance.UID = SQL_Manager.instance.UID;

        Profile_ profile = FindObjectOfType<Profile_>();
        profile._deletePanel.SetActive(true);
    }
    #endregion
}
