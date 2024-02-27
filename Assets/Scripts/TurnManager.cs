using Mirror;
using UnityEngine;

public class TurnManager : NetworkBehaviour
{
    [SerializeField, SyncVar] private Player activePlayer;
    [SerializeField, SyncVar] private int activePlayerIndex;
    [SerializeField, SyncVar] private SyncList<Player> players;

    public Player ActivePlayer => activePlayer;
    
    [Command(requiresAuthority = false)]
    public void NextTurn(Player player)
    {
        if(player != activePlayer)
            return;

        activePlayerIndex++;
        if (activePlayerIndex >= players.Count)
            activePlayerIndex = 0;

        activePlayer = players[activePlayerIndex];
        
        EventBus<OnStartTurn>.Publish(new OnStartTurn());
    }

    public void RegisterNewPlayer(Player player)
    {
        players.Add(player);
    }
}