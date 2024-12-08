using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class CameraManager : NetworkBehaviour
{
    public GameObject sceneCamera;
    void Start()
    {
        if(!isLocalPlayer)
        {
            if(sceneCamera !=null)
            {
                sceneCamera.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
