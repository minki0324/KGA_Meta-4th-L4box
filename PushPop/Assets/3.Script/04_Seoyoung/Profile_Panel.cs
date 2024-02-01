using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile_Panel : MonoBehaviour
{ 
    public static Profile_Panel instance = null;

    //처음 접속 시 보이는 패널
    [SerializeField] GameObject Profile_panel;

    //프로필 생성 패널
    [SerializeField] GameObject CreateName_panel;

    [SerializeField] GameObject CreateImage_panel;


    private void Awake()
    {
        if(instance == null)
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
        
    }

}
