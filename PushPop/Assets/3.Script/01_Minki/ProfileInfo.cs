using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileInfo : MonoBehaviour
{ // player profile prefab
    private ProfileCanvas profileCanvas = null;
    public TMP_Text ProfileName = null;
    public Image ProfileImage = null;
    public GameObject DeleteButton = null;

    public int ReceiveIndex()
    { // 본인이 몇번째 자식인지 확인하는 Method
        return gameObject.transform.GetSiblingIndex();
    }

    public void ReceiveInfo()
    { // Profile 2P 정보 Setting
        ProfileManager.Instance.ProfileName2P = SQL_Manager.instance.ProfileList[ReceiveIndex()].name;
        ProfileManager.Instance.SecondPlayerIndex = SQL_Manager.instance.ProfileList[ReceiveIndex()].index;
        ProfileManager.Instance.IsImageMode2P = SQL_Manager.instance.ProfileList[ReceiveIndex()].imageMode;
        if (ProfileManager.Instance.IsImageMode2P)
        {
            ProfileManager.Instance.DefaultImage2P = SQL_Manager.instance.ProfileList[ReceiveIndex()].defaultImage;
        }

        // select 시 temp에 저장
        ProfileManager.Instance.TempProfileName = ProfileManager.Instance.ProfileName2P;
        ProfileManager.Instance.TempUserIndex = ProfileManager.Instance.SecondPlayerIndex;
        ProfileManager.Instance.TempImageIndex = ProfileManager.Instance.DefaultImage2P;
    }

    public void Join()
    { // Profile Select 했을 시 선택된 프로필로 이동, 선택한 프로필 저장
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        profileCanvas = FindObjectOfType<ProfileCanvas>();

        Player player = ProfileManager.Instance.SelectPlayer;
        ProfileManager.Instance.UID = SQL_Manager.instance.UID;
        ProfileManager.Instance.PlayerInfo[(int)player] = new PlayerInfo(
            SQL_Manager.instance.ProfileList[ReceiveIndex()].name,
            SQL_Manager.instance.ProfileList[ReceiveIndex()].index,
            SQL_Manager.instance.ProfileList[ReceiveIndex()].defaultImage,
            SQL_Manager.instance.ProfileList[ReceiveIndex()].imageMode
            );
        // select 시 프로필 수정을 대비하여 temp에 저장
        ProfileManager.Instance.TempProfileName = SQL_Manager.instance.ProfileList[ReceiveIndex()].name;
        ProfileManager.Instance.TempUserIndex = SQL_Manager.instance.ProfileList[ReceiveIndex()].index;
        ProfileManager.Instance.TempImageIndex = SQL_Manager.instance.ProfileList[ReceiveIndex()].defaultImage;
        ProfileManager.Instance.TempImageMode = SQL_Manager.instance.ProfileList[ReceiveIndex()].imageMode;

        ProfileManager.Instance.PlayerInfo[(int)player].profileImage = ProfileManager.Instance.ProfileImageCaching();
        /*
        if (GameManager.Instance.GameMode.Equals(GameMode.Multi))
        { // 2P 선택 시
            ProfileManager.Instance.PlayerInfo[(int)Player.Player2] = new PlayerInfo(
                SQL_Manager.instance.ProfileList[ReceiveIndex()].name,
                SQL_Manager.instance.ProfileList[ReceiveIndex()].index,
                SQL_Manager.instance.ProfileList[ReceiveIndex()].defaultImage,
                SQL_Manager.instance.ProfileList[ReceiveIndex()].imageMode
                );
            ProfileManager.Instance.PlayerInfo[(int)Player.Player2].profileImage = ProfileManager.Instance.ProfileImageCaching(Player.Player2);

            ReceiveInfo(); // select 2P

            // 프로필 이미지 출력
            SQL_Manager.instance.PrintProfileImage(SQL_Manager.instance.ProfileList[ReceiveIndex()].imageMode, profileCanvas.ProfileIamge, SQL_Manager.instance.ProfileList[ReceiveIndex()].index);
            profileCanvas.ProfileText.text = ProfileManager.Instance.ProfileName2P;
            //bomb.player2PInfo = this;
        }
        else
        { // 1P 선택 시
            ProfileManager.Instance.UID = SQL_Manager.instance.UID;
            ProfileManager.Instance.PlayerInfo[(int)Player.Player1] = new PlayerInfo(
                SQL_Manager.instance.ProfileList[ReceiveIndex()].name,
                SQL_Manager.instance.ProfileList[ReceiveIndex()].index,
                SQL_Manager.instance.ProfileList[ReceiveIndex()].defaultImage,
                SQL_Manager.instance.ProfileList[ReceiveIndex()].imageMode
                );
            ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileImage = ProfileManager.Instance.ProfileImageCaching(Player.Player1);
            
            ProfileManager.Instance.ProfileName1P = SQL_Manager.instance.ProfileList[ReceiveIndex()].name;
            ProfileManager.Instance.FirstPlayerIndex = SQL_Manager.instance.ProfileList[ReceiveIndex()].index;
            ProfileManager.Instance.IsImageMode1P = SQL_Manager.instance.ProfileList[ReceiveIndex()].imageMode;
            ProfileManager.Instance.DefaultImage1P = SQL_Manager.instance.ProfileList[ReceiveIndex()].defaultImage;

            // select 시 temp에 저장
            ProfileManager.Instance.TempProfileName = ProfileManager.Instance.ProfileName1P;
            ProfileManager.Instance.TempUserIndex = ProfileManager.Instance.FirstPlayerIndex;
            ProfileManager.Instance.TempImageIndex = ProfileManager.Instance.DefaultImage1P;
        }
        */
        // 프로필 출력
        SQL_Manager.instance.PrintProfileImage(profileCanvas.ProfileImage, ProfileManager.Instance.PlayerInfo[(int)player].imageMode, ProfileManager.Instance.PlayerInfo[(int)player].playerIndex);
        profileCanvas.ProfileText.text = ProfileManager.Instance.PlayerInfo[(int)player].profileName;
        
        profileCanvas.BlockPanel.SetActive(true);
        profileCanvas.CurrentProfile.SetActive(true);
        profileCanvas.Select.SetActive(false);
    }

    // 선택한 버튼의 name, UID GameManager에 넘겨주면서 삭제 확인 PopUp창 켜주는 Method
    public void DeleteInfo()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        profileCanvas = FindObjectOfType<ProfileCanvas>();

        ProfileManager.Instance.TempProfileName = SQL_Manager.instance.ProfileList[ReceiveIndex()].name; // 왜 넘겨주는 지 모르겠음 ... todo
        ProfileManager.Instance.TempUserIndex = SQL_Manager.instance.ProfileList[ReceiveIndex()].index;

        if (profileCanvas != null) profileCanvas.DeletePanel.SetActive(true);
    }
}
