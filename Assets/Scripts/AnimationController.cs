using UnityEngine;
using EventBus;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string walkingParameterName = "isWalking";
    [SerializeField] private string attackingParameterName = "isAttacking";
    [SerializeField] private string deadParameterName = "isDead";
    [SerializeField] private string hitParameterName = "isGettingHit";
    [SerializeField] private float mageAttackAnimationLenghth;
    [SerializeField] private float knightAttackAnimationLenghth;
    [SerializeField] private float skeletonAttackAnimationLenghth;
    [SerializeField] private float dieAnimationLenghth;
    [SerializeField] private float hitAnimationLenghth;

    private float animationLengthAttack;
    private float currentAnimationLengthAttack;
    private float animationLengthHit;
    private float currentAnimationLengthHit;
    private float animationLengthDead;
    private float currentAnimationLengthDead;
    private Character character;
    private bool isDying;
    private bool isGettingHit;

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
        EventBus<OnCharacterDies>.OnEvent += CharacterDies;
        EventBus<OnCharacterGettingHit>.OnEvent += CharacterHit;
    }

    private void OnDisable()
    {
        EventBus<OnCharacterStartMoving>.OnEvent -= CharacterStartMoving;
        EventBus<OnCharacterStopMoving>.OnEvent -= CharacterStopMoving;
        EventBus<OnCharacterStartAttacking>.OnEvent -= CharacterStartAttacking;
        EventBus<OnCharacterDies>.OnEvent -= CharacterDies;
        EventBus<OnCharacterGettingHit>.OnEvent -= CharacterHit;
    }

    private void FixedUpdate()
    {
        CheckAttackAnimationTime();
        CheckHitAnimationTime();
        CheckDeathAnimationTime();
    }

    private void CheckAttackAnimationTime()
    {
        if (!character.isAttacking)
            return;

        currentAnimationLengthAttack += Time.fixedDeltaTime;
        if (currentAnimationLengthAttack >= animationLengthAttack)
        {
            currentAnimationLengthAttack = 0;
            character.isAttacking = false;
            animator.SetBool(attackingParameterName, false);
        }
    }

    private void CheckHitAnimationTime()
    {
        if (isGettingHit)
            return;

        currentAnimationLengthHit += Time.fixedDeltaTime;
        if (currentAnimationLengthHit >= animationLengthHit)
        {
            currentAnimationLengthHit = 0;
            isGettingHit = false;
            animator.SetBool(hitParameterName, false);
        }
    }

    private void CheckDeathAnimationTime()
    {
        if (!isDying)
            return;

        animationLengthDead += Time.fixedDeltaTime;
        if (currentAnimationLengthDead >= animationLengthDead)
        {
            currentAnimationLengthDead = 0;
            isDying = false;
            animator.SetBool(deadParameterName, false);
            character.DieOnClients();
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
            animationLengthAttack = skeletonAttackAnimationLenghth;
        } else
        {
            if (pOnCharacterStartAttacking.isMage)
            {
                animationLengthAttack = mageAttackAnimationLenghth;
            }
            else
            {
                animationLengthAttack = knightAttackAnimationLenghth;
            }
        }

        character.isAttacking = true;
        animator.SetBool(attackingParameterName, true);
    }

    private void CharacterDies(OnCharacterDies pOnCharacterDies)
    {
        if (pOnCharacterDies.character != character)
            return;

        isDying = true;
        animator.SetBool(deadParameterName, true);
    }

    private void CharacterHit(OnCharacterGettingHit pOnCharacterGettingHit)
    {
        if (pOnCharacterGettingHit.character != character)
            return;

        isGettingHit = true;
        animator.SetBool(hitParameterName, true);
    }
}
