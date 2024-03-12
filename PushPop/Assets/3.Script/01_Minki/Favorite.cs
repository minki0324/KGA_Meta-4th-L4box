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
    public FavoriteList favoriteLists = null; // ���ã�� ��� List
    private List<NetworkProfileInfo> profilePanels = new List<NetworkProfileInfo>(); // �� ������ �ǳڵ鸶�� ������ �ִ� Script

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
        // ������ �������� Favorite List�� SQL���� �޾ƿͼ� �����س���, Network�� �����ų� Application�� Quit�� �� �ٽ� SQL�� ����
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
        // SQL�� favoriteList �����ϴ� �޼ҵ� ȣ�� todo
    }

    private void OnApplicationQuit()
    {
        // SQL�� favoriteList �����ϴ� �޼ҵ� ȣ�� todo
    }
    #endregion

    #region Other Method
    #region Active
    public void FavoritePanelActive()
    {
        bool active = favoriteCanvasPanel.activeSelf;
        if(active)
        { // Favorite Panel ų ��
            OnlineListSet(profiles);
        }
        else
        { // Favorite Panel �� ��
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
            // Network Manager���� �ǳ׹��� profileList��ŭ profilePanel ����
            GameObject profile = Instantiate(networkProfile);
            profile.transform.SetParent(networkProfileParent);

            // �� profilePanel�� ����ִ� infomation�� ����
            NetworkProfileInfo network = profile.GetComponent<NetworkProfileInfo>();
            network.profile = profilelist[i];
            network.PrintInfomation();
            profilePanels.Add(network);
        }

        // ������ network�� List�� ��Ƽ� SQL�� ����� ���ã�� ��ϰ� ���Ͽ� ��ġ�� ��� ����Ʈ �ֻ�ܿ� ��ġ
        for(int i = 0; i < favoriteLists.FriendIndex.Count; i++)
        {
            for(int j = 0; j < profilePanels.Count; j++)
            {
                if(favoriteLists.FriendIndex[i] == profilePanels[j].profile.index)
                { // �� �ε����� ��ġ�ϴٸ� ���ã�� �ص� ����̹Ƿ� �ڽ� ���� �� ù��°�� �̵�
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
                // �˻� ĭ�� �Է��� �ؽ�Ʈ�� profile ������ ������ �ֻ������ ������Ʈ ���� ����
                isSearchProfile = true;
            }
        }
        if(!isSearchProfile)
        { // ã�� �̸��� ���ٸ� ��� ���� ���
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
