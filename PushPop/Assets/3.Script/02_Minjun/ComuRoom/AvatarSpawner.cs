using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AvatarSpawner : NetworkBehaviour
{
    public static AvatarSpawner instance;

    public GameObject[] avatars;
    public GameObject selectAvatars;
    public NetworkIdentity identity;
    private void Awake()
    {
        instance = this;
        identity = GetComponent<NetworkIdentity>();
    }
    [Client]
    public void SelectCharacter(int _selectindex)
    {
        Debug.Log("클라");
        CmdSelectCharacter(_selectindex , identity.connectionToClient);
    }
    [Command(requiresAuthority =false)]
    private void CmdSelectCharacter(int _selectindex, NetworkConnectionToClient conn)
    {
        Debug.Log("서버");
        // 선택한 캐릭터 프리팹 가져오기
        GameObject selectAvatars = avatars[_selectindex];

        // 서버에 캐릭터 추가 요청
        if (identity != null)
        {
            NetworkServer.AddPlayerForConnection(conn, selectAvatars);
        }
        else
        {
            Debug.LogError("NetworkIdentity가 설정되지 않았습니다.");
        }
    }
}
