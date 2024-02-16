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
        //DB그대로 이동하게끔하기
        if (NetworkManager.singleton != null)
        {
            //로비이동
            NetworkManager.singleton.StopClient();
        }
        else
        {
            Debug.Log("네트워크매니저가 존재하지 않습니다.");
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
