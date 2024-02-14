using UnityEditor;
using UnityEngine.UIElements;

namespace Mirror.Hosting.Edgegap.Editor
{
    [CustomEditor(typeof(EdgegapToolScript))]
    public class EdgegapPluginScriptEditor : UnityEditor.Editor
    {
        VisualElement _serverDataContainer;

        private void OnEnable()
        {
            _serverDataContainer = EdgegapServerDataManager.GetServerDataVisualTree();
            EdgegapServerDataManager.RegisterServerDataContainer(_serverDataContainer);
        }

        private void OnDisable()
        {
            EdgegapServerDataManager.DeregisterServerDataContainer(_serverDataContainer);
        }

        public override VisualElement CreateInspectorGUI()
        {
            return _serverDataContainer;
        }
    }
}