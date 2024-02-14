﻿using Mirror.Core;
using UnityEngine;

namespace Mirror.Examples._Common.Scripts.PhysicsSimulator
{
    public class PhysicsSimulator : MonoBehaviour
    {
        PhysicsScene physicsScene;
        PhysicsScene2D physicsScene2D;

        bool simulatePhysicsScene;
        bool simulatePhysicsScene2D;

        void Awake()
        {
            if (NetworkServer.active)
            {
                physicsScene = gameObject.scene.GetPhysicsScene();
                simulatePhysicsScene = physicsScene.IsValid() && physicsScene != Physics.defaultPhysicsScene;

                physicsScene2D = gameObject.scene.GetPhysicsScene2D();
                simulatePhysicsScene2D = physicsScene2D.IsValid() && physicsScene2D != Physics2D.defaultPhysicsScene;
            }
            else
            {
                enabled = false;
            }
        }

        [ServerCallback]
        void FixedUpdate()
        {
            if (simulatePhysicsScene)
                physicsScene.Simulate(Time.fixedDeltaTime);

            if (simulatePhysicsScene2D)
                physicsScene2D.Simulate(Time.fixedDeltaTime);
        }
    }
}
