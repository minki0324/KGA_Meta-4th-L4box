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
        Debug.Log("클라");
        CmdSelectCharacter(_selectindex);
    }
    [Command]
    private void CmdSelectCharacter(int _selectindex)
    {
        Debug.Log("서버");
        // 선택한 캐릭터 프리팹 가져오기
        GameObject selectAvatars = avatars[_selectindex];

        // 서버에 캐릭터 추가 요청
        if (identity != null)
        {
            NetworkServer.AddPlayerForConnection(identity.connectionToClient, selectAvatars);
        }
        else
        {
            Debug.LogError("NetworkIdentity가 설정되지 않았습니다.");
        }
    }
}
