using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using TMPro;

public class FlyEnemyAI : MonoBehaviour
{
    public Transform[] MovePoints;
    private int randomPoint;
    public float PatrolSpeed;

    public static Transform myBody;
    private Transform Player;
    public AIPath aiPath;
    public AIDestinationSetter _AIDestinationSetter;

    public float attackDistance;

    public float attackTime;
    private float _attackTime;

    public float patrolWaitTime;
    private float _patrolWaitTime;

    public float castRadius;
    public LayerMask playerLayer;
    private bool isHit;

    public bool TakeDamageFlyEnemies = false;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y + 1.5f), castRadius);
    }

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player").transform;
        myBody = transform.GetComponentInParent<Transform>();
    }

    private void Start()
    {
        randomPoint = Random.Range(0, MovePoints.Length);
        _attackTime = Time.time + attackTime;
    }

    private void Update()
    {
        Debug.Log(TakeDamageFlyEnemies);

        FlipX();
        if (!TakeDamageFlyEnemies)
        {
            if (Vector2.Distance(transform.position, Player.position) < attackDistance)
                FollowFonc();
            else
                Patrol();
        }else
            FlyEnemyAnimation.State = FlyEnemyAnimation.AnimStates.Hurt;
    }

    private void Patrol()// Patrol Code
    {
        
        for (int i = 0; i < MovePoints.Length; i++)
        {
            _AIDestinationSetter.target = MovePoints[randomPoint];
            aiPath.maxSpeed = PatrolSpeed;
            isHit = true;

            if (Vector2.Distance(transform.position, MovePoints[randomPoint].position) < 1.6f)
            {
                if (_patrolWaitTime <= 0)
                {
                    randomPoint = Random.Range(0, MovePoints.Length);
                    _patrolWaitTime = patrolWaitTime;
                }
                else
                    _patrolWaitTime -= Time.deltaTime;
            }
        }
    }

    private void FollowFonc()
    {
        if (!isHit)
        {
            FlyEnemyAnimation.State = FlyEnemyAnimation.AnimStates.AttackBfHit;

            if (Physics2D.OverlapCircle(myBody.position, castRadius, playerLayer))
                StartCoroutine(HitMoment());

        }

        else if (Vector2.Distance(myBody.position, MovePoints[0].position) <= 0.1f)
        {
            _AIDestinationSetter.target = Player;
            aiPath.maxSpeed = 6;
            isHit = false;
        }

        if (Time.time > _attackTime)
        {
            isHit = true;
            _attackTime = Time.time + attackTime;
            _AIDestinationSetter.target = MovePoints[0];
            aiPath.maxSpeed = 4;
        }

        
    }
    IEnumerator HitMoment()
    {
        isHit = true;
        _attackTime = Time.time + attackTime;
        _AIDestinationSetter.target = Player;
        FlyEnemyAnimation.State = FlyEnemyAnimation.AnimStates.AttackHit;

        yield return new WaitForSeconds(0.5f);

        FlyEnemyAnimation.State = FlyEnemyAnimation.AnimStates.Walk;

        _AIDestinationSetter.target = MovePoints[0];
        aiPath.maxSpeed = 4;
    }

    private void FlipX()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (aiPath.desiredVelocity.x <= -0.01)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
