using Mirror;
using Mirror.Discovery;
using UnityEngine;

public class LobbyListEntry : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;
    private ServerResponse _response;
    private NetworkDiscovery _discovery;

    public void Setup(ServerResponse pResponse, NetworkDiscovery pDiscovery)
    {
        _response = pResponse;
        _discovery = pDiscovery;
    }

    public void Join()
    {
        _discovery.StopDiscovery();
        CustomNetworkManager.singleton.StartClient(_response.uri);
    }
}