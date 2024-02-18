using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using UnityEngine.U2D;

/// <summary>
/// Network Scene ���� Manager Class
/// </summary>
public class CommunicationRoomManager : NetworkBehaviour
{
    [SerializeField] private GameObject popBtn;                         // Pushpop Prefabs
    [SerializeField] private Sprite[] btnSprite;                        // Pushpop Color Sprite
    [SerializeField] private Image[] moamoaImage;                       // PushPush Image
    [SerializeField] private SpriteAtlas atlas;                         // PushPush Image Atlas

    private List<PushPushObject> pushObjs = new List<PushPushObject>(); // PushPushObject Class List

    #region Unity Callback
    private void Start()
    {
        pushObjs = SQL_Manager.instance.SQL_SetPushPush(GameManager.Instance.ProfileIndex);
        SetMoaMoaList();
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

    public void GotoLobby()
    {
        //DB�״�� �̵��ϰԲ��ϱ�
        if (NetworkManager.singleton != null)
        {
            //�κ��̵�
            NetworkManager.singleton.StopClient();
        }
        else
        {
            Debug.Log("��Ʈ��ũ�Ŵ����� �������� �ʽ��ϴ�.");
        }
    }
    #endregion

    #region SyncVar
    #endregion

    #region Hook Method
    #endregion

    #region Client
    #endregion

    #region Command
    #endregion

    #region ClientRPC
    #endregion

    #region Server
    #endregion




}
