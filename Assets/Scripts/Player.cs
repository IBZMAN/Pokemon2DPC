using UnityEngine;

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

    private bool IsPressingSprint
    {
        get
        {
            return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Joystick1Button4);
        }
    }

    void Start ()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        gameObject.GetComponent<SpriteRenderer>().sprite = startingSprite;
    }

    void Update ()
    {
        GetInput();     
        GetInput();
	}

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        direction = Vector2.zero;

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            //myAnimator.SetFloat("velY", myRigidBody.velocity.y);
            ChangeSprite(northSprite);
            direction += Vector2.up;
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            //myAnimator.SetFloat("velX", myRigidBody.velocity.x);
            ChangeSprite(westSprite);
            direction += Vector2.left;
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            //myAnimator.SetFloat("velY", myRigidBody.velocity.y);
            ChangeSprite(southSprite);
            direction += Vector2.down;
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            //myAnimator.SetFloat("velX", myRigidBody.velocity.x);
            ChangeSprite(eastSprite);
            direction += Vector2.right;
        }
    }

    private void ChangeSprite(Sprite newSprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
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
        if (IsPressingSprint)
        {
            myRigidBody.velocity = direction.normalized * (speed + 3f);
        }
        else
        {
            myRigidBody.velocity = direction.normalized * speed;
        }   
    }
}
