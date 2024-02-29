using System;
using AI;
using EventBus;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;

public class TurnManager : NetworkBehaviour
{
    [SerializeField, SyncVar] private CanTakeTurn activeTurnTaker;
    [SerializeField, SyncVar] private int activeTurnTakerIndex;
    [SerializeField, SyncVar] private EnemyAI _enemyAI;
    private readonly SyncList<CanTakeTurn> _turnTakers = new SyncList<CanTakeTurn>();
    

    private void Awake()
    {
        EventBus<OnPlayerJoinedServer>.OnEvent += RegisterNewPlayer;
        EventBus<NextTurnButtonPressed>.OnEvent += NextTurn;
        _turnTakers.Add(_enemyAI);
    }

    public CanTakeTurn ActiveTurnTaker => activeTurnTaker;
    
    [Server]
    private void NextTurn(NextTurnButtonPressed nextTurnButtonPressed)
    {
        if(nextTurnButtonPressed.CanTakeTurn != activeTurnTaker)
            return;
        
        EventBus<OnEndTurn>.Publish(new OnEndTurn());

        activeTurnTakerIndex++;
        if (activeTurnTakerIndex >= _turnTakers.Count)
            activeTurnTakerIndex = 0;

        activeTurnTaker = _turnTakers[activeTurnTakerIndex];
        activeTurnTaker.TakeTurn();
        
        EventBus<OnStartTurn>.Publish(new OnStartTurn());
    }

    private void RegisterNewPlayer(OnPlayerJoinedServer onPlayerJoinedServer)
    {
        _turnTakers.Add(onPlayerJoinedServer.Player);
        if (activeTurnTaker == null)
        {
            activeTurnTakerIndex = _turnTakers.IndexOf(onPlayerJoinedServer.Player);
            activeTurnTaker = onPlayerJoinedServer.Player;
        }
    }
}