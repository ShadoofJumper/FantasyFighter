using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerCombat : CharacterCombat
{
    [SerializeField] private float hitRange;
    private RaycastHit[] enemysHit;

    void Start()
    {
    }

    private void Update()
    {
        Vector3 center = transform.position;
        Vector3 dir = transform.right * transform.localScale.x;
        center.y = 0.5f;
        Debug.DrawLine(center, center + dir, Color.red);

    }

    public override void Attack()
    {
        StartCoroutine(DoDamage());
    }

    IEnumerator DoDamage()
    {
        yield return new WaitForSeconds(attackDelay);
        enemysHit = GetEnemysInRange();
        foreach (RaycastHit hit in enemysHit)
        {
            CharacterCombat hitEnemy = hit.collider.gameObject.GetComponent<CharacterCombat>();
            //Debug.Log("Hit: " + hitEnemy.gameObject.name);
            //Debug.Log($"Helth: {hitEnemy.Health}");
            if (hitEnemy != null)
            {
                hitEnemy.TakeDamage(damage);
            }
        }
    }



    private RaycastHit[] GetEnemysInRange()
    {
        Vector3 origin = transform.position;
        origin.y = 0.5f;
        Vector3 dir = transform.right * transform.localScale.x;

        RaycastHit[] hits;
        hits = Physics.RaycastAll(origin, dir, 100.0F);

        return hits;
    }




    // --------------- Dev ---------------
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, hitRange);
    }

}
