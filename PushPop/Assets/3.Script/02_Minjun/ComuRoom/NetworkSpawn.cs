using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (var Prebab in NetworkManager.singleton.spawnPrefabs)
        {
            NetworkServer.Spawn(Prebab);
        }
    }
}
