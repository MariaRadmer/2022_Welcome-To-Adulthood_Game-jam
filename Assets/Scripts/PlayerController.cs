using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D body;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public float walkSpeed;
    Vector2 direction;
    bool facingRight = true; 

    private Pickup pickup;

    private bool bedTime = false;

    private void Start()
    {
        pickup = GetComponent<Pickup>();
        pickup.Direction = new Vector2(0, -1);
    }


    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Idle", true);
    }

    void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        
        if(direction.x < 0 && facingRight) {
            GetComponent<SpriteRenderer>().flipX = true;
            facingRight = false;
        }
        
        if(direction.x > 0 && !facingRight) {
            GetComponent<SpriteRenderer>().flipX = false;
            facingRight = true;
        }

        if (direction.sqrMagnitude > .1f)
        {
            pickup.Direction = direction.normalized;
        }

       
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);

        body.velocity = direction * walkSpeed;

        bool idle = body.velocity.magnitude < 0.05f;
        animator.SetBool("Idle", idle);
        
    }

    public void stopMovement()
    {
        bedTime = true;
    }
}
