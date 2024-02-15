using Mirror;
using Unity.Mathematics;
using UnityEngine;

public class Character : NetworkBehaviour
{
    [Header("Stats")]
    [SerializeField, SyncVar] public int health;
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

    [SerializeField, SyncVar] public Node location;

    public enum Faction
    {
        players,
        enemies
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
    public void TakeDamage(int amount){

        int modifiedAmount = math.clamp(amount - GetTotalDefence(), 0, int.MaxValue);

        health -= modifiedAmount;
    }
    
    [Server]
    public void MakeAttack(Character target)
    {
        if(Vector3.Distance(transform.position , target.transform.position) > weapon.Range)
            return;

        int damage = attack + weapon.Damage;
        
        target.TakeDamage(damage);
    }
    
    private int GetTotalDefence()
    {
        int headDef = head.DefenceBonus;
        int chestDef = chest.DefenceBonus;
        int legsDef = legs.DefenceBonus;
        int feetDef = feet.DefenceBonus;

        int baseDef = defence;

        return baseDef + headDef + chestDef + legsDef + feetDef;
    }

    private void OnStartTurn(OnStartTurn onStartTurn)
    {
        remainingSpeed = speed;
    }
}