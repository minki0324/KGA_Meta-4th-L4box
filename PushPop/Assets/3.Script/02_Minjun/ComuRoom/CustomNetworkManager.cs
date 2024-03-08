using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Linq;
using System;

public class CustomNetworkManager : NetworkManager
{
    
    public GameObject[] avatars;

    // Start is called before the first frame update
    public Button selectButton;
  
    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
    }
    private void Start()
    {
       
    }
}

