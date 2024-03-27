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
    public string _code;
    public Animator _anim;
    public RuntimeAnimatorController _anicon;
    public bool _horse;
    

    public bool isRideHorse{
        get => _horse;
        set {
            _horse = value;
            UnitTypeChanged?.Invoke();
        }
    }
    public string _horseString;

    public UnityEvent UnitTypeChanged = new UnityEvent();
    private AnimationClip[] _animationClips;
    public AnimationClip[] AnimationClips => _animationClips;
    private Dictionary<string, int> _nameToHashPair = new Dictionary<string, int>();
    private void InitAnimPair(){
        _nameToHashPair.Clear();
        _animationClips = _anim.runtimeAnimatorController.animationClips;
        foreach (var clip in _animationClips)
        {
            int hash = Animator.StringToHash(clip.name);
            _nameToHashPair.Add(clip.name, hash);
        }
    }
    private void Awake() {
        // InitAnimPair();
    }
    private void OnEnable()
    {
        try
        {
            transform.parent.GetComponent<PlayerObj>()._prefabs = this;
        }
        catch
        {

        }
        //_anim= transform.GetChild(0).gameObject.AddComponent<Animator>();
        //_anim.runtimeAnimatorController = _anicon;
        //transform.parent.GetComponent<NetworkAnimator>().animator = _anim;
    }
    private void Start()
    {
        try
        {
            UnitTypeChanged.AddListener(InitAnimPair);
        }
        catch
        {

        }
    }
    // 이름으로 애니메이션 실행
    public void PlayAnimation(string name){

        
        // foreach (var animationName in _nameToHashPair)
        // {
        //     if(animationName.Key.ToLower().Contains(name.ToLower()) ){
        //         _anim.Play(animationName.Value, 0);
        //         break;
        //     }
        // }

        switch(name)
        {
            case "idle":
                _anim.SetBool("Move",false);
            break;

            case "run":
                _anim.SetBool("Move",true);
            break;

            
        }
    }
}
