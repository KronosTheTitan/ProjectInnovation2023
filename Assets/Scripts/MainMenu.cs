using System;
using Mirror;
using Mirror.Discovery;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private NetworkDiscovery networkDiscovery;
    [SerializeField] private LobbyListEntry prefabEntry;
    [SerializeField] private VerticalLayoutGroup lobbyList;

    private void Start()
    {
        networkDiscovery.OnServerFound.AddListener(OnServerFound);
        networkDiscovery.StartDiscovery();
    }

    public void HostNewGame()
    {
        networkManager.StartHost();
        networkDiscovery.AdvertiseServer();
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void OnServerFound(ServerResponse response)
    {
        Instantiate(prefabEntry, lobbyList.transform);
    }
}