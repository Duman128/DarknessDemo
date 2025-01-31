using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public static Movement movement;
    public static Animation anim;
    public static Attack Instance;
    public static AttackForAllChar _attackForAllChar;

    Rigidbody2D rb;

    public bool isAttacking = false;
    public Animator animator;
    public int attackCount = 0;

    public Transform hitPoints;
    public float hitPointRadius;
    public int damage;
    public LayerMask enemyLayer;
    public float waitAfterAttack;

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
            attack1();
        }
        else if (Input.GetMouseButtonDown(0) && !isAttacking && attackCount == 1)
        {
            EnemyAttack.AttackToTarget(damage, hitPoints.position, hitPointRadius, enemyLayer, waitAfterAttack);
            attack2();
        }
        else if (Input.GetMouseButtonDown(0) && !isAttacking && attackCount == 2)
        {
            EnemyAttack.AttackToTarget(damage, hitPoints.position, hitPointRadius, enemyLayer, waitAfterAttack);
            attack3();
        }
    }

    void attack1()
    {
        rb.linearVelocity = Vector2.zero;
        movement.enabled = false;
        isAttacking = true;

        if (_attackForAllChar == null)
            StartCoroutine(AfterAttackNoContect(1));

        else if (_attackForAllChar.contactEnemy)
            StartCoroutine(EnemyContact(1, 0.45f));
        else
            StartCoroutine(AfterAttackNoContect(1));
    }

    void attack2()
    {
        isAttacking = true;
        rb.linearVelocity = Vector2.zero;
        movement.enabled = false;

        if (_attackForAllChar == null)
            StartCoroutine(AfterAttackNoContect(2));

        else if (_attackForAllChar.contactEnemy)
            StartCoroutine(EnemyContact( 2, 0.28f));
        else
            StartCoroutine(AfterAttackNoContect(2));
    }

    void attack3()
    {
        isAttacking = true;
        rb.linearVelocity = Vector2.zero;
        movement.enabled = false;

        if (_attackForAllChar == null)
            StartCoroutine(AfterAttackNoContect(0));

        else if (_attackForAllChar.contactEnemy)
            StartCoroutine(EnemyContact(0, 0.45f));
        else
            StartCoroutine(AfterAttackNoContect(0));
    }

    IEnumerator AfterAttackNoContect(int _attackCount)
    {
        yield return new WaitForSeconds(0.45f);

        isAttacking = false;

        movement.enabled = true;
        attackCount = _attackCount;

        yield return new WaitForSeconds(2f);
        attackCount = 0;
    }

    IEnumerator EnemyContact(int _attackCount, float waitTime)
    {
        _attackForAllChar.enemyTransform.DOShakePosition(0.2f, 0.3f, 5);
        yield return new WaitForSeconds(0.2f);
        _attackForAllChar.contactEnemy = false;

        rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(waitTime);

        isAttacking = false;

        attackCount = _attackCount;

        movement.enabled = true;
        
        yield return new WaitForSeconds(2f);
        attackCount = 0;
    }
}
