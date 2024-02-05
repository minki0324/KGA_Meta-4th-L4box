using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MuseumManager : NetworkBehaviour
{
 
    public void GotoLobby()
    {
        //DB그대로 이동하게끔하기
        if (NetworkManager.singleton != null)
        {
            NetworkManager.singleton.StopClient();
        }
        else
        {
            Debug.Log("네트워크매니저가 존재하지 않습니다.");
        }
    }
}
