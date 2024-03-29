using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Mirror;

public class PlayerManager : NetworkBehaviour 
{
    [HideInInspector] public NetworkIdentity ConnectPrefabs; //본인(로컬상) PlayerObj의 Identity 클라이언트 접속시 할당
    [HideInInspector] public PlayerObj NowObj;//본인(로컬상) PlayerObj
    private List<NetworkIdentity> playersAvatarIdentity = new List<NetworkIdentity>(); //서버에서 관리하는 접속한 Player들의 Identity List
    [SerializeField] private Transform playerObjCircle; //본인의 캐릭터의 위치를 나타내는 서클
    [SerializeField] private Transform goalObjCircle; // 이동할 위치의 서클
    public BoxCollider2D AvatarBoundary; //맵내에서 아바타가 활동할수 있는 바운더리

    private void Awake()
    {
        NetworkServer.OnDisconnectedEvent += (NetworkConnectionToClient) =>
        {//클라이언트가 나갔을때
            ServerListReset();
        };
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsTouchingUI())
        {//터치(마우스클릭) , UI를 터치하지 않았을때
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                //Set move Player object to this point
                if (NowObj != null)
                {
                    Vector2 goalPos = hit.point; 
                    //goalPos 바운더리안으로 보정
                    goalPos = new Vector3( 
          Mathf.Clamp(goalPos.x, AvatarBoundary.bounds.min.x, AvatarBoundary.bounds.max.x),
          Mathf.Clamp(goalPos.y, AvatarBoundary.bounds.min.y, AvatarBoundary.bounds.max.y));
                    goalObjCircle.transform.position = goalPos;//터치위치에 goalPos이동
                    NowObj.SetMovePos(goalPos); // goalPos로 이동시키기 , 클라이언트들끼리 애니메이션 동기화
                }
            }
        }

        if (NowObj != null)
        {
            playerObjCircle.transform.position = NowObj.transform.position;
        }
    }





    #region ETC : UI체크 , 클라이언트접속리스트관리
    private bool IsTouchingUI()
    {//터치한 위치가 UI위인지 체크하는 메서드
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    return true;
                }
            }
        }
        return false;
    }


    public void ServerListReset()
    {
        foreach (var playerIden in playersAvatarIdentity)
        {
            if (playerIden == null)
            { //나간플레이어가 있다면
                Debug.Log(playerIden);
                //리스트에서 삭제해주세요
                playersAvatarIdentity.Remove(playerIden);
            }
        }
    }
    #endregion

    #region 아바타 선택 ,아바타 동기화
    public void ActiveOnAvatar(GameObject target, int targetIndex)
    {//접속해있는 identity(플레이어프리팹)이 가지고있는 선택했던 avatarindex에맞게 avatar를 켜주는 작업.
        target.transform.GetChild(targetIndex).gameObject.SetActive(true);
    }
    //캐릭터 선택 onClick에 참조되있음
    public void AllPlayerActive(List<NetworkIdentity> identities)
    {
        foreach (var player in identities)
        {
            if (player == null)
            {
                continue;
            }
            // 각클라이언트씬에서 접속한 아바타를 동기화
            int playerAvatarIndex = player.GetComponent<PlayerObj>().avatarIndex;

            ActiveOnAvatar(player.gameObject, playerAvatarIndex);
        }
    }
    [Client]
    public void SetPlayer(int index)
    {
        //PlayerObject에 Active가 꺼져있는 아바타중에 선택한 index에 맞는 아바타 Active 켜주기
        CMD_SetPlayer(index, ConnectPrefabs);
        ConnectPrefabs.GetComponent<PlayerObj>().avatarIndex = index;
    }
    [Command(requiresAuthority = false)]
    private void CMD_SetPlayer(int index, NetworkIdentity identity)
    {
        StartCoroutine(SyncDelay(index, identity));
    }
    [ClientRpc]
    private void RPC_SetPlayer(List<NetworkIdentity> identities, NetworkIdentity targetClient, int index)
    {
        if (targetClient.netId == ConnectPrefabs.netId)
        {//새로들어온 클라이언트는 기존 들어와 있던 클라이언트들의 아바타 활성화해주기(본인포함)
            AllPlayerActive(identities);
        }
        else
        {//들어온 클라이언트를 제외한 나머지 클라한테는 새로 들어온 클라이언트의 아바타 활성화 해주기
            int targetAvatarIndex = targetClient.GetComponent<PlayerObj>().avatarIndex;
            ActiveOnAvatar(targetClient.gameObject, targetAvatarIndex);
        }

        for (int i = 0; i < targetClient.transform.childCount; i++)
        {
            if (i != index)
            {
                Animator ani = targetClient.transform.GetChild(i).GetComponent<Animator>();
                Destroy(ani);
            }
        }
    }
    public IEnumerator SyncDelay(int index, NetworkIdentity identity)
    {
        identity.GetComponent<PlayerObj>().avatarIndex = index; // PlayerObj에 avatarIndex를 보내서 맞는 아바타 활성화 
        playersAvatarIdentity.Add(identity); //접속되있는 클라 리스트 추가
        yield return new WaitForSeconds(0.1f);  //동기화 순서 문제때문에 0.1초 여유를둠
        RPC_SetPlayer(playersAvatarIdentity, identity, index);
    }
    #endregion


   

   
}
