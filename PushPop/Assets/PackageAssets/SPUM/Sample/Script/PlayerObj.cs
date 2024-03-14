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
    private NetworkIdentity myIdentity;
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
            transform.position = Vector3.zero;
            playerManager = FindObjectOfType<PlayerManager>();
            myIdentity = GetComponent<NetworkIdentity>();
            Debug.Log("내 아이덴티티 : " + myIdentity);
            playerManager.ConnectPrefabs = myIdentity;
            playerManager._nowObj = this;

            SendProfile();
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

    private void OnDestroy()
    {
        RemoveProfile();
        RemoveServerList();
    }

    private void OnApplicationQuit()
    {
        RemoveProfile();
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
        playerManager.RemoveIdentityToList(myIdentity);
    }
    [Client]
    private void SendProfile()
    {
        Profile profile = ProfileManager.Instance.myProfile;
        CMD_SendProfile(profile.name, profile.index, profile.imageMode, profile.defaultImage);
    }

    [Client]
    private void RemoveProfile()
    {
        Debug.Log("클라 들어옴?");
        CMD_RemoveProfile(ProfileManager.Instance.myProfile.index);
    }

    [Command]
    private void CMD_SendProfile(string profilename, int profileIndex, bool profileMode, int profileImage)
    {
        int TempModeIndex;
        if (profileMode)
        {
            TempModeIndex = 1;
        }
        else
        {

            TempModeIndex = 0;
        }
        ProfileManager.Instance.profileList.Add(new Profile(profilename, profileIndex, TempModeIndex, profileImage));
        Debug.Log(ProfileManager.Instance.profileList.Count);
        for (int i = 0; i < ProfileManager.Instance.profileList.Count; i++)
        {
            Debug.Log(ProfileManager.Instance.profileList[i].name);
        }
    }

    [Command(requiresAuthority = false)]
    private void CMD_RemoveProfile(int profileIndex)
    {
        foreach (Profile profile in ProfileManager.Instance.profileList)
        {
            if (profile.index == profileIndex)
            {
                Debug.Log("CMD 들어옴?");
                ProfileManager.Instance.profileList.Remove(profile);
            }
        }
    }
}
