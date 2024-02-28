using Mirror;
using Mirror.Discovery;
using UnityEngine;

public class LobbyListEntry : MonoBehaviour
{
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
        NetworkManager.singleton.StartClient(_response.uri);
    }
}