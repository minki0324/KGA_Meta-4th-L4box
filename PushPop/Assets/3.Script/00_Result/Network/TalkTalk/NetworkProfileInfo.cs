using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NetworkProfileInfo : MonoBehaviour
{
    public Favorite favorite; // 최상단 favorite script
    public MoaMoaManager moamoa;
    public Profile profile; // SQL Manager에 저장되어 있는 Profile
    [SerializeField] private TMP_Text profileNameText; // Profile에 따른 Name
    [SerializeField] private Image profileImage; // Profile에 따른 Image
    [SerializeField] private Image favoriteStar; // 즐겨찾기 버튼 Image

    #region Other Method
    public void PrintInfomation()
    { // 이름, 인덱스, 별 sprite setting Method
        // name#profileIndex 세팅
        profileNameText.text = $"{profile.name}#{profile.index}";

        // profileIndex에 맞는 Image 세팅
        SQL_Manager.instance.PrintProfileImage(profileImage, profile.imageMode, profile.index);
        
        // 즐겨찾기 별 아이콘 세팅
        if(favorite.favoriteLists != null && favorite.favoriteLists.FriendIndex.Count > 0)
        { // favoriteList가 있는데 count가 0이면 즐겨찾기를 등록했었다가 삭제해서 현재 없는 상태
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
        else
        {
            Debug.Log("즐겨찾기를 등록하지 않았습니다.");
        }
    }

    public void FavoriteStarButton()
    { // 즐겨찾기 버튼 눌렀는지 안눌렀는지 확인 Prefabs 내부 Button 연동 Method
        if(favorite.favoriteLists == null)
        { // favoriteList가 null이라면 처음 즐겨찾기를 등록하는 유저이기 때문에 새롭게 생성
            favorite.favoriteLists = new FavoriteList(new List<int> { profile.index});
            favoriteStar.sprite = favorite.favoriteStars[1];
            return;
        }
        else if(favorite.favoriteLists.FriendIndex.Count == 0)
        { // favorite이 있지만 count가 0이라면 즐겨찾기를 등록했다가 삭제한 유저이기 때문에 추가 해야함
            favorite.favoriteLists.FriendIndex.Add(profile.index);
            favoriteStar.sprite = favorite.favoriteStars[1];
            return;
        }

        for (int i = 0; i < favorite.favoriteLists.FriendIndex.Count; i++)
        {
            if(favorite.favoriteLists.FriendIndex[i] == profile.index)
            { // 동일하다면 즐겨찾기 목록에 있으므로 즐겨찾기 목록에서 해제해야함
                favorite.favoriteLists.FriendIndex.Remove(favorite.favoriteLists.FriendIndex[i]);
                favoriteStar.sprite = favorite.favoriteStars[0];
            }
            else
            {
                Debug.Log("아무것도 안들어 올 때");
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
