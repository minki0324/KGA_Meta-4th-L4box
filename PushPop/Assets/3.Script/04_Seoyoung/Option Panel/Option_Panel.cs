using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option_Panel : MonoBehaviour
{

    public static Option_Panel instance = null;

    [Header("슬라이더")]
    public Slider MasterVolume;
 
    public Slider BGMVolume;

    public Slider SFXVolume;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }
}
