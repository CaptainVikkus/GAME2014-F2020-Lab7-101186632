using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float runForce;
    public Transform lookAheadPoint;
    public Transform lookInFront;
    private Rigidbody2D rigidbody2D;
    public LayerMask collisionGroundLayer;
    public LayerMask collisionWallLayer;
    public bool isGroundAhead;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _LookFront();
        _LookAhead();
        _Move();
    }

    private void _LookFront()
    {
        if (Physics2D.Linecast(transform.position, lookInFront.position, collisionWallLayer))
        {
            _FlipX();
        }
        Debug.DrawLine(transform.position, lookInFront.position, Color.green);
    }

    private void _LookAhead()
    {
        isGroundAhead = Physics2D.Linecast(transform.position, lookAheadPoint.position, collisionGroundLayer);
        Debug.DrawLine(transform.position, lookAheadPoint.position, Color.green);

    }

    void _Move()
    {
        if (isGroundAhead)
        {
            //run

            rigidbody2D.AddForce(Vector2.left * runForce * Time.deltaTime * transform.localScale.x);
            rigidbody2D.velocity *= 0.9f;
        }
        else
        {
            //flip
            _FlipX();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
    }

    private void OnCollisionExit2D(Collision2D other)
    {
    }

    private void _FlipX()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
    }
}
