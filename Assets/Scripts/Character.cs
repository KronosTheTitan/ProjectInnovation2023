using EventBus;
using Mirror;
using Unity.Mathematics;
using UnityEngine;
using AI;

public class Character : NetworkBehaviour
{
    [Header("Stats")]
    [SerializeField, SyncVar] public int health;
    [SerializeField, SyncVar] public int remainingHealth;
    [SerializeField, SyncVar] public int speed;
    [SerializeField, SyncVar] public int remainingSpeed;
    [SerializeField, SyncVar] public int defence;
    [SerializeField, SyncVar] public int attack;
    [SerializeField, SyncVar] public int attacksPerTurn;
    [SerializeField, SyncVar] public int remainingAttacksPerTurn;
    [SerializeField, SyncVar] public int sense;
    [SerializeField, SyncVar] public float rotationSpeed;
    public CharacterMover Mover;

    [Header("Slots")]
    [SerializeField, SyncVar] public HeadArmour head;
    [SerializeField, SyncVar] public ChestArmour chest;
    [SerializeField, SyncVar] public LegArmour legs;
    [SerializeField, SyncVar] public FeetArmour feet;

    [SerializeField, SyncVar] public Weapon weapon;

    [Header("Other")]
    [SerializeField, SyncVar] public Node location;

    public Healthbar healthbar;
    public bool isAttacking = false;
    [SerializeField] protected AudioSource attackSound;
    [SerializeField] private AudioSource takingDamageSound;
    public bool isDead;

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
            isDead = false;
            healthbar = GetComponentInChildren<Healthbar>();
            EventBus<OnStartTurn>.OnEvent += OnStartTurn;
        }
    }
    
    [Server]
    public virtual void MakeAttack(Character target)
    {
        Debug.Log("starting attack");
        Debug.Log("Remaining attacks : " + remainingAttacksPerTurn);
        
        if(remainingAttacksPerTurn == 0)
            return;
        
        Debug.Log("sufficient remaining attacks");
        
        if (Vector3.Distance(transform.position, target.transform.position) > weapon.Range)
            return;
        
        Debug.Log("within range");

        EventBus<OnCharacterStartAttacking>.Publish(new OnCharacterStartAttacking(this, target.transform.position));

        int damage = attack + weapon.Damage;
        remainingAttacksPerTurn--;
        Debug.Log("target: "+ target.name);

        target.TakeDamage(damage);
        attackSound.Play();
    }

    [Server]
    public virtual void TakeDamage(int amount){
        
        Debug.Log(gameObject.name);

        int modifiedAmount = math.clamp(amount - GetTotalDefence(), 0, int.MaxValue);

        remainingHealth -= modifiedAmount;

        EventBus<OnCharacterTakeDamage>.Publish(new OnCharacterTakeDamage());
        takingDamageSound.Play();

        Debug.Log(modifiedAmount);
        if (faction == Faction.Enemies)
        {
            if (remainingHealth <= 0)
            {
                DieOnClients();
                EventBus<OnCharacterDies>.Publish(new OnCharacterDies(this));
            }
            healthbar.SetHealth(remainingHealth, health);
            EventBus<OnCharacterGettingHit>.Publish(new OnCharacterGettingHit(this));
        }
        else if (faction == Faction.Players && remainingHealth <= 0)
        {
            CustomNetworkManager.singleton.StopClient();
            CustomNetworkManager.singleton.StopServer();
            EventBus<GameEnd>.Publish(new GameEnd(false));
        }
    }

    [ClientRpc]
    public void DieOnClients()
    {
        //gameObject.SetActive(false);
        Debug.Log("DIE ON CLIENT");
        isDead = true;
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
        remainingSpeed = speed;
        remainingAttacksPerTurn = attacksPerTurn;
    }
}