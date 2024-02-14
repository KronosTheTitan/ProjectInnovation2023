using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Map
{
    /// <summary>
    /// Represents a hexagonal grid.
    /// </summary>
    public class TileGrid : MonoBehaviour
    {
        public List<GridTile> randomTilesToGenerate = new List<GridTile>();

        [SerializeField] private int width;
        [SerializeField] private int height;

        [SerializeField] private List<GridTile> generatedTiles = new List<GridTile>();

        [SerializeField] private float offsetXAxis = 1.73205f; // Horizontal offset between hex tiles.
        [SerializeField] private float offsetZAxis = 1.5f;     // Vertical offset between hex tiles.

        [SerializeField] private GridTile prefab;

        
        public void GenerateGrid()
        {
            ClearGrid();
            GridTile[][] grid = new GridTile[width][];
            for (int index = 0; index < width; index++)
            {
                grid[index] = new GridTile[height];
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vector3 position = new Vector3(x * offsetXAxis, 0, y * offsetZAxis);

                    // Create a new HexTile using the provided factory method.
                    GridTile tile = Instantiate(prefab, position,quaternion.identity,transform);
                    generatedTiles.Add(tile);

                    grid[x][y] = tile;
                    
                    // Connect the tile with its left neighbor.
                    if(x - 1 >= 0 && y >= 0 )
                        ConnectTiles(tile, grid[x - 1][y]);

                    // Connect the tile with its top neighbor.
                    if(x >= 0 && y - 1 >= 0)
                        ConnectTiles(tile, grid[x][y - 1]);

                    // Set the name of the tile based on its grid coordinates.
                    tile.name = new Vector2(x, y).ToString();
                }
            }
        }

        /// <summary>
        /// Clears the grid and destroys generated tiles.
        /// </summary>
        public void ClearGrid()
        {
            while (generatedTiles.Count > 0)
            {
                GridTile tile = generatedTiles[0];
                generatedTiles.Remove(generatedTiles[0]);

                DestroyImmediate(tile.gameObject);
            }

            generatedTiles.Clear();
        }

        /// <summary>
        /// Connects two hexagonal tiles by adding them to each other's adjacent tiles list.
        /// </summary>
        /// <param name="a">The first hexagonal tile.</param>
        /// <param name="b">The second hexagonal tile.</param>
        private void ConnectTiles(GridTile a, GridTile b)
        {
            a.adjacentTiles.Add(b);
            b.adjacentTiles.Add(a);
        }
    }
}