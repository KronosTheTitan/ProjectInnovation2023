using UnityEngine;

namespace PlayerActions
{
    public class MoveAction : PlayerAction
    {
        [SerializeField] private Pathfinder pathfinder;
        public override Node[] PotentialTargets(Node source)
        {
            throw new System.NotImplementedException();
        }

        public override void PerformAction(Node target, Character character)
        {
            throw new System.NotImplementedException();
        }
    }
}