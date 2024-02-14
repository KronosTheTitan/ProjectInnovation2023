using Map;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    /// <summary>
    /// Custom editor for the HexGrid component, providing additional functionality in the Unity Inspector.
    /// </summary>
    [CustomEditor(typeof(TileGrid))]
    public class ObjectBuilderEditor : UnityEditor.Editor
    {
        /// <summary>
        /// Overrides the default Inspector GUI to include custom buttons for clearing and generating tiles.
        /// </summary>
        public override void OnInspectorGUI()
        {
            // Draw the default Inspector fields.
            DrawDefaultInspector();

            // Cast the target to a HexGrid instance.
            TileGrid tileGrid = (TileGrid)target;

            // Button for clearing the grid.
            if (GUILayout.Button("Clear!"))
            {
                tileGrid.ClearGrid();
            }

            // Button for generating tiles.
            if (GUILayout.Button("Generate!"))
            {
                tileGrid.GenerateGrid();
            }
        }
    }
}