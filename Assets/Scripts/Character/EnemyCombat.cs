using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCombat : CharacterCombat
{
    private CharacterCombat playerCombat;

    private void Start()
    {
        SetUpCombatComp();
    }

    protected override void SetUpCombatComp()
    {
        base.SetUpCombatComp();
        playerCombat = SceneController.instance.Player.GetComponent<CharacterCombat>();
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
        character.IsPause = true;
        base.TakeDamage(damageTake);
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
        playerCombat.TakeDamage(damage);
    }

}
