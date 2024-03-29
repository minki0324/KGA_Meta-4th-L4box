using Mirror;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MyEvent : UnityEvent<PlayerObj.PlayerState>
{

}

public class PlayerObj : NetworkBehaviour
{
    private PlayerManager playerManager; 
    public NetworkIdentity myIdentity; //나의 networkID
    public SPUM_Prefabs _prefabs; //아바타가 선택되고 onEnable됬을때 할당
    public PlayerEmotionControl emotionControl; //아바타가 가지고있는 Canvas에 있는 스크립트

    [SyncVar] public string playerName;
    [SyncVar] public int avatarIndex; //현재 플레이어가 선택한 아바타 인덱스 동기화 클라 -> 서버
    public float _charMS;
    public TMP_Text playerNameText;

    public enum PlayerState
    {
        idle,
        run
    }
    private PlayerState _currentState;
    public PlayerState CurrentState
    {
        get => _currentState;
        set
        {
            _stateChanged.Invoke(value);
            _currentState = value;
        }
    }

    private MyEvent _stateChanged = new MyEvent();

    public Vector3 _goalPos;
    // Start is called before the first frame update

    // Update is called once per frame
    void Start()
    {
        if (isLocalPlayer)
        {// localplayer 조건문 안에 넣지 않으면 클라이언트가 접속 할때마다 접속해있는 모든 클라이언트들이 전부 find하기 때문에 넣어줌

            Init(); //플레이어 접속시 필요한 데이터 할당.
            
        }
        _stateChanged.AddListener(PlayStateAnimation);
        if(!isServer)
        {
            StartCoroutine(NicNameSync_co());
        }

    }
    private void PlayStateAnimation(PlayerState state)
    {
        _prefabs.PlayAnimation(state.ToString());
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.localPosition.y * 0.01f);
        switch (_currentState)
        {
            case PlayerState.idle:
                break;

            case PlayerState.run:
                DoMove();
                break;
        }
    }
    private IEnumerator NicNameSync_co()
    {
        //접속시 데이터받아올때까지 반복후 데이터 있을시 Name할당
        while(true)
        {
            yield return null;
            if(playerName == string.Empty)
            {
                continue;
            }
            else
            {
                playerNameText.text = playerName;
                yield break;
            }
        }
    }
    void DoMove()
    {//플레이어 이동로직
        Vector3 _dirVec = _goalPos - transform.position;
        Vector3 _disVec = (Vector2)_goalPos - (Vector2)transform.position;
        //목표지점 도착시 정지, state변경
        if (_disVec.sqrMagnitude < 0.1f)
        {
            _currentState = PlayerState.idle;
            CMD_SetMove(_currentState);
            return;
        }
        Vector3 _dirMVec = _dirVec.normalized;
        transform.position += (_dirMVec * _charMS * Time.deltaTime);
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, playerManager.AvatarBoundary.bounds.min.x, playerManager.AvatarBoundary.bounds.max.x),
            Mathf.Clamp(transform.position.y, playerManager.AvatarBoundary.bounds.min.y, playerManager.AvatarBoundary.bounds.max.y),
            transform.position.z);
        //플레이어기준 왼쪽오른쪽에 따라 캐릭터 반전시키기
        if (_dirMVec.x > 0) _prefabs.transform.localScale = new Vector3(-1f, 1f, 1f);
        else if (_dirMVec.x < 0) _prefabs.transform.localScale = new Vector3(1f, 1f, 1f);
    }
    private void Init()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        playerManager.ConnectPrefabs = myIdentity;
        playerManager.NowObj = this;
        myIdentity = GetComponent<NetworkIdentity>();
        playerName = ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileName + "#" + ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex;
        if (myIdentity == null)
        {
            Debug.Log("NetworkIdentity가 존재하지않습니다. NetworkManager에 playerPrefab을 추가했는지 확인해주세요. 또는 NetworkIdentity 를 확인하세요.");
        }
    }
    #region 애니메이션 state 동기화

    [Client]
    public void SetMovePos(Vector2 pos)
    {
        _goalPos = pos;
        _currentState = PlayerState.run;
        CMD_SetMove(_currentState);
    }
    [Command(requiresAuthority = true)]
    public void CMD_SetMove(PlayerState state)
    {
        RPC_SetMove(state);
    }
    [ClientRpc]
    public void RPC_SetMove(PlayerState state)
    {
        PlayStateAnimation(state);
    }
    #endregion
}
