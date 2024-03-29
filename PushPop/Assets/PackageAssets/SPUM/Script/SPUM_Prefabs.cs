using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using Mirror;
public class SPUM_Prefabs : MonoBehaviour
{
    public float _version;
    public SPUM_SpriteList _spriteOBj;
    public bool EditChk;
    public Animator _anim;

    private void OnEnable()
    {
        PlayerObj playerObj = transform.parent.GetComponent<PlayerObj>();
        if (playerObj != null)
        {
            transform.parent.GetComponent<PlayerObj>()._prefabs = this;
        }
    }

    // 이름으로 애니메이션 실행
    public void PlayAnimation(string name)
    {
        switch (name)
        {
            case "idle":
                _anim.SetBool("Move", false);
                break;
            case "run":
                _anim.SetBool("Move", true);
                break;
        }
    }
}
