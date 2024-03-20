using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Favorite : MonoBehaviour
{
    [Header("Other Component")] [Space(5)]
    [SerializeField] private MoaMoaManager moamoa;

    [Header("Active Panel")] [Space(5)]
    [SerializeField] private GameObject favoriteCanvasPanel = null;

    [Header("Instantiate Panel")] [Space(5)]
    [SerializeField] private Transform networkProfileParent = null;
    [SerializeField] private GameObject networkProfile = null;

    [Header("UID, Info Text")] [Space(5)]
    [SerializeField] private TMP_Text infoText = null;

    [Header("List")] [Space(5)]
    [SerializeField] private List<Profile> ConnectList;
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
        if(ProfileManager.Instance.PlayerInfo[(int)Player.Player1] != null)
        {
            favoriteLists = SQL_Manager.instance.SQL_FavoriteListSet(ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex);
            if (favoriteLists != null && favoriteLists.FriendIndex == null)
            {
                favoriteLists = null;
            }
            infoText.text = $"���� UID : {ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileName}#{ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex}";
            SQL_Manager.instance.SQL_ConnectCheck(true, ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex);
        }
    }

    private void OnDestroy()
    {
        if (ProfileManager.Instance.PlayerInfo[(int)Player.Player1] != null)
        {
            // SQL�� favoriteList �����ϴ� �޼ҵ� ȣ��
            SQL_Manager.instance.SQL_UpdateFavoriteList(favoriteLists, ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex);

            // ��Ʈ��ũ �������� ����� false�� ����
            SQL_Manager.instance.SQL_ConnectCheck(false, ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex);
        }
    }

    private void OnApplicationQuit()
    {
        if (ProfileManager.Instance.PlayerInfo[(int)Player.Player1] != null)
        {
            // SQL�� favoriteList �����ϴ� �޼ҵ� ȣ��
            SQL_Manager.instance.SQL_UpdateFavoriteList(favoriteLists, ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex);

            // ��Ʈ��ũ �������� ����� false�� ����
            SQL_Manager.instance.SQL_ConnectCheck(false, ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex);
        }
    }
    #endregion

    #region Other Method
    #region Active
    public void FavoritePanelActive()
    { // Favorite Panel Active Ű�� ���� Method
        bool active = favoriteCanvasPanel.activeSelf;
        if(active) // ���� �ִٸ�
        { // Favorite Panel �� ��
            for (int i = 0; i < networkProfileParent.childCount; i++)
            {
                Destroy(networkProfileParent.GetChild(i).gameObject);
            }
        }
        else // ���� �ִٸ�
        { // Favorite Panel ų ��
            OnlineListSet();
        }
        favoriteCanvasPanel.SetActive(!active);
    }

    private void OnlineListSet()
    {
        ConnectList = SQL_Manager.instance.SQL_ConnectListSet(ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex);
        profilePanels.Clear();

        if (ConnectList.Count > 0)
        {
            for(int i = 0; i < ConnectList.Count; i++)
            {
                // Network Manager���� �ǳ׹��� profileList��ŭ profilePanel ����
                GameObject profile = Instantiate(networkProfile);
                profile.transform.SetParent(networkProfileParent);

                // ������ ����
                profile.GetComponent<RectTransform>().transform.localScale = new Vector3(1, 1, 1);
                Vector3 pos = profile.GetComponent<RectTransform>().transform.localPosition;
                profile.GetComponent<RectTransform>().transform.localPosition = new Vector3(pos.x, pos.y, 0);

                // �� profilePanel�� ����ִ� infomation�� ����
                NetworkProfileInfo network = profile.GetComponent<NetworkProfileInfo>();
                network.profile = ConnectList[i];
                network.favorite = this;
                network.moamoa = moamoa;
                network.PrintInfomation();
                profilePanels.Add(network);
            }
        }
        else
        {
            Debug.Log("���� ���� ������ �����ϴ�.");
        }

        // ������ network�� List�� ��Ƽ� SQL�� ����� ���ã�� ��ϰ� ���Ͽ� ��ġ�� ��� ����Ʈ �ֻ�ܿ� ��ġ
        if(favoriteLists != null)
        {
            for (int i = 0; i < favoriteLists.FriendIndex.Count; i++)
            {
                for (int j = 0; j < profilePanels.Count; j++)
                {
                    if (favoriteLists.FriendIndex[i] == profilePanels[j].profile.index)
                    { // �� �ε����� ��ġ�ϴٸ� ���ã�� �ص� ����̹Ƿ� �ڽ� ���� �� ù��°�� �̵�
                        profilePanels[j].gameObject.transform.SetAsFirstSibling();
                    }
                }
            }
        }
    }

    public void ReturnButton()
    {
        for (int i = 0; i < networkProfileParent.childCount; i++)
        {
            Destroy(networkProfileParent.GetChild(i).gameObject);
        }
        favoriteCanvasPanel.SetActive(false);
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
                profilePanels[i].gameObject.transform.SetAsFirstSibling();
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
        searchInput.text = string.Empty;
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
