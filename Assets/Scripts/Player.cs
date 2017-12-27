﻿using UnityEngine;

public class Player : Character
{
    Animator myAnimator;

    [SerializeField]
    private Sprite startingSprite;

    [SerializeField]
    private Sprite northSprite;

    [SerializeField]
    private Sprite eastSprite;

    [SerializeField]
    private Sprite southSprite;

    [SerializeField]
    private Sprite westSprite;

    private bool IsPressingShift
    {
        get
        {
            return Input.GetKey(KeyCode.LeftShift);
        }
    }

    public Animation walkLeftAnim;

    void Start ()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        gameObject.GetComponent<SpriteRenderer>().sprite = startingSprite;      
    }

    void Update ()
    {
        GetInput();     
        GetAnimations();
	}

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            //myAnimator.SetFloat("velY", myRigidBody.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().sprite = northSprite;
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //myAnimator.SetFloat("velX", myRigidBody.velocity.x);
            gameObject.GetComponent<SpriteRenderer>().sprite = westSprite;
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            //myAnimator.SetFloat("velY", myRigidBody.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().sprite = southSprite;
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            //myAnimator.SetFloat("velX", myRigidBody.velocity.x);
            gameObject.GetComponent<SpriteRenderer>().sprite = eastSprite;
            direction += Vector2.right;
        }

    }

    private void GetAnimations()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //animation.Play("WalkDown");
        }
    }
    
    private void Move()
    {
        if (IsPressingShift)
        {
            myRigidBody.velocity = direction.normalized * (speed + 3f);
        }
        else
        {
            myRigidBody.velocity = direction.normalized * speed;
        }

        //myRigidBody.velocity = direction.normalized * speed;
    }

}
