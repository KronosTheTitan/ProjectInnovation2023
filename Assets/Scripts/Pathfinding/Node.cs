using System.Collections.Generic;
using Mirror;
using UnityEngine;


public class Node : NetworkBehaviour
{
    public Character character;
    public List<Node> connections;

    private void Awake()
    {
        
    }

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