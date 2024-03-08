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
        Debug.Log("Ŭ��");
        CmdSelectCharacter(_selectindex , identity.connectionToClient);
    }
    [Command(requiresAuthority =false)]
    private void CmdSelectCharacter(int _selectindex, NetworkConnectionToClient conn)
    {
        Debug.Log("����");
        // ������ ĳ���� ������ ��������
        GameObject selectAvatars = avatars[_selectindex];

        // ������ ĳ���� �߰� ��û
        if (identity != null)
        {
            NetworkServer.AddPlayerForConnection(conn, selectAvatars);
        }
        else
        {
            Debug.LogError("NetworkIdentity�� �������� �ʾҽ��ϴ�.");
        }
    }
}
