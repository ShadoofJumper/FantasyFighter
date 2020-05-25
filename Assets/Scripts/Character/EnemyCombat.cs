using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : CharacterCombat
{
    private CharacterCombat playerCombat;

    private void Start()
    {
        playerCombat = SceneController.instance.Player.GetComponent<CharacterCombat>();
    }

    public override void Attack()
    {
        Debug.Log("BOOM!");
        StartCoroutine(DoDamage());
    }

    IEnumerator DoDamage()
    {
        yield return new WaitForSeconds(attackDelay);
        playerCombat.TakeDamage(damage);
    }

}
