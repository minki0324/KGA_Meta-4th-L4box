using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPush_Result : MonoBehaviour
{
    private void OnEnable()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(6);
    }


    //성공 시 처음으로 버튼
    public void Success_BackBtn_Clicked()
    {
        GameManager.Instance.puzzleClass.Clear();


    }
}
