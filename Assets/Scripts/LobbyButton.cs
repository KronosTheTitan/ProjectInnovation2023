using Mirror;
using TMPro;
using UnityEngine;

public class LobbyButton : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    
    public void JoinButton(CustomNetworkManager networkManager)
    {
        networkManager.StartClient();
    }
}