using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniBoss : MonoBehaviour
{
    public Transform player;

    public bool isFlipped = false;
    public int damage;
    public Transform hitPoint;
    public float hitPointRadius;
    public LayerMask playerLayerMask;

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitPoint.position, hitPointRadius);
    }

    public IEnumerator ExitFonc(Animator animator, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        animator.SetBool("IsInAttackArea", false);
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    

    public void Attack()
    {
        EnemyAttack.AttackToTarget(damage, hitPoint.position, hitPointRadius, playerLayerMask, 0);
    }
}
