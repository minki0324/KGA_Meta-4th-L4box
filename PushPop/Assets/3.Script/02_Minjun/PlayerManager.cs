using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Mirror;

//public class AvatarInfo
//{
//   public NetworkIdentity playerIdentity;
//    public int avatarIndex;
//    public AvatarInfo(int _index , NetworkIdentity _avatar)
//    {
//        playerIdentity = _avatar;
//        avatarIndex = _index;
//    }
    
//}

[ExecuteInEditMode]
public class PlayerManager : NetworkBehaviour 
{
    public PlayerObj _prefabObj;
    public List<GameObject> _savedUnitList = new List<GameObject>();
    public Vector3 _startPos;
    public int _columnNum;
    public NetworkIdentity ConnectPrefabs; //PlayerObj의 Identity
    public SyncList<int> playersAvatarIndex = new SyncList<int>();
    public List<NetworkIdentity> playersAvatarIdentity = new List<NetworkIdentity>();
    public SyncDictionary<NetworkIdentity, int> playerAvatarInfo = new SyncDictionary<NetworkIdentity, int>();
    public Transform _playerPool;
    public List<PlayerObj> _playerList = new List<PlayerObj>();
    public PlayerObj _nowObj;
    public Transform _playerObjCircle;
    public Transform _goalObjCircle;

    public BoxCollider2D AvatarBoundary;
    public Camera OverUICamera;
    //public bool _isMovePause = true;
    public bool _generate;
    public bool isMouseOnUI;
    public float tempScale;

    // Start is called before the first frame update
    private void Awake()
    {
        NetworkServer.OnDisconnectedEvent += (NetworkConnectionToClient) =>
        {
            ServerListReset();
        };
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(_generate)
        {
            GetPlayerList();
            _generate = false;
        }

        if(Input.GetMouseButtonDown(0)&& !IsTouchingUI())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if(hit.collider != null)
            {

                //Set move Player object to this point
                if (_nowObj != null)
                {

                    //if (gameObject.transform.position.x - hit.point.x < 0)
                    //{
                    //    _nowObj.ChangeScale.x = -Mathf.Abs(_nowObj.ChangeScale.x);
                    //}
                    //else
                    //{
                    //    _nowObj.ChangeScale.x = Mathf.Abs( _nowObj.ChangeScale.x);
                    //}
                    //_nowObj.playerNameText.transform.localScale = _nowObj.ChangeScale;

                    Vector2 goalPos = hit.point;
                    goalPos = new Vector3(
          Mathf.Clamp(goalPos.x, AvatarBoundary.bounds.min.x, AvatarBoundary.bounds.max.x),
          Mathf.Clamp(goalPos.y, AvatarBoundary.bounds.min.y, AvatarBoundary.bounds.max.y));
                    _goalObjCircle.transform.position = goalPos;
                    _nowObj.SetMovePos(goalPos);
                }

            }
        }

        if (_nowObj != null)
        {
            _playerObjCircle.transform.position = _nowObj.transform.position;
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

    public void GetPlayerList()
    {
        float numXStart = 0;
        float numYStart = 0;

        float numX = 1f;
        float numY = 1f;

        int sColumnNum = _columnNum;

        for(var i = 0 ; i < _savedUnitList.Count;i++)
        {
            if(i > sColumnNum-1)
            {
                numYStart -= 1f;
                numXStart -= numX * _columnNum;
                sColumnNum += _columnNum;
            }
            
            GameObject ttObj = Instantiate(_prefabObj.gameObject) as GameObject;
            ttObj.transform.SetParent(_playerPool);
            ttObj.transform.localScale = new Vector3(1,1,1);
            

            GameObject tObj = Instantiate(_savedUnitList[i]) as GameObject;
            tObj.transform.SetParent(ttObj.transform);
            tObj.transform.localScale = new Vector3(1,1,1);
            tObj.transform.localPosition = Vector3.zero;

            ttObj.name = _savedUnitList[i].name;

            PlayerObj tObjST = ttObj.GetComponent<PlayerObj>();
            SPUM_Prefabs tObjSTT = tObj.GetComponent<SPUM_Prefabs>();

            tObjST._prefabs = tObjSTT;

            ttObj.transform.localPosition = new Vector3(numXStart + numX * i,numYStart + numY,0);

            _playerList.Add(tObjST);
            
        }
    }

    [Client]
    public void SetPlayer(int index)
    {
        //ConnectPrefabs.gameObject.transform.GetChild(index).gameObject.SetActive(true);
        CMD_SetPlayer(index,ConnectPrefabs);
        ConnectPrefabs.GetComponent<PlayerObj>().avatarIndex = index;
        //HideAvatar(false);
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
        //int id = connectionToClient.connectionId;
        //Debug.Log("요청한 클라이언트 ID : " + id);
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
            //
            ActiveOnAvatar(player.gameObject, playerAvatarIndex);
        }
    }
 
}
