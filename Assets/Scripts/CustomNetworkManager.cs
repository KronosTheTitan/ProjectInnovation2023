using UnityEngine;
using Mirror;

public struct CreateCharacterMessage : NetworkMessage
{
    public bool isKnight;
}

public class CustomNetworkManager : NetworkManager
{
    public GameObject PlayerPrefab2;
    public bool firstPlayerIsKnight;
    public static new CustomNetworkManager singleton => (CustomNetworkManager)NetworkManager.singleton;

    public override void OnStartServer()
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler<CreateCharacterMessage>(OnCreateCharacter);
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();

        CreateCharacterMessage characterMessage = new CreateCharacterMessage
        {
            isKnight = firstPlayerIsKnight
        };

        firstPlayerIsKnight = false;
        NetworkClient.Send(characterMessage);

    }

    void OnCreateCharacter(NetworkConnectionToClient conn, CreateCharacterMessage message)
    {
        if (message.isKnight)
        {
            GameObject gameobject = Instantiate(PlayerPrefab2);
            NetworkServer.AddPlayerForConnection(conn, gameobject);
        }
        else
        {
            GameObject gameobject = Instantiate(playerPrefab);
            NetworkServer.AddPlayerForConnection(conn, gameobject);
        }
    }    
}
