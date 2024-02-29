using System.Collections.Generic;
using EventBus;
using UnityEngine;

namespace AI
{
    public class EnemyAI : CanTakeTurn
    {
        private List<Character> enemies;
        private List<PlayerCharacter> playerCharacters;
        [SerializeField] private Pathfinder pathfinder;

        public override void TakeTurn()
        {
            foreach (Character enemy in enemies)
            {
                Debug.Log("Enemy taking turn");

                bool nextToPlayer = false;
                PlayerCharacter attackTarget = null;
                
                foreach (Node node in enemy.location.connections)
                {
                    if(node.character == null)
                        continue;
                    
                    if(node.character.faction == enemy.faction)
                        continue;

                    if (node.character.GetType() == typeof(PlayerCharacter))
                    {
                        attackTarget = (PlayerCharacter)node.character;
                        nextToPlayer = true;
                    }
                }

                if (attackTarget != null)
                {
                    enemy.MakeAttack(attackTarget);
                    continue;
                }

                PlayerCharacter closestPlayer = null;
                float closestDistance = float.PositiveInfinity;

                foreach (PlayerCharacter playerCharacter in playerCharacters)
                {
                    float dist = Vector3.Distance(enemy.transform.position, playerCharacter.transform.position);

                    if (dist > enemy.sense)
                        continue;

                    if (closestDistance > dist)
                    {
                        closestPlayer = playerCharacter;
                        closestDistance = dist;
                    }
                }
                
                if(closestPlayer == null)
                    continue;

                Node[] path = pathfinder.FindPath(enemy.location, closestPlayer.location).ToArray();
                
                for (int i = 0; 0 < enemy.remainingSpeed && i < path.Length; i++, enemy.remainingSpeed--)
                {
                    enemy.location.character = null;
                    enemy.location = path[i];
                    path[i].character = enemy;
                }

                enemy.transform.position = enemy.location.transform.position;
                enemy.location.character = enemy;
                
                foreach (Node node in enemy.location.connections)
                {
                    if(node.character == null)
                        continue;
                    
                    if(node.character.faction == enemy.faction)
                        continue;

                    if (node.character.GetType() == typeof(PlayerCharacter))
                    {
                        attackTarget = (PlayerCharacter)node.character;
                        nextToPlayer = true;
                    }
                }

                if (attackTarget != null)
                {
                    enemy.MakeAttack(attackTarget);
                }
            }
            
            EventBus<NextTurnButtonPressed>.Publish(new NextTurnButtonPressed(this));
        }
    }
}