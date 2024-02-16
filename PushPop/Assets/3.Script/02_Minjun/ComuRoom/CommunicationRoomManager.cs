using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class CommunicationRoomManager : NetworkBehaviour
{
    [SerializeField] private Image[] moamoaImage;
    [SerializeField] private GameObject popBtn;
    [SerializeField] private Sprite[] btnSprite;

    #region Unity Callback
    #endregion

    #region Other Method
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
