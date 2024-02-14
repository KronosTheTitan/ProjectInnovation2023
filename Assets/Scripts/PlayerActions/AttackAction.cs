using System.Collections.Generic;

namespace PlayerActions
{
    public class AttackAction : PlayerAction
    {
        public override Node[] PotentialTargets(Node source)
        {
            List<Node> output = new List<Node>();

            foreach (Node node in source.connections)
            {
                if (node.Character == null)
                    continue;

                if (source.Character.faction == node.Character.faction)
                    continue;

                output.Add(node);
            }

            return output.ToArray();
        }

        public override void PerformAction(Node target, Character character)
        {
            if(target.Character == null)
                return;
            
            character.MakeAttack(target.Character);
        }
    }
}