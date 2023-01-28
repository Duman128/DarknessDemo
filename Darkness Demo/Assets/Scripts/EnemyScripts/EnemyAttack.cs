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

            if (hitInfo.transform.gameObject.layer == 7)
            {
                if(hitInfo.transform.tag == "FlyEnemies"){
                    Attack._flyEnemyAI = hitInfo.transform.GetComponent<FlyEnemyAI>();
                    Attack.contactThFlyEnemy = true;
                }
                else
                {
                    Attack._patrolAndDetect = hitInfo.transform.GetComponent<PatrolAndDetect>();
                    Attack.contactTheEnemy = true;
                }
                
            }//"Enemy" Layer Value is 7
                
        }
             
    }
}
