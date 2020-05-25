using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int damage;
    [SerializeField] protected float attackRate;
    [SerializeField] protected float attackDelay;
    private float timeToFire;

    public int Health => health;
    public int Damage => damage;

    public void TakeDamage(int damageTake)
    {
        //TO DO play animation

        health -= damageTake;
        if (health <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public virtual void Fight()
    {
        // check if time to shoot
        if (Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / attackRate;
            Attack();
        }
    }

    public virtual void Attack()
    {

    }

}
