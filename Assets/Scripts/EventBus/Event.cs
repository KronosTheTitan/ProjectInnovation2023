using UnityEngine;

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
        public readonly Character character;

        public OnCharacterStartMoving(Character pCharacter)
        {
            character = pCharacter;
        }
    }

    public class OnCharacterStopMoving : Event
    {
        public readonly Character character;

        public OnCharacterStopMoving(Character pCharacter)
        {
            character = pCharacter;
        }
    }

    public class OnCharacterStartAttacking : Event
    {
        public readonly Character character;
        public readonly Vector3 targetPos;
        public readonly bool isMage;
        public OnCharacterStartAttacking(Character pCharacter, Vector3 pTargetPos, bool pIsMage = false)
        {
            character = pCharacter;
            isMage = pIsMage;
            targetPos = pTargetPos;
        }
    }

    public class OnCharacterGettingHit : Event
    {
        public readonly Character character;
        public OnCharacterGettingHit(Character pCharacter)
        {
            character = pCharacter;
        }
    }

    public class OnCharacterDies : Event
    {
        public readonly Character character;
        public OnCharacterDies(Character pCharacter)
        {
            character = pCharacter;
        }
    }

    public class GameEnd : Event
    {
        public readonly bool won;
        public GameEnd(bool pWon)
        {
            won = pWon;
        }
    }
}