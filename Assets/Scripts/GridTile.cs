using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class GridTile : NetworkBehaviour
{
    [SerializeField, SyncVar] private Character character;
    public List<GridTile> adjacentTiles;
    public Character Character => character;
    
    private void OnDrawGizmos()
    {
        foreach (GridTile tile  in adjacentTiles)
        {
            if (tile.adjacentTiles.Contains(this))
                Gizmos.color = Color.green;
            else
                Gizmos.color = Color.red;

            Gizmos.DrawLine(transform.position, tile.transform.position);
        }
    }
}