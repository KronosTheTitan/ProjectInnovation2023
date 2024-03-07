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

        int damage = attack + weapon.Damage;
        remainingAttacksPerTurn--;
        SppawnProjectile(damage);
        attackSound.Play();
    }

    private void SppawnProjectile(int pDamage)
    {

    }
}
