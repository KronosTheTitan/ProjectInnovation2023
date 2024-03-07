using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace PlayerActions
{
    public class AttackAction : PlayerAction
    {
        public override Node[] PotentialTargets(Node source)
        {
            List<Node> output = new List<Node>();

            foreach (Node node in source.connections)
            {
                if (node.character == null)
                    continue;

                if (source.character.faction == node.character.faction)
                    continue;

                output.Add(node);
            }

            return output.ToArray();
        }

        [Command]
        public override void PerformAction(Node target, Character character)
        {
            if(target.character == null || character.remainingAttacksPerTurn == 0)
                return;
            
            character.MakeAttack(target.character);
        }
    }
}