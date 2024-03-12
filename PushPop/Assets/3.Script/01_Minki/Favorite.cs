using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Favorite : MonoBehaviour
{
    [Header("Active Panel")] [Space(5)]
    [SerializeField] private GameObject favoriteCanvasPanel = null;

    [Header("Instantiate Panel")] [Space(5)]
    [SerializeField] private Transform networkProfileParent = null;
    [SerializeField] private GameObject networkProfile = null;

    [Header("UID, Info Text")] [Space(5)]
    [SerializeField] private TMP_Text infoText = null;

    [Header("List")] [Space(5)]
    private List<Profile> profiles = new List<Profile>();
    public FavoriteList favoriteLists = null; // 즐겨찾기 목록 List
    private List<NetworkProfileInfo> profilePanels = new List<NetworkProfileInfo>(); // 각 프로필 판넬들마다 가지고 있는 Script

    [Header("Favorite Sprite")] [Space(5)]
    public Sprite[] favoriteStars;

    [Header("Input")] [Space(5)]
    [SerializeField] private TMP_InputField searchInput;
    [SerializeField] private GameObject warningLog;
    private bool isSearchProfile = false;

    private Coroutine warningLogCoroutine;

    #region Unity Callback
    private void Awake()
    {
        // 켜질때 기준으로 Favorite List를 SQL에서 받아와서 저장해놓고, Network를 나가거나 Application을 Quit할 때 다시 SQL에 저장
        favoriteLists = SQL_Manager.instance.SQL_ListUpFavorite(ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex);
        infoText.text = $"#{ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex}";
    }

    private void OnDisable()
    {
        for (int i = 0; i < networkProfileParent.childCount; i++)
        {
            Destroy(networkProfileParent.GetChild(i).gameObject);
        }
    }

    private void OnDestroy()
    {
        // SQL에 favoriteList 저장하는 메소드 호출 todo
    }

    private void OnApplicationQuit()
    {
        // SQL에 favoriteList 저장하는 메소드 호출 todo
    }
    #endregion

    #region Other Method
    #region Active
    public void FavoritePanelActive()
    {
        bool active = favoriteCanvasPanel.activeSelf;
        if(active)
        { // Favorite Panel 킬 때
            OnlineListSet(profiles);
        }
        else
        { // Favorite Panel 끌 때
            for (int i = 0; i < networkProfileParent.childCount; i++)
            {
                Destroy(networkProfileParent.GetChild(i).gameObject);
            }
        }
        favoriteCanvasPanel.SetActive(active);
    }

    private void OnlineListSet(List<Profile> profilelist)
    {
        for(int i = 0; i < profilelist.Count; i++)
        {
            // Network Manager에게 건네받은 profileList만큼 profilePanel 생성
            GameObject profile = Instantiate(networkProfile);
            profile.transform.SetParent(networkProfileParent);

            // 각 profilePanel이 들고있는 infomation을 세팅
            NetworkProfileInfo network = profile.GetComponent<NetworkProfileInfo>();
            network.profile = profilelist[i];
            network.PrintInfomation();
            profilePanels.Add(network);
        }

        // 생성된 network를 List에 담아서 SQL에 저장된 즐겨찾기 목록과 비교하여 일치할 경우 리스트 최상단에 위치
        for(int i = 0; i < favoriteLists.FriendIndex.Count; i++)
        {
            for(int j = 0; j < profilePanels.Count; j++)
            {
                if(favoriteLists.FriendIndex[i] == profilePanels[j].profile.index)
                { // 두 인덱스가 일치하다면 즐겨찾기 해둔 목록이므로 자식 순서 맨 첫번째로 이동
                    profilePanels[j].gameObject.transform.SetAsFirstSibling();
                }
            }
        }
    }
    #endregion

    #region Util Button
    public void SearchButton()
    {
        for(int i = 0; i < profilePanels.Count; i++)
        {
            if(searchInput.text.Equals(profilePanels[i].profile.name))
            {
                // 검색 칸에 입력한 텍스트와 profile 네임이 같으면 최상단으로 오브젝트 순서 변경
                isSearchProfile = true;
            }
        }
        if(!isSearchProfile)
        { // 찾는 이름이 없다면 경고 문구 출력
            if(warningLog != null)
            {
                StopCoroutine(warningLogCoroutine);
            }
            warningLogCoroutine = StartCoroutine(WarningLog());
        }
    }

    
    #endregion
    private IEnumerator WarningLog()
    {
        warningLog.SetActive(true);
        yield return new WaitForSeconds(2f);
        warningLog.SetActive(false);
        warningLogCoroutine = null;
    }
    #endregion
}
