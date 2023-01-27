using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public static Movement movement;
    public static Animation anim;
    public static Attack Instance;

    Rigidbody2D rb;
    public float pullForce = 50;
    public float waitTÝme;
    private string currentState;

    public bool isAttacking = false;
    public bool isAttacking2 = false;
    public bool isAttacking3 = false;
    public Animator animator;
    public int attackCount = 0;

    public Transform hitPoints;
    public float hitPointRadius;
    public int damage;
    public LayerMask enemyLayer;
    public float waitAfterAttack;

    public static bool contactTheEnemy = false;
    public LayerMask platformLayer;

    private float fastAttackTime = 0;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitPoints.position, hitPointRadius);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        Instance = this;
        anim = GetComponent<Animation>();
        movement = GetComponent<Movement>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            StartCoroutine(FastAttack());

        else if (Input.GetMouseButtonDown(0) && !isAttacking && attackCount == 0)
        {
            EnemyAttack.AttackToTarget(damage, hitPoints.position, hitPointRadius, enemyLayer, waitAfterAttack);
            StartCoroutine(attack1());
        }
        else if (Input.GetMouseButtonDown(0) && !isAttacking && attackCount == 1)
        {
            EnemyAttack.AttackToTarget(damage, hitPoints.position, hitPointRadius, enemyLayer, waitAfterAttack);
            StartCoroutine(attack2());
        }
        else if (Input.GetMouseButtonDown(0) && !isAttacking && attackCount == 2)
        {
            EnemyAttack.AttackToTarget(damage, hitPoints.position, hitPointRadius, enemyLayer, waitAfterAttack);
            StartCoroutine(attack3());
        }
    }

    IEnumerator attack1()
    {
        rb.velocity = Vector2.zero;
        movement.enabled = false;
        isAttacking = true;

        if (contactTheEnemy)
        {
            PatrolAndDetect.takedDamage = true;
            PatrolAndDetect.enemyTransform.DOShakePosition(0.2f, 0.3f, 5);
            contactTheEnemy = false;
        }

        yield return new WaitForSeconds(0.45f);

        attackCount = 1;
        isAttacking = false;
        movement.enabled = true;
        PatrolAndDetect.takedDamage = false;
        yield return new WaitForSeconds(2f);
        attackCount = 0;
    }

    IEnumerator attack2()
    {
        isAttacking2 = true;
        rb.velocity = Vector2.zero;
        movement.enabled = false;

        if (contactTheEnemy)
        {
            PatrolAndDetect.takedDamage = true;
            PatrolAndDetect.enemyTransform.DOShakePosition(0.2f, 0.3f, 5);
            contactTheEnemy = false;
        }

        yield return new WaitForSeconds(0.28f);

        attackCount = 2;
        movement.enabled = true;
        isAttacking2 = false;
        PatrolAndDetect.takedDamage = false;
        yield return new WaitForSeconds(2f);
        attackCount = 0;
    }

    IEnumerator attack3()
    {
        isAttacking3 = true;
        rb.velocity = Vector2.zero;
        movement.enabled = false;

        if (contactTheEnemy)
        {
            PatrolAndDetect.takedDamage = true;
            PatrolAndDetect.enemyTransform.DOShakePosition(0.2f, 0.3f, 5);
            contactTheEnemy = false;
        }

        yield return new WaitForSeconds(0.45f);

        attackCount = 0;
        movement.enabled = true;
        isAttacking3 = false;
        PatrolAndDetect.takedDamage = false;
    }

    IEnumerator FastAttack()
    {
        Vector2 targer = new Vector2(hitPoints.position.x * 2, hitPoints.position.y);
        if (!Physics2D.Linecast(transform.position,targer, platformLayer) && fastAttackTime < Time.time)
        {
            fastAttackTime = Time.time + 5;

            Vector2 directiron;
            if (transform.localScale.x == -1) directiron = Vector2.left;
            else directiron = Vector2.right;

            anim.enabled = false;
            movement.enabled = false;
            rb.velocity = Vector2.zero;

            yield return new WaitForSeconds(0.05f);
            Animation.ChangeAnimationState("FastAttack", currentState, Animation.animator);
            rb.AddForce(Vector2.up * 2, ForceMode2D.Impulse);
            rb.AddForce(directiron * pullForce, ForceMode2D.Impulse);


            yield return new WaitForSeconds(waitTÝme);

            rb.velocity = Vector2.zero;
            anim.enabled = true;
            movement.enabled = true;
            yield return new WaitForSeconds(5);
        }
    }
}
