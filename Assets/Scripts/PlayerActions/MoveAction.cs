using Mirror;
using UnityEngine;

namespace PlayerActions
{
    public class MoveAction : PlayerAction
    {
        [SerializeField] private Pathfinder pathfinder;
        public override Node[] PotentialTargets(Node source)
        {
            return Map.GetInstance().Nodes;
        }

        [Command]
        public override void PerformAction(Node target, Character character)
        {
            Node[] path = pathfinder.FindPath(character.location, target).ToArray();

            foreach (Node node in path)
            {
                if( 0 >= character.remainingSpeed)
                    break;
                
                character.location.character = null;
                character.location = node;
                node.character = character;
                character.remainingSpeed--;
            }

            character.transform.position = character.location.transform.position;
            character.location.character = character;
        }
    }
}