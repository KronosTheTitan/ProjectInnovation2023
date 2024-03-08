using Mirror;
using UnityEngine;

namespace PlayerActions
{
    public class MoveAction : PlayerAction
    {
        public override Node[] PotentialTargets(Node source)
        {
            return Map.GetInstance().Nodes;
        }

        [Command]
        public override void PerformAction(Node target, Character character)
        {
            //Debug.Log("Moving");
            
            Node[] path = Pathfinder.FindPath(character.location, target).ToArray();

            //Debug.Log("");
            
            character.Mover.StartMovement(path);
        }
    }
}