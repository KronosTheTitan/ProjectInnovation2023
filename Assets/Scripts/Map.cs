using UnityEngine;

public class Map : MonoBehaviour
{
    #region Singleton
    private static Map _instance;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject); // Destroy this object if another instance exists.
        }
        else
        {
            _instance = this;
        }
    }

    /// <summary>
    /// Gets the singleton instance of the GameManager.
    /// </summary>
    /// <returns>The GameManager instance.</returns>
    public static Map GetInstance()
    {
        return _instance;
    }

    private void OnDisable()
    {
        _instance = null;
    }

    #endregion

    [SerializeField] private Node[] nodes;
    public Node[] Nodes => nodes;
}
