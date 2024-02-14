using System.Collections.Generic;
using Mirror;
using Mirror.Core;
using UnityEngine;
using UnityEngine.Serialization;

public class Node : NetworkBehaviour
{
    [SerializeField, SyncVar] private Character character;
    public List<Node> connections;
    public Character Character => character;
    
    private void OnDrawGizmos()
    {
        foreach (Node tile  in connections)
        {
            if (tile.connections.Contains(this))
                Gizmos.color = Color.green;
            else
                Gizmos.color = Color.red;

            Gizmos.DrawLine(transform.position, tile.transform.position);
        }
    }
}