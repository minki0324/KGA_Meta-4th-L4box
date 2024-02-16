using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CommunicationRoomManager : NetworkBehaviour
{
 
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
}
