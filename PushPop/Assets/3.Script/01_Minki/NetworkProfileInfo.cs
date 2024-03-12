using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NetworkProfileInfo : MonoBehaviour
{
    private Favorite favorite; // �ֻ�� favorite script
    public Profile profile; // SQL Manager�� ����Ǿ� �ִ� Profile
    [SerializeField] private TMP_Text profileNameText; // Profile�� ���� Name
    [SerializeField] private Image profileImage; // Profile�� ���� Image
    public Image favoriteStar; // ���ã�� ��ư Image

    #region Unity Callback
    private void Awake()
    {
        favorite = transform.root.GetComponent<Favorite>();
    }
    #endregion

    #region Other Method
    public void PrintInfomation()
    { // �̸�, �ε���, �� sprite setting Method
        profileNameText.text = $"{profile.name}#{profile.index}";
        SQL_Manager.instance.PrintProfileImage(profileImage, profile.imageMode, profile.index);
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

    public void FavoriteStarButton()
    { // ���ã�� ��ư �������� �ȴ������� Ȯ�� Prefabs ���� Button ���� Method
        for(int i = 0; i < favorite.favoriteLists.FriendIndex.Count; i++)
        {
            if(favorite.favoriteLists.FriendIndex[i] == profile.index)
            { // �����ϴٸ� ���ã�� ��Ͽ� �����Ƿ� ���ã�� ��Ͽ��� �����ؾ���
                favorite.favoriteLists.FriendIndex.Remove(favorite.favoriteLists.FriendIndex[i]);
                favoriteStar.sprite = favorite.favoriteStars[0];
            }
            else
            { // ��Ͽ� ���ٸ� ���ã�� ��Ͽ� �����Ƿ� ���ã�� ��Ͽ� �߰��ؾ���
                favorite.favoriteLists.FriendIndex.Add(favorite.favoriteLists.FriendIndex[i]);
                favoriteStar.sprite = favorite.favoriteStars[1];
            }
        }
    }
    #endregion
}
