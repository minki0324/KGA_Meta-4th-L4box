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
    { // ������ ���° �ڽ����� Ȯ���ϴ� Method
        return gameObject.transform.GetSiblingIndex();
    }

    public void Join()
    { // Profile Select ���� �� ���õ� �����ʷ� �̵�, ������ ������ ����
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

        // select �� ������ ������ ����Ͽ� temp�� ����
        ProfileManager.Instance.TempProfileName = SQL_Manager.instance.ProfileList[ReceiveIndex()].name;
        ProfileManager.Instance.TempUserIndex = SQL_Manager.instance.ProfileList[ReceiveIndex()].index;
        ProfileManager.Instance.TempImageIndex = SQL_Manager.instance.ProfileList[ReceiveIndex()].defaultImage;
        ProfileManager.Instance.TempImageMode = SQL_Manager.instance.ProfileList[ReceiveIndex()].imageMode;
        ProfileManager.Instance.PlayerInfo[(int)player].profileImage = ProfileManager.Instance.ProfileImageCaching();
        
        // ������ ���
        SQL_Manager.instance.PrintProfileImage(profileCanvas.ProfileImage, ProfileManager.Instance.PlayerInfo[(int)player].imageMode, ProfileManager.Instance.PlayerInfo[(int)player].playerIndex);
        profileCanvas.ProfileText.text = ProfileManager.Instance.PlayerInfo[(int)player].profileName;
        
        profileCanvas.BlockPanel.SetActive(true);
        profileCanvas.CurrentProfile.SetActive(true);
        profileCanvas.BackButton.SetActive(false);
        profileCanvas.Select.SetActive(false);
    }

    // ������ ��ư�� name, UID GameManager�� �Ѱ��ָ鼭 ���� Ȯ�� PopUpâ ���ִ� Method
    public void DeleteInfo()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        profileCanvas = FindObjectOfType<ProfileCanvas>();

        ProfileManager.Instance.TempProfileName = SQL_Manager.instance.ProfileList[ReceiveIndex()].name; // �� �Ѱ��ִ� �� �𸣰��� ... todo
        ProfileManager.Instance.TempUserIndex = SQL_Manager.instance.ProfileList[ReceiveIndex()].index;

        if (profileCanvas != null) profileCanvas.DeletePanel.SetActive(true);
    }
}
