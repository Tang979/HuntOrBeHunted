using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class AutoStartHost : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(!NetworkClient.active&&!NetworkServer.active)
        {
            NetworkManager.singleton.StartHost();
            Debug.Log("Host started automatically.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
