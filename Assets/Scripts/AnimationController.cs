using UnityEngine;
using EventBus;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string walkingParameterName = "isWalking";
    [SerializeField] private string attackingParameterName = "isAttacking";
    [SerializeField] private float mageAttackAnimationLenghth;
    [SerializeField] private float knightAttackAnimationLenghth;
    [SerializeField] private float skeletonAttackAnimationLenghth;

    private float animationLength;
    private float currentAnimationLength;
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

    private void FixedUpdate()
    {
        if (!character.isAttacking)
            return;

        currentAnimationLength += Time.fixedDeltaTime;
        if (currentAnimationLength >= animationLength)
        {
            currentAnimationLength = 0;
            character.isAttacking = false;
            animator.SetBool(attackingParameterName, false);
        }
    }

    private void CharacterStartMoving(OnCharacterStartMoving pOnCharacterStartMoving)
    {
        if (pOnCharacterStartMoving.character != character)
            return;

        animator.SetBool(walkingParameterName, true);
    }

    private void CharacterStopMoving(OnCharacterStopMoving pOnCharacterStopMoving)
    {
        if (pOnCharacterStopMoving.character != character)
            return;

        animator.SetBool(walkingParameterName, false);
    }

    private void CharacterStartAttacking(OnCharacterStartAttacking pOnCharacterStartAttacking)
    {
        if (pOnCharacterStartAttacking.character != character)
            return;

        if (pOnCharacterStartAttacking.character.faction == Character.Faction.Enemies)
        {
            animationLength = skeletonAttackAnimationLenghth;
        } else
        {
            if (pOnCharacterStartAttacking.isMage)
            {
                animationLength = mageAttackAnimationLenghth;
            }
            else
            {
                animationLength = knightAttackAnimationLenghth;
            }
        }

        character.isAttacking = true;
        animator.SetBool(attackingParameterName, true);
    }
}
