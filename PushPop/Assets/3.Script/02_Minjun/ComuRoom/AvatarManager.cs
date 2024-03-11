using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AvatarManager : NetworkBehaviour
{
    public static AvatarManager instance;

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
        CmdSelectCharacter(_selectindex );
    }
    [Command(requiresAuthority =false)]
    private void CmdSelectCharacter(int _selectindex)
    {
        Debug.Log("����");
        // ������ ĳ���� ������ ��������
        GameObject selectAvatars = avatars[_selectindex];
    }
}
