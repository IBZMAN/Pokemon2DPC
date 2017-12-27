using UnityEngine;

public class Player : Character
{

    Animator myAnimator;

    void Start ()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }
	
	void Update ()
    {
        GetInput();
	}

    private void FixedUpdate()
    {
        Move();
        Debug.Log("y " + myRigidBody.velocity.y + "\n x " + myRigidBody.velocity.x);
    }

    private void GetInput()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            myAnimator.SetFloat("velY", myRigidBody.velocity.y);
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            myAnimator.SetFloat("velX", myRigidBody.velocity.x);
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            myAnimator.SetFloat("velY", myRigidBody.velocity.y);
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            myAnimator.SetFloat("velX", myRigidBody.velocity.x);
            direction += Vector2.right;
        }
    }

    private void Move()
    {
        myRigidBody.velocity = direction.normalized * speed;
    }

}
