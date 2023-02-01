using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public static void AttackToTarget(int damage, Vector2 hitPoint, float hitPointRadius, LayerMask PlayerLayer, float waitTime)
    {
        RaycastHit2D hitInfo = Physics2D.CircleCast(hitPoint, hitPointRadius, Vector2.up, hitPointRadius, PlayerLayer);

        if (Physics2D.OverlapCircle(hitPoint, hitPointRadius, PlayerLayer))
        {
            hitInfo.transform.GetComponent<IDamageable>().TakeDamage(damage, waitTime);

            if (hitInfo.transform.gameObject.layer == 7)//"Enemy" Layer Value is 7
            {
                Attack._attackForAllChar = hitInfo.transform.GetComponent<AttackForAllChar>();
                Attack._attackForAllChar.contactEnemy = true;
            }
        }
    }
}
