using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Movement
    public float speed;
    public float jump;
    public float distance;
    float moveVelocity;

    public LayerMask layerMask;

    void FixedUpdate()
    {
        //Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, distance, layerMask))
                GetComponent<Rigidbody2D>().linearVelocity = new Vector2(GetComponent<Rigidbody2D>().linearVelocity.x, jump);
        }

        moveVelocity = 0;

        //Left Right Movement
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            moveVelocity = -speed;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            moveVelocity = speed;
            transform.localScale = new Vector3(1, 1, 1);
        }

        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().linearVelocity.y);

    }
    
}