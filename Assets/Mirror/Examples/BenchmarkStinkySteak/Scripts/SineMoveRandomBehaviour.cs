using Mirror.Core;
using Mirror.Examples.BenchmarkStinkySteak.Dependencies.netcode_benchmarker_util.Runtime.Scripts;
using Mirror.Examples.BenchmarkStinkySteak.Dependencies.netcode_benchmarker_util.Runtime.Scripts.BehaviourWrapper;
using UnityEngine;

namespace Mirror.Examples.BenchmarkStinkySteak.Scripts
{
    public class SineMoveRandomBehaviour : NetworkBehaviour
    {
        [SerializeField] private BehaviourConfig _config;
        private SinRandomMoveWrapper _wrapper;

        public override void OnStartServer()
        {
            if (isClient) return;

            _config.ApplyConfig(ref _wrapper);
            _wrapper.NetworkStart(transform);
        }

        private void FixedUpdate()
        {
            if (isClient) return;

            _wrapper.NetworkUpdate(transform);
        }
    }
}