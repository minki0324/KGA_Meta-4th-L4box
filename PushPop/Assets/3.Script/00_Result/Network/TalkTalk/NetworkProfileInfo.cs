using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NetworkProfileInfo : MonoBehaviour
{
    public Favorite favorite; // �ֻ�� favorite script
    public MoaMoaManager moamoa;
    public Profile profile; // SQL Manager�� ����Ǿ� �ִ� Profile
    [SerializeField] private TMP_Text profileNameText; // Profile�� ���� Name
    [SerializeField] private Image profileImage; // Profile�� ���� Image
    [SerializeField] private Image favoriteStar; // ���ã�� ��ư Image

    #region Other Method
    public void PrintInfomation()
    { // �̸�, �ε���, �� sprite setting Method
        // name#profileIndex ����
        profileNameText.text = $"{profile.name}#{profile.index}";

        // profileIndex�� �´� Image ����
        SQL_Manager.instance.PrintProfileImage(profileImage, profile.imageMode, profile.index);
        
        // ���ã�� �� ������ ����
        if(favorite.favoriteLists != null && favorite.favoriteLists.FriendIndex.Count > 0)
        { // favoriteList�� �ִµ� count�� 0�̸� ���ã�⸦ ����߾��ٰ� �����ؼ� ���� ���� ����
            for (int i = 0; i < favorite.favoriteLists.FriendIndex.Count; i++)
            { 
                if (favorite.favoriteLists.FriendIndex[i] == profile.index)
                { // �����ϴٸ� ���ã�� ��Ͽ� ����
                    favoriteStar.sprite = favorite.favoriteStars[1];
                }
                else
                { // ���ٸ� ���ã�� ��Ͽ� ����
                    favoriteStar.sprite = favorite.favoriteStars[0];
                }
            }
        }
        else
        {
            Debug.Log("���ã�⸦ ������� �ʾҽ��ϴ�.");
        }
    }

    public void FavoriteStarButton()
    { // ���ã�� ��ư �������� �ȴ������� Ȯ�� Prefabs ���� Button ���� Method
        if(favorite.favoriteLists == null)
        { // favoriteList�� null�̶�� ó�� ���ã�⸦ ����ϴ� �����̱� ������ ���Ӱ� ����
            favorite.favoriteLists = new FavoriteList(new List<int> { profile.index});
            favoriteStar.sprite = favorite.favoriteStars[1];
            return;
        }
        else if(favorite.favoriteLists.FriendIndex.Count == 0)
        { // favorite�� ������ count�� 0�̶�� ���ã�⸦ ����ߴٰ� ������ �����̱� ������ �߰� �ؾ���
            favorite.favoriteLists.FriendIndex.Add(profile.index);
            favoriteStar.sprite = favorite.favoriteStars[1];
            return;
        }

        for (int i = 0; i < favorite.favoriteLists.FriendIndex.Count; i++)
        {
            if(favorite.favoriteLists.FriendIndex[i] == profile.index)
            { // �����ϴٸ� ���ã�� ��Ͽ� �����Ƿ� ���ã�� ��Ͽ��� �����ؾ���
                favorite.favoriteLists.FriendIndex.Remove(favorite.favoriteLists.FriendIndex[i]);
                favoriteStar.sprite = favorite.favoriteStars[0];
            }
            else
            {
                Debug.Log("�ƹ��͵� �ȵ�� �� ��");
            }
        }
    }

    public void MoaMoaButton()
    {
        moamoa.CurrentProfileIndex = profile.index;
        moamoa.CurrentProfileName = $"{profile.name}#{profile.index}";
        moamoa.MoaMoaPanelActive();
        moamoa.pushObjs = SQL_Manager.instance.SQL_SetPushPush(profile.index);
        moamoa.SetMoaMoaList();
        moamoa.UpdateLikeAndHeartCount();
    }
    #endregion
}
