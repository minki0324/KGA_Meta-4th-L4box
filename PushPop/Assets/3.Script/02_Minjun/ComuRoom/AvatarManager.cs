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
        Debug.Log("클라");
        CmdSelectCharacter(_selectindex );
    }
    [Command(requiresAuthority =false)]
    private void CmdSelectCharacter(int _selectindex)
    {
        Debug.Log("서버");
        // 선택한 캐릭터 프리팹 가져오기
        GameObject selectAvatars = avatars[_selectindex];
    }
}
