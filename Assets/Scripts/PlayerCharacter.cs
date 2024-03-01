using EventBus;
using Unity.Mathematics;

public class PlayerCharacter : Character
{
    protected override void TakeDamage(int amount)
    {
        int modifiedAmount = math.clamp(amount - GetTotalDefence(), 0, int.MaxValue);

        remainingHealth -= modifiedAmount;
        
        EventBus<OnCharacterTakeDamage>.Publish(new OnCharacterTakeDamage());
        
        if(remainingHealth <= 0)
            Destroy(gameObject);
    }
}