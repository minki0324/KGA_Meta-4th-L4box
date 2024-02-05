using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerEmotionControl : NetworkBehaviour
{

    private Animator animator;
    private NetworkIdentity networkIdentity;
    void Start()
    {
        TryGetComponent(out animator);
        transform.root.TryGetComponent(out networkIdentity);
        if (isLocalPlayer)
        {
            Emotion.instance.playerEmotion = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [Client]
    public void PlayEmotion()
    {
        CMD_EMotion(networkIdentity);
    }
    [Command]
    public void CMD_EMotion(NetworkIdentity targetObject)
    {
        targetObject.GetComponentInChildren<Animator>().SetTrigger("onEmotion");
        RPC_Emotion(targetObject);
    }
    [ClientRpc]
    public void RPC_Emotion(NetworkIdentity targetObject)
    {
        targetObject.GetComponentInChildren<Animator>().SetTrigger("onEmotion");
    }
    
    
}
