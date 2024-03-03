using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EventBus;
using UnityEngine;

namespace AI
{
    public class EnemyAI : CanTakeTurn
    {
        [SerializeField] private List<Character> enemies;
        [SerializeField] private List<Character> playerCharacters;
        [SerializeField] private Pathfinder pathfinder;
        [SerializeField] private bool isTakingActions;

        [SerializeField] private int currentEnemy;
        [SerializeField] private bool currentEnemyHasMoved;

        private void Awake()
        {
            EventBus<OnPlayerJoinedServer>.OnEvent += RegisterNewPlayer;
        }

        private enum AiStage
        {
            InitialAttackAttempt,
            Movement,
            FinalAttackAttempt
        }

        private AiStage currentStage;
        
        private void Update()
        {
            if(!isTakingActions)
                return;

            Debug.Log("Handling Enemy");
            switch (currentStage)
            {
                case AiStage.InitialAttackAttempt:
                    Debug.Log("AttemptingInitialAttack");
                    AttemptAttack(enemies[currentEnemy]);
                    currentStage = AiStage.Movement;
                    break;
                case AiStage.Movement:
                    if (!currentEnemyHasMoved)
                    {
                        Debug.Log("Enemy Starts Moving");
                        AttemptMove(enemies[currentEnemy]);
                        currentEnemyHasMoved = true;
                        break;
                    }
                    
                    if (enemies[currentEnemy].Mover.isMoving)
                    {
                        Debug.Log("Enemy is moving");
                        break;
                    }
                        
                    Debug.Log("Enemy is done moving");
                    currentStage = AiStage.FinalAttackAttempt;
                    
                    break;
                
                case AiStage.FinalAttackAttempt:

                    Debug.Log("AttemptingFinalAttack");
                    AttemptAttack(enemies[currentEnemy]);

                    currentEnemy++;
                    currentEnemyHasMoved = false;
                    currentStage = AiStage.InitialAttackAttempt;
                    break;
            }

            if (currentEnemy == enemies.Count)
            {
                EndTurn();
            }
        }

        private void AttemptAttack(Character enemy)
        {
            Character attackTarget = checkNeighborsForTargets(enemy);
            
            if (attackTarget != null) 
            {
                enemy.MakeAttack(attackTarget);
            }
        }

        private void AttemptMove(Character enemy)
        {
            Character closestPlayer = GetClosestPlayer(enemy);
            
            if(closestPlayer == null) 
                return;
            
            Debug.Log("Starting pathfinder");
            Node[] path = pathfinder.FindPath(enemy.location, closestPlayer.location).ToArray();
            
            enemy.Mover.StartMovement(path);
        }

        private void EndTurn()
        {
            Debug.Log("Ending Turn");
            isTakingActions = false;
            EventBus<NextTurnButtonPressed>.Publish(new NextTurnButtonPressed(this));
        }

        public override async void TakeTurn()
        {
            Debug.Log("Taking Turn");
            isTakingActions = true;
            currentEnemy = 0;
        }

        private void RegisterNewPlayer(OnPlayerJoinedServer onPlayerJoinedServer)
        {
            playerCharacters.Add(onPlayerJoinedServer.Player.Character);
        }

        private Character GetClosestPlayer(Character enemy)
        {
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

            return closestPlayer;
        }

        private Character checkNeighborsForTargets(Character enemy)
        {
            Character attackTarget = null;
            
            foreach (Node node in enemy.location.connections) 
            { 
                if(node.character == null) 
                    continue;

                if(node.character.faction == enemy.faction) 
                    continue;


                attackTarget = node.character; 
            }

            return attackTarget;
        }
    }
}