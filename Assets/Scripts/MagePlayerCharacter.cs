using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBus;

public class MagePlayerCharacter : PlayerCharacter
{
    public override void MakeAttack(Character target)
    {
        if (remainingAttacksPerTurn == 0)
            return;

        if (Vector3.Distance(transform.position, target.transform.position) > weapon.Range)
            return;


        EventBus<OnCharacterStartAttacking>.Publish(new OnCharacterStartAttacking(this, true));

        Node[] pathToEnemy = Pathfinder.FindPath(location,target.location).ToArray();
        
        int damage = attack + weapon.Damage + pathToEnemy.Length;
        remainingAttacksPerTurn--;
        target.TakeDamage(damage);
        attackSound.Play();
    }

    private void SpawnProjectile(int pDamage)
    {

    }
}
