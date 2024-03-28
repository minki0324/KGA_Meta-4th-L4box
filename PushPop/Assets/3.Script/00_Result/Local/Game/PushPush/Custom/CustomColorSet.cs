using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomColorSet : MonoBehaviour
{
    public GameObject[] Ring;
    [SerializeField] private CustomPushpopManager customPushpopManager = null;

    public void ChildActive(int _index)
    {
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        for (int i = 0; i < Ring.Length; i++)
        {
            Ring[i].SetActive(false);
        }
        Ring[_index].SetActive(true);
        customPushpopManager.SpriteIndex = _index;
    }
}
