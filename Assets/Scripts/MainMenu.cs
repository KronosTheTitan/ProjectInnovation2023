using System;
using Mirror;
using Mirror.Discovery;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private NetworkDiscovery networkDiscovery;
    
    public void HostNewGame()
    {
        networkManager.StartHost();
        networkDiscovery.AdvertiseServer();
    }

    private void JoinGame()
    {
        networkDiscovery.OnServerFound.AddListener(Connect);
        networkDiscovery.StartDiscovery();
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    private void Connect(ServerResponse info)
    {
        networkDiscovery.StopDiscovery();
        networkManager.StartClient(info.uri);
    }
}