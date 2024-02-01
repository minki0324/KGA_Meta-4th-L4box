using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile_Panel : MonoBehaviour
{ 
    public static Profile_Panel instance = null;

    //ó�� ���� �� ���̴� �г�
    [SerializeField] GameObject Profile_panel;

    //������ ���� �г�
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
