using System;
using Mirror;
using Mirror.Discovery;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private NetworkDiscovery networkDiscovery;
    public void HostNewGame()
    {
        networkManager.StartHost();
    }

    public void JoinGame()
    {
        
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void Start()
    {
        networkDiscovery.StartDiscovery();
    }
}