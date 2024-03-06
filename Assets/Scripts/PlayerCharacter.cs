using EventBus;
using Unity.Mathematics;
using UnityEngine;

public class PlayerCharacter : Character
{
    protected override void TakeDamage(int amount)
    {
        //Debug.Log("Taking damage");
        int modifiedAmount = math.clamp(amount - GetTotalDefence(), 0, int.MaxValue);
        //Debug.Log(modifiedAmount);
        
        EventBus<OnPlayerTakeDamage>.Publish(new OnPlayerTakeDamage(modifiedAmount));
    }
}