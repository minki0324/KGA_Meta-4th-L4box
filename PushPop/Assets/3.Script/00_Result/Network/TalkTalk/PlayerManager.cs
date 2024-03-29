using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Mirror;

public class PlayerManager : NetworkBehaviour 
{
    [HideInInspector] public NetworkIdentity ConnectPrefabs; //PlayerObj의 Identity
    [HideInInspector] public PlayerObj NowObj;
    private List<NetworkIdentity> playersAvatarIdentity = new List<NetworkIdentity>();
    [SerializeField] private Transform playerObjCircle;
    [SerializeField] private Transform goalObjCircle;
    public BoxCollider2D AvatarBoundary;

    private void Awake()
    {
        NetworkServer.OnDisconnectedEvent += (NetworkConnectionToClient) =>
        {
            ServerListReset();
        };
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsTouchingUI())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                //Set move Player object to this point
                if (NowObj != null)
                {
                    Vector2 goalPos = hit.point;
                    goalPos = new Vector3(
          Mathf.Clamp(goalPos.x, AvatarBoundary.bounds.min.x, AvatarBoundary.bounds.max.x),
          Mathf.Clamp(goalPos.y, AvatarBoundary.bounds.min.y, AvatarBoundary.bounds.max.y));
                    goalObjCircle.transform.position = goalPos;
                    NowObj.SetMovePos(goalPos);
                }
            }
        }

        if (NowObj != null)
        {
            playerObjCircle.transform.position = NowObj.transform.position;
        }
    }

    private bool IsTouchingUI()
    {
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

    [Client]
    public void SetPlayer(int index)
    {
        CMD_SetPlayer(index,ConnectPrefabs);
        ConnectPrefabs.GetComponent<PlayerObj>().avatarIndex = index;
    }
    
    public void ServerListReset()
    {
        foreach (var playerIden in playersAvatarIdentity)
        {
            if (playerIden == null)
            {
                Debug.Log(playerIden);
                playersAvatarIdentity.Remove(playerIden);
            }
        }
    }

    [Command(requiresAuthority = false)]
    private void CMD_SetPlayer(int index ,NetworkIdentity identity)
    {
        StartCoroutine(SyncDelay(index ,identity));
    }

    public IEnumerator SyncDelay(int index,NetworkIdentity identity)
    {
        identity.GetComponent<PlayerObj>().avatarIndex = index;
        playersAvatarIdentity.Add(identity);
        yield return new WaitForSeconds(0.1f);  
        RPC_SetPlayer(playersAvatarIdentity, identity ,index);
    }

    [ClientRpc]
    private void RPC_SetPlayer(List<NetworkIdentity> identities , NetworkIdentity targetClient ,int index)
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

    public void ActiveOnAvatar(GameObject target , int targetIndex)
    {//접속해있는 identity(플레이어프리팹)이 가지고있는 선택했던 avatarindex에맞게 avatar를 켜주는 작업.
        target.transform.GetChild(targetIndex).gameObject.SetActive(true);
    }

    public void RemoveIdentityToList(NetworkIdentity identity)
    {
        playersAvatarIdentity.Remove(identity);
    } 

    public void AllPlayerActive(List<NetworkIdentity> identities)
    {
        foreach (var player in identities)
        {
            if (player == null)
            {
                continue;
            }
            int playerAvatarIndex = player.GetComponent<PlayerObj>().avatarIndex;
            
            ActiveOnAvatar(player.gameObject, playerAvatarIndex);
        }
    }
}
