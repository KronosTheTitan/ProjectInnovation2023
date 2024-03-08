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

    [SerializeField, SyncVar] private int remainingSharedHealth = 100;
    private int startSharedHealth;
    private Healthbar healthbar;

    private void Awake()
    {
        EventBus<OnPlayerJoinedServer>.OnEvent += RegisterNewPlayer;
        EventBus<NextTurnButtonPressed>.OnEvent += NextTurn;
        EventBus<OnPlayerTakeDamage>.OnEvent += OnPlayerTakeDamage;
        startSharedHealth = remainingSharedHealth;
        healthbar = GetComponent<Healthbar>();
        if (healthbar == null)
        {
            throw new System.Exception("There is no Healthbar component.");
        }
        //healthbar.SetHealth(remainingSharedHealth, startSharedHealth);
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
        onPlayerJoinedServer.Player.TurnManager = this;
        if (activeTurnTaker == null)
        {
            activeTurnTakerIndex = _turnTakers.IndexOf(onPlayerJoinedServer.Player);
            activeTurnTaker = onPlayerJoinedServer.Player;
        }
    }

    private void OnPlayerTakeDamage(OnPlayerTakeDamage onPlayerTakeDamage)
    {
        remainingSharedHealth -= onPlayerTakeDamage.TakenDamage;
        healthbar.SetHealth(remainingSharedHealth, startSharedHealth);
    }
}