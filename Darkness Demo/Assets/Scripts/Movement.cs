using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 4;

    float xAxis;
    float yAxis;

    private void FixedUpdate()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");

        GetComponent<Rigidbody2D>().velocity = new Vector2(xAxis * speed, yAxis * speed);
    }
}
