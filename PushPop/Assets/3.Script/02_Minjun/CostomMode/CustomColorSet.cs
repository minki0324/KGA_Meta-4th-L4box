using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomColorSet : MonoBehaviour
{
    public GameObject[] ring;
    [SerializeField] private CustomPushpopManager customPushpopManager = null;

    public void ChildActive(int _index)
    {
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        for (int i = 0; i < ring.Length; i++)
        {
            ring[i].SetActive(false);
        }
        ring[_index].SetActive(true);
        customPushpopManager.SpriteIndex = _index;
    }
}
