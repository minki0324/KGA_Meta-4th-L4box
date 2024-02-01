using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile_Panel : MonoBehaviour
{
    //proofile 끝까지 가져가야하고 어디서나 접근 필수 -> Sigleton
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
