using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cam;
    public float relativeMove = 0.3f;
    Vector2 startPos;

    Vector2 travel => (Vector2)cam.transform.position - startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = new Vector2(travel.x * relativeMove, transform.position.y);
    }
}
