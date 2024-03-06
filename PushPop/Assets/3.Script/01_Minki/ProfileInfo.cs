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

    public void ReceiveInfo()
    { // Profile 2P ���� Setting
        ProfileManager.Instance.ProfileName2P = SQL_Manager.instance.ProfileList[ReceiveIndex()].name;
        ProfileManager.Instance.SecondPlayerIndex = SQL_Manager.instance.ProfileList[ReceiveIndex()].index;
        ProfileManager.Instance.IsImageMode2P = SQL_Manager.instance.ProfileList[ReceiveIndex()].imageMode;
        if (ProfileManager.Instance.IsImageMode2P)
        {
            ProfileManager.Instance.DefaultImage2P = SQL_Manager.instance.ProfileList[ReceiveIndex()].defaultImage;
        }

        // select �� temp�� ����
        ProfileManager.Instance.TempProfileName = ProfileManager.Instance.ProfileName2P;
        ProfileManager.Instance.TempUserIndex = ProfileManager.Instance.SecondPlayerIndex;
        ProfileManager.Instance.TempImageIndex = ProfileManager.Instance.DefaultImage2P;
    }

    public void Join()
    { // Profile Select ���� �� ���õ� �����ʷ� �̵�
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        profileCanvas = FindObjectOfType<ProfileCanvas>();

        if (GameManager.Instance.GameMode.Equals(GameMode.Multi))
        { // 2P ���� ��
            ReceiveInfo(); // select 2P

            // ������ �̹��� ���
            SQL_Manager.instance.PrintProfileImage(SQL_Manager.instance.ProfileList[ReceiveIndex()].imageMode, profileCanvas.ProfileIamge, SQL_Manager.instance.ProfileList[ReceiveIndex()].index);
            profileCanvas.ProfileText.text = ProfileManager.Instance.ProfileName2P;

            //bomb.player2PInfo = this;
        }
        else
        { // 1P ���� ��
            ProfileManager.Instance.UID = SQL_Manager.instance.UID;
            ProfileManager.Instance.ProfileName1P = SQL_Manager.instance.ProfileList[ReceiveIndex()].name;
            ProfileManager.Instance.FirstPlayerIndex = SQL_Manager.instance.ProfileList[ReceiveIndex()].index;
            ProfileManager.Instance.IsImageMode1P = SQL_Manager.instance.ProfileList[ReceiveIndex()].imageMode;
            ProfileManager.Instance.DefaultImage1P = SQL_Manager.instance.ProfileList[ReceiveIndex()].defaultImage;
            
            // select �� temp�� ����
            ProfileManager.Instance.TempProfileName = ProfileManager.Instance.ProfileName1P;
            ProfileManager.Instance.TempUserIndex = ProfileManager.Instance.FirstPlayerIndex;
            ProfileManager.Instance.TempImageIndex = ProfileManager.Instance.DefaultImage1P;
        }

        // ������ �̹��� ���
        SQL_Manager.instance.PrintProfileImage(ProfileManager.Instance.IsImageMode1P, profileCanvas.ProfileIamge, ProfileManager.Instance.FirstPlayerIndex);
        profileCanvas.ProfileText.text = ProfileManager.Instance.ProfileName1P;
        
        profileCanvas.BlockPanel.SetActive(true);
        profileCanvas.CurrentProfile.SetActive(true);

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
