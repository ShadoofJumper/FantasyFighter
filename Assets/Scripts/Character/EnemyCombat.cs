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

    public override void Attack(UnityAction delayFunk)
    {
        Debug.Log("BOOM!");
        StartCoroutine(DoDamage(delayFunk));
    }

    IEnumerator DoDamage(UnityAction delayFunk)
    {
        yield return new WaitForSeconds(attackDelay);
        if(delayFunk!=null) delayFunk.Invoke();
        playerCombat.TakeDamage(damage);
    }

}
