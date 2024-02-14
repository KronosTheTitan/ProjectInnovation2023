using Mirror.Core;
using UnityEngine;

namespace Mirror.Examples.Billiards.Ball
{
    public class RedBall : NetworkBehaviour
    {
        // destroy when entering a pocket.
        // there's only one trigger in the scene (the pocket).
        [ServerCallback]
        void OnTriggerEnter(Collider other)
        {
            NetworkServer.Destroy(gameObject);
        }
    }
}
