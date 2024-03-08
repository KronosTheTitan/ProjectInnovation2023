using EventBus;
using Mirror;
using PlayerActions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hud : NetworkBehaviour
{
    #region Singleton
    private static Hud _instance;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if (_instance != null)
            Destroy(this.gameObject); // Destroy this object if another instance exists.
        else
            _instance = this; // Set this as the singleton instance.
    }

    /// <summary>
    /// Gets the singleton instance of the GameManager.
    /// </summary>
    /// <returns>The GameManager instance.</returns>
    public static Hud GetInstance()
    {
        return _instance;
    }
    #endregion
    
    public void Setup(Player pPlayer)
    {
        player = pPlayer;
    }

    [SerializeField] private Player player;
    [SerializeField] private Healthbar healthbar;

    public Healthbar GetHealthBar()
    {
        return healthbar;
    }

    public void NextTurn()
    {
        CallNextTurn(player);
    }

    [Command(requiresAuthority = false)]
    private void CallNextTurn(Player player)
    {
        //Debug.Log("END TURN CLICKED -----------------------------------");
        EventBus<NextTurnButtonPressed>.Publish(new NextTurnButtonPressed(player));
    }
    
    

    private void OnDrawGizmos()
    {
        Vector3 cornerLeft = new Vector3(1344+256, 589-128-64);
        Gizmos.DrawSphere(cornerLeft, 50);
    }
}