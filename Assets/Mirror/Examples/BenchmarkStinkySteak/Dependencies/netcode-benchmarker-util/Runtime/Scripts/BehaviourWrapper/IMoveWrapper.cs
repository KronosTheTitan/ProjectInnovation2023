using UnityEngine;

namespace Mirror.Examples.BenchmarkStinkySteak.Dependencies.netcode_benchmarker_util.Runtime.Scripts.BehaviourWrapper
{
    public interface IMoveWrapper
    {
        void NetworkStart(Transform transform);
        void NetworkUpdate(Transform transform);
    }
}