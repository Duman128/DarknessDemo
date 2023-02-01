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

    public Transform isHereTriggerPosition;
    public Vector3 isHereTriggerScale;

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

    public bool TakeDamageFlyEnemies = false;

    private bool isHitPlayer;//if the object hit the player valuse is true, when return point[0] value is false
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y + 1.5f), castRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(isHereTriggerPosition.position, isHereTriggerScale);
    }

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player").transform;
        myBody = transform.GetComponentInParent<Transform>();
    }

    private void Start()
    {
        _attackTime = Time.time + attackTime;
        randomPoint = Random.Range(0, MovePoints.Length);
    }

    private void Update()
    {
        TakeDamageFlyEnemies = GetComponent<AttackForAllChar>().contactEnemy;
        FlipX();

        if (!TakeDamageFlyEnemies)
        {
            if ((Vector2.Distance(transform.position, Player.position) < attackDistance) && isHeHere())
                FollowFonc();
            else
                Patrol();
        }
        else
        {
            isHitPlayer = true;
            FlyEnemyAnimation.State = FlyEnemyAnimation.AnimStates.Hurt;
        }
            


    }

    private bool isHeHere()//Check the character is inside the trigger Fonc
    {
        if (Physics2D.OverlapBox(isHereTriggerPosition.position, isHereTriggerScale, 0, playerLayer))
            return true;
        else
            return false;
    }

    private void Patrol()// Patrol Code
    {
        
        for (int i = 0; i < MovePoints.Length; i++)
        {
            if (!isHeHere())
            {
                FlyEnemyAnimation.State = FlyEnemyAnimation.AnimStates.Walk;
                _AIDestinationSetter.target = MovePoints[randomPoint];

                if (Vector2.Distance(transform.position, MovePoints[randomPoint].position) < 1.8f)
                {

                    aiPath.maxSpeed = PatrolSpeed;

                    if (_patrolWaitTime <= 0)
                    {
                        randomPoint = Random.Range(0, MovePoints.Length);
                        _patrolWaitTime = patrolWaitTime;
                    }
                    else
                        _patrolWaitTime -= Time.deltaTime;
                }
            }
            else
                _AIDestinationSetter.target = Player;
        }
    }

    private void FollowFonc()
    {
        if (!isHitPlayer)
        {
            FlyEnemyAnimation.State = FlyEnemyAnimation.AnimStates.AttackBfHit;
            _AIDestinationSetter.target = Player;

            if (Physics2D.OverlapCircle(myBody.position, castRadius, playerLayer))
                StartCoroutine(HitMoment());

            else if (Time.time > _attackTime)
            {
                _attackTime = Time.time + attackTime;
                isHitPlayer = true;

            }
        }

        else
        {
            _AIDestinationSetter.target = MovePoints[0];

            if (Vector2.Distance(myBody.position, MovePoints[0].position) <= 1.8f)
            {
                _AIDestinationSetter.target = Player;
                aiPath.maxSpeed = 6;
                isHitPlayer = false;
            }
        }
    }
    IEnumerator HitMoment()
    {
        Debug.Log("asdasasda");
        _attackTime = Time.time + attackTime;
        _AIDestinationSetter.target = MovePoints[0];
        aiPath.maxSpeed = 4;
        

        FlyEnemyAnimation.State = FlyEnemyAnimation.AnimStates.AttackHit;
        yield return new WaitForSeconds(0.2f);
        FlyEnemyAnimation.State = FlyEnemyAnimation.AnimStates.Walk;
        isHitPlayer = true;
    }

    private void FlipX()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (aiPath.desiredVelocity.x <= -0.01)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
