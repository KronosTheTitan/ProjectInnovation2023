using System;
using Mirror.Core;
using UnityEngine;

namespace PlayerActions
{
    public abstract class PlayerAction : NetworkBehaviour
    {
        public abstract Node[] PotentialTargets(Node source);
    
        public abstract void PerformAction(Node target, Character character);
    }
}