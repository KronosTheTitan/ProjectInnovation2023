using EventBus;
using Mirror;
using Unity.Mathematics;
using UnityEngine;

public class Character : NetworkBehaviour
{
    [Header("Stats")]
    [SerializeField, SyncVar] public int health;
    [SerializeField, SyncVar] public int remainingHealth;
    [SerializeField, SyncVar] public int speed;
    [SerializeField, SyncVar] public int remainingSpeed;
    [SerializeField, SyncVar] public int defence;
    [SerializeField, SyncVar] public int attack;
    [SerializeField, SyncVar] public int sense;

    [Header("Slots")]
    [SerializeField, SyncVar] public HeadArmour head;
    [SerializeField, SyncVar] public ChestArmour chest;
    [SerializeField, SyncVar] public LegArmour legs;
    [SerializeField, SyncVar] public FeetArmour feet;

    [SerializeField, SyncVar] public Weapon weapon;

    [Header("Other")]
    [SerializeField, SyncVar] public Node location;

    public enum Faction
    {
        Players,
        Enemies
    }
    
    [SerializeField, SyncVar] public Faction faction;

    private void Start()
    {
        if (isServer)
        {
            EventBus<OnStartTurn>.OnEvent += OnStartTurn;
        }
    }
    
    [Server]
    public void MakeAttack(Character target)
    {
        if(Vector3.Distance(transform.position , target.transform.position) > weapon.Range)
            return;

        int damage = attack + weapon.Damage;
        
        target.TakeDamage(damage);
    }

    [Server]
    protected virtual void TakeDamage(int amount){

        int modifiedAmount = math.clamp(amount - GetTotalDefence(), 0, int.MaxValue);

        remainingHealth -= modifiedAmount;
        
        EventBus<OnCharacterTakeDamage>.Publish(new OnCharacterTakeDamage());
        
        if(remainingHealth <= 0)
            Destroy(gameObject);
    }
    
    protected int GetTotalDefence()
    {
        int headDef = head.DefenceBonus;
        int chestDef = chest.DefenceBonus;
        int legsDef = legs.DefenceBonus;
        int feetDef = feet.DefenceBonus;

        int baseDef = defence;

        return baseDef + headDef + chestDef + legsDef + feetDef;
    }

    protected void OnStartTurn(OnStartTurn onStartTurn)
    {
        Debug.Log("Starting Turn");
        remainingSpeed = speed;
    }
}