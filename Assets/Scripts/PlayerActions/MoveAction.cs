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

            character.Mover.StartMovement(path);
        }
    }
}