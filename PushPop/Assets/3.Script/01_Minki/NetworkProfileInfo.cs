using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NetworkProfileInfo : MonoBehaviour
{
    private Favorite favorite; // 최상단 favorite script
    public Profile profile; // SQL Manager에 저장되어 있는 Profile
    [SerializeField] private TMP_Text profileNameText; // Profile에 따른 Name
    [SerializeField] private Image profileImage; // Profile에 따른 Image
    public Image favoriteStar; // 즐겨찾기 버튼 Image

    #region Unity Callback
    private void Awake()
    {
        favorite = transform.root.GetComponent<Favorite>();
    }
    #endregion

    #region Other Method
    public void PrintInfomation()
    { // 이름, 인덱스, 별 sprite setting Method
        profileNameText.text = $"{profile.name}#{profile.index}";
        SQL_Manager.instance.PrintProfileImage(profileImage, profile.imageMode, profile.index);
        for (int i = 0; i < favorite.favoriteLists.FriendIndex.Count; i++)
        {
            if (favorite.favoriteLists.FriendIndex[i] == profile.index)
            { // 동일하다면 즐겨찾기 목록에 있음
                favoriteStar.sprite = favorite.favoriteStars[1];
            }
            else
            { // 없다면 즐겨찾기 목록에 없음
                favoriteStar.sprite = favorite.favoriteStars[0];
            }
        }
    }

    public void FavoriteStarButton()
    { // 즐겨찾기 버튼 눌렀는지 안눌렀는지 확인 Prefabs 내부 Button 연동 Method
        for(int i = 0; i < favorite.favoriteLists.FriendIndex.Count; i++)
        {
            if(favorite.favoriteLists.FriendIndex[i] == profile.index)
            { // 동일하다면 즐겨찾기 목록에 있으므로 즐겨찾기 목록에서 해제해야함
                favorite.favoriteLists.FriendIndex.Remove(favorite.favoriteLists.FriendIndex[i]);
                favoriteStar.sprite = favorite.favoriteStars[0];
            }
            else
            { // 목록에 없다면 즐겨찾기 목록에 없으므로 즐겨찾기 목록에 추가해야함
                favorite.favoriteLists.FriendIndex.Add(favorite.favoriteLists.FriendIndex[i]);
                favoriteStar.sprite = favorite.favoriteStars[1];
            }
        }
    }
    #endregion
}
