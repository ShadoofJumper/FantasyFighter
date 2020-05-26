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
    private float timeToFire;
    protected Animator characterAnimator;

    public int Health => health;
    public int Damage => damage;

    protected virtual void SetUpCombatComp()
    {
        characterAnimator   = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(int damageTake)
    {
        characterAnimator.SetTrigger("Hit");

        health -= damageTake;
        if (health <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public virtual void Fight(UnityAction delayFunk = null)
    {
        // check if time to shoot
        if (Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / attackRate;
            //characterAnimator.ResetTrigger("MainAttack");
            characterAnimator.SetTrigger("MainAttack");
            Attack(delayFunk);
        }
    }

    public virtual void Attack(UnityAction delayFunk)
    {

    }

}
