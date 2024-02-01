using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile_Panel : MonoBehaviour
{
    //proofile ������ ���������ϰ� ��𼭳� ���� �ʼ� -> Sigleton
    public static Profile_Panel instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
