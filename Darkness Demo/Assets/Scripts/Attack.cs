using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public static Movement movement;
    public static Animation anim;
    public static Attack Instance;
    public static PatrolAndDetect _patrolAndDetect;
    public static FlyEnemyAI _flyEnemyAI;

    Rigidbody2D rb;

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
    public static bool contactThFlyEnemy = false;


    public LayerMask platformLayer;

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
        


        if (Input.GetMouseButtonDown(0) && !isAttacking && attackCount == 0)
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
            _patrolAndDetect.takedDamage = true;
            PatrolAndDetect.enemyTransform.DOShakePosition(0.2f, 0.3f, 5);
            contactTheEnemy = false;

            yield return new WaitForSeconds(0.45f);

            attackCount = 1;
            isAttacking = false;
            movement.enabled = true;

            if (_patrolAndDetect != null)
                _patrolAndDetect.takedDamage = false;

            yield return new WaitForSeconds(2f);
            attackCount = 0;
        }

        else if (contactThFlyEnemy)
        {
            _flyEnemyAI.TakeDamageFlyEnemies = true;
            FlyEnemyAI.myBody.DOShakePosition(0.2f, 0.3f, 5);
            contactThFlyEnemy = false;

            yield return new WaitForSeconds(0.45f);

            attackCount = 1;
            isAttacking = false;
            movement.enabled = true;

            if (_flyEnemyAI != null)
                _flyEnemyAI.TakeDamageFlyEnemies = false;

            yield return new WaitForSeconds(2f);
            attackCount = 0;
        }
        else
        {
            yield return new WaitForSeconds(0.45f);
            isAttacking = false;
            movement.enabled = true;
            attackCount = 1;

            yield return new WaitForSeconds(2f);
            attackCount = 0;
        }
            
    }

    IEnumerator attack2()
    {
        isAttacking2 = true;
        rb.velocity = Vector2.zero;
        movement.enabled = false;


        if (contactTheEnemy)
        {
            _patrolAndDetect.takedDamage = true;
            PatrolAndDetect.enemyTransform.DOShakePosition(0.2f, 0.3f, 5);
            contactTheEnemy = false;

            yield return new WaitForSeconds(0.28f);

            attackCount = 2;
            isAttacking2 = false;
            movement.enabled = true;

            if (_patrolAndDetect != null)
                _patrolAndDetect.takedDamage = false;

            yield return new WaitForSeconds(2f);
            attackCount = 0;
        }

        else if (contactThFlyEnemy)
        {
            _flyEnemyAI.TakeDamageFlyEnemies = true;
            FlyEnemyAI.myBody.DOShakePosition(0.2f, 0.3f, 5);
            contactThFlyEnemy = false;

            yield return new WaitForSeconds(0.28f);

            attackCount = 2;
            isAttacking2 = false;
            movement.enabled = true;

            if (_flyEnemyAI != null)
                _flyEnemyAI.TakeDamageFlyEnemies = false;

            yield return new WaitForSeconds(2f);
            attackCount = 0;
        }

        else
        {
            yield return new WaitForSeconds(0.28f);
            isAttacking2 = false;
            movement.enabled = true;
            attackCount = 2;

            yield return new WaitForSeconds(2f);
            attackCount = 0;
        }

    }

    IEnumerator attack3()
    {
        isAttacking3 = true;
        rb.velocity = Vector2.zero;
        movement.enabled = false;

        if (contactTheEnemy)
        {
            _patrolAndDetect.takedDamage = true;
            PatrolAndDetect.enemyTransform.DOShakePosition(0.2f, 0.3f, 5);
            contactTheEnemy = false;

            yield return new WaitForSeconds(0.45f);

            attackCount = 0;
            isAttacking3 = false;
            movement.enabled = true;

            if (_patrolAndDetect != null)
                _patrolAndDetect.takedDamage = false;

            yield return new WaitForSeconds(2f);
            attackCount = 0;

        }
        else if (contactThFlyEnemy)
        {
            _flyEnemyAI.TakeDamageFlyEnemies = true;
            FlyEnemyAI.myBody.DOShakePosition(0.2f, 0.3f, 5);
            contactThFlyEnemy = false;

            yield return new WaitForSeconds(0.45f);

            attackCount = 0;
            isAttacking3 = false;
            movement.enabled = true;

            if (_flyEnemyAI != null)
                _flyEnemyAI.TakeDamageFlyEnemies = false;

            yield return new WaitForSeconds(2f);
            attackCount = 0;
        }

        else
        {
            yield return new WaitForSeconds(0.45f);
            isAttacking3 = false;
            movement.enabled = true;
            attackCount = 0;
        }
    }
}
