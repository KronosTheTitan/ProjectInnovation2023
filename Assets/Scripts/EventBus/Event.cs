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
        public readonly int TakenDamage;

        public OnPlayerTakeDamage(int takenDamage)
        {
            TakenDamage = takenDamage;
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

    public class OnCharacterStartMoving : Event
    {
        Character character;

        public OnCharacterStartMoving(Character pCharacter)
        {
            character = pCharacter;
        }
    }

    public class OnCharacterStopMoving : Event
    {
        Character character;

        public OnCharacterStopMoving(Character pCharacter)
        {
            character = pCharacter;
        }
    }

    public class OnCharacterStartAttacking : Event
    {
        Character character;

        public OnCharacterStartAttacking(Character pCharacter)
        {
            character = pCharacter;
        }
    }
}