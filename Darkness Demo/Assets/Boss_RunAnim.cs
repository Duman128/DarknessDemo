using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Boss_RunAnim : StateMachineBehaviour
{
    public GameObject[] hitPoints;
    Rigidbody2D rb;
    public float speed;
    int randomPoint;
    float waitTime;
    public float startWaitTime;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hitPoints = new GameObject[3];
        hitPoints = GameObject.FindGameObjectsWithTag("HitPoint");
        rb = animator.GetComponent<Rigidbody2D>();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int i = 0; i < hitPoints.Length; i++)
        {
            Vector2 TargetPosition = new Vector2(hitPoints[randomPoint].transform.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, TargetPosition, speed * Time.deltaTime);
            rb.MovePosition(newPos);

            if (Vector2.Distance(animator.transform.position, TargetPosition) == 0)
            {
                if (waitTime <= 0)
                {
                    //animator.SetBool("Run", false);
                    randomPoint = Random.Range(0, hitPoints.Length);
                    waitTime = startWaitTime;
                }
                else
                {
                   // animator.SetBool("Run", true);
                    waitTime -= Time.deltaTime;
                }
            }

            //flipX(TargetPosition, transform.position);
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
