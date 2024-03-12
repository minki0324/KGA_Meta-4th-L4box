using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

[System.Serializable]
public class MyEvent : UnityEvent<PlayerObj.PlayerState>
{

}

[ExecuteInEditMode]
public class PlayerObj : NetworkBehaviour
{
    public SPUM_Prefabs _prefabs;
    public float _charMS;
    public enum PlayerState
    {
        idle,
        run,
        attack,
        death,
    }
    private PlayerState _currentState;
    public PlayerState CurrentState{
        get => _currentState;
        set {
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
        if (isLocalPlayer) { 
        transform.position = Vector3.zero;
        PlayerManager playerManager = FindObjectOfType<PlayerManager>();
        playerManager.ConnectPrefabs = GetComponent<NetworkIdentity>();
        playerManager._nowObj = this;
            //SendProfile();
        }


        _stateChanged.AddListener(PlayStateAnimation);


    }
    private void PlayStateAnimation(PlayerState state){
        _prefabs.PlayAnimation(state.ToString());
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y,transform.localPosition.y * 0.01f);
        switch(_currentState)
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
        Vector3 _dirVec  = _goalPos - transform.position ;
        Vector3 _disVec = (Vector2)_goalPos - (Vector2)transform.position ;
        if( _disVec.sqrMagnitude < 0.1f )
        {
            _currentState = PlayerState.idle;
            PlayStateAnimation(_currentState);
            return;
        }
        Vector3 _dirMVec = _dirVec.normalized;
        transform.position += (_dirMVec * _charMS * Time.deltaTime );
        

        if(_dirMVec.x > 0 ) _prefabs.transform.localScale = new Vector3(-1,1,1);
        else if (_dirMVec.x < 0) _prefabs.transform.localScale = new Vector3(1,1,1);
    }

    public void SetMovePos(Vector2 pos)
    {
        _goalPos = pos;
        _currentState = PlayerState.run;
        PlayStateAnimation(_currentState);
    }
    [Client]
    private void SendProfile()
    {
        Profile profile = null; //여기에 자기프로필 추가
        CMD_SendProfile(profile.name , profile.index , profile.imageMode , profile.defaultImage);
    }
    [Command]
    private void CMD_SendProfile(string profilename , int profileIndex , bool profileMode , int profileImage)
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
        ProfileManager.Instance.ddd.Add(new Profile(profilename , profileIndex , TempModeIndex, profileImage));
        for (int i = 0; i < ProfileManager.Instance.ddd.Count; i++)
        {
            Debug.Log(ProfileManager.Instance.ddd[i].name);
        }
    }
}
