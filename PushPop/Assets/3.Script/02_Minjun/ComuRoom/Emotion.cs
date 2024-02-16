using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Emotion : NetworkBehaviour
{
    //단지 EMotion 들 저장하고 있기 위한 스크립트
    public static Emotion instance;
    //이모션 종류 인스펙터에서 추가하기
    public Sprite[] emotions;
    private void Awake()
    {
        instance = this;
    }
}
