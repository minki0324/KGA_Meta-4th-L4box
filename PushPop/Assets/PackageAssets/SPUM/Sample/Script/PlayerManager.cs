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
    public bool _isMovePause = true;
    public bool _generate;
    public bool isMouseOnUI;
    // Start is called before the first frame update
    private void Awake()
    {
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

        if(Input.GetMouseButtonDown(0) && !_isMovePause && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if(hit.collider != null)
            {
               
                    //Set move Player object to this point
                    if(_nowObj!=null)
                    {
                        Vector2 goalPos = hit.point;
                    goalPos = new Vector3(
          Mathf.Clamp(goalPos.x, AvatarBoundary.bounds.min.x, AvatarBoundary.bounds.max.x),
          Mathf.Clamp(goalPos.y, AvatarBoundary.bounds.min.y, AvatarBoundary.bounds.max.y));
                    _goalObjCircle.transform.position = goalPos;
                    _nowObj.SetMovePos(goalPos);
                }

            }
        }

        if (_nowObj!=null)
        {
            _playerObjCircle.transform.position = _nowObj.transform.position;
        }
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

    public void HideAvatar(bool isHide)
    {
        _isMovePause = isHide;
        OverUICamera.gameObject.SetActive(!isHide);
    }

    [Client]
    public void SetPlayer(int index)
    {
        //ConnectPrefabs.gameObject.transform.GetChild(index).gameObject.SetActive(true);
        CMD_SetPlayer(index,ConnectPrefabs);
        Debug.Log("클라 id : "+ ConnectPrefabs.netId);
        ConnectPrefabs.GetComponent<PlayerObj>().avatarIndex = index;
        HideAvatar(false);
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
        yield return new WaitForSeconds(1.5f);
        RPC_SetPlayer(playersAvatarIdentity, identity ,index);
    }
    [ClientRpc]
    private void RPC_SetPlayer(List<NetworkIdentity> identities , NetworkIdentity targetClient ,int index)
    {
        Debug.Log("RPC본인 ID : " + ConnectPrefabs.netId);
        Debug.Log("서버에서보낸 ID : " + targetClient.netId);
        if (targetClient.netId == ConnectPrefabs.netId)
        {//새로들어온 클라이언트는 기존 들어와 있던 클라이언트들의 아바타 활성화해주기(본인포함)
            AllPlayerActive(identities);
            Debug.Log("들어온본인");
        }
        else
        {//들어온 클라이언트를 제외한 나머지 클라한테는 새로 들어온 클라이언트의 아바타 활성화 해주기
            int targetAvatarIndex = targetClient.GetComponent<PlayerObj>().avatarIndex;
            ActiveOnAvatar(targetClient.gameObject, targetAvatarIndex);
            Debug.Log("들어온사람을 제외한 사람들");
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
    [Command]
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
