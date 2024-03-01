using Mirror;
using System;
using UnityEngine;

public class OLDHealth : NetworkBehaviour
{
    /*
    //public static event EventHandler<DeathEventArgs> OnDeath;
    //public event EventHandler<HealthChangedEventArgs> OnHealthChanged;

    public bool IsDead => health == 0f;

    [SerializeField] private float maxHealth = 100f;

    [SyncVar(hook = nameof(HandleHealthUpdated))]
    private float health = 0f;

    public override void OnStartServer()
    {
        health = maxHealth;
    }

    public void DealDamage(float pDamage)
    {
        ChangeHealth(pDamage);
    }

    [ServerCallback]
    private void OnDestroy()
    {
        //OnDeath?.Invoke(this, new DeathEventArgs { ConnectionToClient = connectionToClient });
    }

    [Server]
    private void ChangeHealth(float pDamage)
    {
        health -= pDamage;
        health = Mathf.Clamp(health, 0, maxHealth);

        if (health == 0)
        {
            //OnDeath?.Invoke(this, new DeathEventArgs { ConnectionToClient = connectionToClient });
            RpcHandleDeath();
        }
    }   

    private void HandleHealthUpdated(float pOldValue, float pNewValue)
    {
        /*
        OnHealthChanged?.Invoke(this, new HealthChangedEventArgs
        { 
            Health = health,
            MaxHealth = maxHealth
        });
        
    }

    [ClientRpc]
    private void RpcHandleDeath()
    {
        gameObject.SetActive(false);
    }
    */
}
