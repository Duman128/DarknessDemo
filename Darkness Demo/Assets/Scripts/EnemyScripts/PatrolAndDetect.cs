using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class PatrolAndDetect : MonoBehaviour
{
    
    public static Transform enemyTransform;
    public Transform[] MovePoints;
    private Transform Player;

    public float speed;
    private float waitTime;
    public float startWaitSpeed;

    public bool takedDamage = false;

    public LayerMask PlayerMask;
    public float rayRadius;

    public float minDistance;

    private int randomPoint;

    Vector2 TargetPosition;

    public Transform HitPoint;
    public float HitPointRadius;

    public int damage;
    public float waitTimeAfterAttack;

    public float MinDetectX;
    public float MinDetectY;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, rayRadius);
    }

    private void Awake()
    {
        Player = GameObject.Find("Player").transform;
    }

    void Start()
    {
        enemyTransform = transform;
        waitTime = startWaitSpeed;
        randomPoint = Random.Range(0, MovePoints.Length);    
    }


    void Update()
    {
        EnemyMovementFonc();
    }


    void EnemyMovementFonc()
    {
        if (!takedDamage)
        {
            if (Physics2D.OverlapCircle(transform.position, rayRadius, PlayerMask)) //Player Follow Code
            {
                    TargetPosition = new Vector2(Player.position.x, transform.position.y);

                if (Vector2.Distance(transform.position, TargetPosition) > minDistance)
                {
                    flipX(TargetPosition, transform.position);
                    transform.position = Vector2.MoveTowards(transform.position, TargetPosition, speed * 2 * Time.deltaTime);
                }

                else if (MathF.Abs(transform.position.x - Player.position.x) < MinDetectX)
                {
                    if (MathF.Abs(transform.position.y - Player.position.y) < MinDetectY)
                    {
                        playerAttackAndFlip();
                    }

                    else
                        GetComponent<EnemyAnimation>().State = EnemyAnimation.AnimStates.Idle;
                }
            }
            
            else
                Patrol();
        }

        else
            GetComponent<EnemyAnimation>().State = EnemyAnimation.AnimStates.Hurt;
    }

    private void playerAttackAndFlip()
    {
        float pointXValue = TargetPosition.x;
        float enemyXValue = transform.position.x;

        if (pointXValue < enemyXValue)
        {
            EnemyAttack.AttackToTarget(damage, HitPoint.position, HitPointRadius, PlayerMask, waitTimeAfterAttack);
            GetComponent<EnemyAnimation>().State = EnemyAnimation.AnimStates.Attack;
            transform.localScale = new Vector3(1, 1, 1);
        }

        else if (pointXValue > enemyXValue)
        {
            EnemyAttack.AttackToTarget(damage, HitPoint.position, HitPointRadius, PlayerMask, waitTimeAfterAttack);
            GetComponent<EnemyAnimation>().State = EnemyAnimation.AnimStates.Attack;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Patrol()// Patrol Code
    {
        for (int i = 0; i < MovePoints.Length; i++)
        {
            TargetPosition = new Vector2(MovePoints[randomPoint].position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, TargetPosition, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, TargetPosition) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    randomPoint = Random.Range(0, MovePoints.Length);
                    waitTime = startWaitSpeed;
                }
                else
                    waitTime -= Time.deltaTime;

            }

            flipX(TargetPosition, transform.position);
        }
    }

    void flipX(Vector2 TargetPos, Vector2 currentPos)
    {
        //Flip X Code
        float pointXValue = TargetPos.x;
        float enemyXValue = currentPos.x;

        if (pointXValue < enemyXValue)
        {
            GetComponent<EnemyAnimation>().State = EnemyAnimation.AnimStates.Walk;
            transform.localScale = new Vector3(1, 1, 1);
        }

        else if (pointXValue > enemyXValue)
        {
            GetComponent<EnemyAnimation>().State = EnemyAnimation.AnimStates.Walk;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
            GetComponent<EnemyAnimation>().State = EnemyAnimation.AnimStates.Idle;
    }
}