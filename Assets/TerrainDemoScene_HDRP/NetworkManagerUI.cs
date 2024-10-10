using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button ServerBtn;
    [SerializeField] private Button HostBtn;
    [SerializeField] private Button ClientBtn;
    private void Awake()
    {
        if (NetworkManager.Singleton == null)
        {
            Debug.LogError("NetworkManager not found in the scene!");
            return;
        }
        ServerBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartServer();
        });

        HostBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
        });

        ClientBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartClient();
        });
    }
    private void Update()
    {
        // Listen to key press in Update instead of Awake
        if (Input.GetKeyDown(KeyCode.H))
        {
            NetworkManager.Singleton.StartHost();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            NetworkManager.Singleton.StartServer();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            NetworkManager.Singleton.StartClient();
        }
    }
}
