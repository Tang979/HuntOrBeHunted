using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class AudioManager : NetworkBehaviour
{
    public AudioListener audioListener;
    void Start()
    {
        if(!isLocalPlayer)
        {
            if(audioListener != null)
            {
                audioListener.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
