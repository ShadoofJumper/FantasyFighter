using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterCombat : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int damage;
    [SerializeField] protected float attackRate;
    [SerializeField] protected float attackDelay;
    protected float timeToFire;
    protected AnimationEvents characterAnimationEvents;
    protected Animator characterAnimator;
    protected Character character;
    private int maxHealth;


    public int MaxHealth => maxHealth;
    public int Health => health;
    public int Damage => damage;

    protected virtual void SetUpCombatComp()
    {
        characterAnimator           = GetComponentInChildren<Animator>();
        characterAnimationEvents    = GetComponentInChildren<AnimationEvents>();
        character = GetComponent<Character>();

        characterAnimationEvents.onDie = AfterDeath;
        characterAnimationEvents.onHit = AfterHit;
        maxHealth = health;
    }


    public virtual void HealthCharacter(int healthAmound)
    {
        health += healthAmound;
    }

    public virtual void TakeDamage(int damageTake)
    {
        characterAnimator.SetTrigger("Hit");
        health -= damageTake;
        if (health <= 0)
            Die();
    }

    public virtual void Die()
    {
        character.IsDead = true;
        characterAnimator.SetTrigger("Die");
    }

    protected virtual void AfterDeath()
    {
        SceneController.instance.CreateDeathBody(character.DeathSprite, character.CharacterSpriteObject.transform.position, gameObject.transform.localScale.x);
        Destroy(gameObject);
    }

    private void AfterHit()
    {
        character.IsPause = false;
    }

    public virtual void Fight(UnityAction delayFunk = null)
    {
        characterAnimationEvents.onMainAttackEnd = delayFunk;
        if (Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / attackRate;
            characterAnimator.SetTrigger("MainAttack");
            Attack();
        }
    }

    public virtual void Attack()
    {

    }

}
