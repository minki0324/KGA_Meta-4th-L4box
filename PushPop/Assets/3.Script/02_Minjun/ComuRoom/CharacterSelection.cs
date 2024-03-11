using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class CharacterSelection : NetworkBehaviour
{
    public GameObject[] avatars;
    public GameObject selectAvatars;
    private NetworkIdentity identity;

    private void Awake()
    {
        identity = GetComponent<NetworkIdentity>();
    }
    private void OnEnable()
    {
        //if (isLocalPlayer)
        //{
        //    SelectCharacter(0);
        //}
    }
    private void Start()
    {
        AvatarManager.instance.identity = identity;
    }
    [Client]
    public void SelectCharacter(int _selectindex)
    {
        Debug.Log("Ŭ��");
        CmdSelectCharacter(_selectindex);
    }
    [Command]
    private void CmdSelectCharacter(int _selectindex)
    {
        Debug.Log("����");
        // ������ ĳ���� ������ ��������
        GameObject selectAvatars = avatars[_selectindex];

        // ������ ĳ���� �߰� ��û
        if (identity != null)
        {
            NetworkServer.AddPlayerForConnection(identity.connectionToClient, selectAvatars);
        }
        else
        {
            Debug.LogError("NetworkIdentity�� �������� �ʾҽ��ϴ�.");
        }
    }
}
