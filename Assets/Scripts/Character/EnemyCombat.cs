using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCombat : CharacterCombat
{
    private CharacterCombat playerCombat;
    private AudioSource enemyAudio;

    private void Start()
    {
        SetUpCombatComp();
    }

    public void SetSkeletornParams(int health, int damage)
    {
        this.health = health;
        this.damage = damage;
    }

    protected override void SetUpCombatComp()
    {
        base.SetUpCombatComp();
        playerCombat    = SceneController.instance.Player.GetComponent<CharacterCombat>();
        enemyAudio      = GetComponent<AudioSource>();
    }

    public override void Fight(UnityAction delayFunk = null)
    {
        if (!SceneController.instance.Player.GetComponent<Character>().IsDead)
        {
            base.Fight(delayFunk);
        }
    }

    public override void TakeDamage(int damageTake)
    {
        SoundManager.instance.Play("Hit");
        character.IsPause = true;
        base.TakeDamage(damageTake);
    }

    public override void Die()
    {
        base.Die();
        //update score
        WaveGameManager.instance.DeincrementSkeletonInWave();
        //sound
        SoundManager.instance.Play("SkeletonDie");
        ScoreManager.instance.IncreaseEnemyKilled();
        enemyAudio.Stop();
    }

    protected override void AfterDeath()
    {
        SceneController.instance.CreateDeathBody(character.DeathSprite, character.CharacterSpriteObject.transform.position, gameObject.transform.localScale.x);
        SceneController.instance.OnEnemieDie(gameObject);
    }

    public override void Attack()
    {
        StartCoroutine(DoDamage());
    }

    IEnumerator DoDamage()
    {
        yield return new WaitForSeconds(attackDelay);
        if (!character.IsDead)
        {
            playerCombat.TakeDamage(damage);
        }
    }

}
