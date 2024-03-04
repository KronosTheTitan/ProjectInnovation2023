using UnityEngine;
using EventBus;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string walkingParameterName = "isWalking";
    [SerializeField] private string attackAnimationName = "Attack";

    private Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
        if (character == null)
        {
            throw new System.Exception("There is no Character component.");
        }
    }

    private void OnEnable()
    {
        EventBus<OnCharacterStartMoving>.OnEvent += CharacterStartMoving;
        EventBus<OnCharacterStopMoving>.OnEvent += CharacterStopMoving;
        EventBus<OnCharacterStartAttacking>.OnEvent += CharacterStartAttacking;
    }

    private void OnDisable()
    {
        EventBus<OnCharacterStartMoving>.OnEvent -= CharacterStartMoving;
        EventBus<OnCharacterStopMoving>.OnEvent -= CharacterStopMoving;
        EventBus<OnCharacterStartAttacking>.OnEvent -= CharacterStartAttacking;
    }

    private void CharacterStartMoving(OnCharacterStartMoving pOnCharacterStartMoving)
    {
        animator.SetBool(walkingParameterName, true);
    }

    private void CharacterStopMoving(OnCharacterStopMoving pOnCharacterStopMoving)
    {
        animator.SetBool(walkingParameterName, false);
    }

    private void CharacterStartAttacking(OnCharacterStartAttacking pOnCharacterStartAttacking)
    {
        character.isAttacking = true;
        animator.Play(attackAnimationName);
    }

    public void OnAttackAnimationOver()
    {
        character.isAttacking = false;
    }
}
