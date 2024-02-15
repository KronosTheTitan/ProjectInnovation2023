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

            for (int i = 0; 0 < character.remainingSpeed && i < path.Length; i++, character.remainingSpeed--)
            {
                character.location.character = null;
                character.location = path[i];
                path[i].character = character;
            }

            character.transform.position = character.location.transform.position;
        }
    }
}