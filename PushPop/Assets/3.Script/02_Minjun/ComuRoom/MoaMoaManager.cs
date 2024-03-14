using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using TMPro;

/// <summary>
/// Network Scene ���� Manager Class
/// </summary>
public class MoaMoaManager : MonoBehaviour
{
    public int CurrentProfileIndex;

    [Header("Heart And Like")]
    [SerializeField] private TMP_Text likeCount;
    [SerializeField] private TMP_Text heartCount;

    [SerializeField] private GameObject popBtn;                         // Pushpop Prefabs
    [SerializeField] private Sprite[] btnSprite;                        // Pushpop Color Sprite
    [SerializeField] private Image[] moamoaImage;                       // PushPush Image
    [SerializeField] private SpriteAtlas atlas;                         // PushPush Image Atlas
    private List<PushPushObject> pushObjs = new List<PushPushObject>(); // PushPushObject Class List

    [SerializeField] private GameObject MoaMoaPanel;
    private CustomNetworkManager manager;

    #region Unity Callback
    private void Awake()
    {
        manager = FindObjectOfType<CustomNetworkManager>();
    }
    #endregion

    #region Other Method
    public void SetMoaMoaList()
    { // MoaMoa ����ȭ�鿡 ����� 6���� �÷����� ����ϴ� Btn ���� Method
        for (int i = 0; i < pushObjs.Count; i++)
        { // Profile�� ����� PushpushObj�� ����

            // ����� Sprite ���
            moamoaImage[i].sprite = atlas.GetSprite(pushObjs[i].spriteName.ToString());

            // ���� PushPush���� ����ߴ� ũ��� ���� ����
            moamoaImage[i].SetNativeSize();
            moamoaImage[i].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            for(int j = 0; j < pushObjs[i].childIndex; j++)
            { // PushPushObject Class�� ����Ǿ��ִ� Btn�� index
                // ����� ��ŭ��ư ���� �� �θ���
                GameObject pop = Instantiate(popBtn, moamoaImage[i].transform);

                // ��ư�� ���� Index�� �°� Sprite ����
                pop.GetComponent<Image>().sprite = btnSprite[pushObjs[i].childSpriteIndex[j]];

                // Scale�� Position ����
                pop.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
                pop.transform.localPosition = pushObjs[i].childPosition[j];
            }

            // ������ �������� �÷��� Bubble�� ũ�⸸ŭ ������ ����
            moamoaImage[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    public void MoaMoaPanelActive()
    {
        bool active = MoaMoaPanel.activeSelf;

        MoaMoaPanel.SetActive(!active);
    }

    public void LikeAndHeartButton(bool _isLikeButton)
    { // �Ű����� Bool���� True�� ���ƿ� ��ư False�� �Ⱦ�� ��ư
        HeartAndLike heartAndLikeList = SQL_Manager.instance.SQL_HeartAndLikeListSet(CurrentProfileIndex);

        // �ű� ��� Ȥ�� ����Ʈ �ʱ�ȭ ����
        if (heartAndLikeList == null)
        {
            Debug.Log("�ű� ��� ��?");
            heartAndLikeList = new HeartAndLike(new List<int>(), new List<int>());
        }

        // ���� ��ư�� ���� (bool��) � list�� �����ؾ� �ϴ��� targetlist ����
        List<int> targetList = _isLikeButton ? heartAndLikeList.PressLikeProfileIndex : heartAndLikeList.PressHeartProfileIndex;
        if (targetList == null)
        {
            targetList = new List<int>();
        }

        // ������ �ε��� �߰� ����
        if (!targetList.Contains(ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex))
        { // Ÿ�� ����Ʈ �ȿ� ��ġ�ϴ� �ε����� ���� ��� ���� �߰��ϰ� DB�� ����
            Debug.Log("������Ʈ��?");
            targetList.Add(ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex);

            // �����ͺ��̽� ������Ʈ ȣ��
            SQL_Manager.instance.SQL_UpdateHeartAndLikeCount(CurrentProfileIndex, _isLikeButton);
            SQL_Manager.instance.SQL_UpdateHeartAndLikeList(heartAndLikeList, CurrentProfileIndex);
            UpdateLikeAndHeartCount();
        }
        else
        { // �̹� ���ƿ� Ȥ�� ��Ʈ�� ���� ��쿡 ���� ó��
            Debug.Log(_isLikeButton ? "�̹� ���ƿ並 ���� �����Դϴ�." : "�̹� ��Ʈ�� ���� �����Դϴ�.");
        }
    }

    public void TalktalkPanelMoaMoaButton()
    {
        CurrentProfileIndex = ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex;
    }

    public void UpdateLikeAndHeartCount()
    {
        int[] heartandlikearr = SQL_Manager.instance.SQL_HeartAndLikeCount(CurrentProfileIndex);
        likeCount.text = heartandlikearr[0].ToString();
        heartCount.text = heartandlikearr[1].ToString();
    }

    public void GotoLobby()
    {
        //DB�״�� �̵��ϰԲ��ϱ�
        if (manager != null)
        {
            //�κ��̵�
            manager.StopClient();
        }
        else
        {
            Debug.Log("��Ʈ��ũ�Ŵ����� �������� �ʽ��ϴ�.");
        }
    }
    #endregion
}
