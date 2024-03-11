using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Linq;
using System;

public class CustomNetworkManager : NetworkManager
{
    
    public GameObject[] avatars;
    public GameObject selectPrefab;

    // Start is called before the first frame update
    public Button selectButton;
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Transform startPos = GetStartPosition();

        //클라이언트가 오프라인 씬에서 정한 프리팹을 player에 할당하고싶음.
        //그프리팹은 이 스크립트에 있는 selectPrefab
        //conn을 이용해서 접속한 클라이언트의 customNetworkManager에 접근해서 slectPrefab을 가져올수있나?
        Debug.Log(playerPrefab.name);
        GameObject player = startPos != null
            ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
            : Instantiate(playerPrefab);

        // instantiating a "Player" prefab gives it the name "Player(clone)"
        // => appending the connectionId is WAY more useful for debugging!
        player.name = $"{playerPrefab.name} [connId={conn.connectionId}]";
        NetworkServer.AddPlayerForConnection(conn, player);
    }
}

