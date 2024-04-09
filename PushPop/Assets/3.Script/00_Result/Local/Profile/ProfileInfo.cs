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
    { // 본인이 몇번째 자식인지 확인하는 Method
        return gameObject.transform.GetSiblingIndex();
    }

    public void Join()
    { // Profile Select 했을 시 선택된 프로필로 이동, 선택한 프로필 저장
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        profileCanvas = FindObjectOfType<ProfileCanvas>();
        
        // select 시 프로필 수정을 대비하여 temp에 저장
        ProfileManager.Instance.TempProfileName = SQL_Manager.instance.ProfileList[ReceiveIndex()].name;
        ProfileManager.Instance.TempUserIndex = SQL_Manager.instance.ProfileList[ReceiveIndex()].index;
        ProfileManager.Instance.TempImageIndex = SQL_Manager.instance.ProfileList[ReceiveIndex()].defaultImage;
        ProfileManager.Instance.TempImageMode = SQL_Manager.instance.ProfileList[ReceiveIndex()].imageMode;
        
        // 프로필 출력
        SQL_Manager.instance.PrintProfileImage(profileCanvas.ProfileImage, ProfileManager.Instance.TempImageMode, ProfileManager.Instance.TempUserIndex);
        profileCanvas.ProfileText.text = ProfileManager.Instance.TempProfileName;
        
        profileCanvas.BlockPanel.SetActive(true);
        profileCanvas.CurrentProfile.SetActive(true);
        profileCanvas.BackButton.SetActive(false);
        profileCanvas.Select.SetActive(false);
    }

    // 선택한 버튼의 name, UID GameManager에 넘겨주면서 삭제 확인 PopUp창 켜주는 Method
    public void DeleteInfo()
    {
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        profileCanvas = FindObjectOfType<ProfileCanvas>();

        ProfileManager.Instance.TempProfileName = SQL_Manager.instance.ProfileList[ReceiveIndex()].name; // 왜 넘겨주는 지 모르겠음 ... todo
        ProfileManager.Instance.TempUserIndex = SQL_Manager.instance.ProfileList[ReceiveIndex()].index;

        if (profileCanvas != null) profileCanvas.DeletePanel.SetActive(true);
    }
}
