using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileInfo : MonoBehaviour
{ // player profile prefab
    private ProfileCanvas profileCanvas;
    public TMP_Text ProfileName;
    public Image ProfileImage;
    public GameObject DeleteButton;

    public int ReceiveIndex()
    { // ������ ���° �ڽ����� Ȯ���ϴ� Method
        return gameObject.transform.GetSiblingIndex();
    }

    public void Join()
    { // Profile Select ���� �� ���õ� �����ʷ� �̵�, ������ ������ ����
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        profileCanvas = FindObjectOfType<ProfileCanvas>();
        
        // select �� ������ ������ ����Ͽ� temp�� ����
        ProfileManager.Instance.TempProfileName = SQL_Manager.instance.ProfileList[ReceiveIndex()].name;
        ProfileManager.Instance.TempUserIndex = SQL_Manager.instance.ProfileList[ReceiveIndex()].index;
        ProfileManager.Instance.TempImageIndex = SQL_Manager.instance.ProfileList[ReceiveIndex()].defaultImage;
        ProfileManager.Instance.TempImageMode = SQL_Manager.instance.ProfileList[ReceiveIndex()].imageMode;
        
        // ������ ���
        SQL_Manager.instance.PrintProfileImage(profileCanvas.ProfileImage, ProfileManager.Instance.TempImageMode, ProfileManager.Instance.TempUserIndex);
        profileCanvas.ProfileText.text = ProfileManager.Instance.TempProfileName;
        
        profileCanvas.BlockPanel.SetActive(true);
        profileCanvas.CurrentProfile.SetActive(true);
        profileCanvas.BackButton.SetActive(false);
        profileCanvas.Select.SetActive(false);
    }

    // ������ ��ư�� name, UID GameManager�� �Ѱ��ָ鼭 ���� Ȯ�� PopUpâ ���ִ� Method
    public void DeleteInfo()
    {
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        profileCanvas = FindObjectOfType<ProfileCanvas>();

        ProfileManager.Instance.TempProfileName = SQL_Manager.instance.ProfileList[ReceiveIndex()].name; // �� �Ѱ��ִ� �� �𸣰��� ... todo
        ProfileManager.Instance.TempUserIndex = SQL_Manager.instance.ProfileList[ReceiveIndex()].index;

        if (profileCanvas != null) profileCanvas.DeletePanel.SetActive(true);
    }
}
