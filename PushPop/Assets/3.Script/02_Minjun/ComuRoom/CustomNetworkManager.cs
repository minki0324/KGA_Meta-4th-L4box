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

        //Ŭ���̾�Ʈ�� �������� ������ ���� �������� player�� �Ҵ��ϰ����.
        //���������� �� ��ũ��Ʈ�� �ִ� selectPrefab
        //conn�� �̿��ؼ� ������ Ŭ���̾�Ʈ�� customNetworkManager�� �����ؼ� slectPrefab�� �����ü��ֳ�?
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

