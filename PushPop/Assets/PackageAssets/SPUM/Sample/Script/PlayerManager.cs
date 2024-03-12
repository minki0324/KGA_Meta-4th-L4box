using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public NetworkIdentity ConnectPrefabs;
    public SyncList<int> playersAvatarIndex = new SyncList<int>();
    public List<NetworkIdentity> playersAvatarIdentity = new List<NetworkIdentity>();

    public Transform _playerPool;
    public List<PlayerObj> _playerList = new List<PlayerObj>();
    public PlayerObj _nowObj;
    public Transform _playerObjCircle;
    public Transform _goalObjCircle;

    public bool _generate;
    // Start is called before the first frame update
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

        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit.collider != null)
            {
               
                    //Set move Player object to this point
                    if(_nowObj!=null)
                    {
                        Vector2 goalPos = hit.point;
                        _goalObjCircle.transform.position = hit.point;
                        _nowObj.SetMovePos(goalPos);
                    }

            }
        }

        if(_nowObj!=null)
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
    [Client]
    public void SetPlayer(int index)
    {
        //ConnectPrefabs.gameObject.transform.GetChild(index).gameObject.SetActive(true);
        CMD_SetPlayer(index, ConnectPrefabs);
    }
    [Command(requiresAuthority = false)]
    private void CMD_SetPlayer(int index , NetworkIdentity identity)
    {
       
        playersAvatarIndex.Add(index);
        playersAvatarIdentity.Add(identity);
        RPC_SetPlayer(index, playersAvatarIdentity , identity); 
    }
    [ClientRpc]
    private void RPC_SetPlayer(int index, List<NetworkIdentity> identitys , NetworkIdentity targetClient)
    {
        if (targetClient.connectionToClient == connectionToClient)
        {//새로들어온 클라이언트는 기존 들어와 있던 클라이언트들의 아바타 활성화해주기(본인포함)
            for (int i = 0; i < identitys.Count; i++)
            {
                identitys[0].transform.GetChild(playersAvatarIndex[0]).gameObject.SetActive(true);
            }
            Debug.Log("들어온본인");
        }
        else
        {//들어온 클라이언트를 제외한 나머지 클라한테는 새로 들어온 클라이언트의 아바타 활성화 해주기
            targetClient.gameObject.transform.GetChild(index).gameObject.SetActive(true);
            Debug.Log("들어온사람을 제외한 사람들");
        }
    }
}
