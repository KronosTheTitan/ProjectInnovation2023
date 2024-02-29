using PlayerActions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hud : MonoBehaviour
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
    
    public void Setup(Player pPlayer, MoveAction pMove, AttackAction pAttack)
    {
        player = pPlayer;
        move = pMove;
        attack = pAttack;
    }

    [SerializeField] private Player player;
    [SerializeField] private MoveAction move;
    [SerializeField] private AttackAction attack;
    [SerializeField] private Healthbar healthbar;

    public Healthbar GetHealthBar()
    {
        return healthbar;
    }

    public void SelectMove()
    {
        player.SetSelectedAction(move);
    }

    public void SelectAttack()
    {
        player.SetSelectedAction(attack);
    }
}