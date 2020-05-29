using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
public class PlayerCombat : CharacterCombat
{
    [SerializeField] private float hitRange;
    [SerializeField] private float angleHit = 40;
    private Player player;
    private RaycastHit[] enemysHit;

    void Start()
    {
        SetUpCombatComp();

    }

    protected override void SetUpCombatComp()
    {
        base.SetUpCombatComp();
        player = character as Player;
    }

    private void Update()
    {
        // for test
        Vector3 center = transform.position;
        Vector3 dir1 = transform.right * transform.localScale.x;
        Vector3 dir2 = Quaternion.Euler(0, angleHit/2, 0) * dir1;
        Vector3 dir3 = Quaternion.Euler(0, angleHit/2 * -1, 0) * dir1;

        center.y = 0.5f;
        Debug.DrawLine(center, center + dir1, Color.red);
        Debug.DrawLine(center, center + dir2, Color.red);
        Debug.DrawLine(center, center + dir3, Color.red);
        //
    }

    public override void Die()
    {
        SoundManager.instance.Play("PlayerDeath");
        SoundManager.instance.StopBattleMusic();
        character.IsDead = true;
        character.CharacterRigidbody.velocity = Vector3.zero;
        StartCoroutine(DieAnimWithDelay());
    }

    IEnumerator DieAnimWithDelay()
    {
        for (int i = 0; i < 30; i++)
        {
            yield return null;
        }
        characterAnimator.SetTrigger("Die");
    }

    public override void HealthCharacter(int healthAmound)
    {
        base.HealthCharacter(healthAmound);
        UIController.instance.UpdateHPBar(health);
    }

    public override void TakeDamage(int damageTake)
    {
        SoundManager.instance.Play("GetHit");
        characterAnimationEvents.ShowHitAnim();
        health -= damageTake;
        UIController.instance.UpdateHPBar(health);
        if (health <= 0 && !character.IsDead)
            Die();
    }
    protected override void AfterDeath()
    {
        gameObject.GetComponent<PlayerCombat>().enabled = false;
        WaveGameManager.instance.FailWave();
    }

    public override void Attack()
    {
        SoundManager.instance.Play("SwordSwing");
        StartCoroutine(DoDamage());
    }

    IEnumerator DoDamage()
    {
        yield return new WaitForSeconds(attackDelay);
        //if (player.IsMove)
        //    yield break;

        enemysHit = GetEnemysInRange();
        foreach (RaycastHit hit in enemysHit)
        {
            CharacterCombat hitEnemy = hit.collider.gameObject.GetComponent<CharacterCombat>();
            //Debug.Log($"Helth: {hitEnemy.Health}");
            if (hitEnemy != null)
            {
                hitEnemy.TakeDamage(damage);
            }
        }
    }



    private RaycastHit[] GetEnemysInRange()
    {
        Vector3 origin  = transform.position;
        origin.y = 0.5f;
        Vector3 dir1 = transform.right * transform.localScale.x;
        Vector3 dir2 = Quaternion.Euler(0,  angleHit / 2,        0) * dir1;
        Vector3 dir3 = Quaternion.Euler(0,  angleHit / 2 * -1,   0) * dir1;

        RaycastHit[] hits = { };
        var result  = hits.Union    (Physics.RaycastAll(origin, dir1, hitRange), new EnemyHitComparer());
        result      = result.Union  (Physics.RaycastAll(origin, dir2, hitRange), new EnemyHitComparer());
        result      = result.Union  (Physics.RaycastAll(origin, dir3, hitRange), new EnemyHitComparer());

        return result.ToArray();
    }




    // --------------- Dev ---------------
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, hitRange);
    }

}


