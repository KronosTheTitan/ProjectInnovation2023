using Mirror;
using Mirror.Discovery;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject lobbyList;
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private NetworkDiscovery networkDiscovery;
    public void Host()
    {
        networkManager.StartHost();
    }

    public void OpenLobbyList()
    {
        lobbyList.SetActive(true);
        networkDiscovery.StartDiscovery();
    }

    public void CloseLobbyList()
    {
        lobbyList.SetActive(false);
        networkDiscovery.StopDiscovery();
    }

    public void Quit()
    {
        Application.Quit();
    }
}