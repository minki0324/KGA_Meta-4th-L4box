using Mirror;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MyEvent : UnityEvent<PlayerObj.PlayerState>
{

}

[ExecuteInEditMode]
public class PlayerObj : NetworkBehaviour
{
    private PlayerManager playerManager;
    public NetworkIdentity myIdentity;
    public SPUM_Prefabs _prefabs;
    public NetworkAnimator netAni;
    public PlayerEmotionControl emotionControl;
    [SyncVar] public int avatarIndex;
    public float _charMS;

    public enum PlayerState
    {
        idle,
        run,
        attack,
        death,
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
        {
            // localplayer 조건문 안에 넣지 않으면 클라이언트가 접속 할때마다 접속해있는 모든 클라이언트들이 전부 find하기 때문에 넣어줌
            transform.position = Vector3.zero;
            playerManager = FindObjectOfType<PlayerManager>();
            myIdentity = GetComponent<NetworkIdentity>();
            if(myIdentity == null)
            {
                Debug.Log("NetworkIdentity가 존재하지않습니다. NetworkManager에 playerPrefab을 추가했는지 확인해주세요. 또는 NetworkIdentity 를 확인하세요.");
            }
            playerManager.ConnectPrefabs = myIdentity;
            playerManager._nowObj = this;
        }

        _stateChanged.AddListener(PlayStateAnimation);


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

    void DoMove()
    {
        Vector3 _dirVec = _goalPos - transform.position;
        Vector3 _disVec = (Vector2)_goalPos - (Vector2)transform.position;
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
        if (_dirMVec.x > 0) transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
        else if (_dirMVec.x < 0) transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
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

    [Client]
    public void RemoveServerList()
    {
        Debug.Log("클라");
        CMD_RemoveServerList(myIdentity);
    }
    [Command]
    public void CMD_RemoveServerList(NetworkIdentity identity)
    {
        Debug.Log("커맨드");
        playerManager.RemoveIdentityToList(identity);
    }
}
