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
    public Animator animator;
    public int attackCount = 0;

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
            StartCoroutine(attack1());
        }
        else if (Input.GetMouseButtonDown(0) && !isAttacking && attackCount == 1)
        {
            StartCoroutine(attack2());
        }
    }

    IEnumerator attack1()
    {
        rb.velocity = Vector2.zero;
        movement.enabled = false;
        isAttacking = true;

        yield return new WaitForSeconds(0.28f);

        attackCount = 1;
        isAttacking = false;
        movement.enabled = true;
        
    }

    IEnumerator attack2()
    {
        isAttacking2 = true;
        rb.velocity = Vector2.zero;
        movement.enabled = false;

        yield return new WaitForSeconds(0.28f);

        attackCount = 0;
        movement.enabled = true;
        isAttacking2 = false;
    }
    IEnumerator FastAttack()
    {
        Vector2 directiron;
        if (GetComponent<SpriteRenderer>().flipX) directiron = Vector2.left;
        else directiron = Vector2.right;

        anim.enabled = false;
        movement.enabled = false;
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(0.18f);
        Animation.ChangeAnimationState("FastAttack", currentState, Animation.animator);
        rb.AddForce(Vector2.up * 2, ForceMode2D.Impulse);
        rb.AddForce(directiron * pullForce, ForceMode2D.Impulse);
        

        yield return new WaitForSeconds(waitTÝme);

        rb.velocity = Vector2.zero;
        anim.enabled = true;
        movement.enabled = true;
        yield return new WaitForSeconds(2);
    }
}
