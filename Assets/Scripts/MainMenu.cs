using System;
using Mirror;
using Mirror.Discovery;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private CustomNetworkManager networkManager;
    [SerializeField] private NetworkDiscovery networkDiscovery;
    [SerializeField] private LobbyListEntry prefabEntry;
    [SerializeField] private VerticalLayoutGroup lobbyList;
    private List<long> foundServers = new List<long>();

    private void Start()
    {
        networkDiscovery.OnServerFound.AddListener(OnServerFound);
        networkDiscovery.StartDiscovery();
    }

    public void HostNewGameAsKnight()
    {
        CustomNetworkManager.singleton.firstPlayerIsKnight = true;
        networkManager.StartHost();
        networkDiscovery.AdvertiseServer();
    }

    public void HostNewGameAsMage()
    {
        CustomNetworkManager.singleton.firstPlayerIsKnight = false;
        networkManager.StartHost();
        networkDiscovery.AdvertiseServer();
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    private void OnServerFound(ServerResponse response)
    {
        if (!foundServers.Contains(response.serverId))
        {
            Debug.Log("Lobby Found!");
            TextMeshProUGUI lobbyButtonText = prefabEntry.GetComponentInChildren<TextMeshProUGUI>();
            if (lobbyButtonText != null)
            {
                lobbyButtonText.SetText("Join Server - ID: " + response.serverId);
            }
            Instantiate(prefabEntry, lobbyList.transform).Setup(response, networkDiscovery);
            foundServers.Add(response.serverId);
        }
    }
}