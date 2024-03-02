using System;
using System.Collections.Generic;
using EventBus;
using UnityEngine;

namespace AI
{
    public class EnemyAI : CanTakeTurn
    {
        [SerializeField] private List<Character> enemies;
        [SerializeField] private List<Character> playerCharacters;
        [SerializeField] private Pathfinder pathfinder;

        private void Awake()
        {
            EventBus<OnPlayerJoinedServer>.OnEvent += RegisterNewPlayer;
        }

        public override void TakeTurn()
        {
            
            if (enemies.Count == 0)
            {
                Debug.Log("Exiting AI");
                EventBus<NextTurnButtonPressed>.Publish(new NextTurnButtonPressed(this));
                return;
            }
            
            foreach (Character enemy in enemies)
            {
                Debug.Log("Enemy taking turn");

                bool nextToPlayer = false;
                Character attackTarget = null;
                
                foreach (Node node in enemy.location.connections)
                {
                    if(node.character == null)
                        continue;
                    
                    if(node.character.faction == enemy.faction)
                        continue;
                    
                    attackTarget = node.character;
                    nextToPlayer = true;
                }

                if (attackTarget != null)
                {
                    Debug.Log("Making Attack");
                    enemy.MakeAttack(attackTarget);
                    continue;
                }

                Character closestPlayer = null;
                float closestDistance = float.PositiveInfinity;

                foreach (Character character in playerCharacters)
                {
                    float dist = Vector3.Distance(enemy.transform.position, character.transform.position);

                    if (dist > enemy.sense)
                        continue;

                    if (closestDistance >= dist)
                    {
                        closestPlayer = character;
                        closestDistance = dist;
                    }
                }
                
                Debug.Log("Checking if closest Player is not null");
                
                if(closestPlayer == null)
                    continue;
                
                Debug.Log("Moving To Player");

                Node[] path = pathfinder.FindPath(enemy.location, closestPlayer.location).ToArray();
                
                foreach (Node node in path)
                {
                    if( 0 >= enemy.remainingSpeed)
                        break;
                
                    enemy.location.character = null;
                    enemy.location = node;
                    node.character = enemy;
                    enemy.remainingSpeed--;
                }

                enemy.transform.position = enemy.location.transform.position;
                enemy.location.character = enemy;
                
                foreach (Node node in enemy.location.connections)
                {
                    if(node.character == null)
                        continue;
                    
                    if(node.character.faction == enemy.faction)
                        continue;
                    
                    
                    attackTarget = node.character;
                    nextToPlayer = true;
                }

                if (attackTarget != null)
                {
                    Debug.Log("Making Attack");
                    enemy.MakeAttack(attackTarget);
                }
            }
            
            EventBus<NextTurnButtonPressed>.Publish(new NextTurnButtonPressed(this));
        }

        private void RegisterNewPlayer(OnPlayerJoinedServer onPlayerJoinedServer)
        {
            playerCharacters.Add(onPlayerJoinedServer.Player.Character);
        }
    }
}