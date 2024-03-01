namespace EventBus
{
    public class Event
    {
        
    }

    public class OnStartTurn : Event
    {
    
    }

    public class OnEndTurn : Event
    {
        
    }

    public class OnCharacterTakeDamage : Event
    {
    
    }

    public class OnPlayerTakeDamage : Event
    {
        public readonly Player Player;
        public readonly int TakenDamage;
        public readonly int NewHealth;
        public readonly int MaxHealth;

        public OnPlayerTakeDamage(Player player, int takenDamage, int newHealth, int maxHealth)
        {
            Player = player;
            TakenDamage = takenDamage;
            NewHealth = newHealth;
            MaxHealth = maxHealth;
        }
    }

    public class OnGameStart : Event
    {
    
    }

    public class OnStartCombat : Event
    {

    }

    public class OnEndCombat : Event
    {
    
    }

    public class OnPlayerJoinedServer : Event
    {
        public readonly Player Player;

        public OnPlayerJoinedServer(Player pPlayer)
        {
            Player = pPlayer;
        }
    }

    public class OnPlayerJoinedLocal : Event
    {
        public readonly Player Player;

        public OnPlayerJoinedLocal(Player pPlayer)
        {
            Player = pPlayer;
        }
    }
}